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
            dt = DataProvider.Instance.ExecuteQuery($"sp_getOperators_Dept1 @department", new object[] { departmentId });
            return dt;
        }
        public DataTable GetItem()
        {
            DataTable dt = new DataTable();
            dt = DataProvider.Instance.ExecuteQuery($"sp_getProductsWinline");
            return dt;
        }

        public DataTable GetItemName()
        {
            return DataProvider.Instance.ExecuteQuery("sp_getProductsWinline");
        }
        public DataTable GetOrdersize(string ProductId)
        {
            DataTable dt = new DataTable();
            dt = DataProvider.Instance.ExecuteQuery($"sp_getProductSizeWinline1 @prodId", new object[] { ProductId });
            return dt;
        }

        public int GetMaxIdOrderBook()
        {
            return DataProvider.Instance.ExecuteNonQuery_GetIdIdentity("sp_OrderBookgetMaxId");
        }

        public int GetMaxIdOrderBookLog()
        {
            return DataProvider.Instance.ExecuteNonQuery_GetIdIdentity("sp_OrderBookLogGetMaxId");
        }

        public DataTable GetAll()
        {
            return DataProvider.Instance.ExecuteQuery("sp_OrderBookGetAll");
        }

        public DataTable GetOrderBookLog1Line(string orderId)
        {
            return DataProvider.Instance.ExecuteQuery("sp_OrderBookLogGet1Line @OrderId",new object[] { orderId});
        }


        public DataTable GetAllInsoming()
        {
            return DataProvider.Instance.ExecuteQuery("sp_getOrdersIncoming");
        }

        public DataTable GetOrderCrush(string orderId)
        {
            return DataProvider.Instance.ExecuteQuery("sp_getOrderCrush @OrderId", new object[] { orderId });
        }

        //insert vao bang tblOrderBook
        public int InsertOrderBook(string orderCode, string machine, string itemCode, string itemName, string colorCode, string colorName, string orderAmount, string orderStatus, string note, string operatorId, string orderType, DateTime finishDate, string shiftId, string createdBy)
        {
            return DataProvider.Instance.ExecuteNonQuery("sp_OrderBookInsert @OrderCode , @Machine , @ItemCode , @ItemName , @ColorCode , @ColorName , @OrderAmount , @OrderStatus , @Note , @OperatorId , @OrderType , @FinishDate , @ShiftId , @CreatedBy"
                , new object[] { orderCode,machine,itemCode,itemName,colorCode,colorName,orderAmount,orderStatus,note,operatorId,orderType,finishDate,shiftId,createdBy});
        }

        public int InsertOrderBookSize(string orderId, string sizeName, string qtyPrs, string qtyKg, string createdBy)
        {
            return DataProvider.Instance.ExecuteNonQuery("sp_OrderBookSizeInsert @OrderId , @SizeName , @QtyPrs , @QtyKg , @CreatedBy"
                , new object[] { orderId,sizeName,qtyPrs,qtyKg,createdBy});
        }

        public int InsertOrderBookLog(string orderId, string status, string createdBy)
        {
            return DataProvider.Instance.ExecuteNonQuery("sp_OrderBookLogInsert @OrderId , @Status , @CreatedBy"
                , new object[] { orderId, status, createdBy });
        }

        public int UpdateOrderBook(string OrderStatus, string OrderId)
        {
            return DataProvider.Instance.ExecuteNonQuery("sp_OrderBookUpdateOrderStatus @OrderStatus , @OrderId"
                , new object[] { OrderStatus, OrderId });
        }

        public DataTable GetOederBookSizeWhereOrderId(string orderId)
        {
            return DataProvider.Instance.ExecuteQuery("sp_OrderBookSizeGetOrderId @Orderid",new object[] { orderId});
        }
    }
}
