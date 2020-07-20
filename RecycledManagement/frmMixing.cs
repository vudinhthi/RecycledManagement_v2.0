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
using System.Net.Mail;

namespace RecycledManagement
{
    public partial class frmMixing : DevExpress.XtraEditors.XtraForm
    {
        int status = 0;//xet xem trạng thái của đơn hàng đang ở công đoạn nào để chọn cân cho phù hợp
        double totalMaterialConsumtion = 0, totalRecycle = 0, total = 0;
        double weightRecycle1 = 0, weightRecycle2 = 0, weightCompound = 0, weightClearRecycle = 0, weightFramapur = 0, weightLeftover = 0;

        double rangeColor = 0, rangePlastic = 0;

        MailHelper mailHelper;

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

            lookUpRecycle1.Properties.DataSource = DbMixCode.Instance.GetCrushingCode("0");
            lookUpRecycle1.Properties.ValueMember = "CrushId";
            lookUpRecycle1.Properties.DisplayMember = "CrushedCode";

            lookUpRecycle2.Properties.DataSource = DbMixCode.Instance.GetCrushingCode("0");
            lookUpRecycle2.Properties.ValueMember = "CrushId";
            lookUpRecycle2.Properties.DisplayMember = "CrushedCode";

            lookUpCompound.Properties.DataSource = DbMixCode.Instance.GetCrushingCode("1");
            lookUpCompound.Properties.ValueMember = "CrushId";
            lookUpCompound.Properties.DisplayMember = "CrushedCode";

            lookUpClearRecycle.Properties.DataSource = DbMixCode.Instance.GetCrushingCode("1");
            lookUpClearRecycle.Properties.ValueMember = "CrushId";
            lookUpClearRecycle.Properties.DisplayMember = "CrushedCode";

            lookUpFramapur.Properties.DataSource = DbMixCode.Instance.GetCrushingCode("1");
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

            //color scales
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
                txtTotalRecycleWeight.Enabled = false;

                lookUpReason.Enabled = false;
                checkBoxUsingRecycle.Enabled = false;

