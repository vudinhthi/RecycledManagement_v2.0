using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Common
{
    public class DbUserAction
    {
        #region singleton
        private static DbUserAction _instance;
        public static DbUserAction Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DbUserAction();
                }
                return _instance;
            }
            set => _instance = value;
        }
        public DbUserAction() { }
        #endregion

        //get all tblOtherSource
        //public DataTable SelectAll()
        //{
        //    return DataProvider.Instance.ExecuteQuery("exec sp_getOtherSources");
        //}

        //Insert data
        public int InsertData(int userId, DateTime loginDate, string action)
        {
            return DataProvider.Instance.ExecuteNonQuery("exec sp_insertUserAction @userId , @loginDate , @action", new object[] { userId, loginDate, action }); ;
        }

        //update data
        //public int Update(string sourceId, string sourceName, bool isActive, string createBy)
        //{
        //    return DataProvider.Instance.ExecuteNonQuery("exec sp_updateOtherSource @SourceId , @SourceName , @IsActived , @CreatedBy",
        //        new object[] { sourceId, sourceName, isActive, createBy });
        //}
    }
}
