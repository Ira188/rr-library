using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using rr_library.Models.AuthJwtService;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace rr_library.Models
{
    public static class RRUtility
    {
        /// <summary>
        /// Let IHttpClientFactory to handle the life cycle of HttpClient
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> CallAnotherAzureFunction(IHttpClientFactory httpClientFactory, string url, object data)
        {
            var client = httpClientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            return response;
        }
        [Obsolete]
        public static async Task<HttpResponseMessage> CallAnotherAzureFunction(string url, object data)
        {
            using var client = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            return response;
        }
        public static async Task<Tuple<bool, RRJwtObj?, string>> StandardInitConnectionHandling(IHttpClientFactory httpClientFactory, string? ClientIP, string? token, FunctionContext functionContext)
        {
            var TraceIdentifier = functionContext.InvocationId;
            var Logger = functionContext.GetLogger("StandardInitConnectionHandling");

            Input4ValidateToken input4ValidateToken = new()
            {
                OriginAuthorizationToken = token,
                ClientIP = ClientIP
            };
            using var resp4ValidateToken = await RRUtility.CallAnotherAzureFunction(httpClientFactory, Environment.GetEnvironmentVariable("ValidateTokenUrl")!, input4ValidateToken);
            if (!resp4ValidateToken.IsSuccessStatusCode)
            {
                string? msg = await resp4ValidateToken.Content.ReadAsStringAsync();
                Logger.LogDebug("{TraceIdentifier} ClientIP: {ClientIP}, msg: {msg}", TraceIdentifier, ClientIP, msg);
                return new Tuple<bool, RRJwtObj?, string>(false, null, msg);
            }
            var jwtObj = await resp4ValidateToken.Content.ReadFromJsonAsync<RRJwtObj>();
            return new Tuple<bool, RRJwtObj?, string>(true, jwtObj, string.Empty);
        }
        [Obsolete]
        public static async Task<Tuple<bool, RRJwtObj?, string>> StandardInitConnectionHandling(string? ClientIP, string? token, FunctionContext functionContext)
        {
            var TraceIdentifier = functionContext.InvocationId;
            var Logger = functionContext.GetLogger("StandardInitConnectionHandling");

            Input4ValidateToken input4ValidateToken = new()
            {
                OriginAuthorizationToken = token,
                ClientIP = ClientIP
            };
            using var resp4ValidateToken = await RRUtility.CallAnotherAzureFunction(Environment.GetEnvironmentVariable("ValidateTokenUrl")!, input4ValidateToken);
            if (!resp4ValidateToken.IsSuccessStatusCode)
            {
                string? msg = await resp4ValidateToken.Content.ReadAsStringAsync();
                Logger.LogDebug("{TraceIdentifier} ClientIP: {ClientIP}, msg: {msg}", TraceIdentifier, ClientIP, msg);
                return new Tuple<bool, RRJwtObj?, string>(false, null, msg);
            }
            var jwtObj = await resp4ValidateToken.Content.ReadFromJsonAsync<RRJwtObj>();
            return new Tuple<bool, RRJwtObj?, string>(true, jwtObj, string.Empty);
        }
        public static string CreateRandomCode(int length)
        {
            int rand;
            char code;
            string randomcode = string.Empty;
            //生成一定长度的验证码
            Random random = new();
            for (int i = 0; i < length; i++)
            {
                rand = random.Next();
                if (rand % 3 == 0)
                {
                    code = (char)('A' + (char)(rand % 26));
                }
                else
                {
                    code = (char)('0' + (char)(rand % 10));
                }
                randomcode += code.ToString();
            }
            return randomcode;
        }
    }
}
