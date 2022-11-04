using System;
using System.Collections.Generic;
using System.Text;

namespace MS.WebCore
{
    /// <summary>
    /// 网站配置实体类
    /// </summary>
    public class SiteSetting
    {
        // WorkerId、DataCenterId是雪花算法的两个参数
        // LoginFailedCountLimits是用户登录失败的次数限制
        // LoginLockedTimeout是用户锁定后，多久可以重新登录
        // AllowOrigins中是允许跨域的源，逗号分隔

        /// <summary>
        /// 雪花工作人员id
        /// </summary>
        public long WorkerId { get; set; }

        /// <summary>
        /// 雪花数据中心id
        /// </summary>
        public long DataCenterId { get; set; }

        /// <summary>
        /// 登录失败的次数
        /// </summary>
        public int LoginFailedCountLimits { get; set; }

        /// <summary>
        /// （分钟）帐户锁定超时
        /// </summary>
        public int LoginLockedTimeout { get; set; }
    }
}
