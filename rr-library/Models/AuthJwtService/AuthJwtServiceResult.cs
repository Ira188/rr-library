using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rr_library.Models.AuthJwtService
{
    public record struct AuthJwtServiceResult
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
        public RRJwtObj? JwtObj { get; set; }
    }
}
