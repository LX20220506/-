using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace MS.Component.Jwt
{
    /// <summary>
    /// jwt配置
    /// </summary>
    public class JwtSetting
    {
        /// <summary>
        /// 颁发者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 受众
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 秘钥
        /// </summary>
        public string SecurityKey { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public double LifeTime { get; set; }

    }
}
