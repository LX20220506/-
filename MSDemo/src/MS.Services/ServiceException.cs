using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MS.Services
{
    public static class ServiceException
    {
        /// <summary>
        /// 获取程序集名称
        /// </summary>
        /// <returns></returns>
        public static string GetAssemblyName() {
            // 返回正在执行的程序集
           return Assembly.GetExecutingAssembly().GetName().Name;
        }
    }
}
