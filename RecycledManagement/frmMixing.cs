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
using DevExpress.XtraEditors.Controls;

namespace RecycledManagement
{
    public partial class frmMixing : DevExpress.XtraEditors.XtraForm
    {
        int status = 0;//xet xem trạng thái của đơn hàng đang ở công đoạn nào để chọn cân cho phù hợp
        double totalMaterialConsumtion = 0, totalRecycle = 0, total = 0;
        double weightRecycle1 = 0, weightRecycle2 = 0, weightCompound = 0, weightClearRecycle = 0, weightFramapur = 0, weightLeftover = 0;

        MixingOrderModel orderInfo;
        List<MixProductWinlineModel> productMixList;

        System.Timers.Timer nTimer = new System.Timers.Timer();



        public frmMixing()
        {
            InitializeComponent();
        }

        #region Lookup Recycle change Event
        private void lookUpRecycle1_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lookUpRecycle1.Text) && lookUpRecycle1.Text != "[EditValue is null]")
            {
                txtWeightRecycle1.Enabled = true;
            }
        }

        private void lookUpRecycle2_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lookUpRecycle2.Text) && lookUpRecycle2.Text != "[EditValue is null]")
            {
                txtWeightRecycle2.Enabled = true;
            }
        }

        private void lookUpCompound_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lookUpCompound.Text) && lookUpCompound.Text != "[EditValue is null]")
            {
                txtWeightCompound.Enabled = true;
            }
        }

        private void lookUpClearRecycle_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lookUpClearRecycle.Text) && lookUpClearRecycle.Text != "[EditValue is null]")
            {
                txtWeightClearRecycle.Enabled = true;
            }
        }

        private void lookUpFramapur_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lookUpFramapur.Text) && lookUpFramapur.Text != "[EditValue is null]")
            {
                txtWeightFramapur.Enabled = true;
            }
        }

        private void txtWeightRecycle1_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            ButtonEdit editorWeightRM = (ButtonEdit)sender;
            EditorButton Button = e.Button;

            if (Button.Kind == ButtonPredefines.Delete)
            {
                editorWeightRM.Text = "0";
                weightRecycle1 = 0;
            }
        }

        private void txtWeightRecycle2_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            ButtonEdit editorWeightRM = (ButtonEdit)sender;
            EditorButton Button = e.Button;

            if (Button.Kind == ButtonPredefines.Delete)
            {
                editorWeightRM.Text = "0";
                weightRecycle2 = 0;
            }
        }

        private void txtWeightCompound_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            ButtonEdit editorWeightRM = (ButtonEdit)sender;
            EditorButton Button = e.Button;

            if (Button.Kind == ButtonPredefines.Delete)
            {
                editorWeightRM.Text = "0";
                weightCompound = 0;
            }
        }

        private void txtWeightClearRecycle_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            ButtonEdit editorWeightRM = (ButtonEdit)sender;
            EditorButton Button = e.Button;

            if (Button.Kind == ButtonPredefines.Delete)
            {
                editorWeightRM.Text = "0";
                weightClearRecycle = 0;
            }
        }

        private void txtWeightFramapur_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            ButtonEdit editorWeightRM = (ButtonEdit)sender;
            EditorButton Button = e.Button;

            if (Button.Kind == ButtonPredefines.Delete)
            {
                editorWeightRM.Text = "0";
                weightFramapur = 0;
            }
        }

        private void txtWeightLeftover_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            ButtonEdit editorWeightRM = (ButtonEdit)sender;
            EditorButton Button = e.Button;

            if (Button.Kind == ButtonPredefines.Delete)
            {
                editorWeightRM.Text = "0";
                weightLeftover = 0;
            }
        }

        private void lookUpLeftover_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lookUpLeftover.Text) && lookUpLeftover.Text != "[EditValue is null]")
            {
                txtWeightLeftover.Enabled = true;
            }
            else
            {
                txtWeightLeftover.Enabled = false;
            }
        }
        #endregion

        //Form Load Event
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

            #region get data lookupEdit for Recycle Group

            #region get reason
            lookUpReason.Properties.DataSource = DbReasons.Instance.GetReasonType(1);
            lookUpReason.Properties.ValueMember = "ReasonId";
            lookUpReason.Properties.DisplayMember = "ReasonName";
            #endregion

            lookUpRecycle1.Properties.DataSource = DbMixCode.Instance.GetCrushingCode("RE|BOM%");
            lookUpRecycle1.Properties.ValueMember = "CrushId";
            lookUpRecycle1.Properties.DisplayMember = "CrushedCode";

            lookUpRecycle2.Properties.DataSource = DbMixCode.Instance.GetCrushingCode("RE|BOM%");
            lookUpRecycle2.Properties.ValueMember = "CrushId";
            lookUpRecycle2.Properties.DisplayMember = "CrushedCode";

            lookUpCompound.Properties.DataSource = DbMixCode.Instance.GetCrushingCode("RE|R%");
            lookUpCompound.Properties.ValueMember = "CrushId";
            lookUpCompound.Properties.DisplayMember = "CrushedCode";

            lookUpClearRecycle.Properties.DataSource = DbMixCode.Instance.GetCrushingCode("RE|R%");
            lookUpClearRecycle.Properties.ValueMember = "CrushId";
            lookUpClearRecycle.Properties.DisplayMember = "CrushedCode";

            lookUpFramapur.Properties.DataSource = DbMixCode.Instance.GetCrushingCode("RE|R%");
            lookUpFramapur.Properties.ValueMember = "CrushId";
            lookUpFramapur.Properties.DisplayMember = "CrushedCode";

            lookUpLeftover.Properties.DataSource = DbMixCode.Instance.GetIncomingCode();
            lookUpLeftover.Properties.ValueMember = "IncomingId";
            lookUpLeftover.Properties.DisplayMember = "IncomingCode";

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
            productMixList = DbMixCode.Instance.GetProductWinline(orderInfo.ItemCode, orderInfo.OrderAmount);
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

                grcMaterialConsumption.DataSource = productMixList;//hiển thị danh sách Product lên gridView Material Consumption
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
                if (dataColorScales != null && dataColorScales.Rows.Count > 0)
                {
                    foreach (DataRow item in dataColorScales.Rows)
                    {
                        foreach (var item1 in productMixList)
                        {
                            if (item1.MaterialCode == item[1].ToString())
                            {
                                item1.ActualUsage = item[7].ToString();
                            }
                        }
                    }
                }
                #endregion

                grcMaterialConsumption.DataSource = productMixList;//hiển thị danh sách Product lên gridView Material Consumption

            }
            else if (status == 3)//cân recycle
            {
                lookUpShift.ReadOnly = true;
                lookUpOperator.ReadOnly = true;

                lookUpShift.Text = orderInfo.MixShiftName;
                lookUpOperator.Text = orderInfo.MixOperatorName;

                GlobalVariable.selectScale = "ScalePlastic";//set chon can nhựa vì trạng thái order đang ở cân recycle

                grcMaterialConsumption.Enabled = false;

                txtWeightRecycle1.Enabled = false;
                txtWeightRecycle2.Enabled = false;
                txtWeightCompound.Enabled = false;
                txtWeightClearRecycle.Enabled = false;
                txtWeightFramapur.Enabled = false;
                txtWeightLeftover.Enabled = false;

                #region get color scales
                DataTable dataColorScales = DbMixCode.Instance.GetMaterialCsalesGetMixId(orderInfo.MixId);
                if (dataColorScales != null && dataColorScales.Rows.Count > 0)
                {
                    foreach (DataRow item in dataColorScales.Rows)
                    {
                        foreach (var item1 in productMixList)
                        {
                            if (item1.MaterialCode == item[1].ToString())
                            {
                                item1.ActualUsage = item[7].ToString();
                            }
                        }
                    }
                }
                #endregion

                grcMaterialConsumption.DataSource = productMixList;//hiển thị danh sách Product lên gridView Material Consumption
            }
            #endregion

            #endregion


            //đang ký sự kiện scaleValueChanged
            GlobalVariable.myEvent.ScaleValueChanged += (s, o) =>
            {
                string materialCodeSub;
                Debug.WriteLine($"Mixing event write: {o.ScaleValue} | Allow Focus: {grvControlTextEdit.AllowFocused} | Appearance Focus: ");

                //grvMaterialConsumption.SetFocusedValue(o.ScaleValue);
                if (status == 1 || status == 2)
                {

                    //grvMaterialConsumption.SetFocusedValue(o.ScaleValue);
                    try
                    {
                        materialCodeSub = grvMaterialConsumption.GetRowCellValue(grvMaterialConsumption.FocusedRowHandle, "MaterialCode").ToString().Substring(0, 3);


                        if (status == 1 && (materialCodeSub == "RCP" || materialCodeSub == "RMB" || materialCodeSub == "REX" || materialCodeSub == "RAD"))//can mau
                        {
                            grvMaterialConsumption.SetFocusedRowCellValue("ActualUsage", o.ScaleValue);
                        }
                        else if (status == 2 && (materialCodeSub == "RPM" || materialCodeSub == "RCM" || materialCodeSub == "RRE" || materialCodeSub == "RMX"))//can nhua
                        {
                            grvMaterialConsumption.SetFocusedRowCellValue("ActualUsage", o.ScaleValue - GlobalVariable.boxWeightMixingMaterial);
                        }
                    }
                    catch { }
                }
                else if (status == 3)//can Recycle
                {
                    if (txtWeightRecycle1.ContainsFocus)
                    {
                        weightRecycle1 = o.ScaleValue - GlobalVariable.boxWeightMixingRecycle;
                        txtWeightRecycle1.Text = weightRecycle1.ToString();
                    }
                    else if (txtWeightRecycle2.ContainsFocus)
                    {
                        weightRecycle2 = o.ScaleValue - GlobalVariable.boxWeightMixingRecycle;
                        txtWeightRecycle2.Text = weightRecycle2.ToString();
                    }
                    else if (txtWeightCompound.ContainsFocus)
                    {
                        weightCompound = o.ScaleValue - GlobalVariable.boxWeightMixingRecycle;
                        txtWeightCompound.Text = weightCompound.ToString();
                    }
                    else if (txtWeightClearRecycle.ContainsFocus)
                    {
                        weightClearRecycle = o.ScaleValue - GlobalVariable.boxWeightMixingRecycle;
                        txtWeightClearRecycle.Text = weightClearRecycle.ToString();
                    }
                    else if (txtWeightFramapur.ContainsFocus)
                    {
                        weightFramapur = o.ScaleValue - GlobalVariable.boxWeightMixingRecycle;
                        txtWeightFramapur.Text = weightFramapur.ToString();
                    }
                    else if (txtWeightLeftover.ContainsFocus)
                    {
                        weightLeftover = o.ScaleValue - GlobalVariable.boxWeightMixingRecycle;
                        txtWeightLeftover.Text = weightLeftover.ToString();
                    }
                }
            };


            //khoi tao timer
            nTimer.Interval = 500;
            nTimer.Elapsed += NTimer_Elapsed;
            nTimer.Enabled = true;
        }

        //Timer Event
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

        //CheckBox Recycle Event
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

                //txtWeightRecycle1.Enabled = true;
                //txtWeightRecycle2.Enabled = true;
                //txtWeightCompound.Enabled = true;
                //txtWeightClearRecycle.Enabled = true;
                //txtWeightFramapur.Enabled = true;
                //txtWeightLeftover.Enabled = true;

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
                //insert New Mix to tblMixed table
                listQuery.Add(new SqlTransactionQueryList() { Query = "sp_MixedInsert @MixCode , @OrderId , @ShiftId , @OperatorId , @WeightMixTotal , @ReasonId , @Note , @CreatedBy , @WeightMaterialTotal , @WeightRecycledTotal", Parametter = new object[] { $"MI|{DateTime.Now.ToString("yyyyyMMdd")}|{orderInfo.ItemCode}|", orderInfo.OrderId, lookUpShift.EditValue, lookUpOperator.EditValue, total, null, txtMixNote.Text, GlobalVariable.userId, totalMaterialConsumtion, totalRecycle } });

                //insert product to tblMaterialScale
                foreach (var item in productMixList)
                {
                    if (item.MaterialCode.Contains("RCP") || item.MaterialCode.Contains("RMB") || item.MaterialCode.Contains("REX") || item.MaterialCode.Contains("RAD"))
                    {
                        listQuery.Add(new SqlTransactionQueryList() { Query = "sp_MixMaterialScaledInsertColor @MaterialCode , @WeightMaxScaled , @CreatedBy , @MaterialName , @WeightMacEdited", Parametter = new object[] { item.MaterialCode, item.Total, GlobalVariable.userId, item.MaterialName, item.ActualUsage } });
                    }
                }

                //insert status order to tblOrderLog
                listQuery.Add(new SqlTransactionQueryList() { Query = "sp_OrderBookLogInsert @OrderId , @Status , @CreatedBy", Parametter = new object[] { orderInfo.OrderId, "2", GlobalVariable.userId.ToString() } });

                //get OrderLogId flow OrderId--> update status field in tblMixing table
                listQuery.Add(new SqlTransactionQueryList() { Query = "sp_OrderBookUpdateOrderStatusMix @OrderId", Parametter = new object[] { orderInfo.OrderId } });

            }
            else if (status == 2)//lưu cân nhựa
            {
                //listQuery.Add(new SqlTransactionQueryList() { Query = "sp_MixedInsert @OrderId , @ShiftId , @OperatorId , @WeightMixTotal , @ReasonId , @Note , @CreatedBy , @WeightMaterialTotal , @WeightRecycledTotal", Parametter = new object[] { orderInfo.OrderId, lookUpShift.EditValue, lookUpOperator.EditValue, total, null, null, GlobalVariable.userId, totalMaterialConsumtion, totalRecycle } });

                foreach (var item in productMixList)
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

        //CustomSumaryCalculate Event GridView
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