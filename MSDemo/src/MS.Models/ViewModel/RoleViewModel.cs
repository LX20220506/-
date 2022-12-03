using MS.DbContexts;
using MS.Entities;
using MS.Models.Core;
using MS.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MS.Models.ViewModel
{
    public class RoleViewModel
    {
        /*特性说明：
               Display是该字段的显示名称
               Required注解标记该字段必填，不可为空
               StringLength注解标记该字段长度
               RegularExpression注解是正则表达式验证
               还有个Range注解特性是验证值的范围的，这里没用到
         */

        public long Id { get; set; }

        [Display(Name ="角色名称")]
        [Required(ErrorMessage ="{0}必填")]
        [StringLength(16,ErrorMessage = "不能超过{0}个字符")]
        [RegularExpression(@"^[a-zA-Z0-9_]{4,16}$", ErrorMessage = "只能包含字符、数字和下划线")]
        public string Name { get; set; }

        [Display(Name = "角色显示名")]
        [Required(ErrorMessage = "{0}必填")]
        [StringLength(50, ErrorMessage = "不能超过{0}个字符")]
        public string DisplayName { get; set; }

        [Display(Name= "备注")]
        [StringLength(4000,ErrorMessage = "不能超过{0}个字符")]
        public string Remark { get; set; }


        /// <summary>
        /// 检查操作是否合规
        /// </summary>
        /// <param name="executeType"></param>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        public ExecuteResult CheckField(ExecuteType executeType,IUnitOfWork<MSDbContext> unitOfWork) {

            ExecuteResult result = new ExecuteResult();
            var repo = unitOfWork.GetRepository<Role>();

            if (executeType!=ExecuteType.Create&& !repo.Exists(r=>r.Id==Id))
            {
                return result.SetFailMessage("角色不存在");
            }

            //针对不同的操作，检查逻辑不同
            switch (executeType)
            {
                case ExecuteType.Update:
                    // 角色名称相同，id不同的实体存在，
                    if (repo.Exists(r=>r.Name==Name&&r.Id!=Id))
                    {
                        return result.SetFailMessage($"已存在相同角色名称：{Name}");
                    }
                    break;
                case ExecuteType.Delete:
                    // 判断角色下面是否还存在用户
                    if (unitOfWork.GetRepository<User>().Exists(u=>u.RoleId==Id))
                    {
                        return result.SetFailMessage("还有员工使用该角色，无法删除");
                    }
                    break;
                case ExecuteType.Create:
                default:
                    // 存在相同角色名称 ，报错
                    if (repo.Exists(r=>r.Name==Name))
                    {
                        return result.SetFailMessage($"{Name} 角色已存在，无法重复添加");
                    }
                    break;
            }
            // 没有错误，默认返回成功
            return result;
        }
    }
}
