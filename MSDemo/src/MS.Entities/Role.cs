using System;
using System.Collections.Generic;
using System.Text;
using MS.Entities.Core;

namespace MS.Entities
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role:BaseEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
