using rrlib481.Db.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace rrlib481.Db.Account
{
    public record BOUserAuth : BaseAuth
    {
        #region Pure Copy, 複製區塊
        /// <summary>
        /// 生成Couchbase key: BOUserAuth-{Company Code}-{User ID}
        /// </summary>
        /// <param name="code">Company Code</param>
        /// <param name="userid">BOUser's User ID</param>
        /// <returns>Generated Unique Key for Couchbase</returns>
        public static string GenerateCbKey(string code, string userid)
        {
            return $"{MethodBase.GetCurrentMethod().DeclaringType.Name}-{code}-{userid}";
        }
        public override string ChildGenerateCbKey()
        {
            if (string.IsNullOrEmpty(UserID))
            {
                throw new ArgumentNullException("UserID is null or empty");
            }
            return GenerateCbKey(CompanyCode, UserID);
        }
        #endregion
    }
}
