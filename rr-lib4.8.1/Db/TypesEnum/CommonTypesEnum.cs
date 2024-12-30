using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rrlib481.Db.TypesEnum
{
    /// <summary>
    /// Separate the level of Controller and Action
    /// </summary>
    public enum AclGradeEnum
    {
        System = 100,
        Company = 200,
    }
    /// <summary>
    /// 權限類型
    /// </summary>
    public enum RoleTypeEnum
    {
        /// <summary>
        /// System Role
        /// </summary>
        BOSystem = 100,
        /// <summary>
        /// Company Role
        /// </summary>
        BOCompany = 200,
        /// <summary>
        /// Department Role
        /// </summary>
        BODepartment = 300,
        /// <summary>
        /// User Role
        /// </summary>
        BOUser = 400,
    }
    /// <summary>
    /// 客戶類型
    /// </summary>
    public enum ClientTypesEnum
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// 測試
        /// </summary>
        Test = 1,
        /// <summary>
        /// 銷售團隊
        /// </summary>
        RRTeam = 2,
        /// <summary>
        /// 交易者
        /// </summary>
        Trader = 100,
        /// <summary>
        /// 經紀/代理
        /// </summary>
        IB = 200,
    }
    /// <summary>
    /// 佣金類型
    /// </summary>
    public enum RebateTypesEnum
    {
        /// <summary>
        /// 沒有佣金
        /// </summary>
        None = 0,
        /// <summary>
        /// 撤銷資格
        /// </summary>
        Revoke = 1,
        /// <summary>
        /// 正常 - 不跳Bar
        /// </summary>
        Normal = 2,
        /// <summary>
        /// 跳Bar
        /// </summary>
        Bar = 3,
        /// <summary>
        /// 頭寸
        /// </summary>
        CashPosition = 4,
        /// <summary>
        /// 分成
        /// </summary>
        Share = 5,
    }
}
