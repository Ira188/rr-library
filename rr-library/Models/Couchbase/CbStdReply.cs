using Couchbase.KeyValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rr_library.Models.Couchbase
{
    public readonly record struct CbStdReply<T>
    {
        public bool Success { get; init; }
        public string? Error { get; init; }
        public T? ResultObj { get; init; }
        public IList<T>? ResultList { get; init; }
        public IMutationResult? MutationResult { get; init; }
    }
}