                grcMaterialConsumption.DataSource = productMixList;//hiển thị danh sách Product lên gridView Material Consumption
            }
            //plastic scales
            else if (status == 2)//--> Cân nhựa va Recycle
            {
                GlobalVariable.selectScale = "ScalePlastic";//set chon can nhựa vì trạng thái order đang ở cân recycle

                lookUpShift.Text = orderInfo.MixShiftName;
                lookUpOperator.Text = orderInfo.MixOperatorName;
                txtMixNote.Text = orderInfo.MixNote;

                lookUpShift.ReadOnly = true;
                lookUpOperator.ReadOnly = true;

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
                txtTotalRecycleWeight.Enabled = false;

                lookUpReason.Enabled = true;
                checkBoxUsingRecycle.Enabled = true;

                #region get material scales color
                DataTable dataMaterialScales = DbMixCode.Instance.GetMaterialCsalesColor(orderInfo.MixId);//get color material trong tblMixMaterialScale theo MixId 

                if (dataMaterialScales != null && dataMaterialScales.Rows.Count > 0)
                {
                    foreach (var item1 in productMixList)
                    {
                        if (dataMaterialScales.Select($"MaterialCode='{item1.MaterialCode}'").Count() > 0)
                        {
                            item1.ActualUsage = dataMaterialScales.Select($"MaterialCode='{item1.MaterialCode}'")[0][7].ToString();
                        }

                    }
                }
                #endregion

                grcMaterialConsumption.DataSource = productMixList;//hiển thị danh sách Product lên gridView Material Consumption

            }
            else if (status == 3)//Finish
            {
                btnSave.Enabled = false;
                lookUpShift.Text = orderInfo.MixShiftName;
                lookUpOperator.Text = orderInfo.MixOperatorName;
                txtMixNote.Text = orderInfo.MixNote;

                lookUpShift.ReadOnly = true;
                lookUpOperator.ReadOnly = true;
                txtMixNote.ReadOnly = true;

                GlobalVariable.selectScale = "";//set chon can nhựa vì trạng thái order đang ở cân recycle

                //grcMaterialConsumption.Enabled = false;
                //grvMaterialConsumption.OptionsBehavior.ReadOnly = true;               



                #region get material scales
                DataTable dataMaterialScales = DbMixCode.Instance.GetMaterialCsalesGetMixId(orderInfo.MixId);
                if (dataMaterialScales != null && dataMaterialScales.Rows.Count > 0)
                {
                    foreach (var item1 in productMixList)
                    {
                        item1.ActualUsage = dataMaterialScales.Select($"MaterialCode='{item1.MaterialCode}'")[0][7].ToString();
                    }
                }
                #endregion

                #region get Recycle scales
                List<MixRecycleScaledModel> dataRecycleScales = DbMixCode.Instance.GetRecycledCsalesGetMixId(orderInfo.MixId);
                if (dataRecycleScales != null && dataRecycleScales.Count > 0)//using recycle
                {
                    foreach (var item in dataRecycleScales)
                    {
                        if (item.RecycleName == "Recycle 1 Id")
                        {
                            weightRecycle1 = Convert.ToDouble(item.WeightReScaled);
                            txtWeightRecycle1.Text = weightRecycle1.ToString();

                            lookUpRecycle1.Text = item.RecycleCode;
                        }
                        else if (item.RecycleName == "Recycle 2 Id")
                        {
                            weightRecycle2 = Convert.ToDouble(item.WeightReScaled);
                            txtWeightRecycle2.Text = weightRecycle2.ToString();

                            lookUpRecycle2.Text = item.RecycleCode;
                        }
                        else if (item.RecycleName == "Compound Id")
                        {
                            weightCompound = Convert.ToDouble(item.WeightReScaled);
                            txtWeightCompound.Text = weightCompound.ToString();

                            lookUpCompound.Text = item.RecycleCode;
                        }
                        else if (item.RecycleName == "Clear Recycle Id")
                        {
                            weightClearRecycle = Convert.ToDouble(item.WeightReScaled);
                            txtWeightClearRecycle.Text = weightClearRecycle.ToString();

                            lookUpClearRecycle.Text = item.RecycleCode;
                        }
                        else if (item.RecycleName == "Framapur Id")
                        {
                            weightFramapur = Convert.ToDouble(item.WeightReScaled);
                            txtWeightFramapur.Text = weightFramapur.ToString();

                            lookUpFramapur.Text = item.RecycleCode;
                        }
                        else if (item.RecycleName == "Leftover Id")
                        {
                            weightLeftover = Convert.ToDouble(item.WeightReScaled);
                            txtWeightLeftover.Text = weightLeftover.ToString();

                            lookUpLeftover.Text = item.RecycleCode;
                        }
                    }
                    checkBoxUsingRecycle.Checked = true;
                    lookUpReason.Enabled = false;
                }
                else//don't using recycle
                {
                    checkBoxUsingRecycle.Checked = false;
                    lookUpReason.Text = DbReasons.Instance.GetReasonId(orderInfo.ReasonId).Rows[0][1].ToString();

                    lookUpReason.ReadOnly = true;

                    txtWeightRecycle1.Enabled = false;
                    txtWeightRecycle2.Enabled = false;
                    txtWeightCompound.Enabled = false;
                    txtWeightClearRecycle.Enabled = false;
                    txtWeightFramapur.Enabled = false;
                    txtWeightLeftover.Enabled = false;
                    txtTotalRecycleWeight.Enabled = false;

                    lookUpRecycle1.Enabled = false;
                    lookUpRecycle2.Enabled = false;
                    lookUpCompound.Enabled = false;
                    lookUpClearRecycle.Enabled = false;
                    lookUpFramapur.Enabled = false;
                    lookUpLeftover.Enabled = false;
                }
                checkBoxUsingRecycle.Enabled = false;
                #endregion

                grcMaterialConsumption.DataSource = productMixList;//hiển thị danh sách Product lên gridView Material Consumption
            }
            #endregion

            #endregion

            #region doc gia tri sai so cho phep
            DataTable rangeTable = DbMixCode.Instance.GetMaterialRange();
            if (rangeTable != null && rangeTable.Rows.Count > 0)
            {
                rangePlastic = Convert.ToDouble(rangeTable.Rows[0][2].ToString());
                rangeColor = Convert.ToDouble(rangeTable.Rows[1][2].ToString());
            }
            #endregion


