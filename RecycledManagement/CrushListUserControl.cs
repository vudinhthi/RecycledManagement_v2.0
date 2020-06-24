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
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using RecycledManagement.Common;

namespace RecycledManagement
{
    public partial class CrushListUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        public CrushListUserControl()
        {
            InitializeComponent();
        }

        private void CrushListUserControl_Load(object sender, EventArgs e)
        {
            grvCrush.OptionsBehavior.EditingMode = GridEditingMode.EditFormInplace;
            grvCrush.OptionsEditForm.CustomEditFormLayout = new CrushUserControl();

            grvCrush.OptionsView.NewItemRowPosition = NewItemRowPosition.Top; // Available modes: Top, Bottom, None

            #region su kien khi dong EditForm
            grvCrush.EditFormHidden += (s, o) =>
            {
                if (o.Result == EditFormResult.Update)
                {
                    GridView view = s as GridView;
                    //Neu la hang moi thi add vao database
                    if (!view.IsNewItemRow(o.RowHandle))//update
                    {
                        Debug.WriteLine("update data");
                        ////bool isActive = (o.BindableControls["IsActived"] as CheckEdit).Checked;//get trang thai check trong editForm

                        //string incomingId = grvCrush.GetRowCellValue(grvCrush.FocusedRowHandle, "IncomingId").ToString();//get gia tri cua cell girdView

                        ////goi method Update tblIncomingCrush
                        //Debug.WriteLine(DbIncomingCrush.Instance.Update(incomingId, grvCrush.GetRowCellValue(grvCrush.FocusedRowHandle, "MixId").ToString(), grvCrush.GetRowCellValue(grvCrush.FocusedRowHandle, "ShiftId").ToString(),
                        //    grvCrush.GetRowCellValue(grvCrush.FocusedRowHandle, "LossTypeId").ToString(), grvCrush.GetRowCellValue(grvCrush.FocusedRowHandle, "SourceId").ToString(), grvCrush.GetRowCellValue(grvCrush.FocusedRowHandle, "ReasonId").ToString(),
                        //    grvCrush.GetRowCellValue(grvCrush.FocusedRowHandle, "MaterialCode").ToString(), grvCrush.GetRowCellValue(grvCrush.FocusedRowHandle, "MaterialName").ToString(),
                        //    grvCrush.GetRowCellValue(grvCrush.FocusedRowHandle, "WeightIncoming").ToString(), GlobalVariable.userId.ToString(), $"LE-ORCODE-{DateTime.Now.ToString("yyyyMMdd")}{incomingId}"));
                    }
                    else//new
                    {
                        Debug.WriteLine("insert data");
                        //int incomingId = DbIncomingCrush.Instance.GetMaxIncomingId() + 1;//get gia tri Incoming lon nhat

                        //string weightIncoming = o.BindableControls["WeightIncoming"].Text;

                        //string materialName = o.BindableControls["MaterialCode"].Text;//lookupEdit  trả về text
                        //string lossTypeId = o.BindableControls["LossTypeId"].Text;//radioControl trả về Value

                        //DataTable idCacBang = DbIncomingCrush.Instance.GetIdCacBang(o.BindableControls["MixId"].Text, o.BindableControls["ShiftId"].Text,
                        //    o.BindableControls["SourceId"].Text, o.BindableControls["ReasonId"].Text, o.BindableControls["MaterialCode"].Text);

                        //Debug.WriteLine($"ID cac bang: {idCacBang.Rows[0][0].ToString()}|{idCacBang.Rows[0][1].ToString()}|{idCacBang.Rows[0][2].ToString()}|{idCacBang.Rows[0][3].ToString()}|{idCacBang.Rows[0][4].ToString()}");

                        ////goi method Insert tblIncomingCrush
                        ////Debug.WriteLine(DbIncomingCrush.Instance.InsertData(idCacBang.Rows[0][0].ToString(), idCacBang.Rows[0][1].ToString(), lossTypeId, idCacBang.Rows[0][2].ToString(), idCacBang.Rows[0][3].ToString(), idCacBang.Rows[0][4].ToString(),
                        ////    materialName, grvCrush.GetRowCellValue(grvCrush.FocusedRowHandle, "WeightIncoming").ToString(), GlobalVariable.userId.ToString(), $"LE-ORCODE-{DateTime.Now.ToString("yyyyMMdd")}{incomingId}"));

                        //DbIncomingCrush.Instance.InsertData(idCacBang.Rows[0][0].ToString(), idCacBang.Rows[0][1].ToString(), lossTypeId, idCacBang.Rows[0][2].ToString(), idCacBang.Rows[0][3].ToString()
                        //    , idCacBang.Rows[0][4].ToString(), materialName, weightIncoming, GlobalVariable.userId.ToString(), $"LE-ORCODE-{DateTime.Now.ToString("yyyyMMdd")}{incomingId}");
                    }

                    grcCrush.RefreshDataSource();

                    //fill dada into dataGridView from dataTable
                    grcCrush.DataSource = DbIncomingCrush.Instance.GetDataGridView();

                    //an cot gridView
                    grvCrush.Columns["CrushId"].Visible = false;
                    grvCrush.Columns["ShiftId"].Visible = false;
                    grvCrush.Columns["OperatorId"].Visible = false;
                    grvCrush.Columns["MixId"].Visible = false;
                    grvCrush.Columns["CreatedBy"].Visible = false;
                    grvCrush.Columns["CrushedType"].Visible = false;
                }
            };
            #endregion

            grcCrush.DataSource = DbCrushing.Instance.GetDataGridView();

            //an cot gridView
            grvCrush.Columns["CrushId"].Visible = false;
            grvCrush.Columns["ShiftId"].Visible = false;
            grvCrush.Columns["OperatorId"].Visible = false;
            grvCrush.Columns["MixId"].Visible = false;
            grvCrush.Columns["CreatedBy"].Visible = false;
            grvCrush.Columns["CrushedType"].Visible = false;


        }
    }
}
