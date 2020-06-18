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
    public partial class userControlLossTypes_List : DevExpress.XtraEditors.XtraUserControl
    {
        public userControlLossTypes_List()
        {
            InitializeComponent();
        }

        private void userControlLossTypes_List_Load(object sender, EventArgs e)
        {
            // Display a New Item Row to add rows to the View.
            grvLossType.OptionsBehavior.EditingMode = GridEditingMode.EditForm;
            //grvLossType.OptionsEditForm.CustomEditFormLayout = new userControlShifts();

            grvLossType.OptionsView.NewItemRowPosition = NewItemRowPosition.Top; // Available modes: Top, Bottom, None

            #region su kien khi dong EditForm
            grvLossType.EditFormHidden += (s, o) =>
            {
                if (o.Result == EditFormResult.Update)
                {
                    GridView view = s as GridView;

                    //Neu la hang moi thi add vao database
                    if (!view.IsNewItemRow(o.RowHandle))//update
                    {
                        Debug.WriteLine("update data");
                        bool isActive = (o.BindableControls["IsActive"] as CheckEdit).Checked;//get trang thai check trong editForm
                        string reasonId = grvLossType.GetRowCellValue(grvLossType.FocusedRowHandle, "LossTypeId").ToString();//get gia tri cua cell girdView

                        //goi method Update tblShift
                        if (DbLossType.Instance.Update(reasonId, o.BindableControls["LossTypeName"].Text, o.BindableControls["LossTypeForm"].Text, isActive, GlobalVariable.userId.ToString()) > 0)
                        {
                            //log action vao tblLoginAction
                            DbUserAction.Instance.InsertData(GlobalVariable.userId, GlobalVariable.loginDate, $"Update LossType Name {o.BindableControls["LossTypeName"].Text}");
                        }
                        else
                        {
                            MessageBox.Show("Update Fail!");
                        }
                    }
                    else//new
                    {
                        Debug.WriteLine("insert data");
                        bool isActive = (o.BindableControls["IsActive"] as CheckEdit).Checked;//get trang thai check trong editForm

                        //goi method insert tblShift
                        if (DbLossType.Instance.InsertData(o.BindableControls["LossTypeName"].Text, o.BindableControls["LossTypeForm"].Text, isActive, GlobalVariable.userId.ToString()) > 0)
                        {
                            //log action vao tblLoginAction
                            DbUserAction.Instance.InsertData(GlobalVariable.userId, GlobalVariable.loginDate, $"Insert LossType Name {o.BindableControls["LossTypeName"].Text}");
                        }
                        else
                        {
                            MessageBox.Show("Insert Fail!");
                        }
                    }
                    //grvLossType.RefreshData();
                    //grcLossType.RefreshDataSource();

                    grcLossType.DataSource = DbLossType.Instance.SelectAll();

                    //an cot gridView
                    grvLossType.Columns["LossTypeId"].Visible = false;
                    grvLossType.Columns["CreateDate"].Visible = false;
                    grvLossType.Columns["CreatedBy"].Visible = false;
                }
            };
            #endregion

            //fill dada into dataGridView from dataTable
            grcLossType.DataSource = DbLossType.Instance.SelectAll();

            //an cot gridView
            grvLossType.Columns["LossTypeId"].Visible = false;
            grvLossType.Columns["CreateDate"].Visible = false;
            grvLossType.Columns["CreatedBy"].Visible = false;
        }
    }
}
