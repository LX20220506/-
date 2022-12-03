using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Models.Core
{
    //这个枚举定义了业务操作的类型，对应数据库的CRUD
    public enum ExecuteType
    {
        /// <summary>
        /// 读取资源
        /// </summary>
        Retrieve,

        /// <summary>
        /// 创建资源
        /// </summary>
        Create,

        /// <summary>
        /// 更新资源
        /// </summary>
        Update,

        /// <summary>
        /// 删除资源
        /// </summary>
        Delete
    }
}
