using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace rrlib481.Models.AuthJwtService
{
    public record struct Input4ValidateToken
    {
        public bool? IgnoreInvalidHandling { get; set; }
        public string? OriginAuthorizationToken { get; set; }
        public string? ClientIP { get; set; }
        public string? FunctionName { get; set; }
    }
}
