using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Models
{
    public class AccountModel
    {
        public int id { set; get; }
        public string userName { set; get; }
        public string password { set; get; }
        public string role { set; get; }
        public DateTime creaeDate { set; get; }
        public string fullName { set; get; }
        public string department { set; get; }
        public int employeeId { set; get; }
    }
}
