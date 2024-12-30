using MetaQuotes.MT5CommonAPI;
using MetaQuotes.MT5WebAPI.Common;
using MetaQuotes.MT5WebAPI.Common.Utils;
using rrlib481.Db.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace rrlib481.Db.Trading
{
    public record RrDeal : BaseDbRecord
    {
        /// <summary>
        /// Update from CIMTDeal
        /// </summary>
        /// <param name="deal">CIMTDeal</param>
        public void UpdateFrom(CIMTDeal deal)
        {
            LastUpdated = DateTime.Now;
            //Deal = deal.Deal(); this can't be changed
            ExternalID = deal.ExternalID();
            Login = deal.Login();
            Dealer = deal.Dealer();
            Order = deal.Order();
            Action = (EnDealAction)deal.Action();
            Entry = (EnEntryFlags)deal.Entry();
            Reason = (EnDealReason)deal.Reason();
            Digits = deal.Digits();
            DigitsCurrency = deal.DigitsCurrency();
            ContractSize = deal.ContractSize();
            Time = deal.Time();
            TimeMsc = deal.TimeMsc();
            Symbol = deal.Symbol();
            Price = deal.Price();
            Volume = deal.Volume();
            VolumeExt = deal.VolumeExt();
            Profit = deal.Profit();
            Storage = deal.Storage();
            Commission = deal.Commission();
            //CommissionAgent = deal.CommissionAgent(); // Not available in CIMTDeal
            RateProfit = deal.RateProfit();
            RateMargin = deal.RateMargin();
            ExpertID = deal.ExpertID();
            PositionID = deal.PositionID();
            Comment = deal.Comment();
            ProfitRaw = deal.ProfitRaw();
            PricePosition = deal.PricePosition();
            VolumeClosed = deal.VolumeClosed();
            VolumeClosedExt = deal.VolumeClosedExt();
            TickValue = deal.TickValue();
            TickSize = deal.TickSize();
            Flags = deal.Flags();
            Gateway = deal.Gateway();
            PriceGateway = deal.PriceGateway();
            ModifyFlags = (EnTradeModifyFlags)deal.ModificationFlags();
            PriceSL = deal.PriceSL();
            PriceTP = deal.PriceTP();
        }
        /// <summary>
        /// Create RrMTDeal from CIMTDeal
        /// </summary>
        /// <param name="deal">MT5 CIMTDeal object</param>
        /// <param name="sid">Server ID</param>
        /// <returns>RrMTDeal</returns>
        public static RrDeal CreateFrom(CIMTDeal deal, string sid)
        {
            RrDeal rrMTDeal = new()
            {
                IsActive = true,
                LastUpdated = DateTime.Now,
                ServerID = sid,
                Deal = deal.Deal(),
                ExternalID = deal.ExternalID(),
                Login = deal.Login(),
                Dealer = deal.Dealer(),
                Order = deal.Order(),
                Action = (EnDealAction)deal.Action(),
                Entry = (EnEntryFlags)deal.Entry(),
                Reason = (EnDealReason)deal.Reason(),
                Digits = deal.Digits(),
                DigitsCurrency = deal.DigitsCurrency(),
                ContractSize = deal.ContractSize(),
                Time = deal.Time(),
                TimeMsc = deal.TimeMsc(),
                Symbol = deal.Symbol(),
                Price = deal.Price(),
                Volume = deal.Volume(),
                VolumeExt = deal.VolumeExt(),
                Profit = deal.Profit(),
                Storage = deal.Storage(),
                Commission = deal.Commission(),
                //CommissionAgent = deal.CommissionAgent(); // Not available in CIMTDeal
                RateProfit = deal.RateProfit(),
                RateMargin = deal.RateMargin(),
                ExpertID = deal.ExpertID(),
                PositionID = deal.PositionID(),
                Comment = deal.Comment(),
                ProfitRaw = deal.ProfitRaw(),
                PricePosition = deal.PricePosition(),
                VolumeClosed = deal.VolumeClosed(),
                VolumeClosedExt = deal.VolumeClosedExt(),
                TickValue = deal.TickValue(),
                TickSize = deal.TickSize(),
                Flags = deal.Flags(),
                Gateway = deal.Gateway(),
                PriceGateway = deal.PriceGateway(),
                ModifyFlags = (EnTradeModifyFlags)deal.ModificationFlags(),
                PriceSL = deal.PriceSL(),
                PriceTP = deal.PriceTP(),
            };
            return rrMTDeal;
        }
        /// <summary>
        /// Create RrMTDeal from MTDeal
        /// </summary>
        /// <param name="deal">MTDeal object</param>
        /// <param name="sid">Server ID</param>
        /// <returns>RrMTDeal</returns>
        public static RrDeal CreateFrom(MTDeal deal, string sid)
        {
            RrDeal rrMTDeal = new()
            {
                IsActive = true,
                LastUpdated = DateTime.Now,
                Deal = deal.Deal,
                ExternalID = deal.ExternalID,
                Login = deal.Login,
                Dealer = deal.Dealer,
                Order = deal.Order,
                Action = (EnDealAction)deal.Action,
                Entry = (EnEntryFlags)deal.Entry,
                Reason = (EnDealReason)deal.Reason,
                Digits = deal.Digits,
                DigitsCurrency = deal.DigitsCurrency,
                ContractSize = deal.ContractSize,
                Time = deal.Time,
                TimeMsc = deal.TimeMsc,
                Symbol = deal.Symbol,
                Price = deal.Price,
                Volume = deal.Volume,
                VolumeExt = deal.VolumeExt,
                Profit = deal.Profit,
                Storage = deal.Storage,
                Commission = deal.Commission,
                // CommissionAgent should not exist in MTDeal, since MT5 manual does not mention it
                CommissionAgent = deal.CommissionAgent,
                RateProfit = deal.RateProfit,
                RateMargin = deal.RateMargin,
                ExpertID = deal.ExpertID,
                PositionID = deal.PositionID,
                Comment = deal.Comment,
                ProfitRaw = deal.ProfitRaw,
                PricePosition = deal.PricePosition,
                VolumeClosed = deal.VolumeClosed,
                VolumeClosedExt = deal.VolumeClosedExt,
                TickValue = deal.TickValue,
                TickSize = deal.TickSize,
                Flags = deal.Flags,
                Gateway = deal.Gateway,
                PriceGateway = deal.PriceGateway,
                ModifyFlags = (EnTradeModifyFlags)deal.ModifyFlags,
                PriceSL = deal.PriceSL,
                PriceTP = deal.PriceTP,
                ServerID = sid
            };
            return rrMTDeal;
        }
        /// <summary>
        /// Copy RrMTDeal to CIMTDeal
        /// but the deal ticket can't be changed
        /// </summary>
        /// <param name="deal">CIMTDeal object</param>
        public Tuple<MTRetCode, string> ToCITMTDeal(ref CIMTDeal deal)
        {
            var ret = deal.ExternalID(ExternalID);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "ExternalID");
            }
            ret = deal.Login(Login);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "Login");
            }
            ret = deal.Dealer(Dealer);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "Dealer");
            }
            ret = deal.Order(Order);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "Order");
            }
            ret = deal.Action((uint)(Action));
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "Action");
            }
            ret = deal.Entry((uint)(Entry));
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "Entry");
            }
            //deal.Reason((int)(Reason ?? EnDealReason.DEAL_REASON_CLIENT));
            ret = deal.Digits(Digits);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "Digits");
            }
            ret = deal.DigitsCurrency(DigitsCurrency);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "DigitsCurrency");
            }
            ret = deal.ContractSize(ContractSize);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "ContractSize");
            }
            ret = deal.Time(Time);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "Time");
            }
            ret = deal.TimeMsc(TimeMsc ?? 0);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "TimeMsc");
            }
            ret = deal.Symbol(Symbol);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "Symbol");
            }
            ret = deal.Price(Price);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "Price");
            }
            ret = deal.Volume(Volume);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "Volume");
            }
            ret = deal.VolumeExt(VolumeExt);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "VolumeExt");
            }
            ret = deal.Profit(Profit);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "Profit");
            }
            ret = deal.Storage(Storage);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "Storage");
            }
            ret = deal.Commission(Commission);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "Commission");
            }
            //deal.CommissionAgent(CommissionAgent ?? 0);
            ret = deal.RateProfit(RateProfit);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "RateProfit");
            }
            ret = deal.RateMargin(RateMargin);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "RateMargin");
            }
            ret = deal.ExpertID(ExpertID);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "ExpertID");
            }
            ret = deal.PositionID(PositionID);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "PositionID");
            }
            ret = deal.Comment(Comment);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "Comment");
            }
            ret = deal.ProfitRaw(ProfitRaw);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "ProfitRaw");
            }
            ret = deal.PricePosition(PricePosition);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "PricePosition");
            }
            ret = deal.VolumeClosed(VolumeClosed);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "VolumeClosed");
            }
            ret = deal.VolumeClosedExt(VolumeClosedExt);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "VolumeClosedExt");
            }
            ret = deal.TickValue(TickValue);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "TickValue");
            }
            ret = deal.TickSize(TickSize);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "TickSize");
            }
            ret = deal.Flags(Flags);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "Flags");
            }
            //deal.Gateway(Gateway);
            //deal.PriceGateway(PriceGateway ?? 0);
            //deal.ModificationFlags((int)(ModifyFlags ?? EnTradeModifyFlags.MODIFY_FLAGS_NONE));
            ret = deal.PriceSL(PriceSL);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "PriceSL");
            }
            ret = deal.PriceTP(PriceTP);
            if (ret != MTRetCode.MT_RET_OK && ret != MTRetCode.MT_RET_OK_NONE)
            {
                return new Tuple<MTRetCode, string>(ret, "PriceTP");
            }
            return new Tuple<MTRetCode, string>(MTRetCode.MT_RET_OK_NONE, null);
        }
        /// <summary>
        /// 歸屬服務器代號
        /// </summary>
        public string ServerID { get; set; }
        /// <summary>
        /// 生成Couchbase key
        /// </summary>
        /// <param name="sid">Server ID</param>
        /// <param name="deal">MT5 deal ticket</param>
        /// <returns>Generated Key for Couchbase</returns>
        public static string GenerateCbKey(string sid, ulong deal)
        {
            return $"{MethodBase.GetCurrentMethod().DeclaringType.Name}-{sid}-{deal}";
        }
        #region Pure Copy, 複製區塊
        public override string ChildGenerateCbKey()
        {
            if (string.IsNullOrEmpty(ServerID))
            {
                throw new ArgumentNullException("ServerID is null or empty");
            }
            if (Deal == 0)
            {
                throw new ArgumentNullException("Deal is null or zero");
            }
            return GenerateCbKey(ServerID, Deal);
        }
        #endregion
        /// <summary>
        /// Volume
        /// </summary>
        private ulong m_Volume { get; set; } = 0;
        /// <summary>
        /// Closed Volume
        /// </summary>
        private ulong m_VolumeClosed { get; set; } = 0;
        /// <summary>
        /// Common deal flags
        /// </summary>
        public enum EnDealFlags : uint
        {
            DEAL_FLAGS_NONE = 0x00000000,
            DEAL_FLAGS_ROLLOVER = 0x00000001,
            DEAL_FLAGS_VMARGIN = 0x00000002,
            //--- enumeration borders
            DEAL_FLAGS_ALL = DEAL_FLAGS_ROLLOVER | DEAL_FLAGS_VMARGIN
        };
        /// <summary>
        /// Action deal
        /// </summary>
        public enum EnDealAction : uint
        {
            DEAL_BUY = 0,     // buy
            DEAL_SELL = 1,      // sell
            DEAL_BALANCE = 2,     // deposit operation
            DEAL_CREDIT = 3,      // credit operation
            DEAL_CHARGE = 4,      // additional charges
            DEAL_CORRECTION = 5,      // correction deals
            DEAL_BONUS = 6,     // bouns
            DEAL_COMMISSION = 7,      // commission
            DEAL_COMMISSION_DAILY = 8,      // daily commission
            DEAL_COMMISSION_MONTHLY = 9,      // monthly commission
            DEAL_AGENT_DAILY = 10,      // daily agent commission
            DEAL_AGENT_MONTHLY = 11,      // monthly agent commission
            DEAL_INTERESTRATE = 12,     // interest rate charges
            DEAL_BUY_CANCELED = 13,     // canceled buy deal
            DEAL_SELL_CANCELED = 14,      // canceled sell deal
            DEAL_DIVIDEND = 15,     // dividend
            DEAL_DIVIDEND_FRANKED = 16,     // franked dividend
            DEAL_TAX = 17,      // taxes
            DEAL_AGENT = 18,      // instant agent commission
            DEAL_SO_COMPENSATION = 19,      // negative balance compensation after stop-out
                                            //--- enumeration borders
            DEAL_FIRST = DEAL_BUY,
            DEAL_LAST = DEAL_SO_COMPENSATION
        };
        /// <summary>
        /// deal entry direction
        /// </summary>
        public enum EnEntryFlags : uint
        {
            ENTRY_IN = 0,     // in market
            ENTRY_OUT = 1,      // out of market
            ENTRY_INOUT = 2,      // reverse
            ENTRY_OUT_BY = 3,     // closed by  hedged position
            ENTRY_STATE = 255,      // state record
                                    //--- enumeration borders
            ENTRY_FIRST = ENTRY_IN,
            ENTRY_LAST = ENTRY_STATE
        };
        /// <summary>
        /// deal creation reasons
        /// </summary>
        public enum EnDealReason : uint
        {
            DEAL_REASON_CLIENT = 0,     // deal placed manually
            DEAL_REASON_EXPERT = 1,     // deal placed by expert
            DEAL_REASON_DEALER = 2,     // deal placed by dealer
            DEAL_REASON_SL = 3,     // deal placed due SL
            DEAL_REASON_TP = 4,     // deal placed due TP
            DEAL_REASON_SO = 5,     // deal placed due Stop-Out
            DEAL_REASON_ROLLOVER = 6,     // deal placed due rollover
            DEAL_REASON_EXTERNAL_CLIENT = 7,   // deal placed from the external system by client
            DEAL_REASON_VMARGIN = 8,     // deal placed due variation margin
            DEAL_REASON_GATEWAY = 9,     // deal placed by gateway
            DEAL_REASON_SIGNAL = 10,    // deal placed by signal service
            DEAL_REASON_SETTLEMENT = 11,    // deal placed due settlement
            DEAL_REASON_TRANSFER = 12,    // deal placed due position transfer
            DEAL_REASON_SYNC = 13,    // deal placed due position synchronization
            DEAL_REASON_EXTERNAL_SERVICE = 14, // deal placed from the external system due service issues
            DEAL_REASON_MIGRATION = 15,    // deal placed due migration
            DEAL_REASON_MOBILE = 16,    // deal placed manually by mobile terminal
            DEAL_REASON_WEB = 17,    // deal placed manually by web terminal
            DEAL_REASON_SPLIT = 18,    // deal placed due split
                                       //--- enumeration borders
            DEAL_REASON_FIRST = DEAL_REASON_CLIENT,
            DEAL_REASON_LAST = DEAL_REASON_SPLIT
        };
        /// <summary>
        /// modification flags
        /// </summary>
        public enum EnTradeModifyFlags : uint
        {
            MODIFY_FLAGS_ADMIN = 1,
            MODIFY_FLAGS_MANAGER = 2,
            MODIFY_FLAGS_POSITION = 4,
            MODIFY_FLAGS_RESTORE = 8,
            MODIFY_FLAGS_API_ADMIN = 16,
            MODIFY_FLAGS_API_MANAGER = 32,
            MODIFY_FLAGS_API_SERVER = 64,
            MODIFY_FLAGS_API_GATEWAY = 128,
            MODIFY_FLAGS_API_SERVER_ADD = 256,
            //--- enumeration borders
            MODIFY_FLAGS_NONE = 0,
            MODIFY_FLAGS_ALL = MODIFY_FLAGS_ADMIN | MODIFY_FLAGS_MANAGER | MODIFY_FLAGS_POSITION | MODIFY_FLAGS_RESTORE |
                               MODIFY_FLAGS_API_ADMIN | MODIFY_FLAGS_API_MANAGER | MODIFY_FLAGS_API_SERVER | MODIFY_FLAGS_API_GATEWAY |
                               MODIFY_FLAGS_API_SERVER_ADD
        };
        /// <summary>
        /// deal ticket
        /// </summary>
        public ulong Deal { get; set; }
        /// <summary>
        /// deal ticket in external system (exchange, ECN, etc)
        /// </summary>
        public string ExternalID { get; set; }
        /// <summary>
        /// client login
        /// </summary>
        public ulong Login { get; set; }
        /// <summary>
        /// processed dealer login (0-means auto)
        /// </summary>
        public ulong Dealer { get; set; }
        /// <summary>
        /// deal order ticket
        /// </summary>
        public ulong Order { get; set; }
        /// <summary>
        /// EnDealAction
        /// </summary>
        public EnDealAction Action { get; set; }
        /// <summary>
        /// EnEntryFlags
        /// </summary>
        public EnEntryFlags Entry { get; set; }
        /// <summary>
        /// EnDealReason
        /// </summary>
        public EnDealReason Reason { get; set; }
        /// <summary>
        /// price digits
        /// </summary>
        public uint Digits { get; set; }
        /// <summary>
        /// currency digits
        /// </summary>
        public uint DigitsCurrency { get; set; }
        /// <summary>
        /// symbol contract size
        /// </summary>
        public double ContractSize { get; set; }
        /// <summary>
        /// deal creation datetime
        /// </summary>
        public long Time { get; set; }
        /// <summary>
        /// deal creation datetime in msc since 1970.01.01
        /// </summary>
        public long? TimeMsc { get; set; }
        /// <summary>
        /// deal symbol
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// deal price
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// deal volume
        /// </summary>
        public ulong Volume
        {
            get { return MTUtils.ConvetToOldVolume(m_Volume); }
            set { m_Volume = MTUtils.ConvertToNewVolume(value); }
        }
        /// <summary>
        /// deal volume with exta 8-digits accuracy
        /// </summary>
        public ulong VolumeExt
        {
            get { return m_Volume; }
            set { m_Volume = value; }
        }
        /// <summary>
        /// deal profit
        /// </summary>
        public double Profit { get; set; }
        /// <summary>
        /// deal collected swaps
        /// </summary>
        public double Storage { get; set; }
        /// <summary>
        /// deal commission
        /// </summary>
        public double Commission { get; set; }
        /// <summary>
        /// deal agent commission
        /// </summary>
        public double CommissionAgent { get; set; }
        /// <summary>
        /// profit conversion rate (from symbol profit currency to deposit currency)
        /// </summary>
        public double RateProfit { get; set; }
        /// <summary>
        /// margin conversion rate (from symbol margin currency to deposit currency)
        /// </summary>
        public double RateMargin { get; set; }
        /// <summary>
        /// expert id (filled by expert advisor)
        /// </summary>
        public ulong ExpertID { get; set; }
        /// <summary>
        /// position id
        /// </summary>
        public ulong PositionID { get; set; }
        /// <summary>
        /// deal comment
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// deal profit in symbol's profit currency
        /// </summary>
        public double ProfitRaw { get; set; }
        /// <summary>
        /// closed position price
        /// </summary>
        public double PricePosition { get; set; }
        /// <summary>
        /// closed volume
        /// </summary>
        public ulong VolumeClosed
        {
            get { return MTUtils.ConvetToOldVolume(m_VolumeClosed); }
            set { m_VolumeClosed = MTUtils.ConvertToNewVolume(value); }
        }
        /// <summary>
        /// closed volume
        /// </summary>
        public ulong VolumeClosedExt
        {
            get { return m_VolumeClosed; }
            set { m_VolumeClosed = value; }
        }
        /// <summary>
        /// tick value
        /// </summary>
        public double TickValue { get; set; }
        /// <summary>
        /// tick size
        /// </summary>
        public double TickSize { get; set; }
        /// <summary>
        /// flags
        /// </summary>
        public ulong Flags { get; set; }
        /// <summary>
        /// source gateway name
        /// </summary>
        public string Gateway { get; set; }
        /// <summary>
        /// tick size
        /// </summary>
        public double PriceGateway { get; set; }
        /// <summary>
        /// EnEntryFlags
        /// </summary>
        public EnTradeModifyFlags ModifyFlags { get; set; }
        /// <summary>
        /// SL price
        /// </summary>
        public double PriceSL { get; set; }
        /// <summary>
        /// TP price
        /// </summary>
        public double PriceTP { get; set; }
    }
}
