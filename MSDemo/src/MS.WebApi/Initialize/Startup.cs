using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MS.DbContexts;
using MS.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MS.WebCore;
using MS.Models.Automapper;
using MS.Services;
using Autofac;
using MS.Component.Aop;
using MS.Component.Jwt;
using static Org.BouncyCastle.Math.EC.ECCurve;
using MS.WebApi.Filter;

namespace MS.WebApi
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}


        public IConfiguration Configuration { get; }

        // 添加使用autofac提供的DI容器
        public ILifetimeScope AutofacContainer { get; private set; }

        public Startup(IWebHostEnvironment env) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                // AddJsonFile():设置json配置文件
                //      path：文件
                //      optional：文件是否可选
                //      reloadOnChange：更改时重新加载
                .AddJsonFile(path:"appsettings.json",optional:true,reloadOnChange:true) // 设置全局配置
                .AddJsonFile(path:$"appsettings.{env.EnvironmentName}.json",optional:true)// 设置不同环境下的配置
                .AddEnvironmentVariables();// 添加环境变量


            Configuration=builder.Build();
        }

        // 添加autofac容器的构造器
        // ContainerBuilder：容器构造器
        public void ConfigureContainer(ContainerBuilder builder) {
            //注册IBaseService和IRoleService接口及对应的实现类
            //builder.RegisterType<BaseService>().As<IBaseService>().InstancePerLifetimeScope();
            //builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();

            // 注册aop拦截器
            // 将业务程序集的名称传了进去，给业务层接口和实现做了注册，也给各个业务层方法开启的代理
            builder.AddAopService(ServiceException.GetAssemblyName());
        }



        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //注册跨域策略
            services.AddCorsPolicy(Configuration);

            // 注册webcore服务（网站主要配置）
            services.AddWebCoreService(Configuration);

            // 添加UnitOfWork的服务
            services.AddUnitOfWorkService<MSDbContext>(
                options => options.UseMySql(Configuration.GetSection("ConnectionStrings:MSDbContext").Value));

            // 添加automapper服务
            services.AddAutomapperService();

            // 添加服务接口
            //services.AddScoped<IBaseService, BaseService>();
            //services.AddScoped<IRoleService, RoleService>();

            // 添加Jwt服务
            services.AddJwtService(Configuration);

            // 添加全局过滤器
            services.AddControllers(options => {
                options.Filters.Add<ApiResultFilter>();
                options.Filters.Add<ApiExceptionFilter>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // 启用跨域
            app.UseCors(WebCoreExtensions.MyAllowSpecificOrigins);

            app.UseAuthentication();// 添加认证中间件

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
