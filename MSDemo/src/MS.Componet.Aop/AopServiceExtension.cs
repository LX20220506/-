using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MS.Component.Aop
{
    public static class AopServiceExtension
    {

        /// <summary>
        /// 注册aop拦截服务
        /// 同时注册了各业务接口（service层）于实现
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="serviceAssemblyName">业务层程序集名称</param>
        public static void AddAopService(this ContainerBuilder builder,string serviceAssemblyName) { 
            
            // 注册拦截器，同步异步都要
            builder.RegisterType<LogInterceptor>().AsSelf();
            builder.RegisterType<LogInterceptorAsync>().AsSelf();


            // 注册业务层，同时对业务层的方法进行拦截
            builder.RegisterAssemblyTypes(Assembly.Load(serviceAssemblyName)) // 找到要注入的程序集
                .AsImplementedInterfaces().InstancePerLifetimeScope() // 将程序集（业务层）注入到容器
                .EnableInterfaceInterceptors() // 启用接口拦截器
                // 设置拦截器
                .InterceptedBy(new Type[] {typeof(LogInterceptor) });//这里只有同步的，因为异步方法拦截器还是先走同步拦截器


            //业务层注册拦截器也可以使用[Intercept(typeof(LogInterceptor))]加在类上，
            //但是上面的方法比较好，没有侵入性
        }

    }
}
