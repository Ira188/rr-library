using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace rrlib481.Db.Interfaces
{
    public interface IBaseDbRecord
    {
        HashSet<string> Tags { get; }
        string ClassType { get; }
        DateTime Created { get; }
        DateTime LastUpdated { get; }
        bool IsActive { get; }
    }
}
