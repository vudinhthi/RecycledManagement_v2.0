using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using RecycledManagement.Common;
using System.Diagnostics;
using RecycledManagement.Models;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data;
using System.Threading;
using DevExpress.XtraReports.Native;

namespace RecycledManagement
{
    public partial class userControlBookingOrder : EditFormUserControl
    {
        #region khai báo các biến dùng cho EditForm
        double weight = 0, orderAmount = 0, shotWeight = 0;
        double maxOrderSize = 0;
        int numOfOrderSize = 0, numOfPairs = 0, orderId = 0;
        string orderType = null;//1: normal -- 2: Urgent
        string orderStatus = null;
        string orderCode = "", itemCode = "", itemName = "", colorCode = "", colorName = "";


        BookingOrderSizeModel orderSizeModel;//model de luu cac thong tin add vao danh sach Order

        List<BookingOrderSizeModel> orderSizeList = new List<BookingOrderSizeModel>();

        #region khai bao bien de dung cho viec tắt editForm chủ động
        //public XtraForm OwnerForm { get; set; }//dung cho popupEdiForm

        public GridView view;//dung cho EditFormInplace
        #endregion

        #endregion

        //Contructor
        public userControlBookingOrder()
        {
            InitializeComponent();

            this.SetBoundFieldName(this.lookUpShift, "ShiftId");
            this.SetBoundPropertyName(this.lookUpShift, "EditValue");

            this.SetBoundFieldName(this.lookUpTeamLeader, "OperatorId");
            this.SetBoundPropertyName(this.lookUpTeamLeader, "EditValue");

            // this.SetBoundFieldName(this.lookUpItemCode, "ItemCode");
            //this.SetBoundPropertyName(this.lookUpItemCode, "Text");
            this.SetBoundFieldName(this.lookUpItemName, "ItemCode");
            this.SetBoundPropertyName(this.lookUpItemName, "EditValue");

            //this.SetBoundFieldName(this.radBookOrder, "OrderType");
            //this.SetBoundPropertyName(this.radBookOrder, "EditValue");

            this.SetBoundFieldName(this.dateEditBook, "FinishDate");
            this.SetBoundFieldName(this.txtMachine, "Machine");
            this.SetBoundFieldName(this.txtNote, "Note");

        }


        //sự kiện UserLoad
        private void userControlBookingOrder_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;

            if (!string.IsNullOrEmpty(spinEditBook.Text))
            {
                numOfPairs = int.Parse(spinEditBook.Text);//gan pairs vao bien
            }

            // btnSaveOrder.Enabled = GlobalVariable.newOrUpdateOrderBook;

            #region add item for radio
            RadioGroupItem radioItem;
            radioItem = new RadioGroupItem(0, "Normal Order");
            radBookOrder.Properties.Items.Add(radioItem);
            radioItem = new RadioGroupItem(1, "Urgent Order");
            radBookOrder.Properties.Items.Add(radioItem);
            radBookOrder.SelectedIndex = 0;//chọn mặc định hiển thị radio ban đầu
            radBookOrder.BorderStyle = BorderStyles.Style3D;
            this.SetBoundFieldName(this.radBookOrder, "OrderType");
            this.SetBoundPropertyName(this.radBookOrder, "EditValue");
            //radBookOrder.BackColor = Color.Green;
            #endregion

            #region get shifts
            lookUpShift.Properties.DataSource = DbShift.Instance.GetShiftComming();
            lookUpShift.Properties.ValueMember = "ShiftId";
            lookUpShift.Properties.DisplayMember = "ShiftName";
            lookUpShift.ItemIndex = 0;
            #endregion

