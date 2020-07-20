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
using DevExpress.XtraEditors.DXErrorProvider;

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

            //this.SetBoundFieldName(this.tedItemCode, "ItemCode");
            //this.SetBoundFieldName(this.tedItemName, "ItemName");
            //this.SetBoundFieldName(this.tedColorCode, "ColorCode");
            //this.SetBoundFieldName(this.tedColorName, "ColorName");
            //this.SetBoundFieldName(this.tedMaterialName, "materialname");

            //this.SetBoundPropertyName(this.tedItemName, "EditValue");
            //this.SetBoundPropertyName(this.tedColorName, "EditValue");
            //this.SetBoundPropertyName(this.tedItemCode, "EditValue");
            //this.SetBoundPropertyName(this.tedColorCode, "EditValue");            
            //this.SetBoundPropertyName(this.tedMaterialName, "EditValue");
        }

        #region PageLoad
        private void CrushUserControl_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;

            lookUpMixCode.Enabled = false;
            lookUpMaterial.Enabled = false;

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

            lookUpShift.Properties.Columns.Add(new LookUpColumnInfo("ShiftId", "ShiftId", 40));
            lookUpShift.Properties.Columns.Add(new LookUpColumnInfo("ShiftName", "ShiftName", 120));
            //enable text editing 
            lookUpShift.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            #endregion

            #region Operators
            lookUpOperator.Properties.DataSource = DbCrushing.Instance.GetOperators();
            lookUpOperator.Properties.ValueMember = "OperatorId";
            lookUpOperator.Properties.DisplayMember = "OperatorName";

            lookUpOperator.Properties.Columns.Add(new LookUpColumnInfo("OperatorId", "OperatorId", 40));
            lookUpOperator.Properties.Columns.Add(new LookUpColumnInfo("OperatorName", "OperatorName", 120));
            //enable text editing 
            lookUpOperator.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            #endregion

            #region get MixCode
            lookUpMixCode.Properties.DataSource = DbMixCode.Instance.GetAllIncoming();
            lookUpMixCode.Properties.ValueMember = "MixId";
            lookUpMixCode.Properties.DisplayMember = "MixCode";
            //lookUpOrderId.Properties.Columns["OrderId"].Visible = false;

            //lookUpMixCode.Properties.Columns.Add(new LookUpColumnInfo("MixId", "MixId", 40));
            //lookUpMixCode.Properties.Columns.Add(new LookUpColumnInfo("MixCode", "MixCode", 120));
            this.lookUpMixCode.EditValueChanged += new System.EventHandler(this.lookUpMixCode_EditValueChanged);
            #endregion

            #region get Material
            lookUpMaterial.Properties.DataSource = DbIncomingCrush.Instance.GetMaterialsIncomingWinLine();
            lookUpMaterial.Properties.ValueMember = "materialcode";
            lookUpMaterial.Properties.DisplayMember = "materialcode";

            lookUpMaterial.Properties.Columns.Add(new LookUpColumnInfo("materialcode", "Material Code", 40));
            lookUpMaterial.Properties.Columns.Add(new LookUpColumnInfo("materialname", "Material Name", 120));
            this.lookUpMaterial.EditValueChanged += new System.EventHandler(this.lookUpMaterial_EditValueChanged);
            #endregion

            //CustomValidationRule customValidationRule = new CustomValidationRule();
            //customValidationRule.ErrorText = "Please enter a valid person name";
            //customValidationRule.ErrorType = ErrorType.Warning;

            //dxValidationProvider1.SetValidationRule(lookUpMixCode, customValidationRule);

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

                tedItemName.Enabled = false;
                tedColorName.Enabled = false;
                tedMaterialName.Enabled = false;
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
                if (GlobalVariable.isMixcode)
                {

                    LookUpEdit lookUpEdit = sender as LookUpEdit;
                    if (!string.IsNullOrEmpty(lookUpEdit.EditValue?.ToString()))
                    {
                        tedItemCode.EditValue = lookUpEdit.GetColumnValue("ItemCode").ToString();
                        tedColorCode.EditValue = lookUpEdit.GetColumnValue("ColorCode").ToString();
                        tedItemName.EditValue = lookUpEdit.GetColumnValue("ItemName").ToString();
                        tedColorName.EditValue = lookUpEdit.GetColumnValue("ColorName").ToString();
                    }
                    //else
                    //{
                    //    tedItemCode.EditValue = null;
                    //    tedColorCode.EditValue = null;
                    //    tedItemName.EditValue = null;
                    //    tedColorName.EditValue = null;
                    //}                   

                }

            }
            catch
            {

            }
        }

        private void lookUpMaterial_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalVariable.isMixcode)
                {
                    LookUpEdit lookUpEdit = sender as LookUpEdit;
                    if (!string.IsNullOrEmpty(lookUpEdit.EditValue?.ToString()))
                    {
                        tedMaterialName.EditValue = lookUpMaterial.GetColumnValue("materialname");
                    }
                    //else
                    //{
                    //    tedMaterialName.EditValue = null;
                    //}

                }

            }
            catch
            {

            }
        }

        private void radType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioGroup edit = sender as RadioGroup;
            Debug.WriteLine($"radCrush Type: {edit}");

            if (edit.SelectedIndex == 0)
            {
                lookUpMixCode.Enabled = true;

                GlobalVariable.isMixcode = true;
                lookUpMaterial.EditValue = null;
                tedMaterialName.EditValue = null;

                lookUpMaterial.Enabled = false;
            }
            else
            {
                lookUpMaterial.Enabled = true;

                GlobalVariable.isMixcode = false;

                lookUpMixCode.EditValue = null;
                tedItemCode.EditValue = "";
                tedItemName.EditValue = "";
                tedColorCode.EditValue = "";
                tedColorName.EditValue = "";

                lookUpMixCode.Enabled = false;
            }
        }
        #endregion

        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            if (txtWeight.Text.Trim() != "0")
            {
                float item = 0;
                float.TryParse(txtWeight.Text.Trim(), out item);
                txtNetWeight.Text = (item - GlobalVariable.boxWeightCrushing).ToString("0.00");
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

                    tedItemCode.ReadOnly = true;
                    tedItemName.ReadOnly = true;
                    tedItemCode.ReadOnly = true;
                    tedColorName.ReadOnly = true;
                    tedMaterialName.ReadOnly = true;
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

                tedItemCode.ReadOnly = true;
                tedItemName.ReadOnly = true;
                tedItemCode.ReadOnly = true;
                tedColorName.ReadOnly = true;
                tedMaterialName.ReadOnly = true;
                btnSave.Enabled = false;

                GlobalVariable.enableFlagCrushed = true;
            }
            #endregion
            timer1.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string crushedCode = null;
            string mixId = null, shiftId = null, operatorId = null, materialCode = null, materialName = null, itemCode = null;

            if (dxValidationProvider1.Validate())
            {
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
                    itemCode = tedItemCode.EditValue.ToString();
                    crushedCode = $"RE|{DateTime.Now.ToString("yyyyMMdd")}|{itemCode}|{crushId}";
                }
                else
                {
                    materialCode = lookUpMaterial.EditValue.ToString();
                    materialName = lookUpMaterial.Text;
                    crushedCode = $"RE|{DateTime.Now.ToString("yyyyMMdd")}|{materialCode}|{crushId}";
                }

                Debug.WriteLine($"Insrt Crushing: {DbCrushing.Instance.Insert(shiftId, operatorId, mixId, machine, materialCode, materialName, weightCrush, GlobalVariable.userId.ToString(), crushedCode, crushType)}");
                view.CloseEditForm();
            }            
        }

        private void lookUpMaterial_Properties_BeforePopup(object sender, EventArgs e)
        {
            //lookUpMixCode.EditValue = null;
            //lookUpMaterial.EditValue = null;
        }

        private void lookUpMixCode_Properties_BeforePopup(object sender, EventArgs e)
        {
            //lookUpMixCode.EditValue = null;
            //lookUpMaterial.EditValue = null;
        }

        private class CustomValidationRule : ValidationRule
        {
            public override bool Validate(Control control1, object value)
            {
                bool res = false;
                return res;
            }
        }
    }
}
