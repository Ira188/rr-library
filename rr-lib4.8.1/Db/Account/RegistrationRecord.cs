using rrlib481.Db.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace rrlib481.Db.Account
{
    public record RegistrationRecord : BaseBoRecord
    {
        /// <summary>
        /// 帳號歸屬的MT服務器代號
        /// </summary>
        public string? ServerID { get; set; }
        /// <summary>
        /// 交易服務器的帳號
        /// </summary>
        public string? TradingAccount { get; set; }
        /// <summary>
        /// 姓, Surname, Family Name
        /// </summary>
        public string? FamilyName { get; set; }
        /// <summary>
        /// 名, Given Name
        /// </summary>
        public string? GivenName { get; set; }
        /// <summary>
        /// 電子郵件, Email
        /// </summary>
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? IdDocument { get; set; }
        public string? IB { get; set; }
        #region Pure Copy, 複製區塊
        /// <summary>
        /// 生成Couchbase key: RegistrationRecord-{yyyyMMdd}-{Ref}
        /// </summary>
        /// <param name="yyyyMMdd">記錄生成的 年-月-日 日期 Created</param>
        /// <param name="Ref">交易唯一編號 Unique ID</param>
        /// <returns>Generated Key for Couchbase</returns>
        public static string GenerateCbKey(string yyyyMMdd, string Ref)
        {
            return $"{MethodBase.GetCurrentMethod().DeclaringType.Name}-{yyyyMMdd}-{Ref}";
        }
        public override string ChildGenerateCbKey()
        {
            if (string.IsNullOrEmpty(Reference))
            {
                throw new ArgumentNullException("Reference is null or empty");
            }
            return GenerateCbKey(Created.ToString("yyyyMMdd"), Reference);
        }
        #endregion
    }
}
