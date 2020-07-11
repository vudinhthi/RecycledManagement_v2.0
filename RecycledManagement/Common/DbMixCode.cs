using RecycledManagement.Models;
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

        //get MaterialCode and MaterialName
        public DataTable GetAllIncoming()
        {
            return DataProvider.Instance.ExecuteQuery("sp_getMixCodeIncoming");
        }

        public DataTable GetAllMixed()
        {
            return DataProvider.Instance.ExecuteQuery("sp_MixedGetAll");
        }

        public List<MixingOrderModel> GetAllMixedList()
        {
            List<MixingOrderModel> result = new List<MixingOrderModel>();

            DataTable data = DataProvider.Instance.ExecuteQuery("sp_MixedGetAll");

            foreach (DataRow item in data.Rows)
            {
                result.Add(new MixingOrderModel(item));
            }

            return result;
        }

        public MixingOrderModel GetAllMixed1Row(string orderId)
        {
            try
            {
                return new MixingOrderModel(DataProvider.Instance.ExecuteQuery("sp_MixedGetAll1Line @MixId", new object[] { orderId }).Rows[0]);
            }
            catch
            {
                return null;
            }
        }

        public List<MixProductWinlineModel> GetProductWinline(string itemCode, string orderAmount)
        {
            List<MixProductWinlineModel> result = new List<MixProductWinlineModel>();

            DataTable data = DataProvider.Instance.ExecuteQuery("sp_getMaterialsProductWinLineMix @ProductId", new object[] { itemCode });

            foreach (DataRow item in data.Rows)
            {
                result.Add(new MixProductWinlineModel(item, orderAmount));
            }

            return result;
        }
    }
}
