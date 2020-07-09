using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Common
{
    public class DbOtherSource
    {
        #region singleton
        private static DbOtherSource _instance;
        public static DbOtherSource Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DbOtherSource();
                }
                return _instance;
            }
            set => _instance = value;
        }
        public DbOtherSource() { }
        #endregion

        //get all tblOtherSource
        public DataTable SelectAll()
        {
            return DataProvider.Instance.ExecuteQuery("exec sp_getOtherSources");
        }

        //Insert data
        public int InsertData(string sourceName, bool isActive, string createBy)
        {
            return DataProvider.Instance.ExecuteNonQuery("exec sp_insertOtherSource @SourceName , @IsActived , @CreateBy", new object[] { sourceName, isActive, createBy }); ;
        }

        //update data
        public int Update(string sourceId, string sourceName, bool isActive, string createBy)
        {
            return DataProvider.Instance.ExecuteNonQuery("exec sp_updateOtherSource @SourceId , @SourceName , @IsActived , @CreatedBy",
                new object[] { sourceId, sourceName, isActive, createBy });
        }

        public DataTable GetAllIncoming()
        {
            return DataProvider.Instance.ExecuteQuery("sp_getOtherSourcesIncoming");
        }
    }
}
