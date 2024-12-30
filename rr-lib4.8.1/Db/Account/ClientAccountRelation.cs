using rrlib481.Db.Base;
using rrlib481.Db.TypesEnum;
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
    /// 客戶的上下級關係
    /// </summary>
    public record ClientAccountRelation : BaseBoRecord
    {
        /// <summary>
        /// 所屬銷售團隊
        /// </summary>
        public string? RelatedSalesTeam { get; set; }
        /// <summary>
        /// 客戶類型
        /// </summary>
        public ClientTypesEnum ClientTypes { get; set; }
        /// <summary>
        /// 上級帳號, Upper level account, 只有一個上級, 記錄上級的Reference
        /// </summary>
        public string? Upper { get; set; }
        /// <summary>
        /// IB下級帳號, Lower level accounts, 可能有多個下級, 記錄下級的Reference
        /// </summary>
        public List<string> Lowers { get; set; } = new();
        #region Pure Copy, 複製區塊
        /// <summary>
        /// 生成Couchbase key: ClientAccountRelation-{GUID}
        /// </summary>
        /// <param name="reference">Reference</param>
        /// <returns>Generated Unique Key for Couchbase</returns>
        public static string GenerateCbKey(string reference)
        {
            return $"{MethodBase.GetCurrentMethod().DeclaringType.Name}-{reference}";
        }
        public override string ChildGenerateCbKey()
        {
            if (Reference is null || string.IsNullOrEmpty(Reference))
            {
                throw new ArgumentNullException("Email is null or empty");
            }
            return GenerateCbKey(Reference);
        }
        #endregion
    }
}
