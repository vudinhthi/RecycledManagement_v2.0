using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Models
{
    public class UserActionModel
    {
        private int id;
        private int userId;
        private DateTime loginDate;
        private string action;

        public int Id { get => id; set => id = value; }
        public int UserId { get => userId; set => userId = value; }
        public DateTime LoginDate { get => loginDate; set => loginDate = value; }
        public string Action { get => action; set => action = value; }
    }
}
