using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;

namespace rr_library.Models.AuthJwtService
{
    /// <summary>
    /// rewritten to rr-private service
    /// </summary>
    /// <param name="Token"></param>
    /// <param name="Context"></param>
    /// <param name="Db"></param>
    /// <param name="ClientIP"></param>
    /// <param name="IgnoreInvalidHandling"></param>
    [Obsolete]
    public class AuthJwtClass(string? Token, FunctionContext Context, IDatabase Db, string? ClientIP, bool IgnoreInvalidHandling = false)
    {
        public RRJwtObj? JwtObj { get; private set; } = null;
        public bool IsValid { get; private set; } = false;
        public string Json { get; private set; } = string.Empty;
        public string ErrorMessage { get; private set; } = string.Empty;
        public static string UserDataClaimType { get; } = "UserData";
        public static string UserDataLengthClaimType { get; } = "UserDataLength";
        public ILogger Logger { get; private set; } = Context.GetLogger<AuthJwtClass>();
        public string TraceIdentifier { get; private set; } = Context.InvocationId;

        public async Task HandleAuthenticatedReq()
        {
            string key = $"AuthenticatedReq_{ClientIP}";
            if (!await Db.KeyExistsAsync(key))
            {
                await Db.StringSetAsync(key, 0);
                await Db.KeyExpireAsync(key, TimeSpan.FromMinutes(1));
            }
            var IPCount = await Db.StringIncrementAsync(key);
            if (IPCount > 2000)
            {
                if (IPCount % 1000 == 0)
                {
                    Logger.LogInformation("{TraceIdentifier} [{clientIP}] ValidUserRequest: {IPCount}"
                        , TraceIdentifier
                        , ClientIP
                        , IPCount
                        );
                }
                // Consider to ban the IP on Azure Firewall for a while.
            }
        }
        public async Task HandleValidReq()
        {
            string key = $"ValidReq_{ClientIP}";
            if (!await Db.KeyExistsAsync(key))
            {
                await Db.StringSetAsync(key, 0);
                await Db.KeyExpireAsync(key, TimeSpan.FromMinutes(1));
            }
            var IPCount = await Db.StringIncrementAsync(key);
            if (IPCount > 1000)
            {
                if (IPCount % 1000 == 0)
                {
                    Logger.LogInformation("{TraceIdentifier} [{clientIP}] ValidRequest: {IPCount}"
                        , TraceIdentifier
                        , ClientIP
                        , IPCount
                        );
                }
                // Consider to ban the IP on Azure Firewall for a while.
            }
        }
        public async Task HandleInvalidToken()
        {
            if (IgnoreInvalidHandling)
            {
                return;
            }
            string key = $"InvalidToken_{ClientIP}";
            if (!await Db.KeyExistsAsync(key))
            {
                await Db.StringSetAsync(key, 0);
                await Db.KeyExpireAsync(key, TimeSpan.FromMinutes(1));
            }
            var IPCount = await Db.StringIncrementAsync(key);
            if (IPCount > 1000)
            {
                if (IPCount % 1000 == 0)
                {
                    Logger.LogInformation("{TraceIdentifier} [{clientIP}] InvalidToken: {IPCount}"
                        , TraceIdentifier
                        , ClientIP
                        , IPCount
                        );
                }
                // Consider to ban the IP on Azure Firewall for a while.
            }
        }
        public async Task Process()
        {
            // Check if the value is empty.
            if (string.IsNullOrEmpty(Token))
            {
                IsValid = false;
                ErrorMessage = "Token is empty.";
                await HandleInvalidToken();
                return;
            }

            if (Token.StartsWith("Bearer "))
            {
                Token = Token[7..];
            }

            var result = await GetJsonFromTokenAsync(Token);
            IsValid = result.Item1;
            ErrorMessage = result.Item3;
            if (!IsValid)
            {
                await HandleInvalidToken();
            }
            else
            {
                Json = result.Item2;
                JwtObj = JsonConvert.DeserializeObject<RRJwtObj>(Json);
                if (JwtObj?.User == null)
                {
                    await HandleValidReq();
                }
                else
                {
                    await HandleAuthenticatedReq();
                }
            }
        }
        public AuthJwtServiceResult GetResult()
        {
            return new AuthJwtServiceResult
            {
                IsValid = IsValid,
                ErrorMessage = ErrorMessage,
                JwtObj = JwtObj
            };
        }
        public static async Task<Tuple<bool, string, string>> GetToken(RRJwtObj obj, TimeSpan? timeSpan = null)
        {
            if (timeSpan == null)
            {
                timeSpan = TimeSpan.FromDays(30);
            }
            string json = JsonConvert.SerializeObject(obj);
            // Call another Azure Function to encrypt the data.
            var encryptedJsonLengthResult = await RRUtility.CallAnotherAzureFunction(Environment.GetEnvironmentVariable("CryptoServiceEncryptUrl")!, new { InputString = json.Length.ToString() });
            var encryptedJsonLength = await encryptedJsonLengthResult.Content.ReadAsStringAsync();
            if (!encryptedJsonLengthResult.IsSuccessStatusCode)
            {
                return new Tuple<bool, string, string>(false, string.Empty, encryptedJsonLength);
            }
            var encryptedJsonResult = await RRUtility.CallAnotherAzureFunction(Environment.GetEnvironmentVariable("CryptoServiceEncryptUrl")!, new { InputString = json });
            var encryptedJson = await encryptedJsonResult.Content.ReadAsStringAsync();
            if (!encryptedJsonResult.IsSuccessStatusCode)
            {
                return new Tuple<bool, string, string>(false, string.Empty, encryptedJson);
            }
            //var claimsdata = new[] { new Claim(UserDataClaimType, encryptedJson), new Claim(UserDataLengthClaimType, encryptedJsonLength) };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JwtSecretKey")!));
            var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            //var token = new JwtSecurityToken(
            //    issuer: "Royal Regal Bullion Limited",
            //    audience: "Clients of Royal Regal Bullion Limited",
            //    expires: DateTime.UtcNow.Add(timeSpan.Value),
            //    claims: claimsdata,
            //    signingCredentials: signInCred
            //    );
            //var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            var claimsdata = new[]
            {
                new Claim(UserDataClaimType, encryptedJson),
                new Claim(UserDataLengthClaimType, encryptedJsonLength),
                new Claim(JwtRegisteredClaimNames.Iss, "Royal Regal Bullion Limited"),
                new Claim(JwtRegisteredClaimNames.Aud, "Clients of Royal Regal Bullion Limited")
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.Add(timeSpan.Value),
                Claims = claimsdata.ToDictionary(c => c.Type, c => (object)c.Value),
                SigningCredentials = signInCred
            };
            var tokenHandler = new JsonWebTokenHandler();
            var jwt = tokenHandler.CreateToken(tokenDescriptor);

            return new Tuple<bool, string, string>(true, jwt, string.Empty);
        }
        public static async Task<Tuple<bool, string, string>> ValidateClaimsAsync(ClaimsPrincipal claimsPrincipal)
        {
            string encryptedStrLength = claimsPrincipal?.FindFirstValue(UserDataLengthClaimType) ?? string.Empty;
            if (string.IsNullOrEmpty(encryptedStrLength))
            {
                return new Tuple<bool, string, string>(false, string.Empty, "UserDataLength claim is not available.");
            }

            var decryptedJsonLengthResult = await RRUtility.CallAnotherAzureFunction(Environment.GetEnvironmentVariable("CryptoServiceDecryptUrl")!, new { InputString = encryptedStrLength });
            var strLength = await decryptedJsonLengthResult.Content.ReadAsStringAsync();
            if (!decryptedJsonLengthResult.IsSuccessStatusCode)
            {
                return new Tuple<bool, string, string>(false, string.Empty, $"UserDataLength claim decrypt fail. {strLength}");
            }

            bool success = int.TryParse(strLength, out int length);
            if (!success)
            {
                return new Tuple<bool, string, string>(false, string.Empty, "UserDataLength claim parse fail.");
            }
            string encryptedJson = claimsPrincipal?.FindFirstValue(UserDataClaimType) ?? string.Empty;
            if (string.IsNullOrEmpty(encryptedJson))
            {
                return new Tuple<bool, string, string>(false, string.Empty, "UserData claim is not available.");
            }

            var decryptedJsonResult = await RRUtility.CallAnotherAzureFunction(Environment.GetEnvironmentVariable("CryptoServiceDecryptUrl")!, new { InputString = encryptedJson });
            var json = await decryptedJsonResult.Content.ReadAsStringAsync();
            if (!decryptedJsonResult.IsSuccessStatusCode)
            {
                return new Tuple<bool, string, string>(false, string.Empty, $"UserData claim decrypt fail. {json}");
            }

            if (string.IsNullOrEmpty(json))
            {
                return new Tuple<bool, string, string>(false, string.Empty, "UserData claim is not available.");
            }
            success = success && (length == json.Length);
            if (!success)
            {
                return new Tuple<bool, string, string>(false, string.Empty, $"UserData claim length does not match. Json: {json.Length}, len: {length}");
            }
            return new Tuple<bool, string, string>(success, json, string.Empty);
        }
        public static async Task<Tuple<bool, string, string>> GetJsonFromTokenAsync(string token)
        {
            //var success = GetPrincipalFromToken(token, out ClaimsPrincipal? claimsPrincipal, out string errMsg);
            //if (!success)
            //{
            //    return new Tuple<bool, string, string>(false, string.Empty, errMsg);
            //}
            var resultToken = await GetPrincipalFromTokenAsync(token);
            if (!resultToken.Item1)
            {
                return new Tuple<bool, string, string>(false, string.Empty, resultToken.Item3);
            }
            var claimsPrincipal = resultToken.Item2;
            var result = await ValidateClaimsAsync(claimsPrincipal!);
            return result;
        }
        //private static bool GetPrincipalFromToken(string token, out ClaimsPrincipal? claimsPrincipal, out string errMsg)
        //{
        //    try
        //    {
        //        TokenValidationParameters validationParameters = new()
        //        {
        //            RequireExpirationTime = true,
        //            ValidateLifetime = true,
        //            ValidIssuer = "Royal Regal Bullion Limited",
        //            ValidAudience = "Clients of Royal Regal Bullion Limited",
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JwtSecretKey")!))
        //        };
        //        claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out SecurityToken validatedToken);
        //        errMsg = string.Empty;
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        claimsPrincipal = null;
        //        errMsg = ex.Message;
        //    }
        //    return false;
        //}
        private static async Task<Tuple<bool, ClaimsPrincipal?, string>> GetPrincipalFromTokenAsync(string token)
        {
            try
            {
                TokenValidationParameters validationParameters = new()
                {
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ValidIssuer = "Royal Regal Bullion Limited",
                    ValidAudience = "Clients of Royal Regal Bullion Limited",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JwtSecretKey")!))
                };

                var tokenHandler = new JsonWebTokenHandler();
                var validationResult = await tokenHandler.ValidateTokenAsync(token, validationParameters);

                if (!validationResult.IsValid)
                {
                    return new Tuple<bool, ClaimsPrincipal?, string>(false, null, validationResult.Exception?.Message ?? "Token validation failed.");
                }

                var claimsPrincipal = validationResult.ClaimsIdentity != null ? new ClaimsPrincipal(validationResult.ClaimsIdentity) : null;
                return new Tuple<bool, ClaimsPrincipal?, string>(true, claimsPrincipal, string.Empty);
            }
            catch (Exception ex)
            {
                return new Tuple<bool, ClaimsPrincipal?, string>(false, null, ex.Message);
            }
        }
    }
}
