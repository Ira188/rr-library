using rrlib481.Db.Base;
using rrlib481.Db.TypesEnum;
using rrlib481.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace rrlib481.Db.Account
{
    /// <summary>
    /// 客戶擁有的關聯交易帳戶
    /// </summary>
    public record struct RelatedTradingAccount
    {
        /// <summary>
        /// Server ID
        /// </summary>
        public string? ServerID { get; set; }
        /// <summary>
        /// Trading Account
        /// </summary>
        public string? TradingAccount { get; set; }
    }
    /// <summary>
    /// 客戶帳戶資料
    /// </summary>
    public record ClientAccountInfo : BaseBoRecord
    {
        /// <summary>
        /// 客戶類型
        /// 修改時, 需要同步修改ClientAccountRelation
        /// </summary>
        public ClientTypesEnum ClientTypes { get; set; }
        /// <summary>
        /// 郵箱, Email
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// 姓, Surname, Family Name
        /// </summary>
        public string? FamilyName { get; set; }
        /// <summary>
        /// 名, Given Name, First Name
        /// </summary>
        public string? GivenName { get; set; }
        /// <summary>
        /// 手機號碼, Format: 852-12345678 or 86-12345678901
        /// </summary>
        public string? Mobile { get; set; }
        /// <summary>
        /// Recovery Email
        /// </summary>
        public string? AltEmail { get; set; }
        /// <summary>
        /// Recovery Mobile
        /// </summary>
        public string? AltMobile { get; set; }
        public List<RelatedTradingAccount> RelatedTradingAccounts { get; set; } = new();
        #region Pure Copy, 複製區塊
        /// <summary>
        /// 生成Couchbase key: ClientAccountInfo-{GUID}
        /// </summary>
        /// <param name="reference">Email</param>
        /// <returns>Generated Unique Key for Couchbase</returns>
        public static string GenerateCbKey(string reference)
        {
            return $"{MethodBase.GetCurrentMethod().DeclaringType.Name}-{reference}";
        }
        public override string ChildGenerateCbKey()
        {
            if (Reference is null || string.IsNullOrEmpty(Reference))
            {
                throw new ArgumentNullException("Reference is null or empty");
            }
            return GenerateCbKey(Reference);
        }
        #endregion
    }
}
