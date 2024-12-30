using rrlib481.Db.Base;
using rrlib481.Db.TypesEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace rrlib481.Db.Authorization
{
    public record BORole : BaseBoRecord
    {
        /// <summary>
        /// 可修改此角色的層級
        /// </summary>
        public RoleTypeEnum EditableBy { get; set; } = RoleTypeEnum.BOSystem;
        /// <summary>
        /// 可應用此角色的層級
        /// </summary>
        public RoleTypeEnum UsableBy { get; set; } = RoleTypeEnum.BOSystem;
        /// <summary>
        /// Used to verify the client role version in the token, if the version is different from the Couchbase, then reload the role,
        /// and update the role data in the token.
        /// </summary>
        public int VersionNumber { get; set; } = 0;
        /// <summary>
        /// Role Name
        /// </summary>
        public string RoleName { get; set; } = string.Empty;
        /// <summary>
        /// Description of the Role 
        /// </summary>
        public string Desc { get; set; } = string.Empty;
        /// <summary>
        /// Department Reference (GUID)
        /// </summary>
        public string? Department { get; set; }
        /// <summary>
        /// for search menu's reference
        /// Key: Menu Reference, Value: Menu Name
        /// </summary>
        public Dictionary<string, string> AccessMenuList { get; set; } = new();
        /// <summary>
        /// Allowed Action List
        /// </summary>
        public HashSet<string> AccessActionList { get; set; } = new();
        /// <summary>
        /// Menu map to Action name
        /// Key: Menu Reference, Value: Action Name
        /// </summary>
        public Dictionary<string, HashSet<string>> Menu2ActionsMap { get; set; } = new();
        #region Pure Copy, 複製區塊
        /// <summary>
        /// 生成Couchbase key: BORole-{Reference}
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
