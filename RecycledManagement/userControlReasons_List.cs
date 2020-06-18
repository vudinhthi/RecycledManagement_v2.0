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
    public partial class userControlReasons_List : DevExpress.XtraEditors.XtraUserControl
    {
        public userControlReasons_List()
        {
            InitializeComponent();
        }

        private void userControlReasons_List_Load(object sender, EventArgs e)
        {
            // Display a New Item Row to add rows to the View.
            grvReason.OptionsBehavior.EditingMode = GridEditingMode.EditForm;
            //grvReason.OptionsEditForm.CustomEditFormLayout = new userControlShifts();

            grvReason.OptionsView.NewItemRowPosition = NewItemRowPosition.Top; // Available modes: Top, Bottom, None

            #region su kien khi dong EditForm
            grvReason.EditFormHidden += (s, o) =>
            {
                if (o.Result == EditFormResult.Update)
                {
                    GridView view = s as GridView;

                    //Neu la hang moi thi add vao database
                    if (!view.IsNewItemRow(o.RowHandle))//update
                    {
                        Debug.WriteLine("update data");
                        bool isActive = (o.BindableControls["IsActived"] as CheckEdit).Checked;//get trang thai check trong editForm
                        string reasonId = grvReason.GetRowCellValue(grvReason.FocusedRowHandle, "ReasonId").ToString();//get gia tri cua cell girdView

                        //goi method Update tblShift
                        if (DbReasons.Instance.Update(reasonId, o.BindableControls["ReasonName"].Text, o.BindableControls["ReasonType"].Text, isActive, GlobalVariable.userId.ToString()) > 0)
                        {
                            //log action vao tblLoginAction
                            DbUserAction.Instance.InsertData(GlobalVariable.userId, GlobalVariable.loginDate, $"Update Reason Name {o.BindableControls["ReasonName"].Text}");
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
                        if (DbReasons.Instance.InsertData(o.BindableControls["ReasonName"].Text, o.BindableControls["ReasonType"].Text, isActive, GlobalVariable.userId.ToString()) > 0)
                        {
                            //log action vao tblLoginAction
                            DbUserAction.Instance.InsertData(GlobalVariable.userId, GlobalVariable.loginDate, $"Insert Reason Name {o.BindableControls["ReasonName"].Text}");
                        }
                        else
                        {
                            MessageBox.Show("Insert Fail!");
                        }
                    }
                    //grvReason.RefreshData();
                    //grcReason.RefreshDataSource();

                    grcReason.DataSource = DbReasons.Instance.SelectAll();

                    //an cot gridView
                    grvReason.Columns["ReasonId"].Visible = false;
                    grvReason.Columns["CreatedDate"].Visible = false;
                    grvReason.Columns["CreatedBy"].Visible = false;
                }
            };
            #endregion

            //fill dada into dataGridView from dataTable
            grcReason.DataSource = DbReasons.Instance.SelectAll();

            //an cot gridView
            grvReason.Columns["ReasonId"].Visible = false;
            grvReason.Columns["CreatedDate"].Visible = false;
            grvReason.Columns["CreatedBy"].Visible = false;

        }
    }
}
