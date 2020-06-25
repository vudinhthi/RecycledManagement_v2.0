using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Common
{
    public static class GlobalVariable
    {
        public static int role = 0;
        public static int userId;
        public static DateTime loginDate;

        public static GlobalEvent myEvent = new GlobalEvent();
    }
}
