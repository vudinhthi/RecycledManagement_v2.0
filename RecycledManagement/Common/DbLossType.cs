using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Common
{
    public class DbLossType
    {
        #region singleton
        private static DbLossType _instance;
        public static DbLossType Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DbLossType();
                }
                return _instance;
            }
            set => _instance = value;
        }
        public DbLossType() { }
        #endregion

        //get all tblOtherSource
        public DataTable SelectAll()
        {
            return DataProvider.Instance.ExecuteQuery("exec sp_getLossTypes");
        }

        //get LossTypeName
        public DataTable GetLossTypeName(int lossTypeFrom)
        {
            return DataProvider.Instance.ExecuteQuery("exec sp_getLossTypeFrom @LossTypeForm", new object[] { lossTypeFrom });
        }

        //Insert data
        public int InsertData(string lossTypeName, string lossTypeForm, bool isActive, string createBy)
        {
            return DataProvider.Instance.ExecuteNonQuery("exec sp_insertLossType @LossTypeName , @LossTypeForm , @IsActived , @CreateBy",
                new object[] { lossTypeName, lossTypeForm, isActive, createBy }); ;
        }

        //update data
        public int Update(string lossTypeId, string lossTypeName, string lossTypeForm, bool isActive, string createBy)
        {
            return DataProvider.Instance.ExecuteNonQuery("exec sp_updateLossType @LossTypeId , @LossTypeName , @LossTypeForm , @IsActived , @CreateBy",
                new object[] { lossTypeId, lossTypeName, lossTypeForm, isActive, createBy });
        }
    }
}
