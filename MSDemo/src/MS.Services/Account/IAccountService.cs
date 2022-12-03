using Microsoft.EntityFrameworkCore.Storage;
using MS.Component.Jwt.UserClaim;
using MS.Models.Core;
using MS.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services
{
    public interface IAccountService:IBaseService
    {
        /// <summary>
        /// 登录服务
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<ExecuteResult<UserData>> Login(LoginViewModel viewModel);
    }
}
