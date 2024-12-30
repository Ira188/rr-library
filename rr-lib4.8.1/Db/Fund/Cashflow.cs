using System;
using System.Reflection;
using rrlib481.Db.Base;
using rrlib481.Db.Interfaces;
using rrlib481.Tools;
#nullable enable

namespace rrlib481.Db.Fund
{
    public record struct AmountInCurrency
    {
        /// <summary>
        /// example: 500 USD 入金, For CNY, Amount = 500 * 7.2 = 3600 CNY
        /// </summary>
        public decimal? Amount { get; set; }
        /// <summary>
        /// example: CNY
        /// </summary>
        public string? Currency { get; set; }
        /// <summary>
        /// Exchange rate to base currency, USD => CNY
        /// example: 7.2
        /// </summary>
        public decimal? ExchangeRateToBaseCurrency { get; set; }
    }
    public enum PaymentGatewayType
    {
        XPay,
    }
    public static class PaymentGatewayTypeExtensions
    {
        /// <summary>
        /// 0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToBase62(this PaymentGatewayType value, int minlen = 2)
        {
            return RrToBase62.ToBase62((int)value).PadLeft(minlen, '0');
        }
        public static string ToDisplayString(this PaymentGatewayType type)
        {
            switch (type)
            {
                case PaymentGatewayType.XPay: return "XPay";
                //case PaymentGatewayType.Allinpay: return "通联";
                //case PaymentGatewayType.Chinagpay: return "智惠";
                //case PaymentGatewayType.DoCorporate: return "对公入账";
                //case PaymentGatewayType.Dinpay: return "智付";
                //case PaymentGatewayType.EbcPay: return "银盈通(收银台)";
                //case PaymentGatewayType.GaoHuiTong: return "高汇通";
                //case PaymentGatewayType.Payeco: return "银联";
                //case PaymentGatewayType.WeixinPay: return "微信";
                //case PaymentGatewayType.Zhifubao: return "支付宝";
                //case PaymentGatewayType.YmdPay: return "汇潮";
                //case PaymentGatewayType.ManualPay: return "手动";
                //case PaymentGatewayType.TestPay: return "▲测试通道▼";
                //case PaymentGatewayType.JpaiPay: return "九派";
                //case PaymentGatewayType.EsePay: return "银盈通(裸接口)";
                //case PaymentGatewayType.SandPay: return "杉德";
                //case PaymentGatewayType.ChjPay: return "畅捷";
                //case PaymentGatewayType.PathPay: return "路径";
                //case PaymentGatewayType.IappPay: return "爱贝";
                //case PaymentGatewayType.HchPay: return "红创";
                //case PaymentGatewayType.YsePay: return "银盛";
                //case PaymentGatewayType.HuanPay: return "环银";
                //case PaymentGatewayType.PayPal: return "贝宝";
                //case PaymentGatewayType.BankPay: return "钱包支付";
                //case PaymentGatewayType.MalaiPay: return "钱包支付(马来西亚)";
                //case PaymentGatewayType.VGPay: return "VGPay";
                //case PaymentGatewayType.QRCodePay: return "二维码支付";
                //case PaymentGatewayType.Blockchain: return "区块链支付";
                //case PaymentGatewayType.Blockchain2: return "电子支付";
                default: return "未知";
            }
        }
    }
    public enum PaymentGatewayStatus
    {
        InProgress = 100,
        Success = 200,
        Fail = 300
    }
    public enum CashflowStatus
    {
        Requested = 100,
        InProgress = 200,
        Completed = 300,
        CompletedWithFail = 400,
    }
    public enum PaymentMethod
    {
        Manual = 100, // 手动出金
        Auto = 200,   // 通道出金
    }
    public enum CashflowType
    {
        Deposit = 100,
        Withdraw = 200,
        /// <summary>
        /// 实货扣账
        /// </summary>
        RealWithdraw = 300,
    }
    public enum TradingServerRespStatus
    {
        Waiting = 100,
        InProgress = 200,
        Success = 300,
    }
    /// <summary>
    /// Backoffice後台審批狀態
    /// </summary>
    public enum BOApprovalStatusEnum
    {
        WaitingApproval = 100,
        Approved = 200,
        Rejected = 300,
    }
    /// <summary>
    /// 失敗原因
    /// </summary>
    public enum FailCauseEnum
    {
        None = 0,
        BORejected = 100,
        PaymentGateway = 200,
        InsufficientFunds = 300,
    }
    /// <summary>
    /// TradingAccount: 目標出入金賬戶
    /// ServerID: 目標出入金賬戶的交易服務器代號
    /// </summary>
    public record Cashflow : BaseTxWithGenRef
    {
        /// <summary>
        /// 交易日期年月日
        /// </summary>
        public string? yyyyMMdd { get { return Created.ToString("yyyyMMdd"); } }
        /// <summary>
        /// 帳號, 用戶的唯一識別(GUID), ClientAccountInfo.Reference
        /// </summary>
        public string? MasterAccountReference { get; set; }
        /// <summary>
        /// Cashflow交易完成时间
        /// </summary>
        public DateTime? CompleteTime { get; set; }
        /// <summary>
        /// 正數為入金, 負數為出金
        /// example: 500 USD 入金, Amount = 500
        /// </summary>
        public decimal? BaseAmount { get; set; }
        /// <summary>
        /// 貨幣代碼, Base currency of Client Trading Account
        /// example: USD
        /// </summary>
        public string? BaseCurrency { get; set; }
        public AmountInCurrency? AmountInOtherCurrencies { get; set; }
        public AmountInCurrency? AmountInOtherCurrencies2 { get; set; }
        /// <summary>
        /// 手续费金额
        /// </summary>
        public decimal? BaseServiceFee { get; set; }
        public string? BaseServiceFeeCurrency { get; set; }
        public AmountInCurrency? ServiceFeeInOtherCurrencies { get; set; }
        public AmountInCurrency? ServiceFeeInOtherCurrencies2 { get; set; }
        /// <summary>
        /// Cashflow的處理情況
        /// </summary>
        public CashflowStatus? Status { get; set; }
        /// <summary>
        /// 請求出入金的客戶端IP
        /// </summary>
        public string? IpAddr { get; set; }
        /// <summary>
        /// 交易發出或返回時的備注
        /// </summary>
        public string? Remark { get; set; }
        /// <summary>
        /// 标记是手动出金还是通道出金
        /// </summary>
        public PaymentMethod? Method { get; set; }
        public CashflowType? CfType { get; set; }
        public FailCauseEnum? FailCause { get; set; }
        #region Backoffice Related
        public BOApprovalStatusEnum? BOApprovalStatus { get; set; }
        public string? BOApprovalBy { get; set; }
        #endregion
        #region 銀行相关
        /// <summary>
        /// 要出入金的銀行號碼
        /// </summary>
        public string? BankAccount { get; set; }
        /// <summary>
        /// 銀行號碼戶主姓名
        /// </summary>
        public string? BankAccountName { get; set; }
        /// <summary>
        /// 銀行號碼戶主身份證
        /// </summary>
        public string? BankAccountIDCard { get; set; }
        /// <summary>
        /// 銀行参考號碼
        /// </summary>
        public string? BankReference { get; set; }
        /// <summary>
        /// 銀行號碼戶主的電話
        /// </summary>
        public string? BankMobile { get; set; }
        /// <summary>
        /// 銀行號碼戶主的Email
        /// </summary>
        public string? BankEmail { get; set; }
        /// <summary>
        /// 銀行備註
        /// </summary>
        public string? BankRemark { get; set; }
        #endregion
        #region PaymentGateway Related
        /// <summary>
        /// Payment Gateway 實際支付金額
        /// </summary>
        public decimal? ActualPaymentAmount { get; set; }
        /// <summary>
        /// 支付網關的類型
        /// </summary>
        public string? GwType { get; set; }
        /// <summary>
        /// 支付網關返回的交易序號
        /// </summary>
        public string? GwRef { get; set; }
        /// <summary>
        /// 支付網關返回的交易狀態
        /// </summary>
        public PaymentGatewayStatus? GwStatus { get; set; }
        /// <summary>
        /// 支付網關返回的交易代碼
        /// </summary>
        public string? GwRespCode { get; set; }
        /// <summary>
        /// 支付網關備註
        /// </summary>
        public string? PaymentGatewayRemark { get; set; }
        #endregion
        #region Trading Server 相关
        /// <summary>
        /// Trading服務器實際扣賬或入賬金額
        /// </summary>
        public decimal? ActualAmount { get; set; }
        /// <summary>
        /// 入金成功後對應的MTrading Server(如MT5)出入金成功後的交易單號
        /// </summary>
        public string? DepositOrderID { get; set; }
        /// <summary>
        /// 出金成功後對應的Trading Server(如MT5)出入金成功後的交易單號
        /// </summary>
        public string? WithdrawOrderID { get; set; }
        /// <summary>
        /// Trading Server(如MT5)返回的交易狀態
        /// </summary>
        public TradingServerRespStatus? RespStatus { get; set; }
        /// <summary>
        /// Trading Server(如MT5)交易完成時間
        /// </summary>
        public DateTime? TradingCompleteTime { get; set; }
        #endregion
        #region 退款相关
        /// <summary>
        /// 退款ID（如果有）
        /// </summary>
        public string? RefundCashflowID { get; set; }
        /// <summary>
        /// 退款状态
        /// </summary>
        public PaymentGatewayStatus? RefundStatus { get; set; }
        /// <summary>
        /// 此订单对应的旧的CashflowID
        /// </summary>
        public string? PreviousCashflowID { get; set; }
        /// <summary>
        /// 退款備註
        /// </summary>
        public string? RefundRemark { get; set; }
        #endregion
        #region Pure Copy, 複製區塊
        /// <summary>
        /// 生成Couchbase key - Cashflow-{ServerID}-{yyyyMMdd}-{Ref}
        /// </summary>
        /// <param name="sid">Trading Server ID</param>
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
                throw new ArgumentNullException("Trading ServerID is null or empty");
            }
            if (string.IsNullOrEmpty(Reference))
            {
                throw new ArgumentNullException("Reference is null or empty");
            }
            return GenerateCbKey(ServerID!, Created.ToString("yyyyMMdd"), Reference);
        }
        #endregion
    }
}
