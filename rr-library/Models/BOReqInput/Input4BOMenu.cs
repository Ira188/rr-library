using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rr_library.Models.BOReqInput
{
    public record struct Input4BOMenu
    {
        /// <summary>
        /// Name of the BOMenu
        /// </summary>
        public string? Name { get; set; }
        public string? Description { get; set; }
        /// <summary>
        /// Upper BOMenu Reference
        /// Null means this is the top menu
        /// </summary>
        public string? UpperMenu { get; set; }
        /// <summary>
        /// Lower BOMenu Reference
        /// </summary>
        //public List<string>? LowerMenu { get; set; } remarked, because edit upper menu will affect lower menu
        /// <summary>
        /// BOController Reference
        /// </summary>
        public HashSet<string>? RelatedActions { get; set; }
    }
}
