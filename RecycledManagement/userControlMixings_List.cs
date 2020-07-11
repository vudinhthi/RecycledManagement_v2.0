using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
//using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using RecycledManagement.Common;
using System.Diagnostics;
using RecycledManagement.Models;

namespace RecycledManagement
{
    public partial class userControlMixing_List : DevExpress.XtraEditors.XtraUserControl
    {
        public userControlMixing_List()
        {
            InitializeComponent();
        }

        private void userControlMixing_List_Load(object sender, EventArgs e)
        {

            grcMixing.DataSource = DbMixCode.Instance.GetAllMixedList();

            //grvMixing.Appearance.Row.BackColor = Color.Green;

            GlobalVariable.myEvent.ShowMixingEditorChanged += MyEvent_ShowMixingEditorChanged;

            //an cot gridView
            //grvBookingOrder.Columns["CrushId"].Visible = false;
            //grvBookingOrder.Columns["ShiftId"].Visible = false;
            //grvBookingOrder.Columns["OperatorId"].Visible = false;
            //grvBookingOrder.Columns["MixId"].Visible = false;
            //grvBookingOrder.Columns["CreatedBy"].Visible = false;
            //grvBookingOrder.Columns["CrushedType"].Visible = false;
        }

        //su kien khi đống MĩingEditor thì refresh lại GridView và đóng form
        private void MyEvent_ShowMixingEditorChanged(object sender, ScaleValueChangedEventArgs e)
        {
            if (GlobalVariable.myEvent.ShowMixingEditor==false)
            {
                grcMixing.DataSource = DbMixCode.Instance.GetAllMixedList();
            }
        }


        //sự kiện thay đổi màu hiển thị khi chọn vào 1 dòng nào đó trên GridView
        //private void grvMixing_RowCellStyle(object sender, RowCellStyleEventArgs e)
        //{
        //    GridView view = sender as GridView;
        //    if (view.FocusedRowHandle == e.RowHandle && !view.FocusedColumn.Equals(e.Column))
        //        e.Appearance.BackColor = Color.Orange;
        //}

        private void grvMixing_RowStyle(object sender, RowStyleEventArgs e)
        {
            #region thay đổi backround color cho nguyen 1 row theo 1 điều kiện nào đó
            GridView view = sender as GridView;
            #region Cách 1, dùng properties mặc định trả về của sự kiện
            //DataRowView data = view.GetRow(e.RowHandle) as DataRowView;
            //if (data != null)
            //{

            //    if (data.Row.ItemArray[27].ToString() == "1")
            //    {
            //        e.Appearance.BackColor = Color.Red;
            //    }
            //    else if (data.Row.ItemArray[27].ToString() == "2")
            //    {
            //        e.Appearance.BackColor = Color.Yellow;
            //    }
            //    else if (data.Row.ItemArray[27].ToString() == "3")
            //    {
            //        e.Appearance.BackColor = Color.SkyBlue;
            //    }
            //    else if (data.Row.ItemArray[27].ToString() == "4")
            //    {
            //        e.Appearance.BackColor = Color.Green;
            //    }
            //}
            #endregion

            #region Cách 2. dùng model
            MixingOrderModel data = view.GetRow(e.RowHandle) as MixingOrderModel;

            if (data != null)
            {
                view.OptionsBehavior.Editable = false;//khoa ko cho nhap tren GridView, khoa toan bo gridView

                if (data.Status == "1")
                {
                    e.Appearance.BackColor = Color.Red;
                }
                else if (data.Status == "2")
                {
                    e.Appearance.BackColor = Color.Yellow;
                }
                else if (data.Status == "3")
                {
                    e.Appearance.BackColor = Color.Magenta;
                }
                else if (data.Status == "4")
                {
                    e.Appearance.BackColor = Color.Green;
                }
            }
            #endregion
            #endregion
        }

        private void grvMixing_DoubleClick(object sender, EventArgs e)
        {
            //GlobalVariable.mixId = grvMixing.GetRowCellValue(grvMixing.FocusedRowHandle, "MixId").ToString();
            GlobalVariable.orderId = Convert.ToInt32(grvMixing.GetRowCellValue(grvMixing.FocusedRowHandle, "OrderId").ToString());

            //gán giá trị cho biến ShowMixingEditor để tạo sự kiện
            //GlobalVariable.myEvent.ShowMixingEditor = false;
            GlobalVariable.myEvent.ShowMixingEditor = true;
        }
    }
}
