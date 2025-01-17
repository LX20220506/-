﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace MS.WebApi.Filter
{
    /// <summary>
    /// 给api返回结果包一层状态码
    /// </summary>
    public class ApiResultFilter : IResultFilter
    {
        void IResultFilter.OnResultExecuted(ResultExecutedContext context)
        {
           
        }

        void IResultFilter.OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result != null)
            {
                if (context.Result is ObjectResult objectResult)
                {
                    if (objectResult.DeclaredType is null) //返回的是IActionResult类型
                    {
                        context.Result = new JsonResult(new
                        {
                            status = objectResult.StatusCode,
                            data = objectResult.Value
                        });
                    }
                    else // 返回的是string、list这种其他类型，此时没有statusCode，应尽量使用IActionResult类型
                    {
                        context.Result = new JsonResult(new
                        {
                            status = 200,
                            data = objectResult.Value
                        });
                    }
                }
                else if (context.Result is EmptyResult) // 返回值是空 结果
                {
                    context.Result = new JsonResult(new {
                        status=200,
                        data=""
                    });
                }
                else
                {
                    throw new Exception($"未经处理的Result类型：{context.Result.GetType().Name}");
                }
            }
        }
    }
}
