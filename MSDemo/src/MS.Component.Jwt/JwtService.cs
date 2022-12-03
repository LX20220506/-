using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MS.Component.Jwt.UserClaim;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace MS.Component.Jwt
{
    public class JwtService
    {
        private readonly JwtSetting _jwtSetting;
        private readonly TimeSpan _tokenLifeTime;

        public JwtService(IOptions<JwtSetting> jwtSetting)
        {
            _jwtSetting = jwtSetting.Value;
            _tokenLifeTime = TimeSpan.FromMinutes(_jwtSetting.LifeTime);
        }
        /*
            iss (issuer)：签发人
            exp (expiration time)：过期时间
            sub (subject)：主题
            aud (audience)：受众
            nbf (Not Before)：生效时间
            iat (Issued At)：签发时间
            jti (JWT ID)：编号
        */


        /// <summary>
        /// 生成身份信息
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public Claim[] BuildClaims(UserData userData) {

            var claims = new Claim[] {
                new Claim(UserClaimType.Id,userData.Id.ToString()),
                new Claim(UserClaimType.Name,userData.Name),
                new Claim(UserClaimType.Account,userData.Account),
                new Claim(UserClaimType.RoleName,userData.RoleName),
                new Claim(UserClaimType.RoleDisplayName,userData.RoleDisplayName),
                new Claim(JwtRegisteredClaimNames.Jti,userData.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                //new Claim(JwtRegisteredClaimNames.Iss,_jwtSetting.Issuer),
                //new Claim(JwtRegisteredClaimNames.Aud,_jwtSetting.Audience),
                //new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                //这个就是过期时间，可自定义，注意JWT有自己的缓冲过期时间
                //new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.Add(_tokenLifeTime)).ToUnixTimeSeconds()}"),

            };

            return claims;
        }


        /// <summary>
        /// 生成jwt令牌
        /// </summary>
        /// <param name="claims">自定义的claim</param>
        /// <returns></returns>
        public string BuildToken(Claim[] claims) {

            var nowTime = DateTime.Now;
            // 签名凭证
            var creds = new SigningCredentials(
                // 设置秘钥
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecurityKey)), SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer, // 颁发者
                audience: _jwtSetting.Audience, // 受众
                claims:claims,
                notBefore:nowTime, // 开始时间
                expires:nowTime.Add(_tokenLifeTime), // 结束时间
                signingCredentials: creds); // 秘钥

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
