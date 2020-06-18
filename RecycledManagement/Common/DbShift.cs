using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecycledManagement.Common;

namespace RecycledManagement.Common
{
    public class DbShift
    {
        #region singleton
        private static DbShift _instance;
        public static DbShift Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DbShift();
                }
                return _instance;
            }
            set => _instance = value;
        }
        public DbShift()
        {

        }
        #endregion

        public DataTable SelectAll()
        {
            return DataProvider.Instance.ExecuteQuery("exec sp_getShifts");
        }

        public int InsertData(string shiftName, bool isActive, string createBy)
        {
            return DataProvider.Instance.ExecuteNonQuery("exec sp_insertShift @shiftName , @isActived , @createBy", new object[] { shiftName, isActive, createBy }); ;
        }

        public int Update(string shiftId, string shiftName, bool isActive, string createBy)
        {
            return DataProvider.Instance.ExecuteNonQuery("exec sp_updateShift @shiftId , @shiftName , @isActived , @createdBy",
                new object[] { shiftId, shiftName, isActive, createBy });
        }

        public int Delect(string shiftId)
        {
            return DataProvider.Instance.ExecuteNonQuery("exec sp_deleteShiftLine @shiftId", new object[] { shiftId});
        }
    }
}
