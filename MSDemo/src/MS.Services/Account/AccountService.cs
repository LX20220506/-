using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using MS.Common.Extensions;
using MS.Common.IDCode;
using MS.Component.Jwt;
using MS.Component.Jwt.UserClaim;
using MS.DbContexts;
using MS.Models.Core;
using MS.Models.ViewModel;
using MS.UnitOfWork;
using MS.WebCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly JwtService _jwtService;
        private readonly SiteSetting _siteSetting;
        public AccountService(IUnitOfWork<MSDbContext> unitOfWork, IMapper mapper, IdWorker idWorker, JwtService jwtService, IOptions<SiteSetting> siteSetting) : base(unitOfWork, mapper, idWorker)
        {
            _jwtService = jwtService;
            _siteSetting = siteSetting.Value;
        }

        public async Task<ExecuteResult<UserData>> Login(LoginViewModel viewModel)
        {
            var result = await viewModel.LoginValidate(_unitOfWork,_mapper,_siteSetting);

            if (result.IsSucceed)
            {
                // 获取token
                result.Result.Token = _jwtService.BuildToken(_jwtService.BuildClaims(result.Result));

                // 返回token
                return new ExecuteResult<UserData>(result.Result);
            }
            else
            {
                // 返回错误信息
                return new ExecuteResult<UserData>(result.Message);
            }
        }
    }
}
