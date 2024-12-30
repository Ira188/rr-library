using rrlib481.Db.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace rrlib481.Db.Account
{
    /// <summary>
    /// Backoffice User
    /// </summary>
    public record BOUser : BaseAuth
    {
        /// <summary>
        /// For multiple user id to login, e.g. using email, mobile(+852-98765432), or custom username etc.
        /// must be less than 100 characters
        /// </summary>
        public HashSet<string> UserIDs { get; set; } = new();
        /// <summary>
        /// User related roles
        /// 使用者所屬權限
        /// better limited to 1~3 roles for better performance
        /// </summary>
        public HashSet<string> Roles { get; set; } = new();
        /// <summary>
        /// User related departments
        /// 使用者所屬部門
        /// </summary>
        public HashSet<string> Departments { get; set; } = new();
        /// <summary>
        /// 郵箱, Email
        /// </summary>
        public string? Email { get; set; }
        public bool? IsEmailVerified { get; set; }
        /// <summary>
        /// Display Name
        /// </summary>
        public string? DisplayName { get; set; }
        /// <summary>
        /// 手機號碼, Format: 852-12345678 or 86-12345678901
        /// </summary>
        public string? Mobile { get; set; }
        public bool? IsMobileVerified { get; set; }
        /// <summary>
        /// Recovery Email
        /// </summary>
        public string? AltEmail { get; set; }
        public bool? IsAltEmailVerified { get; set; }
        /// <summary>
        /// Recovery Mobile
        /// </summary>
        public string? AltMobile { get; set; }
        public bool? IsAltMobileVerified { get; set; }
        #region Pure Copy, 複製區塊
        /// <summary>
        /// 生成Couchbase key: BOUser-{GUID}
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