            #region đang ký sự kiện scaleValueChanged
            GlobalVariable.myEvent.ScaleValueChanged += (s, o) =>
            {
                string materialCodeSub;
                double maxWeight = 0;
                Debug.WriteLine($"Mixing event write: {o.ScaleValue} | Allow Focus: {grvControlTextEdit.AllowFocused} | Appearance Focus: ");

                if (status == 1)
                {
                    try

                    {
                        string materialCode = grvMaterialConsumption.GetRowCellValue(grvMaterialConsumption.FocusedRowHandle, "MaterialCode").ToString();
                        materialCodeSub = materialCode.Substring(0, 3);
                        maxWeight = Convert.ToDouble(grvMaterialConsumption.GetRowCellValue(grvMaterialConsumption.FocusedRowHandle, "Total").ToString());
                        if (status == 1 && (materialCodeSub == "RCP" || materialCodeSub == "RMB" || materialCodeSub == "REX" || materialCodeSub == "RAD"))//can mau
                        {
                            grvMaterialConsumption.SetFocusedRowCellValue("ActualUsage", o.ScaleValue);
                            if (o.ScaleValue > (maxWeight + rangeColor))
                            {
                                mailHelper.Subject = "Recycled Management System - Scale color material over standar weight";
                                mailHelper.Body = $"Material code '{materialCode}'{Environment.NewLine}Max weight = {maxWeight}{Environment.NewLine}Range weight = {rangeColor}{Environment.NewLine}Actual usage = {o.ScaleValue}{Environment.NewLine}Over wieght = {o.ScaleValue - maxWeight}";

                                Task<bool> task = new Task<bool>(() => mailHelper.SendEmail());
                                task.Start();
                                task.ContinueWith(t => XtraMessageBox.Show(t.Result.ToString()));
                            }
                            grvMaterialConsumption.FocusedRowHandle += 1;
                        }
                    }
                    catch { }
                }
                else if (status == 2)//can Plastic & Recycle
                {
                    //scale recycle
                    if (txtWeightRecycle1.ContainsFocus || txtWeightRecycle2.ContainsFocus || txtWeightCompound.ContainsFocus || txtWeightClearRecycle.ContainsFocus || txtWeightFramapur.ContainsFocus || txtWeightLeftover.ContainsFocus)
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
                    else//scale plastic 
                    {
                        materialCodeSub = grvMaterialConsumption.GetRowCellValue(grvMaterialConsumption.FocusedRowHandle, "MaterialCode").ToString().Substring(0, 3);

                        if (status == 2 && (materialCodeSub == "RPM" || materialCodeSub == "RCM" || materialCodeSub == "RRE" || materialCodeSub == "RMX"))//can nhua
                        {
                            grvMaterialConsumption.SetFocusedRowCellValue("ActualUsage", o.ScaleValue - GlobalVariable.boxWeightMixingMaterial);
                            grvMaterialConsumption.FocusedRowHandle += 1;
                        }
                    }
                }
            };
            #endregion

