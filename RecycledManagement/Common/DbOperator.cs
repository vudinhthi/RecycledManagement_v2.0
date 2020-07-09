using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Common
{
    public class DbOperator
    {
        #region singleton
        private static DbOperator _instance;
        public static DbOperator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DbOperator();
                }
                return _instance;
            }
            set => _instance = value;
        }
        public DbOperator() { }
        #endregion

        //get all tblOtherSource
        public DataTable SelectAll()
        {
            return DataProvider.Instance.ExecuteQuery("exec sp_getOperator1s");
        }

        //Insert data
        public int InsertData(string operatorName, string department, bool isActive, string createBy)
        {
            return DataProvider.Instance.ExecuteNonQuery("exec sp_insertOperator @OperatorName , @Department , @IsActived , @CreateBy",
                new object[] { operatorName, department, isActive, createBy }); ;
        }

        //update data
        public int Update(string oepratorId, string operatorName, string department, bool isActive, string createBy)
        {
            return DataProvider.Instance.ExecuteNonQuery("exec sp_updateOperator @OperatorId , @OperatorName , @Department , @IsActived , @CreateBy",
                new object[] { oepratorId, operatorName, department, isActive, createBy });
        }
    }
}
