using rrlib481.Db.Base;
using rrlib481.Db.Interfaces;
using rrlib481.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace rrlib481.Db.Fund
{
    /// <summary>
    /// Internal Fund Transfer
    /// 內部資金轉移
    /// </summary>
    public record InternalFundTransfer : BaseTxWithGenRef
    {
        #region Pure Copy, 複製區塊
        /// <summary>
        /// 生成Couchbase key
        /// </summary>
        /// <param name="sid">MT Server ID</param>
        /// <param name="yyyyMMdd">記錄生成的 年-月-日 日期 Created</param>
        /// <param name="reference">交易唯一編號 Unique ID</param>
        /// <returns>Generated Key for Couchbase</returns>
        public static string GenerateCbKey(string sid, string yyyyMMdd, string reference)
        {
            return $"{MethodBase.GetCurrentMethod().DeclaringType.Name}-{sid}-{yyyyMMdd}-{reference}";
        }
        public override string ChildGenerateCbKey()
        {
            if (string.IsNullOrEmpty(ServerID))
            {
                throw new ArgumentNullException("MTServerID is null or empty");
            }
            if (string.IsNullOrEmpty(Reference))
            {
                throw new ArgumentNullException("Reference is null or empty");
            }
            return GenerateCbKey(ServerID!, Created.ToString("yyyyMMdd"), Reference);
        }
        #endregion
        #region 交易相關
        /// <summary>
        /// Transfer Amount
        /// 轉移金額
        /// </summary>
        public decimal? Amount { get; set; }
        /// <summary>
        /// Destination Server ID
        /// 目標服務器代號
        /// </summary>
        public string? DestServerID { get; set; }
        /// <summary>
        /// Destination MT Account
        /// 目標MT帳號
        /// </summary>
        public string? DestAccount { get; set; }
        #endregion
    }
}
