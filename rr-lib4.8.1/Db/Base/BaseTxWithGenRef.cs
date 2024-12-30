using rrlib481.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace rrlib481.Db.Base
{
    /// <summary>
    /// Mainly for transaction records, with a shortened generated reference ID for better display in the MT5 terminal UI.
    /// </summary>
    public abstract record BaseTxWithGenRef : BaseDbRecord
    {
        /// <summary>
        /// 交易序號, this Base62 encoded ID is designed to be better displayed in the MT5 terminal UI transaction comments.
        /// </summary>
        public string Reference { get; set; } = RrToBase62.GenId(12);
        /// <summary>
        /// 帳號歸屬的Trading Server(如MT5)代號
        /// </summary>
        public string? ServerID { get; set; }
        /// <summary>
        /// 交易服務器的帳號
        /// </summary>
        public string? TradingAccount { get; set; }
    }
}
