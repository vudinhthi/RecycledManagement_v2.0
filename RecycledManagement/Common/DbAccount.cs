﻿using RecycledManagement.Common;
using RecycledManagement.Common;
using System;
using System.Collections.Generic;
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
            get {
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
            string _pass = Encryptor.MD5Hash(password);

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
    }
}