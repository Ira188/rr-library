using rrlib481.Db.Account;
using rrlib481.Db.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace rrlib481.Models
{
    public record RoleObj
    {
        public string? Reference { get; set; }
        public int VersionNumber { get; set; }
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
        /// for search action's reference
        /// Action name to Menu map
        /// </summary>
        public Dictionary<string, HashSet<string>> Menu2ActionsMap { get; set; } = new();
        public bool IsAuthorized(string funcName)
        {
            if (AccessActionList.Contains("*"))
            {
                return true;
            }
            var list = AccessActionList.Where(s => funcName.StartsWith(s));
            int cnt = list.Count();
            if (cnt == 1)
            {
                return true;
            }
            if (cnt > 1)
            {
                throw new Exception($"Function name duplicated: {string.Join(", ", list)}");
            }
            return false;
        }
    }
    public record struct RRJwtObj
    {
        public string? Ip { get; set; }
        public string? SessionId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public BOUser? BOUser { get; set; }
        public ClientAccountInfo? ClientUser { get; set; }
        public RRAuthObj? Auth { get; set; }
        public RoleObj? Role { get; set; }
        public bool? Authorized { get; set; }
    }
}
