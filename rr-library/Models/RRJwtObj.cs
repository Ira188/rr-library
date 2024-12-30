using System;
using System.Collections.Generic;
using System.Text;

namespace rr_library.Models
{
    public record struct RRJwtObj
    {
        public string? Ip { get; set; }
        public string? SessionId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public RRUserObj? User { get; set; }
        public RRRegistrationObj? Registration { get; set; }
        public RRAuthObj? Auth { get; set; }
    }
}
