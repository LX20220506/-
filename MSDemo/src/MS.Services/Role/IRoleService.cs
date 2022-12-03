using Microsoft.EntityFrameworkCore.Storage;
using MS.Entities;
using MS.Models.Core;
using MS.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services
{
    public interface IRoleService:IBaseService
    {
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="viewModel">角色的ViewModel</param>
        /// <returns>执行结果和新创建的角色</returns>
        Task<ExecuteResult<Role>> Create(RoleViewModel viewModel);

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="viewModel">角色的ViewModel</param>
        /// <returns>执行结果</returns>
        Task<ExecuteResult> Update(RoleViewModel viewModel);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="viewModel">角色的ViewModel</param>
        /// <returns>执行结果</returns>
        Task<ExecuteResult> Delete(RoleViewModel viewModel);


    }
}