            #region get ItemName
            lookUpItemName.Properties.DataSource = lookUpItemCode.Properties.DataSource = DbBookingOrder.Instance.GetItemName();
            lookUpItemName.Properties.ValueMember = "ItemCode";
            lookUpItemName.Properties.DisplayMember = "ItemName";
            //lookUpItemName.ReadOnly = true;
            lookUpItemCode.Properties.ValueMember = "ItemCode";
            lookUpItemCode.Properties.DisplayMember = "ItemCode";
            lookUpItemName.EditValue = lookUpItemCode.EditValue;
            //lookUpItemCode.Properties.DataSource = DbBookingOrder.Instance.GetItemName();
            //lookUpItemCode.Properties.ValueMember = "ItemName";
            //lookUpItemCode.Properties.DisplayMember = "ItemCode";
            lookUpItemCode.EditValueChanged += (s, o) =>
            {
                LookUpEdit lkeItemCode = s as LookUpEdit;
                lookUpItemName.EditValue = lkeItemCode.EditValue;
            };
            #region Sự kiện thay đổi focusrowhandle của gridControl Order
            grvOrder.FocusedRowChanged += (s, o) =>
            {
                GridView view = s as GridView;
                lookUpSize.ItemIndex = o.FocusedRowHandle;
            };
            #endregion
            #endregion

            #region get TeamLeader
            lookUpTeamLeader.Properties.DataSource = DbBookingOrder.Instance.GetTeamLeader(3);
            lookUpTeamLeader.Properties.ValueMember = "OperatorId";
            lookUpTeamLeader.Properties.DisplayMember = "OperatorName";
            lookUpTeamLeader.ItemIndex = 0;
            #endregion
            //dateEditBook.Text = DateTime.Now.ToString();
            #region check nếu ko có quyền nhập thì disable các control đi
            if (GlobalVariable.importOrder == false)
            {
                lookUpShift.ReadOnly = true;
                lookUpTeamLeader.ReadOnly = true;
                lookUpItemName.ReadOnly = true;
                lookUpSize.ReadOnly = true;
                txtMachine.ReadOnly = true;
                txtNote.ReadOnly = true;
                dateEditBook.ReadOnly = true;
                spinEditBook.ReadOnly = true;
                labMaxOrderSize.Enabled = false;
                radBookOrder.ReadOnly = true;
                grvOrder.OptionsBehavior.ReadOnly = true;

                btnAdd.Enabled = false;
                btnSaveOrder.Enabled = false;
                lookUpSize.EditValue = null;
                grcOrder.DataSource = null;
            }
            #endregion
        }

