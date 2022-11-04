using MS.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class UserLogin:IEntity
    {
        /// <summary>
        /// 用户Id
        /// 用户表外键
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 哈希密码
        /// 密码长度不能超过16位
        /// </summary>
        public string HashedPassword { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 登录失败次数
        /// </summary>
        public int AccessFailedCount { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// 锁定时间
        /// </summary>
        public DateTime? LockedTime { get; set; }

        public User User { get; set; }

    }
}
