using Castle.Core.Logging;
using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using MS.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Component.Aop
{
    public class LogInterceptorAsync : IAsyncInterceptor
    {
        private readonly ILogger<LogInterceptorAsync> _logger;

        public LogInterceptorAsync(ILogger<LogInterceptorAsync> logger) {
            _logger = logger;
        }


        /// <summary>
        /// 异步方法返回Task时使用
        /// </summary>
        /// <param name="invocation"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void InterceptAsynchronous(IInvocation invocation)
        {
            try
            {
                // 调用业务方法
                invocation.Proceed();

                // 记录日志
                LogExecuteInfo(invocation,invocation.ReturnValue.ToJsonString());
            }
            catch (Exception ex)
            {
                LogExecuteError(ex,invocation);
                throw new AopHandledException();
            }
        }


        /// <summary>
        /// 异步方法使用Task<T>时使用
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="invocation"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous<TResult>(invocation);
        }
        
        // 内部异步拦截
        private async Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation) {
            try
            {
                invocation.Proceed();

                Task<TResult> task = (Task<TResult>)invocation.ReturnValue;
                TResult result = await task; // 获取返回结果
                LogExecuteInfo(invocation, result.ToJsonString());
                return result;
            }
            catch (Exception ex)
            {
                LogExecuteError(ex, invocation);
                throw new AopHandledException();
            }
                    
        }

        /// <summary>
        /// 拦截同步方法时使用
        /// </summary>
        /// <param name="invocation"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void InterceptSynchronous(IInvocation invocation)
        {
            try
            {
                // 调用业务方法
                invocation.Proceed();
                // 记录日志
                LogExecuteInfo(invocation,invocation.ReturnValue.ToJsonString());
            }
            catch (Exception ex)
            {
                LogExecuteError(ex, invocation);
                throw new AopHandledException();
            }
        }

        #region helpMethod

        /// <summary>
        /// 获取拦截方法的信息（类名，方法名，参数）
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        private string GetMethodInfo(IInvocation invocation) {

            // 获取类名称
            string className=invocation.Method.DeclaringType.Name;
            // 获取方法名称
            string methodName = invocation.Method.Name;

            // 获取参数
            string args = string.Join(",", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray());

            if (string.IsNullOrWhiteSpace(args))
            {
                return $"{className}.{methodName}";
            }
            else
            {
                return $"{className}.{methodName}:{args}";
            }
        }

        /// <summary>
        /// 日志执行信息
        /// </summary>
        /// <param name="invocation"></param>
        /// <param name="result"></param>
        private void LogExecuteInfo(IInvocation invocation, string result)
        {
            _logger.LogDebug($"方法{GetMethodInfo(invocation)}，返回值{result}");
        }


        /// <summary>
        /// 日志执行错误
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="invocation"></param>
        private void LogExecuteError(Exception ex, IInvocation invocation) {
            _logger.LogError(ex,$"执行{GetMethodInfo(invocation)}时发生错误！");
        }

        #endregion


    }
}
