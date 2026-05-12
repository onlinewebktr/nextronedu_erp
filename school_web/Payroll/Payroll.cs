using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace school_web.Payroll
{
    public static class Payroll
    {
        internal static string currentSession()
        {
            return PayrollMy.getGlobalData("currentSession"); 
        }

        internal static string currentSessionId()
        {
           return PayrollMy.getGlobalData("currentSession");
        }
    }
}