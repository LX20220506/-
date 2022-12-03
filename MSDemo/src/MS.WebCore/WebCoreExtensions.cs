using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using MS.Common.Extensions;
using MS.Common.IDCode;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;

namespace MS.WebCore
{
    //封装了跨域策略AddCorsPolicy
    //绑定appsetting中的SiteSetting
    //实例化雪花算法IdWorker并注册为单例（保证全局只有一个单例）
    public static class WebCoreExtensions
    {

        // 跨域策略名称
        public const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        /// <summary>
        /// 添加跨域策略，从appsetting中读取配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services,IConfiguration configuration) {

            services.AddCors(option => {
                option.AddPolicy(MyAllowSpecificOrigins, builder =>
                {
                    builder
                    .WithOrigins(configuration.GetSection("Startup:Cors:AllowOrigins").Value.Split(','))
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            
            return services;
        }


        /// <summary>
        /// 注册WebCore服务，配置网站
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddWebCoreService(this IServiceCollection services,IConfiguration configuration) {

            // 绑定appsetting中的SiteSetting
            // 直接将SiteSetting类注入到了IServiceCollection中，后续可以直接使用
            services.Configure<SiteSetting>(configuration.GetSection(nameof(SiteSetting)));
            //var siteSetting = configuration.GetSection(nameof(SiteSetting));
            //services.AddSingleton(siteSetting.Get<SiteSetting>());

            #region 单例雪花算法
            string workIdStr = configuration.GetSection("SiteSetting:WorkerId").Value;
            string datacenterIdStr = configuration.GetSection("SiteSetting:DataCenterId").Value;

            long workId;
            long datacenterId;

            try
            {
                workId = long.Parse(workIdStr);
                datacenterId=long.Parse(datacenterIdStr);
            }
            catch (Exception)
            {

                throw;
            }

            IdWorker idWorker = new IdWorker(workId,datacenterId);

            services.AddSingleton(idWorker);

            #endregion

            return services;
        }
    }
}
