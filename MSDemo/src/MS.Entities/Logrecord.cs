using MS.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    /// <summary>
    /// 日志
    /// </summary>
    public class Logrecord:IEntity
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 日志日期
        /// </summary>
        public DateTime LogDate { get; set; }

        /// <summary>
        /// 日志等级
        /// </summary>
        public string LogLevel { get; set; }

        /// <summary>
        /// 记录器
        /// </summary>
        public string Logger { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 异常
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// 机械名
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        /// 机械ip地址
        /// </summary>
        public string MachineIp { get; set; }

        /// <summary>
        /// 请求方法
        /// </summary>
        public string NetRequestMethod { get; set; }

        /// <summary>
        /// 请求url
        /// </summary>
        public string NetRequestUrl { get; set; }

        /// <summary>
        /// 用户是否认证
        /// 0-未认证，1-认证
        /// </summary>
        public string NetUserIsauthenticated { get; set; }

        /// <summary>
        /// 用户认证类型
        /// </summary>
        public string NetUserAuthtype { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string NetUserIdentity { get; set; }
    }
}
