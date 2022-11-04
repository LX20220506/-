using MS.Common.Security;
using MS.DbContexts;
using MS.Entities;
using MS.Entities.Core;
using MS.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS.WebApi.Initialize
{
    public static class DBSeed
    {
        /// <summary>
        /// 数据初始化
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <returns>返回是否创建了数据库（非迁移）</returns>
        public static bool Initialize(IUnitOfWork<MSDbContext> unitOfWork) {
            bool isCreateDb = false;

            //直接自动执行迁移,如果它创建了数据库，则返回true
            if (unitOfWork.DbContext.Database.EnsureCreated())// EnsureCreated方法确保创建了数据库（如果数据库不存在则创建并返回true，存在则返回false）
            {
                isCreateDb = true;
                //打印log-创建数据库及初始化期初数据

                long rootUserId = 1219490056771866624;

                #region 角色、用户、登录

                Role rootRole = new Role
                {
                    Id = 1219490056771866625,
                    Name = "SuperAdmin",
                    DisplayName = "超级管理员",
                    Remark = "系统内置超级管理员",
                    Creator = rootUserId,
                    CreateTime = DateTime.Now
                };
                User rootUser = new User
                {
                    Id = rootUserId,
                    Account = "admin",
                    Name = "admin",
                    RoleId = rootRole.Id,
                    StatusCode = StatusCode.Enable,
                    Creator = rootUserId,
                    CreateTime = DateTime.Now,
                };
                // 创建了一个超级管理员角色，创建了一个超级管理员用户admin(密码同账号)
                unitOfWork.GetReposirory<User>().Inster(rootUser);
                unitOfWork.GetReposirory<Role>().Inster(rootRole);

                unitOfWork.GetReposirory<UserLogin>().Inster(new UserLogin()
                {
                    UserId = rootUserId,
                    Account = rootUser.Account,
                    HashedPassword = Crypto.HashPassword(rootUser.Account),//默认密码同账号名
                    IsLocked = false
                }) ;

                unitOfWork.SaveChanges();
                #endregion
            }

            return isCreateDb;
        }
    }
}
