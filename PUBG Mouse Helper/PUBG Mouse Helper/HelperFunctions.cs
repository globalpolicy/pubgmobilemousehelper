using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUBG_Mouse_Helper
{
    public static class HelperFunctions
    {
        public static void WaitUntilTimeoutWhileTrue(Func<bool> condition, int timeoutMs)
        {
            int startTime = Environment.TickCount;
            while (Environment.TickCount - startTime < timeoutMs && condition.Invoke())
            {
            }
        }

        public static string GetApplicationName()
        {
            return "PUBG Mouse Helper 2.1";
        }
    }
}
