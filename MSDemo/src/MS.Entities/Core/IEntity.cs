using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities.Core
{
    //没有Id主键的实体继承这个
    public interface IEntity
    {
    }
    //有Id主键的实体继承这个
    public abstract class BaseEntity : IEntity {
        /// <summary>
        /// id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public StatusCode StatusCode { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public long? Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        public long? Modifier { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
    }
}
