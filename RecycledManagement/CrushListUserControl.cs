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

                        string crushId = grvCrush.GetRowCellValue(grvCrush.FocusedRowHandle, "CrushId").ToString();//get gia tri cua cell girdView
                        string shiftId = grvCrush.GetRowCellValue(grvCrush.FocusedRowHandle, "ShiftId").ToString();//get gia tri cua cell girdView
                        string operatorId = grvCrush.GetRowCellValue(grvCrush.FocusedRowHandle, "OperatorId").ToString();//get gia tri cua cell girdView
                        string mixId = grvCrush.GetRowCellValue(grvCrush.FocusedRowHandle, "MixId").ToString();//get gia tri cua cell girdView
                        string machine = grvCrush.GetRowCellValue(grvCrush.FocusedRowHandle, "Machine").ToString();//get gia tri cua cell girdView
                        string materialCode = grvCrush.GetRowCellValue(grvCrush.FocusedRowHandle, "MaterialCode").ToString();//get gia tri cua cell girdView
                        string materialName = grvCrush.GetRowCellValue(grvCrush.FocusedRowHandle, "MaterialName").ToString();//get gia tri cua cell girdView
                        string weightCrush = grvCrush.GetRowCellValue(grvCrush.FocusedRowHandle, "WeightCrushed").ToString();//get gia tri cua cell girdView
                        string createdBy = grvCrush.GetRowCellValue(grvCrush.FocusedRowHandle, "CreatedBy").ToString();//get gia tri cua cell girdView

                        string crushType = o.BindableControls["CrushedType"].Text;//radioControl trả về Value

                        string crushedCode = null;
                        if (crushType == "0")
                        {
                            crushedCode = $"RE-BOM-{DateTime.Now.ToString("yyyyMMdd")}{crushId}";
                            materialCode = materialName = null;
                        }
                        else
                        {
                            crushedCode = $"RE-{materialCode}-{DateTime.Now.ToString("yyyyMMdd")}{crushId}";

                            mixId = null;
                        }

                        //goi method Update tblIncomingCrush
                        Debug.WriteLine($"Update Crushing: {DbCrushing.Instance.Update(crushId, shiftId, operatorId, mixId, machine, materialCode, materialName, weightCrush, GlobalVariable.userId.ToString(), crushedCode, crushType)}");
                    }
                    else//new
                    {
                        Debug.WriteLine("insert data");
                        int crushId = DbCrushing.Instance.GetMaxId() + 1;//get gia tri crushId lon nhat

                        string weightCrush = o.BindableControls["WeightCrushed"].Text;
                        string machine = o.BindableControls["Machine"].Text;
                        //string materialName = o.BindableControls["MaterialCode"].Text;//lookupEdit  trả về text
                        string crushType = o.BindableControls["CrushedType"].Text;//radioControl trả về Value
                        string crushedCode = null;


                        string mixId = null, shiftId = null, operatorid = null, materialCode = null, materialName = null;
                        #region lấy ID của các dữ liệu
                        DataTable dataIdCacBang = DbCrushing.Instance.GetIdCacBang(o.BindableControls["ShiftId"].Text, o.BindableControls["OperatorId"].Text, o.BindableControls["MixId"].Text,
                             o.BindableControls["MaterialCode"].Text);

                        if (dataIdCacBang != null && dataIdCacBang.Rows.Count > 0)
                        {
                            //Other
                            if (dataIdCacBang.Columns.Count == 4)
                            {
                                shiftId = dataIdCacBang.Rows[0][0].ToString();
                                materialCode = dataIdCacBang.Rows[0][1].ToString();
                                materialName = dataIdCacBang.Rows[0][2].ToString();
                                operatorid = dataIdCacBang.Rows[0][3].ToString();
                            }
                            //Recycle
                            else if (dataIdCacBang.Columns.Count == 3)
                            {
                                shiftId = dataIdCacBang.Rows[0][0].ToString();
                                mixId = dataIdCacBang.Rows[0][1].ToString();
                                operatorid = dataIdCacBang.Rows[0][2].ToString();
                            }
                        }

                        if (crushType == "0")
                        {
                            crushedCode = $"RE-BOM-{DateTime.Now.ToString("yyyyMMdd")}{crushId}";
                        }
                        else
                        {
                            crushedCode = $"RE-{materialCode}-{DateTime.Now.ToString("yyyyMMdd")}{crushId}";
                        }
                        #endregion

                        Debug.WriteLine($"Insrt Crushing: {DbCrushing.Instance.Insert(shiftId,operatorid,mixId,machine,materialCode,materialName,weightCrush,GlobalVariable.userId.ToString(),crushedCode,crushType)}");

                        grcCrush.RefreshDataSource();

                        //fill dada into dataGridView from dataTable
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
