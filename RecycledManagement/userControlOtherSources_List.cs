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
using System.Diagnostics;
using RecycledManagement.Common;

namespace RecycledManagement
{
    public partial class userControlOtherSources_List : DevExpress.XtraEditors.XtraUserControl
    {
        public userControlOtherSources_List()
        {
            InitializeComponent();
        }

        private void userControlOtherSources_List_Load(object sender, EventArgs e)
        {
            // Display a New Item Row to add rows to the View.
            grvOtherSource.OptionsBehavior.EditingMode = GridEditingMode.EditForm;
            //grvOtherSource.OptionsEditForm.CustomEditFormLayout = new userControlShifts();

            grvOtherSource.OptionsView.NewItemRowPosition = NewItemRowPosition.Top; // Available modes: Top, Bottom, None

            #region su kien khi dong EditForm
            grvOtherSource.EditFormHidden += (s, o) =>
            {
                if (o.Result == EditFormResult.Update)
                {
                    GridView view = s as GridView;

                    //Neu la hang moi thi add vao database
                    if (!view.IsNewItemRow(o.RowHandle))//update
                    {
                        Debug.WriteLine("update data");
                        bool isActive = (o.BindableControls["IsActived"] as CheckEdit).Checked;//get trang thai check trong editForm
                        string sourceId = grvOtherSource.GetRowCellValue(grvOtherSource.FocusedRowHandle, "SourceId").ToString();//get gia tri cua cell girdView

                        //goi method Update tblShift
                        if (DbOtherSource.Instance.Update(sourceId, o.BindableControls["SourceName"].Text, isActive, GlobalVariable.userId.ToString()) > 0)
                        {
                            //log action vao tblLoginAction
                            DbUserAction.Instance.InsertData(GlobalVariable.userId, GlobalVariable.loginDate, $"Update OtherSource Name {o.BindableControls["SourceName"].Text}");
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
                        if (DbOtherSource.Instance.InsertData(o.BindableControls["SourceName"].Text, isActive, GlobalVariable.userId.ToString()) > 0)
                        {
                            //log action vao tblLoginAction
                            DbUserAction.Instance.InsertData(GlobalVariable.userId, GlobalVariable.loginDate, $"Insert OtherSource Name {o.BindableControls["SourceName"].Text}");
                        }
                        else
                        {
                            MessageBox.Show("Insert Fail!");
                        }
                    }
                    grcOtherSource.RefreshDataSource();
                    //fill dada into dataGridView from dataTable
                    grcOtherSource.DataSource = DbOtherSource.Instance.SelectAll();

                    //an cot gridView
                    grvOtherSource.Columns["SourceId"].Visible = false;
                    grvOtherSource.Columns["CreatedDate"].Visible = false;
                    grvOtherSource.Columns["CreatedBy"].Visible = false;
                }
            };
            #endregion

            //fill dada into dataGridView from dataTable
            grcOtherSource.DataSource = DbOtherSource.Instance.SelectAll();

            //an cot gridView
            grvOtherSource.Columns["SourceId"].Visible = false;
            grvOtherSource.Columns["CreatedDate"].Visible = false;
            grvOtherSource.Columns["CreatedBy"].Visible = false;

        }
    }
}
