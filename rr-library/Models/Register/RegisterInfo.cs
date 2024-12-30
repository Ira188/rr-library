using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rr_library.Models.Register
{
    public record struct RegisterInfo
    {
        /// <summary>
        /// 地区
        /// </summary>
        public string? Region { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string? Phone { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string? Password { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string? Captcha { get; set; }
        /// <summary>
        /// 邀请码
        /// </summary>
        public string? InviteCode { get; set; }
    } 
}
