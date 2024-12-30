using rrlib481.Db.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace rrlib481.Db.Fund
{
    public record struct RebateRecordDetail
    {
        /// <summary>
        /// IB account
        /// </summary>
        public string IbAccount { get; set; }
        /// <summary>
        /// Rebate amount
        /// </summary>
        public decimal RebateAmount { get; set; }
        /// <summary>
        /// 返佣比例
        /// </summary>
        public decimal RebateRate { get; set; }
        /// <summary>
        /// 佣金訂單序號
        /// </summary>
        public string RebateTxId { get; set; }
    }
    /// <summary>
    /// 返佣记录
    /// </summary>
    public record RebateRecord : BaseDbRecord
    {
        /// <summary>
        /// 是否返佣完成
        /// </summary>
        public bool IsCompleted { get; set; } = false;
        /// <summary>
        /// Transaction ID, For MT5, its Deal ID 交易序號
        /// </summary>
        public string? TxId { get; set; }
        /// <summary>
        /// 帳號歸屬的MT服務器代號
        /// </summary>
        public string? ServerID { get; set; }
        /// <summary>
        /// 這筆訂單的交易帳號
        /// </summary>
        public string? TradeAccount { get; set; }
        /// <summary>
        /// Rebate records
        /// </summary>
        public List<RebateRecordDetail> RebatedRecords { get; set; } = new List<RebateRecordDetail>();
        /// <summary>
        /// 生成Couchbase key: RebateRecord-{ServerID}-{TxId}
        /// </summary>
        /// <param name="sid">Server ID</param>
        /// <param name="tx_id">Transaction ID, For MT5, its Deal ID</param>
        /// <returns>Generated Key for Couchbase</returns>
        public static string GenerateCbKey(string sid, string tx_id)
        {
            return $"{MethodBase.GetCurrentMethod().DeclaringType.Name}-{sid}-{tx_id}";
        }
        #region Pure Copy, 複製區塊
        public override string ChildGenerateCbKey()
        {
            if (string.IsNullOrEmpty(ServerID))
            {
                throw new ArgumentNullException("MTServerID is null or empty");
            }
            if (string.IsNullOrEmpty(TxId))
            {
                throw new ArgumentNullException("TxId is null or empty");
            }
            return GenerateCbKey(ServerID!, TxId!);
        }
        #endregion
    }
}
