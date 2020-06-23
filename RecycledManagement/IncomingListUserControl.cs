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
using DevExpress.Utils.Extensions;

namespace RecycledManagement
{
    public partial class IncomingListUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        public IncomingListUserControl()
        {
            InitializeComponent();
        }

        private void IncomingListUserControl_Load(object sender, EventArgs e)
        {
            grvIncoming.OptionsBehavior.EditingMode = GridEditingMode.EditFormInplace;
            grvIncoming.OptionsEditForm.CustomEditFormLayout = new IncomingUserControl();

            grvIncoming.OptionsView.NewItemRowPosition = NewItemRowPosition.Top; // Available modes: Top, Bottom, None

            #region su kien khi dong EditForm
            grvIncoming.EditFormHidden += (s, o) =>
            {
                if (o.Result == EditFormResult.Update)
                {
                    GridView view = s as GridView;
                    //Neu la hang moi thi add vao database
                    if (!view.IsNewItemRow(o.RowHandle))//update
                    {
                        Debug.WriteLine("update data");
                        //bool isActive = (o.BindableControls["IsActived"] as CheckEdit).Checked;//get trang thai check trong editForm

                        string incomingId = grvIncoming.GetRowCellValue(grvIncoming.FocusedRowHandle, "IncomingId").ToString();//get gia tri cua cell girdView

                        //goi method Update tblIncomingCrush
                        Debug.WriteLine(DbIncomingCrush.Instance.Update(incomingId, grvIncoming.GetRowCellValue(grvIncoming.FocusedRowHandle, "MixId").ToString(), grvIncoming.GetRowCellValue(grvIncoming.FocusedRowHandle, "ShiftId").ToString(),
                            grvIncoming.GetRowCellValue(grvIncoming.FocusedRowHandle, "LossTypeId").ToString(), grvIncoming.GetRowCellValue(grvIncoming.FocusedRowHandle, "SourceId").ToString(), grvIncoming.GetRowCellValue(grvIncoming.FocusedRowHandle, "ReasonId").ToString(),
                            grvIncoming.GetRowCellValue(grvIncoming.FocusedRowHandle, "MaterialCode").ToString(), grvIncoming.GetRowCellValue(grvIncoming.FocusedRowHandle, "MaterialName").ToString(),
                            grvIncoming.GetRowCellValue(grvIncoming.FocusedRowHandle, "WeightIncoming").ToString(), GlobalVariable.userId.ToString(), $"LE-ORCODE-{DateTime.Now.ToString("yyyyMMdd")}{incomingId}"));
                    }
                    else//new
                    {
                        Debug.WriteLine("insert data");
                        int incomingId = DbIncomingCrush.Instance.GetMaxIncomingId() + 1;//get gia tri Incoming lon nhat

                        #region get cac data để insert vào DB
                        #region lấy thông tin từ EditForm trả về
                        string weightIncoming = o.BindableControls["WeightIncoming"].Text;
                        //string materialName = o.BindableControls["MaterialCode"].Text;//lookupEdit  trả về text
                        string lossTypeId = o.BindableControls["LossTypeId"].Text;//radioControl trả về Value
                        #endregion

                        string mixId = null, shiftId = null, sourceId = null, reasonId = null, materialCode = null, materialName = null;
                        #region lấy ID của các dữ liệu
                        DataTable dataIdCacBang = DbIncomingCrush.Instance.GetIdCacBang(o.BindableControls["MixId"].Text, o.BindableControls["ShiftId"].Text,
                            o.BindableControls["SourceId"].Text, o.BindableControls["ReasonId"].Text, o.BindableControls["MaterialCode"].Text);

                        if (dataIdCacBang!=null && dataIdCacBang.Rows.Count>0)
                        {
                            //LossType chon Runner/Defect/Contaminated
                            if (dataIdCacBang.Columns.Count==2)
                            {
                                mixId = dataIdCacBang.Rows[0][0].ToString();
                                shiftId = dataIdCacBang.Rows[0][1].ToString();
                            }
                            //LossType chọn Leftover
                            else if (dataIdCacBang.Columns.Count == 3)
                            {
                                mixId = dataIdCacBang.Rows[0][0].ToString();
                                shiftId = dataIdCacBang.Rows[0][1].ToString();
                                reasonId = dataIdCacBang.Rows[0][2].ToString();
                            }
                            //LossType chọn OtherSource
                            else if (dataIdCacBang.Columns.Count == 4)
                            {
                                shiftId = dataIdCacBang.Rows[0][0].ToString();
                                sourceId = dataIdCacBang.Rows[0][1].ToString();
                                materialCode = dataIdCacBang.Rows[0][2].ToString();
                                materialName = dataIdCacBang.Rows[0][3].ToString();
                            }
                        }


                        Debug.WriteLine($"ID cac bang: ShiftId={shiftId}|MixId:{mixId}|SourceId:{sourceId}|ReasonId:{reasonId}|MaterialCode:{materialCode}");
                        #endregion
                        #endregion

                        //goi method Insert tblIncomingCrush
                        DbIncomingCrush.Instance.InsertData(mixId, shiftId, lossTypeId, sourceId, reasonId, materialCode, materialName, weightIncoming,
                            GlobalVariable.userId.ToString(), $"LE-ORCODE-{DateTime.Now.ToString("yyyyMMdd")}{incomingId}");
                    }

                    grcIncoming.RefreshDataSource();

                    //fill dada into dataGridView from dataTable
                    grcIncoming.DataSource = DbIncomingCrush.Instance.GetDataGridView();

                    //an cot gridView
                    grvIncoming.Columns["IncomingId"].Visible = false;
                    grvIncoming.Columns["MixId"].Visible = false;
                    grvIncoming.Columns["OrderId"].Visible = false;
                    grvIncoming.Columns["ShiftId"].Visible = false;
                    grvIncoming.Columns["LossTypeId"].Visible = false;
                    grvIncoming.Columns["ReasonId"].Visible = false;
                    grvIncoming.Columns["ReasonName"].Visible = false;
                    grvIncoming.Columns["SourceId"].Visible = false;
                    grvIncoming.Columns["SourceName"].Visible = false;
                    grvIncoming.Columns["ItemCode"].Visible = false;
                    grvIncoming.Columns["CreatedBy"].Visible = false;
                }
            };
            #endregion

            #region sự kiện trước khi editForm đc mở lên
            //DateEdit dateEdit = null;
            //grvIncoming.EditFormPrepared += (s, o) => {
            //    // The e.BindableControls collection contains a list of editors located onto the EditForm
            //    //foreach (Control item in o.BindableControls)
            //    //{
            //    //    dateEdit = item as DateEdit;
            //    //    if (dateEdit != null)
            //    //    {
            //    //        // Focus the DateEdit editor
            //    //        grvIncoming.BeginInvoke(new MethodInvoker(() => { dateEdit.Focus(); }));
            //    //        return;
            //    //    }
            //    //}

            //    //var a = o.BindableControls["MixId"];
            //};
            #endregion

            grcIncoming.DataSource = DbIncomingCrush.Instance.GetDataGridView();

            //an cot gridView
            grvIncoming.Columns["IncomingId"].Visible = false;
            grvIncoming.Columns["MixId"].Visible = false;
            grvIncoming.Columns["OrderId"].Visible = false;
            grvIncoming.Columns["ShiftId"].Visible = false;
            grvIncoming.Columns["LossTypeId"].Visible = false;
            grvIncoming.Columns["ReasonId"].Visible = false;
            grvIncoming.Columns["ReasonName"].Visible = false;
            grvIncoming.Columns["SourceId"].Visible = false;
            grvIncoming.Columns["SourceName"].Visible = false;
            grvIncoming.Columns["ItemCode"].Visible = false;
            grvIncoming.Columns["CreatedBy"].Visible = false;

        }
    }
}
