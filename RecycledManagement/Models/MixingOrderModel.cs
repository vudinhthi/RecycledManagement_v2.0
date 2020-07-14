using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycledManagement.Models
{
    public class MixingOrderModel
    {
        public MixingOrderModel() { }

        public MixingOrderModel(DataRow row)
        {
            this.mixId = row[0].ToString();
            this.mixCode = row[1].ToString();
            this.mixShiftId = row[2].ToString();
            this.mixShiftName = row[3].ToString();
            this.mixOperatorId = row[4].ToString();
            this.mixOperatorName = row[5].ToString();
            this.weightMixTotal = row[6].ToString();
            this.reasonId = row[7].ToString();
            this.mixNote = row[8].ToString();
            this.mixCreatedDate = row[9].ToString();

            this.createdBy = row[10].ToString();
            this.weightMaterialTotal = row[11].ToString();
            this.weightRecycleTotal = row[12].ToString();


            this.orderId = row[13].ToString();
            this.orderCode = row[14].ToString();
            this.machine = row[15].ToString();
            this.itemCode = row[16].ToString();
            this.itemName = row[17].ToString();
            this.colorCode = row[18].ToString();
            this.colorName = row[19].ToString();
            this.orderAmount = row[20].ToString();
            this.orderStatus = row[21].ToString();
            this.orderCreatedDate = row[22].ToString();
            this.orderNote = row[23].ToString();
            this.orderOperatorId = row[24].ToString();
            this.orderOperatorName = row[25].ToString();
            this.orderType = row[26].ToString();
            this.finishDate = row[27].ToString();
            this.orderShiftId = row[28].ToString();

            this.orderLogId = row[29].ToString();
            this.status = row[30].ToString();

        }

        #region Mix
        private string mixId;
        public string MixId { get => mixId; set => mixId = value; }

        private string mixCode;
        public string MixCode { get => mixCode; set => mixCode = value; }

        private string mixShiftId;
        public string MixShiftId { get => mixShiftId; set => mixShiftId = value; }

        private string mixShiftName;
        public string MixShiftName { get => mixShiftName; set => mixShiftName = value; }

        private string mixOperatorId;
        public string MixOperatorId { get => mixOperatorId; set => mixOperatorId = value; }

        private string mixOperatorName;
        public string MixOperatorName { get => mixOperatorName; set => mixOperatorName = value; }

        private string weightMixTotal;
        public string WeightMixTotal { get => weightMixTotal; set => weightMixTotal = value; }

        private string reasonId;
        public string ReasonId { get => reasonId; set => reasonId = value; }

        private string mixNote;
        public string MixNote { get => mixNote; set => mixNote = value; }

        private string mixCreatedDate;
        public string MixCreatedDate { get => mixCreatedDate; set => mixCreatedDate = value; }

        private string createdBy;
        public string CreatedBy { get => createdBy; set => createdBy = value; }

        private string weightMaterialTotal;
        public string WeightMaterialTotal { get => weightMaterialTotal; set => weightMaterialTotal = value; }

        private string weightRecycleTotal;
        public string WeightRecycledTotal { get => weightRecycleTotal; set => weightRecycleTotal = value; }
        #endregion

        #region Order
        private string orderId;
        public string OrderId { get => orderId; set => orderId = value; }

        private string orderCode;
        public string OrderCode { get => orderCode; set => orderCode = value; }

        private string machine;
        public string Machine { get => machine; set => machine = value; }

        private string itemCode;
        public string ItemCode { get => itemCode; set => itemCode = value; }

        private string itemName;
        public string ItemName { get => itemName; set => itemName = value; }

        private string colorCode;
        public string ColorCode { get => colorCode; set => colorCode = value; }

        private string colorName;
        public string ColorName { get => colorName; set => colorName = value; }

        private string orderAmount;
        public string OrderAmount { get => orderAmount; set => orderAmount = value; }

        private string orderStatus;
        public string OrderStatus { get => orderStatus; set => orderStatus = value; }

        private string orderCreatedDate;
        public string OrderCreatedDate { get => orderCreatedDate; set => orderCreatedDate = value; }

        private string orderNote;
        public string OrderNote { get => orderNote; set => orderNote = value; }

        private string orderOperatorId;
        public string OrderOperatorId { get => orderOperatorId; set => orderOperatorId = value; }

        private string orderOperatorName;
        public string OrderOperatorName { get => orderOperatorName; set => orderOperatorName = value; }

        private string orderType;
        public string OrderType { get => orderType; set => orderType = value; }

        private string finishDate;
        public string FinishDate { get => finishDate; set => finishDate = value; }

        private string orderShiftId;
        public string OrderShiftId { get => orderShiftId; set => orderShiftId = value; }
        #endregion

        private string orderLogId;
        public string OrderLogId { get => orderLogId; set => orderLogId = value; }

        private string status;
        public string Status { get => status; set => status = value; }
    }
}
