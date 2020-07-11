using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using RecycledManagement.Common;
using RecycledManagement.Models;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System.Diagnostics;
using DevExpress.XtraGrid;
using DevExpress.Data;
using System.Threading;

namespace RecycledManagement
{
    public partial class frmMixing : DevExpress.XtraEditors.XtraForm
    {
        int status;//xet xem trạng thái của đơn hàng đang ở công đoạn nào để chọn cân cho phù hợp
        double totalMaterialConsumtion, totalMaterialConsumtionNet = 0;


        public frmMixing()
        {
            InitializeComponent();
        }

        private void frmMixing_Load(object sender, EventArgs e)
        {
            #region get shifts
            lookUpShift.Properties.DataSource = DbShift.Instance.GetShiftComming();
            lookUpShift.Properties.ValueMember = "ShiftId";
            lookUpShift.Properties.DisplayMember = "ShiftName";
            #endregion

            #region get TeamLeader
            lookUpOperator.Properties.DataSource = DbOperator.Instance.SelectAll();
            lookUpOperator.Properties.ValueMember = "OperatorId";
            lookUpOperator.Properties.DisplayMember = "OperatorName";
            #endregion

            #region get reason
            lookUpReason.Properties.DataSource = DbReasons.Instance.GetReasonType(1);
            lookUpReason.Properties.ValueMember = "ReasonId";
            lookUpReason.Properties.DisplayMember = "ReasonName";
            #endregion


            #region set trang thai order
            //MixingOrderModel orderInfo = DbMixCode.Instance.GetAllMixed1Row(!string.IsNullOrEmpty(GlobalVariable.mixId) ? GlobalVariable.mixId : null);
            MixingOrderModel orderInfo = DbMixCode.Instance.GetAllMixed1Row(GlobalVariable.orderId.ToString());

            if (orderInfo != null)
            {
                txtOrderId.Text = orderInfo.OrderId;
                txtMachine.Text = orderInfo.Machine;
                txtTeamLeader.Text = orderInfo.OrderOperatorName;
                txtReceivingTime.Text = orderInfo.FinishDate;
                txtOrderAmount.Text = orderInfo.OrderAmount;
                txtItemName.Text = orderInfo.ItemName;
                txtColorName.Text = orderInfo.ColorName;
                txtOrderNote.Text = orderInfo.OrderNote;
                txtItemCode.Text = orderInfo.ItemCode;
                txtColorCode.Text = orderInfo.ColorCode;
            }
            //kiểm tra trạng thái đơn hàng để set biến chọn cân để cân
            //chỉ set trong pageLoad trạng thái 3, vì 1 2 sẽ đc set trọng event của gridview
            status = Convert.ToInt32(orderInfo.Status);
            if (status == 1 || status == 2)
            {
                lookUpRecycle1.Enabled = false;
                lookUpRecycle2.Enabled = false;
                lookUpCompound.Enabled = false;
                lookUpClearRecycle.Enabled = false;
                lookUpFramapur.Enabled = false;
                lookUpLeftover.Enabled = false;

                txtWeightRecycle1.Enabled = false;
                txtWeightRecycle2.Enabled = false;
                txtWeightCompound.Enabled = false;
                txtWeightClearRecycle.Enabled = false;
                txtWeightFramapur.Enabled = false;
                txtWeightLeftover.Enabled = false;

                lookUpReason.Enabled = false;
                checkBoxUsingRecycle.Enabled = false;
                txtTotalRecycleWeight.Enabled = false;
                txtNetWeightRecycle.Enabled = false;

                if (status == 2)
                {
                    lookUpShift.ReadOnly = true;
                    lookUpOperator.ReadOnly = true;

                    lookUpShift.EditValue = (object)orderInfo.MixShiftId;

                }
            }
            else if (status == 3)
            {
                lookUpShift.ReadOnly = true;
                lookUpOperator.ReadOnly = true;
                lookUpShift.EditValue = orderInfo.MixShiftId;
                lookUpOperator.EditValue = orderInfo.MixOperatorId;

                GlobalVariable.selectScale = "ScalePlastic";//set chon can nhựa vì trạng thái order đang ở cân recycle

                grcMaterialConsumption.Enabled = false;
            }
            #endregion

            List<MixProductWinlineModel> data = DbMixCode.Instance.GetProductWinline(orderInfo.ItemCode, orderInfo.OrderAmount);

            grcMaterialConsumption.DataSource = data;

            //đang ký sự kiện scaleValueChanged
            GlobalVariable.myEvent.ScaleValueChanged += (s, o) =>
            {
                Debug.WriteLine($"Mixing event write: {o.ScaleValue} | Allow Focus: {grvControlTextEdit.AllowFocused} | Appearance Focus: ");

                //grvMaterialConsumption.SetFocusedValue(o.ScaleValue);
                if (status == 1 || status == 2)
                {
                    //Thread.Sleep(500);
                    //grvMaterialConsumption.SetFocusedValue(o.ScaleValue);
                    string materialCodeSub = grvMaterialConsumption.GetFocusedRowCellDisplayText("MaterialCode").Substring(0, 3);
                    if (status == 1 && (materialCodeSub == "RCP" || materialCodeSub == "RMB" || materialCodeSub == "REX" || materialCodeSub == "RAD"))//can mau
                    {
                        grvMaterialConsumption.SetFocusedRowCellValue("ActualUsage", o.ScaleValue);
                    }
                    else if (status == 2 && (materialCodeSub == "RPM" || materialCodeSub == "RCM" || materialCodeSub == "RRE" || materialCodeSub == "RMX"))//can nhua
                    {
                        grvMaterialConsumption.SetFocusedRowCellValue("ActualUsage", o.ScaleValue);
                    }
                }
                else if (status == 3)
                {
                    if (txtWeightRecycle1.ContainsFocus)
                    {
                        txtWeightRecycle1.Text = o.ScaleValue.ToString();
                    }
                }
            };

        }

