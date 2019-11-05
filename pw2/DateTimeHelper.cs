using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pw2
{
    internal static class DateTimeHelper
    {
        private static readonly System.DateTime Jan1st1970 = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        public static int CurrentUnixTimeMillis()
        {
            return (int)(System.DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }
    }
}
