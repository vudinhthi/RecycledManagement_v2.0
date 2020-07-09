using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using RecycledManagement.Common;
using RecycledManagement.Models;
using DevExpress.XtraEditors.Repository;

namespace RecycledManagement
{
    public partial class frmMixing : DevExpress.XtraEditors.XtraForm
    {
        public frmMixing()
        {
            InitializeComponent();
        }

        private void frmMixing_Load(object sender, EventArgs e)
        {
            #region get shifts
            lookUpShift.Properties.DataSource = DbShift.Instance.GetShiftComming();
            lookUpShift.Properties.ValueMember = "ShiftId";
            lookUpShift.Properties.DisplayMember = "ShiftName";
            #endregion

            #region get TeamLeader
            lookUpOperator.Properties.DataSource = DbOperator.Instance.SelectAll();
            lookUpOperator.Properties.ValueMember = "OperatorId";
            lookUpOperator.Properties.DisplayMember = "OperatorName";
            #endregion

            #region get reason
            lookUpReason.Properties.DataSource = DbReasons.Instance.GetReasonType(1);
            lookUpReason.Properties.ValueMember = "ReasonId";
            lookUpReason.Properties.DisplayMember = "ReasonName";
            #endregion

            //MixingOrderModel orderInfo = DbMixCode.Instance.GetAllMixed1Row(!string.IsNullOrEmpty(GlobalVariable.mixId) ? GlobalVariable.mixId : null);
            MixingOrderModel orderInfo = DbMixCode.Instance.GetAllMixed1Row(GlobalVariable.orderId.ToString());

            if (orderInfo != null)
            {
                txtOrderId.Text = orderInfo.OrderId;
                txtMachine.Text = orderInfo.Machine;
                txtTeamLeader.Text = orderInfo.OrderOperatorName;
                txtReceivingTime.Text = orderInfo.FinishDate;
                txtOrderAmount.Text = orderInfo.OrderAmount;
                txtItemName.Text = orderInfo.ItemName;
                txtColorName.Text = orderInfo.ColorName;
                txtOrderNote.Text = orderInfo.OrderNote;
            }

            List<MixProductWinlineModel> data = DbMixCode.Instance.GetProductWinline(orderInfo.ItemCode, orderInfo.OrderAmount);

            grcMaterialConsumption.DataSource = data;

            //RepositoryItemButtonEdit edit = new RepositoryItemButtonEdit();
            //grcMaterialConsumption.RepositoryItems.Add(edit);
            //grvMaterialConsumption.Columns["ActualUsage"].ColumnEdit = edit;

        }

        private void checkBoxUsingRecycle_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUsingRecycle.Checked == true)
            {
                lookUpRecycle1.Enabled = true;
                lookUpRecycle2.Enabled = true;
                lookUpCompound.Enabled = true;
                lookUpClearRecycle.Enabled = true;
                lookUpFramapur.Enabled = true;
                lookUpLeftover.Enabled = true;

                txtWeightRecycle1.Enabled = true;
                txtWeightRecycle2.Enabled = true;
                txtWeightCompound.Enabled = true;
                txtWeightClearRecycle.Enabled = true;
                txtWeightFramapur.Enabled = true;
                txtWeightLeftover.Enabled = true;

                lookUpReason.Enabled = false;
            }
            else
            {
                lookUpRecycle1.Enabled = false;
                lookUpRecycle2.Enabled = false;
                lookUpCompound.Enabled = false;
                lookUpClearRecycle.Enabled = false;
                lookUpFramapur.Enabled = false;
                lookUpLeftover.Enabled = false;

                txtWeightRecycle1.Enabled = false;
                txtWeightRecycle2.Enabled = false;
                txtWeightCompound.Enabled = false;
                txtWeightClearRecycle.Enabled = false;
                txtWeightFramapur.Enabled = false;
                txtWeightLeftover.Enabled = false;

                lookUpReason.Enabled = true;
            }
        }
    }
}