using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rrlib481.Errors
{
    public record struct ErrorTypes
    {
        public static string SystemError { get; } = "SystemError";
        public static string ReferenceError { get; } = "ReferenceError";
        public static string MTServerIdError { get; } = "MTServerIdError";
    }
}
