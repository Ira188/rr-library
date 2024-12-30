using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rr_library.Models.EmailService
{
    public record struct Input4EmailAuthCode
    {
        public string? Login { get; set; }
    }
}
