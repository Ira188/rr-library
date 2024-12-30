using System;
using System.Collections.Generic;
using System.Text;

namespace rr_library.Models
{
    public record struct RRAuthObj
    {
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? CaptchaCodeId { get; set; }
        public string? EmailCodeId { get; set; }
    }
}
