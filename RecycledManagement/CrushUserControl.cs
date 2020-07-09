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
        public GridView view;//dung cho EditFormInplace

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

            this.SetBoundFieldName(this.txtCrushMachine, "Machine");
            this.SetBoundFieldName(this.txtNetWeight, "WeightCrushed");

            this.SetBoundFieldName(this.labItemName, "ItemName");
            this.SetBoundFieldName(this.labColorName, "ColorName");
            this.SetBoundFieldName(this.labMaterialCode, "MaterialCode");


        }

        #region PageLoad
        private void CrushUserControl_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;

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

            //radType.SelectedIndex = 0;//chọn mặc định hiển thị radio ban đầu
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

            //đang ký sự kiện scaleValueChanged
            GlobalVariable.myEvent.ScaleValueChanged += (s, o) =>
            {
                Debug.WriteLine($"Crushing event write: {o.ScaleValue}");
                if (txtWeight.ContainsFocus)
                {
                    txtWeight.Text = o.ScaleValue.ToString();
                }
            };

            #region check role Import
            if (GlobalVariable.importCrush == false)
            {
                lookUpShift.ReadOnly = true;
                lookUpOperator.ReadOnly = true;
                txtCrushMachine.ReadOnly = true;
                lookUpMixCode.ReadOnly = true;
                lookUpMaterial.ReadOnly = true;
                txtWeight.ReadOnly = true;
                txtNetWeight.ReadOnly = true;
                radType.ReadOnly = true;

                labItemName.Enabled = false;
                labColorName.Enabled = false;
                labMaterialCode.Enabled = false;
                btnSave.Enabled = false;
            }
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
                if (!string.IsNullOrEmpty(lookUpMixCode.Text) && lookUpMixCode.Text != "[EditValue is null]")
                {
                    DataTable _data = DbBookingOrder.Instance.GetOrderCrush(lookUpMixCode.GetColumnValue("OrderId").ToString());
                    labItemName.Text = _data.Rows[0][0].ToString();
                    labColorName.Text = _data.Rows[0][1].ToString();
                    labMaterialCode.Text = "-----";
                }
                //labMaterialCode.Text = "-----";
            }
            catch
            {

            }
        }

        private void lookUpMaterial_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(lookUpMaterial.Text) && lookUpMaterial.Text != "[EditValue is null]")
                {
                    labMaterialCode.Text = lookUpMaterial.GetColumnValue("materialcode").ToString();
                    labItemName.Text = "-----";
                    labColorName.Text = "-----";
                }
                //labItemName.Text = "-----";
                //labColorName.Text = "-----";
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


        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            if (txtWeight.Text.Trim() != "0")
            {
                float item = 0;
                float.TryParse(txtWeight.Text.Trim(), out item);
                txtNetWeight.Text = (item - 1.14).ToString("0.00");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            #region xu ly khi add new hay xem order
            //add new order
            if (GlobalVariable.newOrUpdateCrushed == true && GlobalVariable.enableFlagCrushed == true)
            {
                //check quyen nhap
                if (GlobalVariable.importCrush == true)
                {
                    lookUpShift.ReadOnly = false;
                    lookUpOperator.ReadOnly = false;
                    txtCrushMachine.ReadOnly = false;
                    lookUpMixCode.ReadOnly = false;
                    lookUpMaterial.ReadOnly = false;
                    txtWeight.ReadOnly = false;
                    txtNetWeight.ReadOnly = false;
                    radType.ReadOnly = false;

                    labItemName.Enabled = true;
                    labColorName.Enabled = true;
                    labMaterialCode.Enabled = true;
                    btnSave.Enabled = true;
                }
                GlobalVariable.enableFlagCrushed = false;
            }
            else if (GlobalVariable.newOrUpdateCrushed == false && GlobalVariable.enableFlagCrushed == false)
            {
                lookUpShift.ReadOnly = true;
                lookUpOperator.ReadOnly = true;
                txtCrushMachine.ReadOnly = true;
                lookUpMixCode.ReadOnly = true;
                lookUpMaterial.ReadOnly = true;
                txtWeight.ReadOnly = true;
                txtNetWeight.ReadOnly = true;
                radType.ReadOnly = true;

                labItemName.Enabled = false;
                labColorName.Enabled = false;
                labMaterialCode.Enabled = false;
                btnSave.Enabled = false;

                GlobalVariable.enableFlagCrushed = true;
            }
            #endregion
            timer1.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string crushedCode = null;
            string mixId = null, shiftId = null, operatorId = null, materialCode = null, materialName = null;

            //chung
            shiftId = lookUpShift.EditValue.ToString();
            operatorId = lookUpOperator.EditValue.ToString();
            string machine = txtCrushMachine.Text;
            int crushId = DbCrushing.Instance.GetMaxId() + 1;//get gia tri crushId lon nhat

            string weightCrush = txtNetWeight.Text;
            string crushType = radType.EditValue.ToString();//radioControl trả về Value

            //rieng            
            if (crushType == "0")//Recycle Material
            {
                mixId = lookUpMixCode.EditValue.ToString();

                crushedCode = $"RE-BOM-{DateTime.Now.ToString("yyyyMMdd")}{crushId}";
            }
            else
            {
                materialCode = lookUpMaterial.EditValue.ToString();
                materialName = lookUpMaterial.Text;
                crushedCode = $"RE-{materialCode}-{DateTime.Now.ToString("yyyyMMdd")}{crushId}";
            }

            Debug.WriteLine($"Insrt Crushing: {DbCrushing.Instance.Insert(shiftId, operatorId, mixId, machine, materialCode, materialName, weightCrush, GlobalVariable.userId.ToString(), crushedCode, crushType)}");
            view.CloseEditForm();
        }
    }
}
