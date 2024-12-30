using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace rrlib481.Models
{
    public record struct BOStdReply
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
        public string? ObjRef { get; set; }
        public object? ResultObj { get; set; }
    }
}
