using rrlib481.Db.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace rrlib481.Db.Company
{
    public record BOSystem : BaseDbRecord
    {
        public HashSet<string> Admins { get; set; } = new();
        #region Pure Copy, 複製區塊
        public static string GenerateCbKey()
        {
            return nameof(BOSystem);
        }
        public override string ChildGenerateCbKey()
        {
            return GenerateCbKey();
        }
        #endregion
    }
}
