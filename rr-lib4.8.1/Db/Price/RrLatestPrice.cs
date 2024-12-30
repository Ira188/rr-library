using MetaQuotes.MT5CommonAPI;
using rrlib481.Db.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace rrlib481.Db.Price
{
    public record RrLatestPrice : BaseDbRecord
    {
        public string Symbol { get; set; }
        public long Dt { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
        public decimal Last { get; set; }
        public long Datetime_msc { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal BidHigh { get; set; }
        public decimal BidLow { get; set; }
        public decimal AskHigh { get; set; }
        public decimal AskLow { get; set; }
        public decimal LastHigh { get; set; }
        public decimal LastLow { get; set; }

        public void FromMT(MTTickShort mTTickShort)
        {
            Dt = mTTickShort.datetime;
            Bid = new(mTTickShort.bid);
            Ask = new(mTTickShort.ask);
            Last = new(mTTickShort.last);
            Datetime_msc = mTTickShort.datetime_msc;
        }
        public void FromMT(MTTickStat mTTickStat)
        {
            Open = new(mTTickStat.price_open);
            Close = new(mTTickStat.price_close);
            BidHigh = new(mTTickStat.bid_high);
            BidLow = new(mTTickStat.bid_low);
            AskHigh = new(mTTickStat.ask_high);
            AskLow = new(mTTickStat.ask_low);
        }
        #region Pure Copy, 複製區塊
        /// <summary>
        /// 生成Couchbase key: RrLatestPrice-{Symbol}
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <returns>Generated Unique Key for Couchbase</returns>
        public static string GenerateCbKey(string symbol)
        {
            return $"{MethodBase.GetCurrentMethod().DeclaringType.Name}-{symbol}";
        }
        public override string ChildGenerateCbKey()
        {
            return GenerateCbKey(Symbol);
        }
        #endregion
    }
}
