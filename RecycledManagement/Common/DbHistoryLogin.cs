using RecycledManagement.Models;
using RecycledManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Common
{
    public class DbHistoryLogin
    {
        #region tạo Degin Parttern Singleton
        private static DbHistoryLogin _instance;
        public static DbHistoryLogin Instance
        {
            get
            {
                if (_instance==null)
                {
                    _instance = new DbHistoryLogin();
                }
                return _instance;
            }
            private set => _instance = value;
        }
        public DbHistoryLogin()
        {

        }
        #endregion


        #region SELECT
        /// <summary>
        /// Query trả về list dữ liệu theo 1 model nào đó.
        /// </summary>
        /// <param name="query">câu query truyền vào.</param>
        /// <returns>list data theo 1 model nào đó.</returns>
        public List<HistoryLoginModel> SelectReturnList(string query)
        {
            List<HistoryLoginModel> res = new List<HistoryLoginModel>();//tao list tra e theo lop Employee

            DataTable data = DataProvider.Instance.ExecuteQuery(query);//goi method truy van db
            //xet xem co data trả về hay ko
            //nếu có thì add từng row trong data vào lít trả về
            if (data != null && data.Rows.Count > 0)
            {
                HistoryLoginModel _add;
                foreach (DataRow item in data.Rows)
                {
                    _add = new HistoryLoginModel(item);
                    res.Add(_add);
                }
            }

            return res;
        }

        public DataTable SelectReturnDataTable(string query)
        {
            return DataProvider.Instance.ExecuteQuery(query);
        }


        /// <summary>
        /// method query dùng store procedure.
        /// </summary>
        /// <param name="query">câu query store procedure.</param>
        /// <param name="where">object[] chứa các values của đối số truyền vào.</param>
        /// <returns>dataTable.</returns>
        public DataTable SelectWhere(string query, object[] where)
        {
            //DataProvider.Instance.ExecuteQuery("cau query store procedure", new object[]{properties1,properties2});
            return DataProvider.Instance.ExecuteQuery(query, where);
        }
        #endregion

        public int InsertCmd(string colunm, string values)
        {
            return DataProvider.Instance.ExecuteNonQuery($"insert into tblHistoryLogin ({colunm}) values ({values})");
        }

        /// <summary>
        /// Update DB.
        /// </summary>
        /// <param name="colunmUpdate">colunm=value.</param>
        /// <param name="where">Dieu kien.</param>
        /// <returns></returns>
        public int UpdateCmd(string colunmUpdate, string where)
        {
            return DataProvider.Instance.ExecuteNonQuery($"update tblHistoryLogin set {colunmUpdate} where {where}"); ;
        }

        public int DeleteCmd(string where)
        {
            return DataProvider.Instance.ExecuteNonQuery($"delete from tblHistoryLogin where {where}");
        }
    }
}
