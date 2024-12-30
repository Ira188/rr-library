using rrlib481.Db.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace rrlib481.Db.Authorization
{
    public record BOMenu : BaseBoRecord
    {
        /// <summary>
        /// Name of the BOMenu
        /// </summary>
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Upper BOMenu Reference
        /// Null means this is the top menu
        /// </summary>
        public string UpperMenu { get; set; } = null;
        /// <summary>
        /// Lower BOMenu Reference
        /// </summary>
        public HashSet<string> LowerMenu { get; set; } = new();
        /// <summary>
        /// BOController Reference
        /// </summary>
        public HashSet<string> RelatedActions { get; set; } = new();
        #region Pure Copy, 複製區塊
        /// <summary>
        /// 生成Couchbase key: BOAction-{Reference}
        /// </summary>
        /// <param name="guid">Reference (GUID)</param>
        /// <returns>Generated Unique Key for Couchbase</returns>
        public static string GenerateCbKey(string guid)
        {
            return $"{MethodBase.GetCurrentMethod().DeclaringType.Name}-{guid}";
        }
        public override string ChildGenerateCbKey()
        {
            return GenerateCbKey(Reference);
        }
        #endregion
    }
}
