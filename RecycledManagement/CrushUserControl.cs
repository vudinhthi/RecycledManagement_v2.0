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
using DevExpress.XtraEditors.Controls;

namespace RecycledManagement
{
    public partial class CrushUserControl : EditFormUserControl
    {
        public CrushUserControl()
        {
            InitializeComponent();

            this.SetBoundFieldName(this.lookUpShift, "ShiftId");
            this.SetBoundPropertyName(this.lookUpShift, "EditValue");

            this.SetBoundFieldName(this.lookUpOperator, "OperatorId");
            this.SetBoundPropertyName(this.lookUpOperator, "EditValue");

            this.SetBoundFieldName(this.lookUpMixCode, "MixId");
            this.SetBoundPropertyName(this.lookUpMixCode, "EditValue");

            this.SetBoundFieldName(this.lookUpMaterial, "MaterialCode");
            this.SetBoundPropertyName(this.lookUpMaterial, "EditValue");

            this.SetBoundFieldName(this.radType, "CrushedType");
            this.SetBoundPropertyName(this.radType, "EditValue");
        }

        #region PageLoad
        private void CrushUserControl_Load(object sender, EventArgs e)
        {



            lookUpMixCode.Enabled = false;
            labItemName.Enabled = false;
            labColorName.Enabled = false;

            lookUpMaterial.Enabled = false;
            labMaterialCode.Enabled = false;

            #region add item for radio
            RadioGroupItem radioItem;
            radioItem = new RadioGroupItem(0, "Recycle Material");
            radType.Properties.Items.Add(radioItem);
            radioItem = new RadioGroupItem(1, "Other Recycle Type");
            radType.Properties.Items.Add(radioItem);
            //radioItem = new RadioGroupItem(index, "Other Source");
            //radLossType.Properties.Items.Add(radioItem);

            radType.SelectedIndex = 0;//chọn mặc định hiển thị radio ban đầu
            radType.BorderStyle = BorderStyles.Style3D;
            //radType.BackColor = Color.Green;
            #endregion

            #region get shifts
            lookUpShift.Properties.DataSource = DbShift.Instance.GetShiftComming();
            lookUpShift.Properties.ValueMember = "ShiftId";
            lookUpShift.Properties.DisplayMember = "ShiftName";
            #endregion

            #region Operators
            lookUpOperator.Properties.DataSource = DbCrushing.Instance.GetOperators();
            lookUpOperator.Properties.ValueMember = "OperatorId";
            lookUpOperator.Properties.DisplayMember = "OperatorName";
            #endregion

            #region get MixCode
            lookUpMixCode.Properties.DataSource = DbMixCode.Instance.GetAllIncoming();
            lookUpMixCode.Properties.ValueMember = "MixId";
            lookUpMixCode.Properties.DisplayMember = "MixCode";
            //lookUpOrderId.Properties.Columns["OrderId"].Visible = false;
            #endregion

            #region get Material
            lookUpMaterial.Properties.DataSource = DbIncomingCrush.Instance.GetMaterialsIncomingWinLine();
            lookUpMaterial.Properties.ValueMember = "materialcode";
            lookUpMaterial.Properties.DisplayMember = "materialname";
            #endregion
        }
        #endregion

        #region Events
        private void lookUpMixCode_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                ////int rowIndex= lookUpMixCode.Properties.GetDataSourceRowIndex("OrderId",100);
                //var rowIndex = lookUpMixCode.GetColumnValue("OrderId");
                //var s = lookUpMixCode.Properties.GetDataSourceValue("OrderId", 3);
                //Debug.WriteLine($"Mix: {s}");

                DataTable _data = DbBookingOrder.Instance.GetOrderCrush(lookUpMixCode.GetColumnValue("OrderId").ToString());
                labItemName.Text = _data.Rows[0][0].ToString();
                labColorName.Text = _data.Rows[0][1].ToString();
                labMaterialCode.Text = "-----";
            }
            catch
            {

            }
        }

        private void lookUpMaterial_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                labMaterialCode.Text = lookUpMaterial.GetColumnValue("materialcode").ToString();
                labItemName.Text = "-----";
                labColorName.Text = "-----";
            }
            catch
            {

            }
        }

        private void radType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioGroup edit = sender as RadioGroup;
            Debug.WriteLine($"radCrush Type: {edit.SelectedIndex}");

            if (edit.SelectedIndex == 0)
            {
                lookUpMixCode.Enabled = true;
                labItemName.Enabled = true;
                labColorName.Enabled = true;

                lookUpMaterial.Enabled = false;
                labMaterialCode.Enabled = false;
            }
            else
            {
                lookUpMixCode.Enabled = false;
                labItemName.Enabled = false;
                labColorName.Enabled = false;

                lookUpMaterial.Enabled = true;
                labMaterialCode.Enabled = true;
            }
        }
        #endregion
    }
}