            #region khởi tạo đối tượng sendEmail
            mailHelper = new MailHelper()
            {
                FromMailAddress = new MailAddress("fvn-itsupport@framas.com"),
                Host = "smtp.office365.com",
                Password = "fvnIT23",
                Port = "587",

                ToMailAddress = "cong.nguyen@framas.com",
                CCMailAddress = "sang.nguyen@framas.com",
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnalbleSsl = true,
                isBodyHtml = false,
                UseDefaultCredentials = false,
                Subject = "Test",
                Body = "This is email to test"
            };
            #endregion

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
            if (status != 3)
            {
                if (checkBoxUsingRecycle.Checked == true)
                {
                    lookUpRecycle1.Enabled = true;
                    lookUpRecycle2.Enabled = true;
                    lookUpCompound.Enabled = true;
                    lookUpClearRecycle.Enabled = true;
                    lookUpFramapur.Enabled = true;
                    lookUpLeftover.Enabled = true;

                    txtTotalRecycleWeight.Enabled = true;

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

                    txtTotalRecycleWeight.Enabled = false;

                    lookUpReason.Enabled = true;
                }
            }
            else if (status == 3 && checkBoxUsingRecycle.Checked == true)
            {
                lookUpRecycle1.ReadOnly = true;
                lookUpRecycle2.ReadOnly = true;
                lookUpCompound.ReadOnly = true;
                lookUpClearRecycle.ReadOnly = true;
                lookUpFramapur.ReadOnly = true;
                lookUpLeftover.ReadOnly = true;

                txtWeightRecycle1.ReadOnly = true;
                txtWeightRecycle2.ReadOnly = true;
                txtWeightCompound.ReadOnly = true;
                txtWeightClearRecycle.ReadOnly = true;
                txtWeightFramapur.ReadOnly = true;
                txtWeightLeftover.ReadOnly = true;

                txtTotalRecycleWeight.ReadOnly = true;
            }
        }

        //Cteated Mix
        private void btnSave_Click(object sender, EventArgs e)
        {
            List<SqlTransactionQueryList> listQuery = new List<SqlTransactionQueryList>();

            if (status == 1)//lưu cân màu
            {
                //insert New Mix to tblMixed table
                listQuery.Add(new SqlTransactionQueryList()
                {
                    Query = "sp_MixedInsert @MixCode , @OrderId , @ShiftId , @OperatorId , @WeightMixTotal , @ReasonId , @Note , @CreatedBy , @WeightMaterialTotal , @WeightRecycledTotal",
                    Parametter = new object[] { $"MI|{DateTime.Now.ToString("yyyyyMMdd")}|{orderInfo.ItemCode}|", orderInfo.OrderId, lookUpShift.EditValue, lookUpOperator.EditValue, total, null, txtMixNote.Text, GlobalVariable.userId, totalMaterialConsumtion, totalRecycle }
                });

                //insert product to tblMaterialScale
                foreach (var item in productMixList)
                {
                    if (item.MaterialCode.Contains("RCP") || item.MaterialCode.Contains("RMB") || item.MaterialCode.Contains("REX") || item.MaterialCode.Contains("RAD"))
                    {
                        listQuery.Add(new SqlTransactionQueryList()
                        {
                            Query = "sp_MixMaterialScaledInsertColor @MaterialCode , @WeightMaxScaled , @CreatedBy , @MaterialName , @WeightMacEdited",
                            Parametter = new object[] { item.MaterialCode, item.Total, GlobalVariable.userId, item.MaterialName, item.ActualUsage }
                        });
                    }
                }

                //insert status order to tblOrderLog
                listQuery.Add(new SqlTransactionQueryList()
                {
                    Query = "sp_OrderBookLogInsert @OrderId , @Status , @CreatedBy",
                    Parametter = new object[] { orderInfo.OrderId, "2", GlobalVariable.userId.ToString() }
                });

                //get OrderLogId flow OrderId--> update status field in tblMixing table
                listQuery.Add(new SqlTransactionQueryList()
                {
                    Query = "sp_OrderBookUpdateOrderStatusMix @OrderId",
                    Parametter = new object[] { orderInfo.OrderId }
                });
            }
            else if (status == 2)//lưu cân nhựa va can recycle
            {
                if (checkBoxUsingRecycle.Checked)//có dùng recycle
                {
                    //Update to tblMixed table
                    listQuery.Add(new SqlTransactionQueryList()
                    {
                        Query = "sp_MixedUpdate @WeightMixTotal , @ReasonId , @Note , @CreatedBy , @WeightMaterialTotal , @WeightRecycledTotal , @MixId",
                        Parametter = new object[] { total, null, txtMixNote.Text, GlobalVariable.userId, totalMaterialConsumtion, totalRecycle, orderInfo.MixId }
                    });

                    #region insert recycleScales
                    //Recycle 1
                    if (weightRecycle1 > 0)
                    {
                        listQuery.Add(new SqlTransactionQueryList()
                        {
                            Query = "sp_MixRecycledScaledInsert @WeightReScaled , @MixId , @CreatedBy , @RecycledCode , @RecycledName",
                            Parametter = new object[] { weightRecycle1.ToString(), orderInfo.MixId, GlobalVariable.userId.ToString(), lookUpRecycle1.GetColumnValue("CrushedCode"), "Recycle 1 Id" }
                        });
                    }

                    //Recycle 2
                    if (weightRecycle2 > 0)
                    {
                        listQuery.Add(new SqlTransactionQueryList()
                        {
                            Query = "sp_MixRecycledScaledInsert @WeightReScaled , @MixId , @CreatedBy , @RecycledCode , @RecycledName",
                            Parametter = new object[] { weightRecycle2.ToString(), orderInfo.MixId, GlobalVariable.userId.ToString(), lookUpRecycle2.GetColumnValue("CrushedCode"), "Recycle 2 Id" }
                        });
                    }

                    //Compound
                    if (weightCompound > 0)
                    {
                        listQuery.Add(new SqlTransactionQueryList()
                        {
                            Query = "sp_MixRecycledScaledInsert @WeightReScaled , @MixId , @CreatedBy , @RecycledCode , @RecycledName",
                            Parametter = new object[] { weightCompound.ToString(), orderInfo.MixId, GlobalVariable.userId.ToString(), lookUpCompound.GetColumnValue("CrushedCode"), "Compound Id" }
                        });
                    }

                    //Clear Recycle
                    if (weightClearRecycle > 0)
                    {
                        listQuery.Add(new SqlTransactionQueryList()
                        {
                            Query = "sp_MixRecycledScaledInsert @WeightReScaled , @MixId , @CreatedBy , @RecycledCode , @RecycledName",
                            Parametter = new object[] { weightClearRecycle.ToString(), orderInfo.MixId, GlobalVariable.userId.ToString(), lookUpClearRecycle.GetColumnValue("CrushedCode"), "Clear Recycle Id" }
                        });
                    }

                    //Framapur
                    if (weightFramapur > 0)
                    {
                        listQuery.Add(new SqlTransactionQueryList()
                        {
                            Query = "sp_MixRecycledScaledInsert @WeightReScaled , @MixId , @CreatedBy , @RecycledCode , @RecycledName",
                            Parametter = new object[] { weightFramapur.ToString(), orderInfo.MixId, GlobalVariable.userId.ToString(), lookUpFramapur.GetColumnValue("CrushedCode"), "Framapur Id" }
                        });
                    }

                    //leftOver
                    if (weightLeftover > 0)
                    {
                        listQuery.Add(new SqlTransactionQueryList()
                        {
                            Query = "sp_MixRecycledScaledInsert @WeightReScaled , @MixId , @CreatedBy , @RecycledCode , @RecycledName",
                            Parametter = new object[] { weightLeftover.ToString(), orderInfo.MixId, GlobalVariable.userId.ToString(), lookUpLeftover.GetColumnValue("IncomingCode"), "Leftover Id" }
                        });
                    }
                    #endregion
                }
                else//không dùng recycle
                {
                    //Update to tblMixed table
                    listQuery.Add(new SqlTransactionQueryList()
                    {
                        Query = "sp_MixedUpdate @WeightMixTotal , @ReasonId , @Note , @CreatedBy , @WeightMaterialTotal , @WeightRecycledTotal , @MixId",
                        Parametter = new object[] { total, lookUpReason.EditValue.ToString(), txtMixNote.Text, GlobalVariable.userId, totalMaterialConsumtion, totalRecycle, orderInfo.MixId }
                    });
                }

                #region color scales
                foreach (var item in productMixList)
                {
                    if (item.MaterialCode.Contains("RPM") || item.MaterialCode.Contains("RCM") || item.MaterialCode.Contains("RRE") || item.MaterialCode.Contains("RMX"))
                    {
                        listQuery.Add(new SqlTransactionQueryList()
                        {
                            Query = "sp_MixMaterialScaledInsertPlastic @MaterialCode , @WeightMaxScaled , @MixId , @CreatedBy , @MaterialName , @WeightMacEdited",
                            Parametter = new object[] { item.MaterialCode, item.Total, orderInfo.MixId, GlobalVariable.userId, item.MaterialName, item.ActualUsage }
                        });
                    }
                }
                #endregion

                //insert status order to tblOrderLog
                listQuery.Add(new SqlTransactionQueryList()
                {
                    Query = "sp_OrderBookLogInsert @OrderId , @Status , @CreatedBy",
                    Parametter = new object[] { orderInfo.OrderId, "3", GlobalVariable.userId.ToString() }
                });

                //get OrderLogId flow OrderId--> update status field in tblMixing table
                listQuery.Add(new SqlTransactionQueryList()
                {
                    Query = "sp_OrderBookUpdateOrderStatusMix @OrderId",
                    Parametter = new object[] { orderInfo.OrderId }
                });
            }
            else if (status == 3)//lưu cân recycle
            {
                //Update to tblMixed table
                if (checkBoxUsingRecycle.Checked == true)
                {
                    listQuery.Add(new SqlTransactionQueryList()
                    {
                        Query = "sp_MixedUpdate @WeightMixTotal , @ReasonId , @Note , @CreatedBy , @WeightMaterialTotal , @WeightRecycledTotal , @MixId",
                        Parametter = new object[] { total, null, txtMixNote.Text, GlobalVariable.userId, totalMaterialConsumtion, totalRecycle, orderInfo.MixId }
                    });

                    #region insert recycleScales
                    //Recycle 1
                    if (weightRecycle1 > 0)
                    {
                        listQuery.Add(new SqlTransactionQueryList()
                        {
                            Query = "sp_MixRecycledScaledInsert @WeightReScaled , @MixId , @CreatedBy , @RecycledCode , @RecycledName",
                            Parametter = new object[] { weightRecycle1.ToString(), orderInfo.MixId, GlobalVariable.userId.ToString(), lookUpRecycle1.GetColumnValue("CrushedCode"), "Recycle 1 Id" }
                        });
                    }

                    //Recycle 2
                    if (weightRecycle2 > 0)
                    {
                        listQuery.Add(new SqlTransactionQueryList()
                        {
                            Query = "sp_MixRecycledScaledInsert @WeightReScaled , @MixId , @CreatedBy , @RecycledCode , @RecycledName",
                            Parametter = new object[] { weightRecycle2.ToString(), orderInfo.MixId, GlobalVariable.userId.ToString(), lookUpRecycle2.GetColumnValue("CrushedCode"), "Recycle 2 Id" }
                        });
                    }

                    //Compound
                    if (weightCompound > 0)
                    {
                        listQuery.Add(new SqlTransactionQueryList()
                        {
                            Query = "sp_MixRecycledScaledInsert @WeightReScaled , @MixId , @CreatedBy , @RecycledCode , @RecycledName",
                            Parametter = new object[] { weightCompound.ToString(), orderInfo.MixId, GlobalVariable.userId.ToString(), lookUpCompound.GetColumnValue("CrushedCode"), "Compound Id" }
                        });
                    }

                    //Clear Recycle
                    if (weightClearRecycle > 0)
                    {
                        listQuery.Add(new SqlTransactionQueryList()
                        {
                            Query = "sp_MixRecycledScaledInsert @WeightReScaled , @MixId , @CreatedBy , @RecycledCode , @RecycledName",
                            Parametter = new object[] { weightClearRecycle.ToString(), orderInfo.MixId, GlobalVariable.userId.ToString(), lookUpClearRecycle.GetColumnValue("CrushedCode"), "Clear Recycle Id" }
                        });
                    }

                    //Framapur
                    if (weightFramapur > 0)
                    {
                        listQuery.Add(new SqlTransactionQueryList()
                        {
                            Query = "sp_MixRecycledScaledInsert @WeightReScaled , @MixId , @CreatedBy , @RecycledCode , @RecycledName",
                            Parametter = new object[] { weightFramapur.ToString(), orderInfo.MixId, GlobalVariable.userId.ToString(), lookUpFramapur.GetColumnValue("CrushedCode"), "Framapur Id" }
                        });
                    }

                    //leftOver
                    if (weightLeftover > 0)
                    {
                        listQuery.Add(new SqlTransactionQueryList()
                        {
                            Query = "sp_MixRecycledScaledInsert @WeightReScaled , @MixId , @CreatedBy , @RecycledCode , @RecycledName",
                            Parametter = new object[] { weightLeftover.ToString(), orderInfo.MixId, GlobalVariable.userId.ToString(), lookUpLeftover.GetColumnValue("IncomingCode"), "Leftover Id" }
                        });
                    }
                    #endregion
                }
                else
                {
                    listQuery.Add(new SqlTransactionQueryList()
                    {
                        Query = "sp_MixedUpdate @WeightMixTotal , @ReasonId , @Note , @CreatedBy , @WeightMaterialTotal , @WeightRecycledTotal , @MixId",
                        Parametter = new object[] { total, lookUpReason.EditValue.ToString(), txtMixNote.Text, GlobalVariable.userId, totalMaterialConsumtion, totalRecycle, orderInfo.MixId }
                    });
                }


                //insert status order to tblOrderLog
                listQuery.Add(new SqlTransactionQueryList()
                {
                    Query = "sp_OrderBookLogInsert @OrderId , @Status , @CreatedBy",
                    Parametter = new object[] { orderInfo.OrderId, "4", GlobalVariable.userId.ToString() }
                });

                //get OrderLogId flow OrderId--> update status field in tblMixing table
                listQuery.Add(new SqlTransactionQueryList()
                {
                    Query = "sp_OrderBookUpdateOrderStatusMix @OrderId",
                    Parametter = new object[] { orderInfo.OrderId }
                });
            }

            //goi method truy van DB
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
            else if (status == 3)//Process--> Cân Recycle
            {
                GlobalVariable.selectScale = "ScalePlastic";
                e.Cancel = true;
            }
            else if (status == 4)//Finish
            {
                e.Cancel = true;
            }
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