        private void checkBoxUsingRecycle_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUsingRecycle.Checked == true)
            {
                lookUpRecycle1.Enabled = true;
                lookUpRecycle2.Enabled = true;
                lookUpCompound.Enabled = true;
                lookUpClearRecycle.Enabled = true;
                lookUpFramapur.Enabled = true;
                lookUpLeftover.Enabled = true;

                txtWeightRecycle1.Enabled = true;
                txtWeightRecycle2.Enabled = true;
                txtWeightCompound.Enabled = true;
                txtWeightClearRecycle.Enabled = true;
                txtWeightFramapur.Enabled = true;
                txtWeightLeftover.Enabled = true;

                lookUpReason.Enabled = false;
            }
            else
            {
                lookUpRecycle1.Enabled = false;
                lookUpRecycle2.Enabled = false;
                lookUpCompound.Enabled = false;
                lookUpClearRecycle.Enabled = false;
                lookUpFramapur.Enabled = false;
                lookUpLeftover.Enabled = false;

                txtWeightRecycle1.Enabled = false;
                txtWeightRecycle2.Enabled = false;
                txtWeightCompound.Enabled = false;
                txtWeightClearRecycle.Enabled = false;
                txtWeightFramapur.Enabled = false;
                txtWeightLeftover.Enabled = false;

                lookUpReason.Enabled = true;
            }
        }

        //Cteated Mix
        private void btnSave_Click(object sender, EventArgs e)
        {


            GlobalVariable.myEvent.ShowMixingEditor = false;
            this.Close();
        }

        private void grvMaterialConsumption_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //GridView view = sender as GridView;

            //MixProductWinlineModel data = view.GetRow(e.RowHandle) as MixProductWinlineModel;

            //if (data != null)
            //{
            //    string materialCodeSub = "";
            //    view.OptionsBehavior.Editable = false;//khoa ko cho nhap tren GridView

            //    if (status == 1)//New order--> Cân color
            //    {
            //        materialCodeSub = data.MaterialCode.Substring(0, 3);
            //        if (materialCodeSub == "RCP" || materialCodeSub == "RMB" || materialCodeSub == "REX" || materialCodeSub == "RAD")
            //        {
            //            view.OptionsBehavior.Editable = true;//mở khóa nhap tren GridView

            //        }
            //    }
            //    else if (status == 2)//Process-->Cân nhựa
            //    {
            //        e.Appearance.BackColor = Color.Yellow;
            //    }
            //    else if (status == 3)//Process--> Cân Recycle
            //    {
            //        e.Appearance.BackColor = Color.Magenta;
            //    }
            //    else if (status == 4)//finish
            //    {
            //        e.Appearance.BackColor = Color.Green;
            //    }
            //}
        }

        //Disable row theo dieu kien
        private void grvMaterialConsumption_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;

            string ma = grvMaterialConsumption.GetRowCellValue(grvMaterialConsumption.FocusedRowHandle, "MaterialCode").ToString();
            string materialCodeSub = ma.Substring(0, 3);

            if (status == 1)//New order--> Cân color
            {
                GlobalVariable.selectScale = "ScaleColor";

                if (materialCodeSub == "RPM" || materialCodeSub == "RCM" || materialCodeSub == "RRE" || materialCodeSub == "RMX")
                {
                    e.Cancel = true;//disable Row
                }
            }
            else if (status == 2)//Process-->Cân nhựa
            {
                GlobalVariable.selectScale = "ScalePlastic";
                if (materialCodeSub == "RCP" || materialCodeSub == "RMB" || materialCodeSub == "REX" || materialCodeSub == "RAD")
                {
                    e.Cancel = true;
                }
            }
            //else if (status == 3)//Process--> Cân Recycle
            //{
            //    GlobalVariable.selectScale = "ScalePlastic";
            //    e.Cancel = true;
            //}
            //else if (status == 4)//finish
            //{
            //    e.Cancel = true;
            //}
        }

        private void grvMaterialConsumption_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            GridView view = sender as GridView;
            // Get the summary ID. 
            int summaryID = Convert.ToInt32((e.Item as GridSummaryItem).Tag);

            // Initialization. 
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                //discontinuedProductsCount = 0;
                if (summaryID == 1)
                {
                    totalMaterialConsumtion = 0;
                }
            }

            // Calculation.
            if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                switch (summaryID)
                {
                    case 1: // The total summary calculated against the 'UnitPrice' column. 
                        double unitsInStock = Convert.ToDouble(view.GetRowCellValue(e.RowHandle, "ActualUsage"));
                        totalMaterialConsumtion += Math.Round(Convert.ToDouble(e.FieldValue), 3);
                        break;
                        //case 2: // The group summary. 
                        //maxOrderSize
                        //break;
                }
            }

            // Finalization. 
            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                switch (summaryID)
                {
                    case 1:
                        e.TotalValue = totalMaterialConsumtion;
                        break;
                        //case 2:
                        //    maxOrderSize = Math.Round(((25 - (1 * numOfOrderSize)) / shotWeight) - 20, 3);
                        //    e.TotalValue = maxOrderSize;
                        //    break;
                }
            }
        }
    }
}