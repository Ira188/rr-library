using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rr_library.Models.BOReqInput
{
    public record struct Input4Company
    {
        public string? CompanyCode { get; set; }
        /// <summary>
        /// Company English Name
        /// </summary>
        public string? DisplayNameEn { get; set; }
        /// <summary>
        /// 繁體中文名稱
        /// </summary>
        public string? DisplayNameZht { get; set; }
        /// <summary>
        /// 簡體中文名稱
        /// </summary>
        public string? DisplayNameZhs { get; set; }
        /// <summary>
        /// BODepartment's Reference
        /// </summary>
        public string? AdminRole { get; set; }
    }
}
