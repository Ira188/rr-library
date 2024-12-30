using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rrlib481.Models.AuthJwtService
{
    public record struct AuthJwtServiceResult
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
        public RRJwtObj? JwtObj { get; set; }
    }
}
