using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS.Common.Extensions;
using MS.Component.Jwt.UserClaim;
using MS.Models.Core;
using MS.Models.ViewModel;
using MS.Services;
using MS.WebCore;
using System.Threading.Tasks;

namespace MS.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : AuthorizeController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpPost]
        [AllowAnonymous]//Login方法上打了[AllowAnonymous]特性，所以Login未授权也可以访问（用户登录的接口肯定不能有认证限制）
        public async Task<ExecuteResult<UserData>> Login(LoginViewModel viewModel) {
            return await _accountService.Login(viewModel);
        }
    }
}
