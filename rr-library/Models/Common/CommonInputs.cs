using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rr_library.Models.Common
{
    public record struct Input4ModifyObjs
    {
        public HashSet<string> AddObjs { get; set; }
        public HashSet<string> RemoveObjs { get; set; }
    }
}
