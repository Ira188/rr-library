using rrlib481.Db.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace rrlib481.Db.Account
{
    /// <summary>
    /// 用於登入認證的帳號
    /// 在登入後我們用這個Reference去找到ClientAccountInfo, ClientAccountRelation
    /// </summary>
    public record ClientAccountAuth : BaseAuth
    {
        #region Pure Copy, 複製區塊
        /// <summary>
        /// 生成Couchbase key: ClientAccountAuth-{Email}
        /// </summary>
        /// <param name="userid">Login</param>
        /// <returns>Generated Unique Key for Couchbase</returns>
        public static string GenerateCbKey(string userid)
        {
            return $"{MethodBase.GetCurrentMethod().DeclaringType.Name}-{userid}";
        }
        public override string ChildGenerateCbKey()
        {
            if (string.IsNullOrEmpty(UserID))
            {
                throw new ArgumentNullException("UserID is null or empty");
            }
            return GenerateCbKey(UserID);
        }
        #endregion
    }
}
