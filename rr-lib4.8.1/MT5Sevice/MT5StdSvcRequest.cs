using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace rrlib481.MT5Service
{
    /// <summary>
    /// MT5 Standard Service Request
    /// </summary>
    public record struct MT5StdSvcRequest
    {
        public enum MT5StdSvcRequestType
        {
            /// <summary>
            /// Deposit with duplication checking, Cashflow class
            /// </summary>
            DepositWithCheck = 0,
            /// <summary>
            /// Withdraw with duplication checking, Cashflow class
            /// </summary>
            WithdrawWithCheck = 1,
            /// <summary>
            /// Internal Fund Transfer, InternalFundTransfer class
            /// </summary>
            InternalFundTransfer = 2,
            /// <summary>
            /// Create MT5 Account, RegistrationRecord class
            /// </summary>
            RegistrationRecord = 3,
        }
        public MT5StdSvcRequestType? RequestType { get; set; }
        /// <summary>
        /// MT Account Number
        /// </summary>
        public string? MTAccount { get; set; }
        /// <summary>
        /// MT Server ID
        /// </summary>
        public string? MTServerID { get; set; }
        /// <summary>
        /// Reference
        /// </summary>
        public string? Reference { get; set; }
        /// <summary>
        /// Record Transaction Date Time
        /// </summary>
        public DateTime? TxDateTime { get; set; }
        /// <summary>
        /// Current Request Date Time
        /// </summary>
        public DateTime? ReqDateTime { get; set; }
    }
}
