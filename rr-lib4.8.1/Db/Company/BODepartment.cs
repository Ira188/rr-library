using rrlib481.Db.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace rrlib481.Db.Company
{
    public record BODepartment : BaseBoRecord
    {
        /// <summary>
        /// Department Name
        /// </summary>
        public string DepartmentNameEn { get; set; } = string.Empty;
        /// <summary>
        /// BOUser's Reference
        /// Admin can add or remove members
        /// </summary>
        public HashSet<string> Admins { get; set; } = new();
        /// <summary>
        /// BOUser's Reference
        /// </summary>
        public HashSet<string> Members { get; set; } = new();
        /// <summary>
        /// Roles assigned to the department admin, AdminRoles can only be assigned by Company Admin
        /// 使用者所屬權限
        /// better limited to 1~3 roles for better performance
        /// </summary>
        public HashSet<string> AdminRoles { get; set; } = new();
        /// <summary>
        /// Base Roles assigned to every member in the department except admin
        /// </summary>
        public HashSet<string> MemberRoles { get; set; } = new();
        #region Pure Copy, 複製區塊
        /// <summary>
        /// 生成Couchbase key: BODepartment-{GUID}
        /// </summary>
        /// <param name="guid">GUID</param>
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
