using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace rrlib481.Db.Base
{
    /// <summary>
    /// Base Authentication Record
    /// 用於登入認證的帳號
    /// </summary>
    public abstract record BaseAuth : BaseBoRecord
    {
        /// <summary>
        /// 是否第一次登入, 用來判斷是否需要更改密碼
        /// </summary>
        public bool? IsFirstLogin { get; set; } = true;
        /// <summary>
        /// Login, 用作登入帳號, using Email currently
        /// must be less than 100 characters
        /// </summary>
        public string? UserID { get; set; }
        /// <summary>
        /// Hashed Password, PasswordHasher<ClientAccountAuth>
        /// </summary>
        public string? HashedPassword { get; set; }
    }
}
