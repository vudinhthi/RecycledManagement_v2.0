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
    public partial class userControlOperators_List : DevExpress.XtraEditors.XtraUserControl
    {
        public userControlOperators_List()
        {
            InitializeComponent();
        }

        private void userControlOperators_List_Load(object sender, EventArgs e)
        {
            // Display a New Item Row to add rows to the View.
            grvOperator.OptionsBehavior.EditingMode = GridEditingMode.EditFormInplace;
            //grvOperator.OptionsEditForm.CustomEditFormLayout = new userControlShifts();

            grvOperator.OptionsView.NewItemRowPosition = NewItemRowPosition.Top; // Available modes: Top, Bottom, None

            #region su kien khi dong EditForm
            grvOperator.EditFormHidden += (s, o) =>
            {
                if (o.Result == EditFormResult.Update)
                {
                    GridView view = s as GridView;

                    //Neu la hang moi thi add vao database
                    if (!view.IsNewItemRow(o.RowHandle))//update
                    {
                        Debug.WriteLine("update data");
                        bool isActive = (o.BindableControls["IsActived"] as CheckEdit).Checked;//get trang thai check trong editForm
                        string operatorId = grvOperator.GetRowCellValue(grvOperator.FocusedRowHandle, "OperatorId").ToString();//get gia tri cua cell girdView

                        //goi method Update tblShift
                        if (DbOperator.Instance.Update(operatorId, o.BindableControls["OperatorName"].Text, o.BindableControls["Department"].Text, isActive, GlobalVariable.userId.ToString()) > 0)
                        {
                            //log action vao tblLoginAction
                            DbUserAction.Instance.InsertData(GlobalVariable.userId, GlobalVariable.loginDate, $"Update Operator Name {o.BindableControls["OperatorName"].Text}");
                        }
                        else
                        {
                            MessageBox.Show("Update Fail!");
                        }
                    }
                    else//new
                    {
                        Debug.WriteLine("insert data");
                        bool isActive = (o.BindableControls["IsActived"] as CheckEdit).Checked;//get trang thai check trong editForm

                        //goi method insert tblShift
                        if (DbOperator.Instance.InsertData(o.BindableControls["OperatorName"].Text, o.BindableControls["Department"].Text, isActive, GlobalVariable.userId.ToString()) > 0)
                        {
                            //log action vao tblLoginAction
                            DbUserAction.Instance.InsertData(GlobalVariable.userId, GlobalVariable.loginDate, $"Insert Operator Name {o.BindableControls["OperatorName"].Text}");
                        }
                        else
                        {
                            MessageBox.Show("Insert Fail!");
                        }
                    }
                    //grvOperator.RefreshData();
                    //grcOperator.RefreshDataSource();

                    grcOperator.DataSource = DbOperator.Instance.SelectAll();

                    //an cot gridView
                    grvOperator.Columns["OperatorId"].Visible = false;
                    grvOperator.Columns["CreatedDate"].Visible = false;
                    grvOperator.Columns["CreatedBy"].Visible = false;
                }
            };
            #endregion

            //fill dada into dataGridView from dataTable
            grcOperator.DataSource = DbOperator.Instance.SelectAll();

            //an cot gridView
            grvOperator.Columns["OperatorId"].Visible = false;
            grvOperator.Columns["CreatedDate"].Visible = false;
            grvOperator.Columns["CreatedBy"].Visible = false;
        }
    }
}
