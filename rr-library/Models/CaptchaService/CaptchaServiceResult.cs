using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rr_library.Models.CaptchaService
{
    public record struct CaptchaServiceResult
    {
        public string? Token { get; set; }
        public string? Svg { get; set; }
    }
}
