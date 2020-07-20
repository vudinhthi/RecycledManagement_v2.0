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
            this.mixId = row["MixId"].ToString();
            this.mixCode = row["MixCode"].ToString();
            this.mixShiftId = row["MixShiftId"].ToString();
            this.mixShiftName = row["MixShiftName"].ToString();
            this.mixOperatorId = row["MixOperatorId"].ToString();
            this.mixOperatorName = row["MixOperatorName"].ToString();
            this.weightMixTotal = row["WeightMixTotal"].ToString();
            this.reasonId = row["ReasonId"].ToString();
            this.mixNote = row["MixNote"].ToString();
            this.mixCreatedDate = row["MixCreatedDate"].ToString();

            this.createdBy = row["CreatedBy"].ToString();
            this.weightMaterialTotal = row["WeightMaterialTotal"].ToString();
            this.weightRecycleTotal = row["WeightRecycledTotal"].ToString();


            this.orderId = row["OrderId"].ToString();
            this.orderCode = row["OrderCode"].ToString();
            this.machine = row["Machine"].ToString();
            this.itemCode = row["ItemCode"].ToString();
            this.itemName = row["ItemName"].ToString();
            this.colorCode = row["ColorCode"].ToString();
            this.colorName = row["ColorName"].ToString();
            this.orderAmount = row["OrderAmount"].ToString();
            this.orderStatus = row["OrderStatus"].ToString();
            this.orderCreatedDate = row["OrderCreatedDate"].ToString();
            this.orderNote = row["OrderNote"].ToString();
            this.orderOperatorId = row["OrderOperatorId"].ToString();
            this.orderOperatorName = row["OrderOperatorName"].ToString();
            this.orderType = row["OrderType"].ToString();
            this.finishDate = row["FinishDate"].ToString();
            this.orderShiftId = row["OrderShiftId"].ToString();

            this.orderLogId = row["OrderLogId"].ToString();
            this.status = row["Status"].ToString();

        }

        private string orderLogId;
        public string OrderLogId { get => orderLogId; set => orderLogId = value; }

        private string status;
        public string Status { get => status; set => status = value; }

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

       
    }
}