        //button delete trong GrifView
        private void repositoryItemBtnDelete_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (GlobalVariable.newOrUpdateOrderBook)
            {
                //Debug.WriteLine($"RowIndex={grvOrder.FocusedRowHandle} |Delete row:{grvOrder.GetRowCellValue(grvOrder.FocusedRowHandle, "Size").ToString()}");

                orderSizeList.RemoveAt(grvOrder.FocusedRowHandle);//xóa dữ liệu trong list

                //refesh lại GridView
                grcOrder.DataSource = null;
                grcOrder.DataSource = orderSizeList;
                //Kiểm tra nếu không còn Size nào thì mở lại ookup ItemCode cho User chọn 
                if (grvOrder.RowCount==0)
                {
                    lookUpItemCode.Enabled = true;
                }
            }
        }

        //tinh tong OrderAmount trong DridView
        private void grvOrder_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            GridView view = sender as GridView;
            // Get the summary ID. 
            int summaryID = Convert.ToInt32((e.Item as GridSummaryItem).Tag);

            // Initialization. 
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                //discontinuedProductsCount = 0;
                if (summaryID == 1 && GlobalVariable.newOrUpdateOrderBook == true)
                {
                    orderAmount = 0;
                }
            }

            // Calculation.
            if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                switch (summaryID)
                {
                    case 1: // The total summary calculated against the 'UnitPrice' column. 
                        double unitsInStock = Convert.ToDouble(view.GetRowCellValue(e.RowHandle, "QtyKg"));
                        orderAmount += Math.Round(Convert.ToDouble(e.FieldValue), 3);
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
                        orderAmount = orderAmount + (1 * numOfOrderSize);
                        e.TotalValue = orderAmount;
                        break;
                        //case 2:
                        //    maxOrderSize = Math.Round(((25 - (1 * numOfOrderSize)) / shotWeight) - 20, 3);
                        //    e.TotalValue = maxOrderSize;
                        //    break;
                }
            }
        }

        //sự kiện chọn ỎderType
        private void radBookOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioGroup edit = sender as RadioGroup;
            Debug.WriteLine($"radCrush Type: {edit.SelectedIndex}");

            orderType = edit.SelectedIndex.ToString();//0-Normal     1-Urgent
        }

        //event LookupSize
        bool isAlllowAdd = false;
        private void lookUpSize_EditValueChanged(object sender, EventArgs e)
        {
            //if (lookUpSize.Text != "[EditValue is null]" && !string.IsNullOrEmpty(lookUpSize.Text))
            //{
            //    shotWeight = float.Parse(lookUpSize.GetColumnValue("ShotWeight").ToString());
            //}
            isAlllowAdd = true;
        }

        //event lookupItemName thay đổi
        private void lookUpItemName_EditValueChanged(object sender, EventArgs e)
        {
            if (GlobalVariable.newOrUpdateOrderBook == true)
            {
                //lookUpItemName.Properties.GetDisplayText(lookUpItemName.EditValue)--lấy DisplayMembers
                //Debug.WriteLine($"BookKing ItemEvent: DispalyMembers:{lookUpItemName.Text } | ValueMembers:{lookUpItemName.EditValue}|{lookUpItemName.GetColumnValue("ColorCode")}");
                if (lookUpItemName.Text != "[EditValue is null]" && !string.IsNullOrEmpty(lookUpItemName.Text))
                {
                    //lookUpItemCode.EditValue= lookUpItemName.GetColumnValue("ColorName").ToString();
                    itemCode = lookUpItemName.EditValue.ToString();
                    itemName = lookUpItemName.Text;
                    colorCode = lookUpItemName.GetColumnValue("ColorCode").ToString();
                    colorName = lookUpItemName.GetColumnValue("ColorName").ToString();
                    #region get Size theo itemName
                    var _data = DbBookingOrder.Instance.GetOrdersize(lookUpItemName.EditValue.ToString());
                    lookUpSize.Properties.DataSource = _data;
                   
                    //lay trung bình shotWeight va tinh maxOrderSize
                    if (_data != null && _data.Rows.Count > 0)
                    {
                        shotWeight = 0;
                        foreach (DataRow item in _data.Rows)
                        {
                            shotWeight = shotWeight + double.Parse(item["ShotWeight"].ToString());
                        }

                        shotWeight = (shotWeight / _data.Rows.Count) / 1000;//tính trung bình shotWeight
                        if (shotWeight>0)
                        {
                            maxOrderSize = Math.Round(((25 - 1) / shotWeight) - 20, 0);//làm tròn số nguyên
                        }
                        else
                        {
                            maxOrderSize = 0;
                        }
                    }
                    else
                    {
                        shotWeight = maxOrderSize = 0;
                    }
                    lookUpSize.Properties.ValueMember = "ItemCode";
                    lookUpSize.Properties.DisplayMember = "Size";
                    //lookUpSize.ItemIndex = 0;
                    #endregion
                }
                else
                {
                    itemCode = itemName = colorCode = colorName = null;
                }
            }
        }

        //save Order
        private void btnSaveOrder_Click(object sender, EventArgs e)
        {
           
            if (!string.IsNullOrEmpty(lookUpShift.Text) && !string.IsNullOrEmpty(lookUpTeamLeader.Text) && !string.IsNullOrEmpty(txtMachine.Text) && !string.IsNullOrEmpty(dateEditBook.Text)
                && !string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(itemName) && !string.IsNullOrEmpty(colorCode) && !string.IsNullOrEmpty(colorName))
            {
                if (grvOrder.RowCount == 0)
                {
                    XtraMessageBox.Show("Order size is empty!");
                }
                else
                {
                    //tạo OrderCode theo format: OR-itemName-yyyyMMddOrderId
                    orderId = DbBookingOrder.Instance.GetMaxIdOrderBook() + 1;
                    orderCode = $"OR|{DateTime.Now.ToString("yyyymmdd")}|{itemCode}|{orderId}";

                    if (DbBookingOrder.Instance.InsertOrderBook(orderCode, txtMachine.Text, itemCode, itemName, colorCode, colorName, orderAmount.ToString(), orderStatus, txtNote.Text, lookUpTeamLeader.GetColumnValue("OperatorId").ToString()
                        , orderType, (DateTime)dateEditBook.EditValue, lookUpShift.GetColumnValue("ShiftId").ToString(), GlobalVariable.userId.ToString()) == 1)
                    {
                        //insert vao bang orderBookLog
                        if (DbBookingOrder.Instance.InsertOrderBookLog(orderId.ToString(), "1", GlobalVariable.userId.ToString()) == 1)
                        {
                            //update lai orderStatus cua bang orderBook 
                            if (DbBookingOrder.Instance.UpdateOrderBook(DbBookingOrder.Instance.GetOrderBookLog1Line(orderId.ToString()).Rows[0][0].ToString(), orderId.ToString()) == 1)
                            {
                                //insert danh sach order vao bang tblOederBookSize
                                foreach (var item in orderSizeList)
                                {
                                    DbBookingOrder.Instance.InsertOrderBookSize(orderId.ToString(), item.SizeName, item.QtyPrs.ToString(), item.QtyKg.ToString(), GlobalVariable.userId.ToString());
                                }
                                //XtraMessageBox.Show($"Successful!");
                                //dong editform
                                //OwnerForm.Close();
                                view.CloseEditForm();//tăt editForm
                            }
                            else
                            {
                                XtraMessageBox.Show($"Insert Databases Error!");
                                //error, do somthing
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show($"Insert Databases Error!");
                            //error, do somthing
                        }


                    }
                    else
                    {
                        XtraMessageBox.Show($"Insert Databases Error!");
                        //error, do somthing
                    }
                    lookUpItemCode.Enabled = true;
                }
            }
            else
            {
                XtraMessageBox.Show($"Fill in the missing information, please enter again!");
            }
        }


        //su kien chon pairs
        private void spinEditBook_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(spinEditBook.Text))
            {
                numOfPairs = int.Parse(spinEditBook.Text);//gan pairs vao bien
            }
        }


        //add OrderList
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (orderAmount + weight >= 25)
            {
                XtraMessageBox.Show("Order Amount is greater than 25 Kg");
            }
            else
            {
                //kiểm tra size đã tồn tại hay chưa
                BookingOrderSizeModel ExistBookingOrderBook = orderSizeList.FirstOrDefault(ordersize => ordersize.SizeName == lookUpSize.Text);
                if (ExistBookingOrderBook != null)
                {
                    //XtraMessageBox.Show("Value has already existed");
                    //ExistBookingOrderBook.SizeName = lookUpSize.Text;
                    ExistBookingOrderBook.QtyPrs = numOfPairs;
                    ExistBookingOrderBook.QtyKg = weight;
                }
                else
                {
                    numOfOrderSize = orderSizeList.Count + 1;

                    //add Order vào list OrderSize
                    orderSizeModel = new BookingOrderSizeModel();
                    orderSizeModel.SizeName = lookUpSize.Text;
                    orderSizeModel.QtyPrs = numOfPairs;
                    orderSizeModel.QtyKg = weight;
                    orderSizeList.Add(orderSizeModel);
                }
                
            }
            lookUpItemCode.Enabled = false;
            //hiển thị lên GridControl
            grcOrder.DataSource = null;
            grcOrder.DataSource = orderSizeList;
            //đếm số lượng order trong list

        }

        //timer dùng để tính weight tự động khi chọn size và Qty(pairs)
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            if (lookUpSize.Text != "[EditValue is null]" && !string.IsNullOrEmpty(lookUpSize.Text))
            {
                weight = Math.Round(shotWeight * (numOfPairs + 20), 3);

                //2 thong so  dưới đc tính trong customCaculate cua gridView
                //orderAmount = weight + (1 * numOfOrderSize);

                //maxOrderSize = Math.Round(((25 - (1 * numOfOrderSize)) / shotWeight) - 20, 2);

                //Debug.WriteLine($"Order Info: Weight={weight}|Maximin Orer={maxOrderSize}|Order Amount={orderAmount}------Order List Count={numOfOrderSize}|NumOfPairs={numOfPairs}");
            }

            txtWeight.Invoke(new Action(() =>
            {
                txtWeight.Text = weight.ToString();
            }));

            labMaxOrderSize.Invoke(new Action(() =>
            {
                labMaxOrderSize.Text = maxOrderSize.ToString();
            }));

            #region xu ly khi add new hay xem order
            //add new order
            if (GlobalVariable.newOrUpdateOrderBook == true && GlobalVariable.enableFlagOrderBook == true & isAlllowAdd)
            {
                //neu có quyền nhập
                if (GlobalVariable.importOrder == true)
                {
                    lookUpShift.ReadOnly = false;
                    lookUpTeamLeader.ReadOnly = false;
                    lookUpItemName.ReadOnly = false;
                    lookUpSize.ReadOnly = false;
                    txtMachine.ReadOnly = false;
                    txtNote.ReadOnly = false;
                    dateEditBook.ReadOnly = false;
                    spinEditBook.ReadOnly = false;
                    labMaxOrderSize.Enabled = true;
                    radBookOrder.ReadOnly = false;
                    grvOrder.OptionsBehavior.ReadOnly = true;
                    btnAdd.Enabled = true;
                    btnSaveOrder.Enabled = true;
                    grcOrder.DataSource = null;
                    lookUpSize.EditValue = null;
                }

                spinEditBook.EditValue = 1;
                weight = 0;
                maxOrderSize = 0;
                numOfOrderSize = 0;
                orderAmount = 0;

                //remove het list
                orderSizeList.Clear();

                GlobalVariable.enableFlagOrderBook = false;
            }
            else if (GlobalVariable.newOrUpdateOrderBook == false && GlobalVariable.enableFlagOrderBook == false)
            {
                lookUpShift.ReadOnly = true;
                lookUpTeamLeader.ReadOnly = true;
                lookUpItemName.ReadOnly = true;
                lookUpSize.ReadOnly = true;
                txtMachine.ReadOnly = true;
                txtNote.ReadOnly = true;
                dateEditBook.ReadOnly = true;
                spinEditBook.ReadOnly = true;
                btnAdd.Enabled = false;
                labMaxOrderSize.Enabled = false;
                radBookOrder.ReadOnly = true;
                grvOrder.OptionsBehavior.ReadOnly = true;

                btnSaveOrder.Enabled = false;
                lookUpSize.EditValue = null;
                grcOrder.DataSource = null;

                spinEditBook.EditValue = 1;
                weight = 0;
                maxOrderSize = 0;
                orderAmount = 0;


                #region lấy danh sách Order theo OrderId
                DataTable _data = DbBookingOrder.Instance.GetOederBookSizeWhereOrderId(GlobalVariable.idSelect.ToString());
                if (_data != null && _data.Rows.Count > 0)
                {
                    List<BookingOrderSizeModel> listData = new List<BookingOrderSizeModel>();
                    foreach (DataRow row in _data.Rows)
                    {
                        var item = new BookingOrderSizeModel()
                        {
                            SizeName = row["SizeName"].ToString(),
                            QtyPrs = (int)row["QtyPrs"],
                            QtyKg = Convert.ToDouble(row["QtyKg"].ToString())
                        };
                        listData.Add(item);
                    }

                    //orderAmount = orderAmount + (1 * listData.Count);
                    numOfOrderSize = listData.Count;

                    grcOrder.Invoke(new Action(() =>
                    {
                        grcOrder.DataSource = listData;
                       // grvOrder.Columns["Note"].Visible = false;
                    }
                        ));
                }
                #endregion

                GlobalVariable.enableFlagOrderBook = true;
               
            }
            #endregion

            //if (GlobalVariable.importOrder)
            //{
            //    btnSaveOrder.Enabled = true;
            //}
            //else
            //{
            //    btnSaveOrder.Enabled = false;
            //}
            //btnAdd.Enabled = false;
           
            timer1.Enabled = true;

        }
    }
}
