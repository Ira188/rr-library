using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase.Query;

namespace rr_library.Models.Couchbase
{
    public record struct CbQryReply<T>
    {
        public bool Success { get; set; }
        public IList<Error>? Error { get; set; }
        public IList<T>? ResultList { get; set; }
    }
}
