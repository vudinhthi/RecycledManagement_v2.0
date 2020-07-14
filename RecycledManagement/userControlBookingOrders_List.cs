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
            grvBookingOrder.Columns["CreatedBy"].Visible = false;
            grvBookingOrder.Columns["Note"].Visible = false;
            grvBookingOrder.Columns["OrderType"].Visible = false;
        }

        private void grvBookingOrder_ShowingEditor(object sender, CancelEventArgs e)
        {
            //GlobalVariable.idSelect = (int)grvBookingOrder.GetRowCellValue(grvBookingOrder.FocusedRowHandle, "OrderId");

            if (grvBookingOrder.FocusedRowHandle >= 0)
            {
                GlobalVariable.idSelect = (int)grvBookingOrder.GetRowCellValue(grvBookingOrder.FocusedRowHandle, "OrderId");
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
