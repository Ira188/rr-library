using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace rrlib481.Db.Base
{
    /// <summary>
    /// Base Backoffice Record
    /// Used GUID as the reference
    /// </summary>
    public abstract record BaseBoRecord : BaseDbRecord
    {
        /// <summary>
        /// Unique ID of the record - GUID currently
        /// Once created, it cannot be changed
        /// </summary>
        public string Reference { get; set; } = Guid.NewGuid().ToString("N");
    }
}
