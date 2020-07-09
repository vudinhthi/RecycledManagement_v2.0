using RecycledManagement.Common;
using RecycledManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Common
{
    public class DbOperatorRole
    {
        private static DbOperatorRole _instance;
        public static DbOperatorRole Instance
        {
            get
            {
                if (_instance==null)
                {
                    _instance = new DbOperatorRole();
                }
                return _instance;
            }
            private set => _instance = value;
        }
        public DbOperatorRole()
        {

        }

        public DataTable GetOperatorRole(string userId)
        {
            return DataProvider.Instance.ExecuteQuery("sp_OperatorRoleSelectId @UserId",new object[] { userId});
        }
    }
}
