using System;
using System.Collections.Generic;
using System.Text;
#nullable enable

namespace rrlib481.Models
{
    public record struct RRAuthObj
    {
        public string? LoginId { get; set; }
        public string? Reference { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? CaptchaCodeId { get; set; }
        public string? EmailCodeId { get; set; }
    }
}
