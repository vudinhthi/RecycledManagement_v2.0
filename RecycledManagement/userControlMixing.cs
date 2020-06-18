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
using DevExpress.XtraEditors.Controls;

namespace RecycledManagement
{
    public partial class userControlMixing : DevExpress.XtraEditors.XtraUserControl
    {
        DataTable dt;
        public userControlMixing()
        {
            InitializeComponent();
        }
        private void UserControlMixing_Load(object sender, EventArgs e)
        {
            LoadShifts();
            LoadOprators();
            SetEnableRecycled();            
        }

        //Use for check Recycled Using
        private void CheckUsingRecycled_CheckedChanged(object sender, EventArgs e)
        {
            SetEnableRecycled();
        }

        //Set enable or not for Recycle Lots if check using Recycled is True
        private void SetEnableRecycled()
        {
            if (checkUsingRecycled.Checked)
            {
                lueReasonMixing.Enabled = false;
                lueRecycled1.Enabled = true;
                lueRecycled2.Enabled = true;
                lueCompound.Enabled = true;
                lueClearRecycled.Enabled = true;
                lueFramapur.Enabled = true;
                lueLeftover.Enabled = true;
            }
            else
            {
                lueReasonMixing.Enabled = true;
                lueRecycled1.Enabled = false;
                lueRecycled2.Enabled = false;
                lueCompound.Enabled = false;
                lueClearRecycled.Enabled = false;
                lueFramapur.Enabled = false;
                lueLeftover.Enabled = false;
            }
        }
        //Load list of Shifts show to form
        private void LoadShifts()
        {
            dt = DbMixing.Instance.GetShifts();
            lueShifts.Properties.DataSource = dt;
            lueShifts.Properties.DisplayMember = "ShiftName";
            lueShifts.Properties.ValueMember = "ShiftId";
            lueShifts.Properties.Columns.Add(new LookUpColumnInfo("ShiftName", "ShiftName"));                      
        }
        //Load list of Operator for Mixing Station
        private void LoadOprators()
        {
            dt = DbMixing.Instance.GetOperators(1);
            lueOperators.Properties.DataSource = dt;
            lueOperators.Properties.DisplayMember = "OperatorName";
            lueOperators.Properties.ValueMember = "OperatorId";
            lueOperators.Properties.Columns.Add(new LookUpColumnInfo("OperatorName", "OperatorName"));
        }        
    }
}
