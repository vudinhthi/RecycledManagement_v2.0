using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Common
{
    public class SqlTransactionQueryList
    {
        private string query;
        private object[] parametter;

        public string Query { get => query; set => query = value; }
        public object[] Parametter { get => parametter; set => parametter = value; }
    }
}
