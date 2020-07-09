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

        //get toan bộ các recode trong bang tblCrushed kết hợp thêm các data liên kết
        public DataTable GetDataGridView()
        {
            return DataProvider.Instance.ExecuteQuery("sp_CrushingGetLienBang");
        }

        //get 1 dong data theo cruchedId
        public DataTable GetLable(string crushedId)
        {
            return DataProvider.Instance.ExecuteQuery("sp_CrushingGetLable @CrushedId", new object[] { crushedId });
        }

        public DataTable GetOperators()
        {
            return DataProvider.Instance.ExecuteQuery("sp_CrushingGetOperator");
        }

        public int Update(string crushId, string shiftId, string operatorId, string mixId, string machine, string materialCode, string materialName, string weightCrush, string createdBy, string crushedCode, string crushType)
        {
            return DataProvider.Instance.ExecuteNonQuery("sp_CrushUpdate @CrushId , @ShiftId , @OperatorId , @MixId , @Machine , @MaterialCode , @MaterialName , @WeightCrushed , @CeratedBy , @CrushedCode , @CrushedType"
                , new object[] { crushId, shiftId, operatorId, mixId, machine, materialCode, materialName, weightCrush, createdBy, crushedCode, crushType });
        }

        public int Insert(string shiftId, string operatorId, string mixId, string machine, string materialCode, string materialname, string weightCrush, string createdBy, string crushCode, string crushType)
        {
            return DataProvider.Instance.ExecuteNonQuery("sp_CrushInsert @ShiftId , @OperatorId , @MixId , @Machine , @MaterialCode , @MaterialName , @WeightCrushed , @CeratedBy , @CrushedCode , @CrushedType", new object[] { shiftId, operatorId, mixId, machine, materialCode, materialname, weightCrush, createdBy, crushCode, crushType });
        }

        public DataTable GetIdCacBang(string shiftName, string operatorName, string mixName, string materialName)
        {
            return DataProvider.Instance.ExecuteQuery("sp_CrushGetIdCacBang @ShiftName , @OperatorName , @MixName , @MaterialName", new object[] { shiftName, operatorName, mixName, materialName });
        }

        public int GetMaxId()
        {
            return DataProvider.Instance.ExecuteNonQuery_GetIdIdentity("sp_CrushGetMaxCrushId");
        }
    }
}
