using rrlib481.Db.Base;
using rrlib481.Db.TypesEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace rrlib481.Db.Authorization
{
    /// <summary>
    /// Hierarchy for system and company
    /// </summary>
    public record BOCtrlActHierarchy : BaseDbRecord
    {
        /// <summary>
        /// Type of this Hierarchy
        /// </summary>
        public AclGradeEnum AclGrade { get; set; } = AclGradeEnum.Company;
        /// <summary>
        /// for search controller's reference
        /// Controller name to Reference mapping
        /// </summary>
        public Dictionary<string, string> Controller2RefMap { get; set; } = new();
        /// <summary>
        /// for search action's reference
        /// Action name to Reference mapping
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> Action2RefMap { get; set; } = new();

        #region Pure Copy, 複製區塊
        /// <summary>
        /// 生成Couchbase key: BOCtrlActHierarchy-Com
        /// </summary>
        /// <returns>Generated Unique Key for Couchbase</returns>
        public static string GenerateCompanyCbKey()
        {
            return $"{MethodBase.GetCurrentMethod().DeclaringType.Name}-Com";
        }
        /// <summary>
        /// 生成Couchbase key: BOCtrlActHierarchy-Sys
        /// </summary>
        /// <returns>Generated Unique Key for Couchbase</returns>
        public static string GenerateSystemCbKey()
        {
            return $"{MethodBase.GetCurrentMethod().DeclaringType.Name}-Sys";
        }
        public override string ChildGenerateCbKey()
        {
            if (AclGrade == AclGradeEnum.Company)
            {
                return GenerateCompanyCbKey();
            }
            return GenerateSystemCbKey();
        }
        #endregion
    }
}
