using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MS.Component.Jwt.UserClaim
{
    // 定义用户信息访问接口，开发时通过获取IClaimsAccessor接口来获取登录用户的信息。
    public class ClaimAccessor : IClaimAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal UserPrincipal {
            get {
                ClaimsPrincipal user = _httpContextAccessor.HttpContext.User;
                if (user.Identity.IsAuthenticated) {
                    return user;
                }
                throw new Exception("用户未认证");
            }
        }


        public string UserName => UserPrincipal.Claims.First(x=>x.Type==UserClaimType.Name).Value;

        public long UserId => long.Parse(UserPrincipal.Claims.First(x=>x.Type==UserClaimType.Id).Value);

        public string UserAccount => UserPrincipal.Claims.First(x=>x.Type==UserClaimType.Account).Value;

        public string UserRole => UserPrincipal.Claims.First(x=>x.Type==UserClaimType.RoleName).Value;

        public string UserRoleDisplayName => UserPrincipal.Claims.First(x=>x.Type==UserClaimType.RoleDisplayName).Value;
    }
}
