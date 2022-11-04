using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MS.WebCore.Logger
{
    public static class NLogExtensions
    {
        // 第一个EnsureNlogConfig方法是确保NLog配置文件sql连接字符串
        //  和appsettings.json文件中一致，NLog写入数据库功能有对应的DbProvider，
        //  我这里只内置了MySQL和SQL server的，如有需要自行修改

        // 第二个AddNlogService是对IHostBuilder的一个方法扩展，把NLog开启的配置封装在里面


        //优先级：Trace<Debug<Info<Warn<Error<Fatal

        const string _mssqlDbProvider = "Microsoft.Data.SqlClient.SqlConnection, Microsoft.Data.SqlClient";
        const string _mysqlDbProvider = "MySql.Data.MySqlClient.MySqlConnection, MySql.Data";


        /// <summary>
        /// 确保NLog配置文件sql连接字符串正确
        /// </summary>
        /// <param name="nlogPage">nlog配置文件路径</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="sqlConnectionStr">数据库连接字符串</param>
        public static void EnsureNlogConfig(string nlogPage,string dbType,string sqlConnectionStr) 
        {
            XDocument xd = XDocument.Load(nlogPage);

            // 查找xml节点  targets节点--》target节点--》target节点的name属性--》判断name是不是等于log_database
            if (xd.Root.Elements().FirstOrDefault(a => a.Name.LocalName == "targets")
                is XElement targetsNode && targetsNode != null &&
                targetsNode.Elements().FirstOrDefault(a => a.Name.LocalName == "target" && a.Attribute("name").Value == "log_database")
                is XElement targetNode && targetNode != null)
            {

                if (!targetNode.Attribute("connectionString").Value.Equals(sqlConnectionStr))
                {
                    targetNode.Attribute("connectionString").Value = sqlConnectionStr;

                    if (dbType == "mysql")
                    {
                        targetNode.Attribute("dbProvider").Value = _mysqlDbProvider;
                    }
                    else
                    {
                        targetNode.Attribute("dbProvider").Value = _mssqlDbProvider;
                    }

                    xd.Save(nlogPage);
                    //编辑后重新载入配置文件（不依靠NLog自己的autoReload，有延迟）
                    LogManager.Configuration = new XmlLoggingConfiguration(nlogPage);
                }
            }

        }

        /// <summary>
        /// 注入NLog服务
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IHostBuilder AddNlogService(this IHostBuilder builder) {

            return builder
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();// 清除提供程序
                    logging.AddDebug(); // 将名为“debug”的调试记录器添加到工厂
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);// 设置最低级别
                })
                .UseNLog();// 替换NLog作为日志管理
        }
    }
}
