using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Common
{
    public class DbReasons
    {
        #region singleton
        private static DbReasons _instance;
        public static DbReasons Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DbReasons();
                }
                return _instance;
            }
            set => _instance = value;
        }
        public DbReasons() { }
        #endregion

        //get all tblOtherSource
        public DataTable SelectAll()
        {
            return DataProvider.Instance.ExecuteQuery("exec sp_getReasons");
        }

        //get all tblReason where reasontype
        public DataTable GetReasonType(int reasonType)
        {
            return DataProvider.Instance.ExecuteQuery("exec sp_getReasonType @ReasonType", new object[] { reasonType });
        }

        public DataTable GetReasonId(string reasonId)
        {
            return DataProvider.Instance.ExecuteQuery("sp_getReasonId @ReasonId", new object[] { reasonId });
        }

        //Insert data
        public int InsertData(string reasonName, string reasonType, bool isActive, string createBy)
        {
            return DataProvider.Instance.ExecuteNonQuery("exec sp_insertReason @ReasonName , @ReasonType , @IsActived , @CreateBy",
                new object[] { reasonName, reasonType, isActive, createBy }); ;
        }

        //update data
        public int Update(string reasonId, string reasonName, string reasonType, bool isActive, string createBy)
        {
            return DataProvider.Instance.ExecuteNonQuery("exec sp_updateReason @ReasonId , @ReasonName , @ReasonType , @IsActived , @CreateBy",
                new object[] { reasonId, reasonName, reasonType, isActive, createBy });
        }
    }
}
