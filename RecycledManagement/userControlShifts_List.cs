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
using RecycledManagement.Common;
using DevExpress.XtraGrid.Views.Grid;
using System.Diagnostics;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;

namespace RecycledManagement
{
    public partial class userControlShifts_List : DevExpress.XtraEditors.XtraUserControl
    {
        public userControlShifts_List()
        {
            InitializeComponent();
        }

        private void userControlShifts_List_Load(object sender, EventArgs e)
        {
            try
            {
                // Display a New Item Row to add rows to the View.
                gridViewShift.OptionsBehavior.EditingMode = GridEditingMode.EditFormInplace;
                //gridViewShift.OptionsEditForm.CustomEditFormLayout = new userControlShifts();

                gridViewShift.OptionsView.NewItemRowPosition = NewItemRowPosition.Top; // Available modes: Top, Bottom, None

                #region su kien khi dong EditForm
                gridViewShift.EditFormHidden += (s, o) =>
                {
                    if (o.Result == EditFormResult.Update)
                    {
                        GridView view = s as GridView;
                        //Neu la hang moi thi add vao database
                        if (!view.IsNewItemRow(o.RowHandle))//update
                        {
                            Debug.WriteLine("update data");
                            bool isActive = (o.BindableControls["IsActived"] as CheckEdit).Checked;//get trang thai check trong editForm

                            string shiftId = gridViewShift.GetRowCellValue(gridViewShift.FocusedRowHandle, "ShiftId").ToString();//get gia tri cua cell girdView

                            //goi method Update tblShift
                            Debug.WriteLine(DbShift.Instance.Update(shiftId, o.BindableControls["ShiftName"].Text, isActive, GlobalVariable.userId.ToString()));
                        }
                        else//new
                        {
                            Debug.WriteLine("insert data");
                            bool isActive = (o.BindableControls["IsActived"] as CheckEdit).Checked;//get trang thai check trong editForm

                            //goi method insert tblShift
                            Debug.WriteLine(DbShift.Instance.InsertData(o.BindableControls["ShiftName"].Text, isActive, GlobalVariable.userId.ToString()));
                        }
                        gridControlShift.RefreshDataSource();
                        //fill dada into dataGridView from dataTable
                        gridControlShift.DataSource = DbShift.Instance.SelectAll();

                        //an cot gridView
                        gridViewShift.Columns["ShiftId"].Visible = false;
                        gridViewShift.Columns["CreatedDate"].Visible = false;
                        gridViewShift.Columns["CreatedBy"].Visible = false;
                    }
                };
                #endregion

                //fill dada into dataGridView from dataTable
                gridControlShift.DataSource = DbShift.Instance.SelectAll();

                //an cot gridView
                gridViewShift.Columns["ShiftId"].Visible = false;
                gridViewShift.Columns["CreatedDate"].Visible = false;
                gridViewShift.Columns["CreatedBy"].Visible = false;


                #region nhan Ctrl+delete xoa record
                //gridViewShift.OptionsBehavior.Editable = false;
                //// Handle the ProcessGridKey event to process key presses before they are processed by the gridcontrol.
                //gridControlShift.ProcessGridKey += (s, o) => {
                //    if (o.KeyCode == Keys.Delete && o.Modifiers == Keys.Control)
                //    {
                //        if (XtraMessageBox.Show("Delete row(s)?", "Delete rows dialog", MessageBoxButtons.YesNo) !=
                //          DialogResult.Yes)
                //            return;

                //        DbShift.Instance.Delect(gridViewShift.GetRowCellValue(gridViewShift.FocusedRowHandle, "ShiftId").ToString());
                //        GridControl grid = s as GridControl;
                //        GridView view = grid.FocusedView as GridView;
                //        //Deletes the selected rows in multiple selection mode or focused row
                //        //  in single selection mode. 
                //        view.DeleteSelectedRows();
                //    }
                //};
                #endregion
            }
            catch
            {
            }
        }

        private void gridViewShift_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OperatorId").ToString();
            XtraUserControl shiftUserControl = new userControlShifts();

            shiftUserControl.Show();
        }
    }
}
