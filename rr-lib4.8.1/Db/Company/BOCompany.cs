using rrlib481.Db.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace rrlib481.Db.Company
{
    public record BOCompany : BaseDbRecord
    {
        /// <summary>
        /// Company English Name
        /// </summary>
        public string DisplayNameEn { get; set; } = string.Empty;
        /// <summary>
        /// 繁體中文名稱
        /// </summary>
        public string DisplayNameZht { get; set; } = string.Empty;
        /// <summary>
        /// 簡體中文名稱
        /// </summary>
        public string DisplayNameZhs { get; set; } = string.Empty;
        /// <summary>
        /// BODepartment's Reference
        /// </summary>
        public HashSet<string> Departments { get; set; } = new();
        /// <summary>
        /// 公司管理員 BOUser's Reference
        /// Admins can create departments and assign roles to BODepartment
        /// </summary>
        public HashSet<string> Admins { get; set; } = new();
        /// <summary>
        /// BORole's Reference
        /// </summary>
        public string AdminRole { get; set; } = string.Empty;
        /// <summary>
        /// BOUser's Reference belongs to the company
        /// </summary>
        public HashSet<string> Users { get; set; } = new();
        #region Pure Copy, 複製區塊
        /// <summary>
        /// 生成Couchbase key: BOCompany-{CompanyCode}
        /// </summary>
        /// <param name="code">CompanyCode</param>
        /// <returns>Generated Unique Key for Couchbase</returns>
        public static string GenerateCbKey(string code)
        {
            return $"{MethodBase.GetCurrentMethod().DeclaringType.Name}-{code}";
        }
        public override string ChildGenerateCbKey()
        {
            if (string.IsNullOrEmpty(CompanyCode))
                throw new ArgumentNullException("CompanyCode is null or empty");
            return GenerateCbKey(CompanyCode);
        }
        #endregion
    }
}
