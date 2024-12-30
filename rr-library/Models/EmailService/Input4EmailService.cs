using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

namespace rr_library.Models.EmailService
{
    public record struct Input4EmailService
    {
        public string RecipientAddress { get; init; }
        public string Subject { get; init; }
        public string HtmlContent { get; init; }
        public string PlainTextContent { get; init; }
    }
}
