using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleTodo
{
    public static class MyExtensions
    {
        public static string InnerExceptionMessage(this Exception ex)
        {
            if (ex.InnerException != null)
            {
                return ex.InnerExceptionMessage();
            }
            else
            {
                return ex.Message;
            }
        }
    }
}
