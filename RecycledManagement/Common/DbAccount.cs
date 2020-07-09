using RecycledManagement.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Common
{
    public class DbAccount
    {
        #region tạo Degin Parttern Singleton
        private static DbAccount _instance;
        public static DbAccount Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DbAccount();
                }
                return _instance;
            }

            private set => _instance = value;
        }
        public DbAccount()
        {

        }
        #endregion
        //public bool Login(string userName,string password, string role = null)
        //{
        //    bool res = false;

        //    var data = DataProvider.Instance.ExecuteScalar($"Select COUNT(*) from Account where userName='{userName}' and password='{password}' and role='{role}'");

        //    if (data!=null && (int)data >0)
        //    {
        //        res = true;
        //    }

        //    return res;
        //}

        /// <summary>
        /// Mehtod kiểm tra login cho User.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <returns>True--> thành công. False--> thất bại.</returns>
        public bool Login(string username, string password, string role)
        {
            bool result = false;//Bien tra ve

            #region chuyen doi Role
            int _role = 0;
            if (role == "Admin")
            {
                _role = 1;
            }
            else if (role == "Operator")
            {
                _role = 2;
            }
            #endregion

            //chuyển đổi passwrod sang mã MD5
            //string _pass = Encryptor.MD5Hash(password);//1 cach ma hoa MD5 ko có pass
            string _pass = EncodeMD5.EncryptString(password, "ITFramasBDVN");//mã hóa MD5 có pass

            //string giaiMa = EncodeMD5.DecryptString("Lf13NE5F4Iw=", "ITFramasBDVN");//giả mã

            //gọi methos truy vấn Db từ lớp chung DataProvider
            //trả về DataTable
            var data = DataProvider.Instance.ExecuteQuery("exec sp_Login @userName , @password , @role", new object[] { username, _pass, _role });

            //Login thành công
            if (data != null && data.Rows.Count > 0)
            {
                GlobalVariable.role = _role;
                GlobalVariable.userId = (int)data.Rows[0][0];
                GlobalVariable.loginDate = DateTime.Now;



                //insert vào bảng historyLogin hành động của User, lưu lại thời điểm login
                DbHistoryLogin.Instance.InsertCmd("userId,loginDate", $"{data.Rows[0][0]},'{GlobalVariable.loginDate}'");

                result = true;
            }

            return result;
        }

        //lay tong tin cua User theo ID
        public DataTable GetUser(string userId)
        {
            return DataProvider.Instance.ExecuteQuery("sp_getUserInfo @UserID", new object[] { userId });
        }

        //dung cho user operator update
        public int UpdateAccountUser(string userId, string userName, string password)
        {
            return DataProvider.Instance.ExecuteNonQuery("sp_AccountUpdate @Userid , @UserName , @Password", new object[] { userId, userName, password });
        }

        public DataTable GetUserReset()
        {
            return DataProvider.Instance.ExecuteQuery("sp_AccountSelectUserReset");
        }

        public int GetMaxIdAccount()
        {
            return DataProvider.Instance.ExecuteNonQuery_GetIdIdentity("sp_AccountGetMaxId");
        }

        public int InsertAccount(string userName, string fullName, string pass, string role)
        {
            return DataProvider.Instance.ExecuteNonQuery("sp_AccountInsert @UserName , @FullName , @Pass , @Role", new object[] { userName, fullName, pass, role });
        }

        public int InsertOperatorRole(string userId, string booking, string mixing, string incoming, string crush)
        {
            return DataProvider.Instance.ExecuteNonQuery("sp_OperatorRoleInsert @userId , @booking , @mixing , @incoming , @crushing"
                , new object[] { userId, booking, mixing, incoming, crush });
        }
    }
}
