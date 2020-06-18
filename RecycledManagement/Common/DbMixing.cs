using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Common
{
    class DbMixing
    {
        private static DbMixing _instance;
        public static DbMixing Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DbMixing();
                }
                return _instance;
            }
            private set => _instance = value;
        }
        public DbMixing()
        {

        }
        //Get list of Shift from Database
        public DataTable GetShifts()
        {
            DataTable dt = new DataTable();
            dt = DataProvider.Instance.ExecuteQuery("sp_getShiftsActived");
            return dt;
        }
        //Get list Operator from Database
        public DataTable GetOperators(int departmentId)
        {
            DataTable dt = new DataTable();
            dt = DataProvider.Instance.ExecuteQuery("sp_getOperators_Dept @department", new object[] { departmentId });
            return dt;
        }
        //Get list Items from Database
        public DataTable GetItems()
        {
            DataTable dt = new DataTable();
            dt = DataProvider.Instance.ExecuteQuery("sp_getProductsWinline");
            return dt;
        }
        //Get list Materials by Items from Database
        public DataTable GetMaterialByItems(string productId)
        {
            DataTable dt = new DataTable();
            dt = DataProvider.Instance.ExecuteQuery("sp_getMaterialsProductWinLine @ProductId", new object[] { productId });
            return dt;
        }
        //Get list Reason from Database
        public DataTable GetReasons(int reasonType)
        {
            DataTable dt = new DataTable();
            dt = DataProvider.Instance.ExecuteQuery("sp_getReasonType @ReasonType", new object[] { reasonType });
            return dt;
        }
        //Get list Recycled by Loss Type from Database
        public DataTable GetRecycledByLossType(int lossType)
        {
            DataTable dt = new DataTable();
            dt = DataProvider.Instance.ExecuteQuery("sp_getCrushedByLossType @lossTypeId", new object[] { lossType });
            return dt;
        }
    }
}
