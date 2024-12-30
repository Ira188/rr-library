using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace rrlib481.MT5Service
{
    /// <summary>
    /// MT5 Standard Service Reply
    /// </summary>
    public record struct MT5StdSvcReply
    {
        /// <summary>
        /// 成功與否
        /// </summary>
        public bool? Success { get; set; }
        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string? Error { get; set; }
        /// <summary>
        /// Extra information in JSON format
        /// </summary>
        public string? Json { get; set; }
    }
}
