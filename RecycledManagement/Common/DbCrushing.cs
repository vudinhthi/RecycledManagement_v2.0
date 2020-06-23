using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Common
{
    public class DbCrushing
    {
        #region tạo Degin Parttern Singleton
        private static DbCrushing _instance;
        public static DbCrushing Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DbCrushing();
                }
                return _instance;
            }

            private set => _instance = value;
        }
        public DbCrushing()
        {

        }
        #endregion

        public DataTable GetDataGridView()
        {
            return DataProvider.Instance.ExecuteQuery("sp_CrushingGetLienBang");
        }
    }
}
