using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Common
{
    public class DataProvider
    {
        //connection string kết nối DB server
        public string connectionStr = "";//$"Data Source={RecycledManagement.Properties.Settings.Default.serverName};Database={RecycledManagement.Properties.Settings.Default.dbName}" +
            //$";UID={RecycledManagement.Properties.Settings.Default.userName};Password={RecycledManagement.Properties.Settings.Default.password}; Min Pool Size=0;Max Pool Size=1000;Pooling=true; Connect Timeout=65535;";        

        #region tạo Degin Parttern Singleton
        private static DataProvider _instance;
        public static DataProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataProvider();
                }
                return _instance;
            }
            private set => _instance = value;
        }
        public DataProvider() { }
        #endregion

        /// <summary>
        /// Method query DB trả về bảng dữ liệu kiểu DataTable.
        /// SELECT.
        /// </summary>
        /// <param name="query">câu truy vấn, có thể là câu query binh thường hoặc store procedure.</param>
        /// <param name="parametter">Object chứa các giá trị của parametter truyền vào, dùng cho query dùng store procedure, nếu sql bình thường thì để trống properties này.
        ///  /// Lưu ý: khi truyền store procedure thì các para phải cách cách dấu ',' và 1 khoảng trắng.
        /// Ex: @abc , @def.</param>
        /// <returns>Trả về bảng dữ liệu kiểu DataTable. ==null or rows.count==0 là ko có data hoặc error.</returns>
        public DataTable ExecuteQuery(string query, object[] parametter = null)
        {
            DataTable res = new DataTable();
            
            //try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);

                    //trường hợp câu query là store procedure
                    //set object giá trị tham số truyền vào, nếu khác null thì tiến hành add các giá trị cho tham số
                    if (parametter != null)
                    {
                        int i = 0;
                        string[] arrayParas = query.Split(' ');
                        foreach (string item in arrayParas)
                        {
                            if (item.Contains("@"))
                            {
                                if (parametter[i] != null)
                                {
                                    command.Parameters.AddWithValue(item, parametter[i]);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue(item, DBNull.Value);
                                }
                                i++;
                            }

                        }
                    }
                    //chạy câu query fill vào DataTable
                    SqlDataAdapter adaptor = new SqlDataAdapter(command);
                    adaptor.Fill(res);

                    //đóng kết nối hủy đối tượng connection đến DB server
                    connection.Close();
                    connection.Dispose();
                }
            }
            //catch { }

            return res;
        }

        /// <summary>
        /// Method query ko trả về bảng dữ liệu, chỉ trả về trạng thái thành công hay thất bại.
        /// INSERT, UPDATE, DELETE.
        /// </summary>
        /// <param name="query">Câu query truyền vào.</param>
        /// <param name="parametter">object chứa các giá trị của parametter.có thể để trống properties này.</param>
        /// <returns>1 là thành công, 0 thất bại.</returns>
        public int ExecuteNonQuery(string query, object[] parametter = null)
        {
            int res = 0;

            //try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);

                    if (parametter != null)
                    {
                        int i = 0;
                        string[] arrayParas = query.Split(' ');
                        foreach (string item in arrayParas)
                        {
                            if (item.Contains("@"))
                            {
                                if (parametter[i]!=null)
                                {
                                    command.Parameters.AddWithValue(item, parametter[i]);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue(item, DBNull.Value);
                                }
                                i++;
                            }
                        }
                    }

                    res = command.ExecuteNonQuery();

                    connection.Close();
                    connection.Dispose();
                }
            }
            //catch { }

            return res;
        }

        /// <summary>
        /// Method dem so record trong table.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parametter"></param>
        /// <returns>Object chứa số record đếm đc.</returns>
        public object ExecuteScalar(string query, object[] parametter = null)
        {
            object res = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    if (parametter != null)
                    {
                        int i = 0;
                        string[] arrayParas = query.Split(' ');
                        foreach (string item in arrayParas)
                        {
                            if (item.Contains("@"))
                            {
                                if (parametter[i] != null)
                                {
                                    command.Parameters.AddWithValue(item, parametter[i]);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue(item, DBNull.Value);
                                }
                                i++;
                            }
                        }
                    }
                    res = command.ExecuteScalar();

                    connection.Close();
                    connection.Dispose();
                }
            }
            catch { }

            return res;
        }

        /// <summary>
        /// Method dung de get  ra ID identity.
        /// </summary>
        /// <param name="queryStoreProcedure"></param>
        /// <returns></returns>
        public int ExecuteNonQuery_GetIdIdentity(string queryStoreProcedure)
        {
            int result = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(queryStoreProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter returnParameter = new SqlParameter("@LastIdentity", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(returnParameter);
                    
                    command.ExecuteNonQuery();

                    result = (int)command.Parameters["@LastIdentity"].Value;
                    
                    connection.Close();
                    connection.Dispose();
                }
            }
            catch { }
            return result;
        }


        /// <summary>
        /// Method sử dụng Transaction
        /// </summary>
        /// <param name="listQuery">list chứa dang sách các lệnh thực hiện trong Transaction. chỉ sử dụng các lệnh sửa đổi dữ liệu như: insert, update, delete,...
        /// Danh sách truyền vào là list gồm 2 phần tử.
        /// 1. query: chứa câu truy vấn, có thể là lệnh trực tiếp hoặc Stored Procedures.
        /// 2. parametter: Object[], dùng cho Stored Procedures.</param>
        /// <returns>0:Thất bại; 1: thành công.</returns>
        public int ExecuteSqlTransaction(List<SqlTransactionQueryList> listQuery)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("SampleTransaction");

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    foreach (SqlTransactionQueryList item in listQuery)
                    {
                        this.ExecuteNonQuery(item.Query,item.Parametter);
                    }
                    // Attempt to commit the transaction.
                    transaction.Commit();
                    Debug.WriteLine("Both records are written to database.");

                    result = 1;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Debug.WriteLine("  Message: {0}", ex.Message);

                    // Attempt to roll back the transaction.
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        Debug.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Debug.WriteLine("  Message: {0}", ex2.Message);
                    }

                    result = 0;
                }

                connection.Close();
                connection.Dispose();
            }
            return result;
        }
    }
}
