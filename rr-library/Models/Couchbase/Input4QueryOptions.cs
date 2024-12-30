using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rr_library.Models.Couchbase
{
    public record Input4QueryOptions
    {
        public enum OrderbyEnum
        {
            Created,
            LastUpdated,
        }
        public int? Limit { get; set; }
        public int? Offset { get; set; }
        public OrderbyEnum? Orderby { get; set; }
        public bool? Descend { get; set; }
    }

    public record InputQueryOptions4Action : Input4QueryOptions
    {
    }
}
