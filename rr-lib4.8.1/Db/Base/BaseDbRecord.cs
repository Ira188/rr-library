using Newtonsoft.Json;
using rrlib481.Db.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace rrlib481.Db.Base
{
    public abstract record BaseDbRecord : IBaseDbRecord
    {
        /// <summary>
        /// 公司代碼
        /// </summary>
        public string? CompanyCode { get; set; }
        /// <summary>
        /// 標籤
        /// </summary>
        public HashSet<string> Tags { get; set; } = new();
        /// <summary>
        /// Class Type to be recorded in Couchbase
        /// </summary>
        public string ClassType
        {
            get
            {
                return GetType().Name;
            }
        }
        /// <summary>
        /// 生成時間
        /// </summary>
        public DateTime Created { get; set; } = DateTime.Now;
        /// <summary>
        /// 最後更新時間
        /// </summary>
        public DateTime LastUpdated { get; set; } = DateTime.Now;
        /// <summary>
        /// Active or not
        /// </summary>
        public bool IsActive { get; set; } = true;
        /// <summary>
        /// Backoffice user who created the record
        /// Reference of the user
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;
        /// <summary>
        /// Backoffice user who updated the record
        /// Reference of the user
        /// </summary>
        public string LastUpdatedBy { get; set; } = string.Empty;
        /// <summary>
        /// Records of the update history of Datetime and User Reference
        /// Keeping the latest 10 records, up to the implementer to decide
        /// </summary>
        public List<Tuple<DateTime, string>> UpdatedByList { get; set; } = new();
        /// <summary>
        /// 生成Couchbase key
        /// </summary>
        public abstract string ChildGenerateCbKey();
        [JsonIgnore]
        /// <summary>
        /// 自我生成Couchbase key
        /// </summary>
        public string? Key => ChildGenerateCbKey();
    }
}
