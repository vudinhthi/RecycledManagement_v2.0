using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Common
{
    public class DbMixCode
    {
        #region singleton
        private static DbMixCode _instance;
        public static DbMixCode Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DbMixCode();
                }
                return _instance;
            }
            set => _instance = value;
        }
        public DbMixCode() { }
        #endregion

        //get id and name for incomingCrush
        public DataTable GetAllIncoming()
        {
            return DataProvider.Instance.ExecuteQuery("sp_getMixCodeIncoming");
        }
    }
}
