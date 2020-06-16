using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Models
{
    public class OperatroRoleModel
    {
        //public UserActionModel()
        //{
            
        //}
        //public UserActionModel(DataRow row)
        //{
        //    this.id = (int)row[0];
        //    this.userId = (int)row[1];
        //    this.booking = (string)row[2];
        //    this.mixing = (string)row[3];
        //    this.incoming = (string)row[4];
        //    this.crushing = (string)row[5];
        //}

        private int id;
        private int userId;
        private string booking;
        private string mixing;
        private string incoming;
        private string crushing;

        public int Id { get => id; set => id = value; }
        public int UserId { get => userId; set => userId = value; }
        public string Booking { get => booking; set => booking = value; }
        public string Mixing { get => mixing; set => mixing = value; }
        public string Incoming { get => incoming; set => incoming = value; }
        public string Crushing { get => crushing; set => crushing = value; }
    }
}
