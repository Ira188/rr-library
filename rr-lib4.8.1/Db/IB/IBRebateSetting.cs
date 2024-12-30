using rrlib481.Db.Base;
using rrlib481.Db.TypesEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace rrlib481.Db.IB
{
    public record IBRebateSetting : BaseDbRecord
    {
        /// <summary>
        /// 最高返佣比例
        /// </summary>
        public decimal? UpperRebateRate { get; set; }
        /// <summary>
        /// 最低返佣比例
        /// </summary>
        public decimal? LowerRebateRate { get; set; }
        /// <summary>
        /// 返佣類型
        /// Normal(2): 正常 - 不跳Bar
        /// Bar(3): 跳Bar
        /// 以上兩個當 LowerRebateRate > 0 時, 需要程序計算佣金
        /// 其他需要財務計算佣金
        /// </summary>
        public RebateTypesEnum? RebateTypes { get; set; }
        /// <summary>
        /// 交易品種代號, 用作識別交易品種
        /// </summary>
        public string? Symbol { get; set; }
        /// <summary>
        /// Unique ID of the account - GUID currently
        /// </summary>
        public string? Reference { get; set; }
        #region Pure Copy, 複製區塊
        /// <summary>
        /// 生成Couchbase key: IBRebateSetting-{Symbol}-{GUID}
        /// </summary>
        /// <param name="symbol">Symbol 交易品種</param>
        /// <param name="reference">Reference</param>
        /// <returns>Generated Unique Key for Couchbase</returns>
        public static string GenerateCbKey(string symbol, string reference)
        {
            return $"{MethodBase.GetCurrentMethod().DeclaringType.Name}-{symbol}-{reference}";
        }
        public override string ChildGenerateCbKey()
        {
            if (Reference is null || string.IsNullOrEmpty(Reference))
            {
                throw new ArgumentNullException("Email is null or empty");
            }
            if (Symbol is null || string.IsNullOrEmpty(Symbol))
            {
                throw new ArgumentNullException("Symbol is null or empty");
            }
            return GenerateCbKey(Symbol, Reference);
        }
        #endregion
    }
}
