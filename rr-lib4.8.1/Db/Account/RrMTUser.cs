using MetaQuotes.MT5CommonAPI;
using MetaQuotes.MT5WebAPI.Common;
using rrlib481.Db.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace rrlib481.Db.Account
{
    public record RrMTUser : BaseDbRecord
    {
        /// <summary>
        /// Update from CIMTUser
        /// </summary>
        /// <param name="user">CIMTUser</param>
        public void UpdateFrom(CIMTUser user)
        {
            LastUpdated = DateTime.Now;
            //Login = user.Login(); this should be changed
            Group = user.Group();
            CertSerialNumber = user.CertSerialNumber();
            Rights = (EnUsersRights)user.Rights();
            MQID = user.MQID();
            Registration = user.Registration();
            LastAccess = user.LastAccess();
            LastPassChange = user.LastPassChange();
            LastIP = user.LastIP();
            Name = user.Name();
            Company = user.Company();
            Account = user.Account();
            Country = user.Country();
            Language = user.Language();
            ClientID = user.ClientID();
            City = user.City();
            State = user.State();
            ZIPCode = user.ZIPCode();
            Address = user.Address();
            Phone = user.Phone();
            Email = user.EMail();
            ID = user.ID();
            Status = user.Status();
            Comment = user.Comment();
            Color = user.Color();
            PhonePassword = user.PhonePassword();
            Leverage = user.Leverage();
            Agent = user.Agent();
            Balance = user.Balance();
            Credit = user.Credit();
            InterestRate = user.InterestRate();
            CommissionDaily = user.CommissionDaily();
            CommissionMonthly = user.CommissionMonthly();
            CommissionAgentDaily = user.CommissionAgentDaily();
            CommissionAgentMonthly = user.CommissionAgentMonthly();
            BalancePrevDay = user.BalancePrevDay();
            BalancePrevMonth = user.BalancePrevMonth();
            EquityPrevDay = user.EquityPrevDay();
            EquityPrevMonth = user.EquityPrevMonth();
            //rrMTUser.TradeAccounts = user.TradeAccounts(); //Don't have this property in CIMTUser
            LeadCampaign = user.LeadCampaign();
            LeadSource = user.LeadSource();
        }
        /// <summary>
        /// Create RrMTUser from CIMTUser
        /// </summary>
        /// <param name="user">CIMTUser object</param>
        /// <param name="sid">Server ID</param>
        /// <returns>RrMTUser</returns>
        public static RrMTUser CreateFrom(CIMTUser user, string sid, string reference)
        {
            RrMTUser rrMTUser = new()
            {
                ServerID = sid,
                Reference = reference,
                Login = user.Login(),
                Group = user.Group(),
                CertSerialNumber = user.CertSerialNumber(),
                Rights = (EnUsersRights)user.Rights(),
                MQID = user.MQID(),
                Registration = user.Registration(),
                LastAccess = user.LastAccess(),
                LastPassChange = user.LastPassChange(),
                LastIP = user.LastIP(),
                Name = user.Name(),
                Company = user.Company(),
                Account = user.Account(),
                Country = user.Country(),
                Language = user.Language(),
                ClientID = user.ClientID(),
                City = user.City(),
                State = user.State(),
                ZIPCode = user.ZIPCode(),
                Address = user.Address(),
                Phone = user.Phone(),
                Email = user.EMail(),
                ID = user.ID(),
                Status = user.Status(),
                Comment = user.Comment(),
                Color = user.Color(),
                PhonePassword = user.PhonePassword(),
                Leverage = user.Leverage(),
                Agent = user.Agent(),
                Balance = user.Balance(),
                Credit = user.Credit(),
                InterestRate = user.InterestRate(),
                CommissionDaily = user.CommissionDaily(),
                CommissionMonthly = user.CommissionMonthly(),
                CommissionAgentDaily = user.CommissionAgentDaily(),
                CommissionAgentMonthly = user.CommissionAgentMonthly(),
                BalancePrevDay = user.BalancePrevDay(),
                BalancePrevMonth = user.BalancePrevMonth(),
                EquityPrevDay = user.EquityPrevDay(),
                EquityPrevMonth = user.EquityPrevMonth(),
                //rrMTUser.TradeAccounts = user.TradeAccounts(); //Don't have this property in CIMTUser
                LeadCampaign = user.LeadCampaign(),
                LeadSource = user.LeadSource(),
            };
            return rrMTUser;
        }
        public static RrMTUser CreateFrom(MTUser user, string sid, string reference)
        {
            RrMTUser rrMTUser = new();
            rrMTUser.ServerID = sid;
            rrMTUser.Reference = reference;
            rrMTUser.Login = user.Login;
            rrMTUser.Group = user.Group;
            rrMTUser.CertSerialNumber = user.CertSerialNumber;
            rrMTUser.Rights = (EnUsersRights)user.Rights;
            rrMTUser.MQID = user.MQID;
            rrMTUser.Registration = user.Registration;
            rrMTUser.LastAccess = user.LastAccess;
            rrMTUser.LastPassChange = user.LastPassChange;
            rrMTUser.LastIP = user.LastIP;
            rrMTUser.Name = user.Name;
            rrMTUser.Company = user.Company;
            rrMTUser.Account = user.Account;
            rrMTUser.Country = user.Country;
            rrMTUser.Language = user.Language;
            rrMTUser.ClientID = user.ClientID;
            rrMTUser.City = user.City;
            rrMTUser.State = user.State;
            rrMTUser.ZIPCode = user.ZIPCode;
            rrMTUser.Address = user.Address;
            rrMTUser.Phone = user.Phone;
            rrMTUser.Email = user.Email;
            rrMTUser.ID = user.ID;
            rrMTUser.Status = user.Status;
            rrMTUser.Comment = user.Comment;
            rrMTUser.Color = user.Color;
            rrMTUser.PhonePassword = user.PhonePassword;
            rrMTUser.Leverage = user.Leverage;
            rrMTUser.Agent = user.Agent;
            rrMTUser.Balance = user.Balance;
            rrMTUser.Credit = user.Credit;
            rrMTUser.InterestRate = user.InterestRate;
            rrMTUser.CommissionDaily = user.CommissionDaily;
            rrMTUser.CommissionMonthly = user.CommissionMonthly;
            rrMTUser.CommissionAgentDaily = user.CommissionAgentDaily;
            rrMTUser.CommissionAgentMonthly = user.CommissionAgentMonthly;
            rrMTUser.BalancePrevDay = user.BalancePrevDay;
            rrMTUser.BalancePrevMonth = user.BalancePrevMonth;
            rrMTUser.EquityPrevDay = user.EquityPrevDay;
            rrMTUser.EquityPrevMonth = user.EquityPrevMonth;
            rrMTUser.LeadCampaign = user.LeadCampaign;
            rrMTUser.LeadSource = user.LeadSource;
            return rrMTUser;
        }
        /// <summary>
        /// Unique ID of the account - GUID currently
        /// 用這個Reference去找到ClientAccountInfo, ClientAccountRelation
        /// </summary>
        public string Reference { get; set; }
        /// <summary>
        /// 帳號歸屬的服務器代號
        /// </summary>
        public string ServerID { get; set; }
        private int EXTERNAL_ID_MAXLEN { get; } = 32;
        private int EXTERNAL_ID_LIMIT { get; } = 128;
        public enum EnUsersRights : ulong
        {
            USER_RIGHT_NONE = 0x0000000000000000,  // none
            USER_RIGHT_ENABLED = 0x0000000000000001,  // client allowed to connect
            USER_RIGHT_PASSWORD = 0x0000000000000002,  // client allowed to change password
            USER_RIGHT_TRADE_DISABLED = 0x0000000000000004,  // client trading disabled
            USER_RIGHT_INVESTOR = 0x0000000000000008,  // client is investor
            USER_RIGHT_CONFIRMED = 0x0000000000000010,  // client certificate confirmed
            USER_RIGHT_TRAILING = 0x0000000000000020,  // trailing stops are allowed
            USER_RIGHT_EXPERT = 0x0000000000000040,  // expert advisors are allowed
            USER_RIGHT_OBSOLETE = 0x0000000000000080,  // obsolete value
            USER_RIGHT_REPORTS = 0x0000000000000100,  // trade reports are allowed
            USER_RIGHT_READONLY = 0x0000000000000200,  // client is readonly
            USER_RIGHT_RESET_PASS = 0x0000000000000400,  // client must change password at next login
            USER_RIGHT_OTP_ENABLED = 0x0000000000000800,  // client allowed to use one-time password
            USER_RIGHT_CLIENT_CONFIRMED = 0x0000000000001000,
            USER_RIGHT_SPONSORED_HOSTING = 0x0000000000002000,  // client allowed to use sponsored by broker MetaTrader Virtual Hosting
            USER_RIGHT_API_ENABLED = 0x0000000000004000,  // client API are allowed
                                                          //--- enumeration borders
            USER_RIGHT_DEFAULT = USER_RIGHT_ENABLED | USER_RIGHT_PASSWORD | USER_RIGHT_TRAILING | USER_RIGHT_EXPERT | USER_RIGHT_REPORTS,
            USER_RIGHT_ALL = USER_RIGHT_ENABLED | USER_RIGHT_PASSWORD | USER_RIGHT_TRADE_DISABLED |
            USER_RIGHT_INVESTOR | USER_RIGHT_CONFIRMED | USER_RIGHT_TRAILING |
            USER_RIGHT_EXPERT | USER_RIGHT_REPORTS |
            USER_RIGHT_READONLY | USER_RIGHT_RESET_PASS | USER_RIGHT_OTP_ENABLED | USER_RIGHT_CLIENT_CONFIRMED | USER_RIGHT_SPONSORED_HOSTING
        };
        /// <summary>
        /// password types
        /// </summary>
        public enum EnUsersPasswords : uint
        {
            USER_PASS_MAIN = 0,
            USER_PASS_INVESTOR = 1,
            USER_PASS_API = 2,
            //--- 
            USER_PASS_FIRST = USER_PASS_MAIN,
            USER_PASS_LAST = USER_PASS_API,
        };
        /// <summary>
        /// connection types
        /// </summary>
        public enum EnUsersConnectionTypes : uint
        {
            /// <summary>
            /// client types
            /// </summary>
            USER_TYPE_CLIENT = 0,
            USER_TYPE_CLIENT_WINMOBILE = 1,
            USER_TYPE_CLIENT_WINPHONE = 2,
            USER_TYPE_CLIENT_IPHONE = 4,
            USER_TYPE_CLIENT_ANDROID = 5,
            USER_TYPE_CLIENT_BLACKBERRY = 6,
            USER_TYPE_CLIENT_WEB = 11,
            /// <summary>
            /// manager types
            /// </summary>
            USER_TYPE_ADMIN = 32,
            USER_TYPE_MANAGER = 33,
            USER_TYPE_MANAGER_API = 34,
            USER_TYPE_ADMIN_API = 36,
            /// <summary>
            /// enumeration borders
            /// </summary>
            USER_TYPE_FIRST = USER_TYPE_CLIENT,
            USER_TYPE_LAST = USER_TYPE_ADMIN_API
        };
        /// <summary>
        /// login
        /// </summary>
        public ulong Login { get; set; }
        /// <summary>
        /// group
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// certificate serial number
        /// </summary>
        public ulong CertSerialNumber { get; set; }
        /// <summary>
        /// EnUsersRights
        /// </summary>
        public EnUsersRights Rights { get; set; }
        /// <summary>
        /// client's MetaQuotes ID
        /// </summary>
        public string MQID { get; set; }
        /// <summary>
        /// registration datetime (filled by MT5)
        /// </summary>
        public long Registration { get; set; }
        /// <summary>
        /// last access datetime (filled by MT5)
        /// </summary>
        public long LastAccess { get; set; }
        /// <summary>
        /// last password change datetime (filled by MT5)
        /// </summary>
        public long LastPassChange { get; set; }
        /// <summary>
        /// last ip-address
        /// </summary>
        public string LastIP { get; set; }
        /// <summary>
        /// name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// company
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// external system account (exchange, ECN, etc)
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// country
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// client language (WinAPI LANGID)
        /// </summary>
        public uint Language { get; set; }
        /// <summary>
        /// identificator by client
        /// </summary>
        public ulong ClientID { get; set; }
        /// <summary>
        /// city
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// state
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// ZIP code
        /// </summary>
        public string ZIPCode { get; set; }
        /// <summary>
        /// address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// phone
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// additional ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// additional status
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// comment
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// color
        /// </summary>
        public uint Color { get; set; }
        /// <summary>
        /// phone password
        /// </summary>
        public string PhonePassword { get; set; }
        /// <summary>
        /// leverage
        /// </summary>
        public uint Leverage { get; set; }
        /// <summary>
        /// agent account
        /// </summary>
        public ulong Agent { get; set; }
        /// <summary>
        /// balance
        /// </summary>
        public double Balance { get; set; }
        /// <summary>
        /// credit
        /// </summary>
        public double Credit { get; set; }
        /// <summary>
        /// accumulated interest rate
        /// </summary>
        public double InterestRate { get; set; }
        /// <summary>
        /// accumulated daily commissions
        /// </summary>
        public double CommissionDaily { get; set; }
        /// <summary>
        /// accumulated monthly commissions
        /// </summary>
        public double CommissionMonthly { get; set; }
        /// <summary>
        /// accumulated daily agent commissions
        /// </summary>
        public double CommissionAgentDaily { get; set; }
        /// <summary>
        /// accumulated  monthly agent commissions
        /// </summary>
        public double CommissionAgentMonthly { get; set; }
        /// <summary>
        /// previous balance state day
        /// </summary>
        public double BalancePrevDay { get; set; }
        /// <summary>
        /// previous balance state month
        /// </summary>
        public double BalancePrevMonth { get; set; }
        /// <summary>
        /// previous equity state day
        /// </summary>
        public double EquityPrevDay { get; set; }
        /// <summary>
        /// previous equity state month
        /// </summary>
        public double EquityPrevMonth { get; set; }
        /// <summary>
        /// external trade accounts
        /// </summary>
        //public string TradeAccounts { get; set; }
        /// <summary>
        /// lead campaign
        /// </summary>
        public string LeadCampaign { get; set; }
        /// <summary>
        /// lead source
        /// </summary>
        public string LeadSource { get; set; }
        /// <summary>
        /// Create user with default values
        /// </summary>
        public static RrMTUser CreateDefault()
        {
            RrMTUser user = new();
            //---
            user.Rights = EnUsersRights.USER_RIGHT_DEFAULT;
            user.Leverage = 100;
            user.Color = 0xFF000000;
            //---
            return user;
        }
        #region Pure Copy, 複製區塊
        /// <summary>
        /// 生成Couchbase key: RrMTUser-{ServerID}-{Login}
        /// </summary>
        /// <param name="login">MT5 Login</param>
        /// <returns>Generated Unique Key for Couchbase</returns>
        public static string GenerateCbKey(string sid, ulong login)
        {
            return $"{MethodBase.GetCurrentMethod().DeclaringType.Name}-{sid}-{login}";
        }
        public override string ChildGenerateCbKey()
        {
            if (Login == 0)
            {
                throw new ArgumentNullException("Login is null or zero");
            }
            if (string.IsNullOrEmpty(ServerID))
            {
                throw new ArgumentNullException("ServerID is null or empty");
            }
            return GenerateCbKey(ServerID, Login);
        }
        #endregion
    }
}
