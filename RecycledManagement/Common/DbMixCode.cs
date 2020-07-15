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

        public DataTable GetMaterialCsalesColor(string mixId)
        {
            return DataProvider.Instance.ExecuteQuery("sp_MixMaterialScalesColor @MixId", new object[] { mixId });
        }

        public DataTable GetMaterialCsalesGetMixId(string mixId)
        {
            return DataProvider.Instance.ExecuteQuery("sp_MixMaterialScalesGetMixId @MixId", new object[] { mixId });
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

        public int CreatedMix(List<SqlTransactionQueryList> listQuery)
        {
            return DataProvider.Instance.ExecuteSqlTransaction(listQuery);
        }

        public int GetMaxId()
        {
            return DataProvider.Instance.ExecuteNonQuery_GetIdIdentity("sp_MixedGetMaxId");
        }

        /// <summary>
        /// get CrushingCode de fill vao cac lookupEdit cua recycle.
        /// </summary>
        /// <param name="crushLike"> @CrushedCode='RE|R%'.</param>
        /// <returns>DataTable.</returns>
        public DataTable GetCrushingCode(string crushLike)
        {
            return DataProvider.Instance.ExecuteQuery("sp_CrushingGetCrushedCode @CrushedCode", new object[] { crushLike });
        }

        /// <summary>
        /// Get incomingCode from IncomingCrush table. Fill to lookupEdit Leftover.
        /// </summary>
        /// <returns></returns>
        public DataTable GetIncomingCode()
        {
            return DataProvider.Instance.ExecuteQuery("sp_IncomingGetIncomingCode");
        }


    }
}
