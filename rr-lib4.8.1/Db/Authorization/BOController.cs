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
    public record BOController : BaseBoRecord
    {
        /// <summary>
        /// Type of this Controller
        /// </summary>
        public AclGradeEnum AclGrade { get; set; } = AclGradeEnum.Company;
        /// <summary>
        /// Controller Name
        /// </summary>
        public string ControllerName { get; set; } = string.Empty;
        /// <summary>
        /// Controller Description
        /// </summary>
        public string Description { get; set; } = string.Empty;
        #region Pure Copy, 複製區塊
        /// <summary>
        /// 生成Couchbase key: BOController-{Reference}
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
