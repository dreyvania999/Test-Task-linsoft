using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    internal static class DateTimeExtensions
    {
        private static readonly DateTime UnixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long ToUnixTimeMilliseconds(this DateTime dateTime)
        {
            return (long)(dateTime.ToUniversalTime() - UnixEpoch).TotalMilliseconds;
        }

        public static DateTime FromUnixTimeMilliseconds(this long milliseconds)
        {
            return UnixEpoch.AddMilliseconds(milliseconds);
        }
    }
}
