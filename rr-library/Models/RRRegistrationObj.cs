using System;
using System.Collections.Generic;
using System.Text;

namespace rr_library.Models
{
    public record struct RRRegistrationObj
    {
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? CaptchaCodeId { get; set; }
    }
}
