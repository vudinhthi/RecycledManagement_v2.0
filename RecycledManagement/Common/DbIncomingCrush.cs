using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Common
{
    public class DbIncomingCrush
    {
        #region singleton
        private static DbIncomingCrush _instance;
        public static DbIncomingCrush Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DbIncomingCrush();
                }
                return _instance;
            }
            set => _instance = value;
        }
        public DbIncomingCrush() { }
        #endregion

        //get all tblOtherSource
        public DataTable GetAll()
        {
            return DataProvider.Instance.ExecuteQuery("sp_IncomingCrushGet");
        }

        //get dữ liệu liên bảng để lấy hiển thị lên dataGridView
        public DataTable GetDataGridView()
        {
            return DataProvider.Instance.ExecuteQuery("sp_IncomingCrushGetLienBang");
        }

        //get 1 dong data theo cruchedId
        public DataTable GetLable(string incomingId)
        {
            return DataProvider.Instance.ExecuteQuery("sp_IncomingGetLable @IncomingId", new object[] { incomingId });
        }


        //Insert data
        public int InsertData(string MixId, string ShiftId, string LossTypeId, string SourceId, string reasonId, string MaterialCode, string MaterialName, string WeightIncoming, string CeratesBy, string IncomingCode)
        {
            return DataProvider.Instance.ExecuteNonQuery("sp_IncomingCrushInsertAll @MixId , @ShiftId , @LossTypeId , @SourceId , @ReasonId , @MaterialCode" +
                " , @MaterialName , @WeightIncoming , @CeratesBy , @IncomingCode",
                new object[] { MixId, ShiftId, LossTypeId, SourceId, reasonId, MaterialCode, MaterialName, WeightIncoming, CeratesBy, IncomingCode });
        }

        //update data
        public int Update(string incomingId, string MixId, string ShiftId, string LossTypeId, string SourceId, string reasonId, string MaterialCode, string MaterialName, string WeightIncoming, string CeratesBy, string IncomingCode)
        {
            return DataProvider.Instance.ExecuteNonQuery("sp_IncomingCrushUpdate @InconingId , @MixId , @ShiftId , @LossTypeId , @SourceId , @ReasonId , @MaterialCode" +
                " , @MaterialName , @WeightIncoming , @CeratesBy , @IncomingCode",
                new object[] { incomingId, MixId, ShiftId, LossTypeId, SourceId, reasonId, MaterialCode, MaterialName, WeightIncoming, CeratesBy, IncomingCode });
        }

        //lay ve 2 cot materialCode va MaterialName
        public DataTable GetMaterialsIncomingWinLine()
        {
            return DataProvider.Instance.ExecuteQuery("sp_getMaterialsIncomingWinLine");
        }

        //get max IncomingId
        public int GetMaxIncomingId()
        {
            return DataProvider.Instance.ExecuteNonQuery_GetIdIdentity("sp_IncomingCrushGetMaxIncomingId");
        }

        //get ve MaterialCode từ điều kiện materialName
        public DataTable GetIdCacBang(string mixCode, string shiftName, string sourceName, string reasonName, string materialName)
        {
            return DataProvider.Instance.ExecuteQuery("sp_IncomingCrushGetIdCacBang @MixName , @ShiftName , @SourceName , @ReasonName , @MaterialName"
                , new object[] { mixCode, shiftName, sourceName, reasonName, materialName });
        }
    }
}
