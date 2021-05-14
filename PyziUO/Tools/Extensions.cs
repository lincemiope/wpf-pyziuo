using System;
using System.Text;

namespace PyziUO.Tools
{
    public static class Extensions
    {
        public static string GetAscii(this object s)
        {
            byte[] uni = Encoding.Unicode.GetBytes(Convert.ToString(s));
            return Encoding.ASCII.GetString(uni);
        }
        public static double DateTime2Unix(this DateTime d)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (d.ToUniversalTime() - epoch).TotalMilliseconds;
        }
    }
}
