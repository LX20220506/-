using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Component.Aop
{
    //这个是主拦截器，继承自IInterceptor，
    //不管是同步方法还是异步方法，都将会走其中的Intercept方法，
    //然后会在LogInterceptorAsync中再去区分是异步方法还是同步方法
    public class LogInterceptor : IInterceptor
    {
        private readonly LogInterceptorAsync _logInterceptorAsync;
        public LogInterceptor(LogInterceptorAsync logInterceptorAsync)
        {
            _logInterceptorAsync = logInterceptorAsync;
        }

        public void Intercept(IInvocation invocation)
        {
            _logInterceptorAsync.ToInterceptor().Intercept(invocation);
        }
    }
}
