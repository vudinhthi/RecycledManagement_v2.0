﻿using System;
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
using DevExpress.XtraEditors.Controls;
using System.Diagnostics;

namespace RecycledManagement
{
    public partial class IncomingUserControl : EditFormUserControl
    {
        public IncomingUserControl()
        {
            InitializeComponent();

            this.SetBoundFieldName(this.lookUpMixCode, "MixId");
            this.SetBoundPropertyName(this.lookUpMixCode, "EditValue");

            this.SetBoundFieldName(this.lookUpShift, "ShiftId");
            this.SetBoundPropertyName(this.lookUpShift, "EditValue");

            this.SetBoundFieldName(this.lookUpOtherSource, "SourceId");
            this.SetBoundPropertyName(this.lookUpOtherSource, "EditValue");

            this.SetBoundFieldName(this.lookUpReason, "ReasonId");
            this.SetBoundPropertyName(this.lookUpReason, "EditValue");

            this.SetBoundFieldName(this.lookUpMaterial, "MaterialCode");
            this.SetBoundPropertyName(this.lookUpMaterial, "EditValue");

            this.SetBoundFieldName(this.txtNetWeight, "WeightIncoming");

            this.SetBoundFieldName(this.radLossType, "LossTypeId");
            this.SetBoundPropertyName(this.radLossType, "EditValue");

        }

        private void IncomingUserControl_Load(object sender, EventArgs e)
        {
            lookUpMixCode.Enabled = true;
            lookUpMaterial.Enabled = true;
            lookUpOtherSource.Enabled = true;
            lookUpReason.Enabled = true;

            #region get LossType
            DataTable data = DbLossType.Instance.GetLossTypeName(1);
            if (data != null && data.Rows.Count > 0)
            {
                //insert radio Item vao radio control
                RadioGroupItem radioItem;
                int index = 0;
                foreach (DataRow item in data.Rows)
                {
                    //radioItem = new RadioGroupItem();
                    //radioItem.Description = item[1].ToString();
                    radioItem = new RadioGroupItem(item[0], item[1].ToString());
                    radLossType.Properties.Items.Add(radioItem);
                    index++;
                }
                //radioItem = new RadioGroupItem(index, "Other Source");
                //radLossType.Properties.Items.Add(radioItem);

                radLossType.SelectedIndex = 1;
                //radLossType.EditValue = data.Rows[0][0];//gắn cho chọn ở item nào
                radLossType.BorderStyle = BorderStyles.Style3D;

                

                //var a=   radLossType.Properties.Items.GetItemByValue(3);//lấy ra text theo value
            }
            #endregion

            #region get shifts
            lookUpShift.Properties.DataSource = DbShift.Instance.GetShiftComming();
            lookUpShift.Properties.ValueMember = "ShiftId";
            lookUpShift.Properties.DisplayMember = "ShiftName";
            #endregion

            #region get Reason
            lookUpReason.Properties.DataSource = DbReasons.Instance.GetReasonType(2);
            lookUpReason.Properties.ValueMember = "ReasonId";
            lookUpReason.Properties.DisplayMember = "ReasonName";
            #endregion

            #region get MixCode
            lookUpMixCode.Properties.DataSource = DbMixCode.Instance.GetAllIncoming();
            lookUpMixCode.Properties.ValueMember = "MixId";
            lookUpMixCode.Properties.DisplayMember = "MixCode";
            //lookUpOrderId.Properties.Columns["OrderId"].Visible = false;
            #endregion

            #region get Source
            lookUpOtherSource.Properties.DataSource = DbOtherSource.Instance.GetAllIncoming();
            lookUpOtherSource.Properties.ValueMember = "SourceId";
            lookUpOtherSource.Properties.DisplayMember = "SourceName";
            #endregion

            #region get Material
            lookUpMaterial.Properties.DataSource = DbIncomingCrush.Instance.GetMaterialsIncomingWinLine();
            lookUpMaterial.Properties.ValueMember = "materialcode";
            lookUpMaterial.Properties.DisplayMember = "materialname";
            #endregion

            //Đăng ký sự kiện scaleValueChanged
            GlobalVariable.myEvent.ScaleValueChanged += (s, o) => {
                Debug.WriteLine($"Incoming Event write: {o.ScaleValue}");
                if (txtWeight.ContainsFocus)
                {
                    txtWeight.Text = o.ScaleValue.ToString();
                }
            };
        }


        #region Event
        //sự kiện radioGroup Select
        private void radLossType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioGroup edit = sender as RadioGroup;
            Debug.WriteLine($"Loss Type: {edit.SelectedIndex}");

            if (edit.SelectedIndex == 0 || edit.SelectedIndex == 1 || edit.SelectedIndex == 2 || edit.SelectedIndex == 3)
            {
                lookUpMixCode.Enabled = true;
                lookUpMaterial.Enabled = false;
                lookUpOtherSource.Enabled = false;
                lookUpReason.Enabled = false;
                if (edit.SelectedIndex == 3)//nếu là LeftOver thì enable lookUpReason
                {
                    lookUpReason.Enabled = true;
                }
            }
            else if(edit.SelectedIndex==4)//Other Source
            {
                lookUpMixCode.Enabled = false;
                lookUpMaterial.Enabled = true;
                lookUpOtherSource.Enabled = true;
                lookUpReason.Enabled = false;
            }
            else
            {
                lookUpMixCode.Enabled = false;
                lookUpMaterial.Enabled = false;
                lookUpOtherSource.Enabled = false;
                lookUpReason.Enabled = false;
            }
        }

        #endregion


        //su kien txtWeight khi co sự thay đổi cân thì nó sẽ vào tru voi so lượng rồi fill vào txtNetWeight
        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            if (txtWeight.Text.Trim()!="0")
            {
                float item = 0;
                float.TryParse(txtWeight.Text.Trim(), out item);
                txtNetWeight.Text = (item - 2.1966).ToString("0.00");
            }
            //else
            //{
            //    txtNetWeight.Text = "0";
            //}
        }

        #region test
        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    timer1.Enabled = false;
        //    if (txtWeight.ContainsFocus)
        //    {
        //        txtWeight.Text = GlobalVariable.scale.ToString();
        //    }

        //    //if (txttest.ContainsFocus)
        //    //{
        //    //    txttest.Text = GlobalVariable.scale.ToString();
        //    //}
        //    //Debug.WriteLine($"CheckFocus txtTest: {txttest.ContainsFocus}");//get focus cua control

        //    timer1.Enabled = true;
        //}

        //private void lookUpShift_EditValueChanged(object sender, EventArgs e)
        //{
        //    //Debug.WriteLine($"Lookup Shift Selct: {lookUpShift.EditValue.ToString()}");//get value cua lookupEdit
        //}
        #endregion
    }
}
