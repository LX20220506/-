using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Common.IDCode
{
    public static class TimeExtensions
    {
        public static Func<long> currentTimeFunc = InternalCurrentTimeMillis;

        /// <summary>
        /// 当前时间毫秒
        /// </summary>
        /// <returns></returns>
        public static long CurrentTimeMillis()
        {
            return currentTimeFunc();
        }

        /// <summary>
        /// 存根当前时间
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IDisposable StubCurrentTime(Func<long> func)
        {
            currentTimeFunc = func;
            return new DisposableAction(() =>
            {
                currentTimeFunc = InternalCurrentTimeMillis;
            });
        }


        /// <summary>
        /// 存根当前时间
        /// </summary>
        /// <param name="millis"></param>
        /// <returns></returns>
        public static IDisposable StubCurrentTime(long millis)
        {
            currentTimeFunc = () => millis;
            return new DisposableAction(() =>
            {
                currentTimeFunc = InternalCurrentTimeMillis;
            });
        }

        /// <summary>
        /// 1970年1月1日
        /// </summary>
        private static readonly DateTime Jan1st1970 = new DateTime
           (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// 1970年到现在的豪秒数
        /// </summary>
        /// <returns></returns>
        private static long InternalCurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

    }
}
