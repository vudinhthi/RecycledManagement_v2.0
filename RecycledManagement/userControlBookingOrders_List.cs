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
using DevExpress.XtraGrid.Views.Grid;
using RecycledManagement.Common;
using System.Diagnostics;
using DevExpress.Utils.Filtering.Internal;

namespace RecycledManagement
{
    public partial class userControlBookingOrders_List : DevExpress.XtraEditors.XtraUserControl
    {
        public userControlBookingOrders_List()
        {
            InitializeComponent();

        }

        private void userControlBookingOrders_List_Load(object sender, EventArgs e)
        {
            grvBookingOrder.OptionsBehavior.EditingMode = GridEditingMode.EditFormInplace;
            //grvBookingOrder.OptionsEditForm.CustomEditFormLayout = new userControlBookingOrder();
            grvBookingOrder.OptionsEditForm.CustomEditFormLayout = new userControlBookingOrder() { view = grvBookingOrder };

            grvBookingOrder.OptionsView.NewItemRowPosition = NewItemRowPosition.Top; // Available modes: Top, Bottom, None

            grvBookingOrder.OptionsEditForm.ShowUpdateCancelPanel = DevExpress.Utils.DefaultBoolean.False;//Disabale button Update Cancel EditForm

            #region su kien khi dong EditForm
            grvBookingOrder.EditFormHidden += (s, o) =>
            {
                grcBookingOrder.RefreshDataSource();

                //fill dada into dataGridView from dataTable
                grcBookingOrder.DataSource = DbBookingOrder.Instance.GetAll();

                //an cot gridView
                //grvBookingOrder.Columns["CrushId"].Visible = false;
                //grvBookingOrder.Columns["ShiftId"].Visible = false;
                //grvBookingOrder.Columns["OperatorId"].Visible = false;
                //grvBookingOrder.Columns["MixId"].Visible = false;
                //grvBookingOrder.Columns["CreatedBy"].Visible = false;
                //grvBookingOrder.Columns["CrushedType"].Visible = false;

                //if (o.Result == EditFormResult.Update)
                //{

                //}
            };
            #endregion

            grcBookingOrder.DataSource = DbBookingOrder.Instance.GetAll();

            //an cot gridView
            //grvBookingOrder.Columns["CrushId"].Visible = false;
            //grvBookingOrder.Columns["ShiftId"].Visible = false;
            //grvBookingOrder.Columns["OperatorId"].Visible = false;
            //grvBookingOrder.Columns["MixId"].Visible = false;
            //grvBookingOrder.Columns["CreatedBy"].Visible = false;
            //grvBookingOrder.Columns["CrushedType"].Visible = false;
            //grvBookingOrder.PopulateColumns();

            grvBookingOrder.Columns["Id"].Width = 10;
            grvBookingOrder.Columns["Date"].Width = 40;
            grvBookingOrder.Columns["Shift"].Width = 15;
            grvBookingOrder.Columns["Operator"].Width = 40;
            grvBookingOrder.Columns["Amount"].Width = 30;
            grvBookingOrder.Columns["Machine"].Width = 30;
            grvBookingOrder.Columns["ColorCode"].Width = 30;
            grvBookingOrder.Columns["ItemId"].Width = 50;
            grvBookingOrder.Columns["OrderLotsId"].Width = 90;
            grvBookingOrder.Columns["FinishDate"].Width = 40;
            //grvBookingOrder.Columns["Date"].Width = 50;
            grvBookingOrder.Columns["Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            grvBookingOrder.Columns["Date"].DisplayFormat.FormatString = "MM/dd/yyyy HH:mm:ss";
            grvBookingOrder.Columns["FinishDate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            grvBookingOrder.Columns["FinishDate"].DisplayFormat.FormatString = "MM/dd/yyyy HH:mm:ss";
            grvBookingOrder.Columns["CreatedBy"].Visible = false;
            grvBookingOrder.Columns["Note"].Visible = false;
            grvBookingOrder.Columns["OrderType"].Visible = false;
        }

        private void grvBookingOrder_ShowingEditor(object sender, CancelEventArgs e)
        {
            //GlobalVariable.idSelect = (int)grvBookingOrder.GetRowCellValue(grvBookingOrder.FocusedRowHandle, "OrderId");

            if (grvBookingOrder.FocusedRowHandle >= 0)
            {
                GlobalVariable.idSelect = (int)grvBookingOrder.GetRowCellValue(grvBookingOrder.FocusedRowHandle, "Id");
                GlobalVariable.newOrUpdateOrderBook = GlobalVariable.enableFlagOrderBook = false;
            }
            else
            {
                GlobalVariable.newOrUpdateOrderBook = GlobalVariable.enableFlagOrderBook = true;
            }

            Debug.WriteLine($"orderBook Rowhandle: {grvBookingOrder.FocusedRowHandle}|newOrView{GlobalVariable.newOrUpdateOrderBook}-Flag{GlobalVariable.enableFlagOrderBook}");
        }


        //dung cho PopUpDefitForm de dong
        //private void grvBookingOrder_ShowingPopupEditForm(object sender, ShowingPopupEditFormEventArgs e)
        //{
        //    DevExpress.XtraEditors.XtraForm editForm = e.EditForm;
        //    ((userControlBookingOrder)editForm.Controls[0].Controls[0].Controls[0]).OwnerForm = editForm;
        //    if (editForm != null)
        //    {
        //        editForm.Controls[0].Controls[1].Hide();
        //    }
        //}
    }
}
