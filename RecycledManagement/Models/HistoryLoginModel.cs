using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Models
{
    public class HistoryLoginModel
    {
        public HistoryLoginModel(int id, int idUser)
        {
            this.id = id;
            this.userId = idUser;
        }

        //tạo hàm dựng khi khởi tạo với object dataRow
        public HistoryLoginModel(DataRow row)
        {
            this.id = (int)row[0];
            this.id = (int)row[1];
            this.loginDate = (DateTime)row[2];

            //cach 1
            //DateTime logoutDate;
            //DateTime.TryParse(row[3].ToString(), out logoutDate);
            //this.logoutDate = logoutDate;

            //cach 2
            DateTime.TryParse(row[3].ToString(), out _logoutDate);
        }
        public int id { set; get; }
        public int userId { set; get; }
        public DateTime loginDate { set; get; }//cach 1
                                               //public DateTime logoutDate { set; get; }

        //cach 2
        private DateTime _logoutDate;
        public DateTime LogoutDate { get => _logoutDate; set => _logoutDate = value; }
    }
}
