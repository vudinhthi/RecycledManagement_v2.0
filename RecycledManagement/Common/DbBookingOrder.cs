using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Common
{
    public class DbBookingOrder
    {
        private static DbBookingOrder _instance;
        public static DbBookingOrder Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DbBookingOrder();
                }
                return _instance;
            }
            private set => _instance = value;
        }
        public DbBookingOrder()
        {



        }

        public DataTable GetShift()
        {
            DataTable dt = new DataTable();
            dt = DataProvider.Instance.ExecuteQuery("sp_getShiftsActived");
            return dt;
        }
        public DataTable GetTeamLeader(int departmentId)
        {
            DataTable dt = new DataTable();
            dt = DataProvider.Instance.ExecuteQuery($"sp_getOperators_Dept @department", new object[] { departmentId });
            return dt;
        }
        public DataTable GetItem()
        {
            DataTable dt = new DataTable();
            dt = DataProvider.Instance.ExecuteQuery($"sp_getProductsWinline");
            return dt;
        }
        public DataTable GetOrdersize(string ProductId)
        {
            DataTable dt = new DataTable();
            dt = DataProvider.Instance.ExecuteQuery($"sp_getProductSizeWinline @prodId", new object[] { ProductId });
            return dt;
        }

        public DataTable GetAll()
        {
            return DataProvider.Instance.ExecuteQuery("sp_getFullOrders");
        }

        public DataTable GetAllInsoming()
        {
            return DataProvider.Instance.ExecuteQuery("sp_getOrdersIncoming");
        }
    }
}
