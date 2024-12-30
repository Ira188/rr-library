using rrlib481.Db.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace rrlib481.Db.Server
{
    /// <summary>
    /// This record is preset to store all Server Information in Couchbase manually.
    /// We can create ExtraServerInfo record class to store extra information for each server in the future for further development.
    /// </summary>
    public record AllServerInfo : BaseDbRecord
    {
        /// <summary>
        /// Contains all Server ID, e.g. S1, S2, S3, etc.
        /// </summary>
        public List<string> AllServerID { get; } = new List<string>();
        /// <summary>
        /// Contains all Server ID to Platform Type, e.g. S1 -> MT5, S2 -> MT4, etc.
        /// </summary>
        public Dictionary<string, string> ServerIDToPlatformType { get; } = new Dictionary<string, string>();
        #region Pure Copy, 複製區塊
        /// <summary>
        /// 生成Couchbase key: AllServerInfo
        /// </summary>
        /// <returns>Generated Unique Key for Couchbase</returns>
        public static string GenerateCbKey()
        {
            return $"AllServerInfo";
        }
        public override string ChildGenerateCbKey()
        {
            return GenerateCbKey();
        }
        #endregion
    }
}
