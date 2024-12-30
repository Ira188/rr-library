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
    public record BOAction : BaseBoRecord
    {
        /// <summary>
        /// Type of this Action
        /// </summary>
        public AclGradeEnum AclGrade { get; set; } = AclGradeEnum.Company;
         /// <summary>
        /// Action Name
        /// </summary>
        public string ActionName { get; set; } = string.Empty;
        /// <summary>
        /// Action Description
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Related Controller Reference
        /// </summary>
        public string ControllerRef { get; set; } = string.Empty;
        #region Pure Copy, 複製區塊
        /// <summary>
        /// 生成Couchbase key: BOAction-{ActionName}
        /// </summary>
        /// <param name="actionName">Action Name</param>
        /// <returns>Generated Unique Key for Couchbase</returns>
        public static string GenerateCbKey(string actionName)
        {
            return $"{MethodBase.GetCurrentMethod().DeclaringType.Name}-{actionName}";
        }
        public override string ChildGenerateCbKey()
        {
            return GenerateCbKey(ActionName);
        }
        #endregion
    }
}
