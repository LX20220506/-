using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MS.Common.Extensions;
using MS.Component.Jwt.UserClaim;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Component.Jwt
{
    public static class JwtServiceExtension
    {
        public static IServiceCollection AddJwtService(this IServiceCollection services, IConfiguration configuration) {


            //绑定appsetting中的jwtsetting
            services.Configure<JwtSetting>(configuration.GetSection(nameof(JwtSetting)));// 直接将JwtSetting注入到容器中，后续可以直接使用
            
            //services.AddSingleton(jwtConfig.Get<JwtSetting>());

            // 注册jwt服务
            services.AddSingleton<JwtService>();

            // 注册IHttpContextAccessor，在ClaimAccessor中会用到
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IClaimAccessor, ClaimAccessor>();


            var jwtConfig = configuration.GetSection("JwtSetting");

            services
                .AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,// 启用了密钥验证
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["SecurityKey"])),

                        ValidateIssuer = true,// 启用了颁发者验证
                        ValidIssuer = jwtConfig["Issuer"],

                        ValidateAudience = true,// 启用了受众者验证
                        ValidAudience = jwtConfig["Audience"],

                        //总的Token有效时间 = JwtRegisteredClaimNames.Exp + ClockSkew ；
                        RequireExpirationTime = true,
                        ValidateLifetime = true,// 是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比.同时启用ClockSkew 
                        ClockSkew = TimeSpan.Zero//注意这是缓冲过期时间，总的有效时间等于这个时间加上jwt的过期时间，如果不配置，默认是5分钟，
                                                 //这里把ClockSkew缓冲时间改成了0，默认是5分钟（也就是去掉了缓冲时间）
                    };
                });
            
            return services;
        }
    }
}
