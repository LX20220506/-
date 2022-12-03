using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MS.Common.Extensions;
using MS.Component.Aop;
using MS.Entities.Core;
using Newtonsoft.Json;

namespace MS.WebApi.Filter
{
    /// <summary>
    /// api异常处理器
    /// </summary>
    public class ApiExceptionFilter : IExceptionFilter
    {

        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }


        // 发生异常时触发
        public void OnException(ExceptionContext context)
        {
            string methodInfo = $"{context.RouteData.Values["controller"] as string}Controller.{context.RouteData.Values["action"] as string}:{context.HttpContext.Request.Method}";

            // 如果不是AopHandledException异常，则可能没有记录过日志，进行日志记录
            if (!(context.Exception is AopHandledException))
            {
                _logger.LogError(context.Exception,methodInfo);
            }

            // 返回结果
            context.Result = new JsonResult(new { 
                status=501,
                data="服务器出错"
            });
        }
    }
}
