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
        int status = 0;//xet xem trạng thái của đơn hàng đang ở công đoạn nào để chọn cân cho phù hợp
        double totalMaterialConsumtion = 0, totalRecycle = 0, total = 0;
        double weightRecycle1 = 0, weightRecycle2 = 0, weightCompound = 0, weightClearRecycle = 0, weightFramapur = 0, weightLeftover = 0;

        MixingOrderModel orderInfo;
        List<MixProductWinlineModel> productMix;

        System.Timers.Timer nTimer = new System.Timers.Timer();



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
            orderInfo = DbMixCode.Instance.GetAllMixed1Row(GlobalVariable.orderId.ToString());//get đơn hàng về

            //hiển thị thông tin đơn hàng
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

            #region kiểm tra trạng thái đơn hàng để set biến chọn cân để cân
            //chỉ set " GlobalVariable.selectScale" trong pageLoad khi status= 3, vì 1 2 sẽ đc set trong event của gridview
            //lấy danh sách product theo mã itemCode hiển thị lên gridView material Comsumption
            /*LƯU Ý:
             * -nếu status=1 (new Order)->thì chỉ cần get product từ winline lên đưa vào gridView
             * -nếu status=2 (đã cân màu rồi-->cân nhựa) -> get thêm khối lượng cân màu fill vào các row màu
             * -nếu status=3 (đã cân màu và nhựa-->cân recycle) -> get thêm khối lượng cân màu và nhựa fill vào các dòng trong gridView materialConsumption
            */
            productMix = DbMixCode.Instance.GetProductWinline(orderInfo.ItemCode, orderInfo.OrderAmount);
            status = Convert.ToInt32(orderInfo.Status);

            if (status == 1) //new order-->cân màu
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

                grcMaterialConsumption.DataSource = productMix;//hiển thị danh sách Product lên gridView Material Consumption
            }
            else if (status == 2)//--> Cân nhựa
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
         
                lookUpShift.Text = orderInfo.MixShiftName;
                lookUpOperator.Text = orderInfo.MixOperatorName;

                lookUpShift.ReadOnly = true;
                lookUpOperator.ReadOnly = true;

                #region get color scales
                DataTable dataColorScales = DbMixCode.Instance.GetMaterialCsalesColor(orderInfo.MixId);
                if (dataColorScales!=null&&dataColorScales.Rows.Count>0)
                {
                    foreach (DataRow item in dataColorScales.Rows)
                    {
                        foreach (var item1 in productMix)
                        {
                            //if ()
                            //{

                            //}
                        }
                    }
                }
                #endregion

                grcMaterialConsumption.DataSource = productMix;//hiển thị danh sách Product lên gridView Material Consumption

            }
            else if (status == 3)//cân recycle
            {
                lookUpShift.ReadOnly = true;
                lookUpOperator.ReadOnly = true;

                lookUpShift.Text = orderInfo.MixShiftName;
                lookUpOperator.Text = orderInfo.MixOperatorName;

                GlobalVariable.selectScale = "ScalePlastic";//set chon can nhựa vì trạng thái order đang ở cân recycle

                grcMaterialConsumption.Enabled = false;

                grcMaterialConsumption.DataSource = productMix;//hiển thị danh sách Product lên gridView Material Consumption
            }
            #endregion

            #endregion


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
                        grvMaterialConsumption.SetFocusedRowCellValue("ActualUsage", o.ScaleValue - GlobalVariable.boxWeightMixingMaterial);
                    }
                }
                else if (status == 3)//can Recycle
                {
                    if (txtWeightRecycle1.ContainsFocus)
                    {
                        txtWeightRecycle1.Text = (o.ScaleValue - GlobalVariable.boxWeightMixingRecycle).ToString();
                    }
                    else if (txtWeightRecycle2.ContainsFocus)
                    {
                        txtWeightRecycle2.Text = (o.ScaleValue - GlobalVariable.boxWeightMixingRecycle).ToString();
                    }
                    else if (txtWeightCompound.ContainsFocus)
                    {
                        txtWeightCompound.Text = (o.ScaleValue - GlobalVariable.boxWeightMixingRecycle).ToString();
                    }
                    else if (txtWeightClearRecycle.ContainsFocus)
                    {
                        txtWeightClearRecycle.Text = (o.ScaleValue - GlobalVariable.boxWeightMixingRecycle).ToString();
                    }
                    else if (txtWeightFramapur.ContainsFocus)
                    {
                        txtWeightFramapur.Text = (o.ScaleValue - GlobalVariable.boxWeightMixingRecycle).ToString();
                    }
                    else if (txtWeightLeftover.ContainsFocus)
                    {
                        txtWeightLeftover.Text = (o.ScaleValue - GlobalVariable.boxWeightMixingRecycle).ToString();
                    }
                }
            };


            //khoi tao timer
            nTimer.Interval = 500;
            nTimer.Elapsed += NTimer_Elapsed;
            nTimer.Enabled = true;
        }

        private void NTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            nTimer.Enabled = false;
            try
            {
                totalRecycle = weightRecycle1 + weightRecycle2 + weightCompound + weightClearRecycle + weightFramapur + weightLeftover;

                txtTotalRecycleWeight.Invoke(new Action(() =>
                {
                    txtTotalRecycleWeight.Text = totalRecycle.ToString();
                }));

                total = totalMaterialConsumtion + totalRecycle;

                txtTotalMaterialWeight.Invoke(new Action(() =>
                {
                    txtTotalMaterialWeight.Text = total.ToString();
                }));
            }
            catch { }
            nTimer.Enabled = true;
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
            List<SqlTransactionQueryList> listQuery = new List<SqlTransactionQueryList>();

            if (status == 1)//lưu cân màu
            {
                listQuery.Add(new SqlTransactionQueryList() { Query = "sp_MixedInsert @OrderId , @ShiftId , @OperatorId , @WeightMixTotal , @ReasonId , @Note , @CreatedBy , @WeightMaterialTotal , @WeightRecycledTotal", Parametter = new object[] { orderInfo.OrderId, lookUpShift.EditValue, lookUpOperator.EditValue, total, null, null, GlobalVariable.userId, totalMaterialConsumtion, totalRecycle } });

                foreach (var item in productMix)
                {
                    if (item.MaterialCode.Contains("RCP") || item.MaterialCode.Contains("RMB") || item.MaterialCode.Contains("REX") || item.MaterialCode.Contains("RAD"))
                    {
                        listQuery.Add(new SqlTransactionQueryList() { Query = "sp_MixMaterialScaledInsert @MaterialCode , @WeightMaxScaled , @MixId , @CreatedBy , @MaterialName , @WeightMacEdited", Parametter = new object[] { } });
                    }
                }

                listQuery.Add(new SqlTransactionQueryList() { Query = "sp_MixRecycledScaledInsert @WeightReScaled , @WeightReTotal , @MixId , @CreatedBy , @RecycledCode", Parametter = new object[] { } });
            }
            else if (status == 2)//lưu cân nhựa
            {
                //listQuery.Add(new SqlTransactionQueryList() { Query = "sp_MixedInsert @OrderId , @ShiftId , @OperatorId , @WeightMixTotal , @ReasonId , @Note , @CreatedBy , @WeightMaterialTotal , @WeightRecycledTotal", Parametter = new object[] { orderInfo.OrderId, lookUpShift.EditValue, lookUpOperator.EditValue, total, null, null, GlobalVariable.userId, totalMaterialConsumtion, totalRecycle } });

                foreach (var item in productMix)
                {
                    if (item.MaterialCode.Contains("RPM") || item.MaterialCode.Contains("RCM") || item.MaterialCode.Contains("RRE") || item.MaterialCode.Contains("RMX"))
                    {
                        listQuery.Add(new SqlTransactionQueryList() { Query = "sp_MixMaterialScaledInsert @MaterialCode , @WeightMaxScaled , @MixId , @CreatedBy , @MaterialName , @WeightMacEdited", Parametter = new object[] { } });
                    }
                }
            }
            else if (status == 3)//lưu cân recycle
            {
                //listQuery.Add(new SqlTransactionQueryList() { Query = "sp_MixedInsert @OrderId , @ShiftId , @OperatorId , @WeightMixTotal , @ReasonId , @Note , @CreatedBy , @WeightMaterialTotal , @WeightRecycledTotal", Parametter = new object[] { orderInfo.OrderId, lookUpShift.EditValue, lookUpOperator.EditValue, total, null, null, GlobalVariable.userId, totalMaterialConsumtion, totalRecycle } });
                listQuery.Add(new SqlTransactionQueryList() { Query = "sp_MixRecycledScaledInsert @WeightReScaled , @WeightReTotal , @MixId , @CreatedBy , @RecycledCode", Parametter = new object[] { } });
            }


            if (DbMixCode.Instance.CreatedMix(listQuery) > 0)
            {
                GlobalVariable.myEvent.ShowMixingEditor = false;
                this.Close();
            }
            else
            {
                MessageBox.Show($"Created Mixing Fail!");
            }
        }


        private void txtWeightRecycle1_EditValueChanged(object sender, EventArgs e)
        {
            weightRecycle1 = Convert.ToDouble(txtWeightRecycle1.Text);
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

        private void grvMaterialConsumption_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
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
                }
            }
        }
    }
}