using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rrlib481.Models.AuthJwtService
{
    public record struct Input4ProduceToken
    {
        public RRJwtObj? JwtObj { get; set; }
    }
}
