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
using DevExpress.XtraEditors.Controls;

namespace RecycledManagement
{
    public partial class frmAdminUser : DevExpress.XtraEditors.XtraForm
    {
        //password default la "11111"
        public frmAdminUser()
        {
            InitializeComponent();
        }

        private void frmAdminUser_Load(object sender, EventArgs e)
        {
            DataTable _data = DbAccount.Instance.GetUserReset();

            lookUpUserName.Properties.DataSource = _data;
            lookUpUserName.Properties.ValueMember = "id";
            lookUpUserName.Properties.DisplayMember = "useName";


        }

        //reset ve pass mac dinh "11111"
        private void btnReset_Click(object sender, EventArgs e)
        {
            if (lookUpUserName.Text != "[EditValue is null]" && !string.IsNullOrEmpty(lookUpUserName.Text))
            {
                if (DbAccount.Instance.UpdateAccountUser(lookUpUserName.EditValue.ToString(), lookUpUserName.Text, EncodeMD5.EncryptString("11111", "ITFramasBDVN")) > 0)
                {
                    MessageBox.Show($"Reset password successfull.");
                }
                else
                {
                    MessageBox.Show($"Fail, please try again!");
                }
            }
            else
            {
                MessageBox.Show($"Fail, please try again!");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFullName.Text) && !string.IsNullOrEmpty(txtUserName.Text))
            {
                if (DbAccount.Instance.InsertAccount(txtUserName.Text, txtFullName.Text, EncodeMD5.EncryptString("11111", "ITFramasBDVN"), "2") > 0)
                {
                    int _id = DbAccount.Instance.GetMaxIdAccount();
                    string order = $"{checkEditImportOrder.Checked}|{checkEditPrintOrder.Checked}|{checkEditScaleOrder.Checked}";
                    string mixing = $"{checkEditImportMixing.Checked}|{checkEditPrintMixing.Checked}|{checkEditScaleMixing.Checked}";
                    string incoming = $"{checkEditImportIncoming.Checked}|{checkEditPrintIncoming.Checked}|{checkEditScaleIncoming.Checked}";
                    string crush = $"{checkEditImportCrush.Checked}|{checkEditPrintCrush.Checked}|{checkEditScaleCrush.Checked}";
                    DbAccount.Instance.InsertOperatorRole(_id.ToString(), order, mixing, incoming, crush);

                    //refresh laij danh sach user
                    DataTable _data = DbAccount.Instance.GetUserReset();
                    lookUpUserName.Properties.DataSource = _data;

                    MessageBox.Show($"Successfull.");
                }
            }
            else
            {
                MessageBox.Show($"Fail, please try again!");
            }
        }

        private void checkEditPrintOrder_CheckedChanged(object sender, EventArgs e)
        {
            checkEditPrintMixing.Checked = checkEditPrintIncoming.Checked = checkEditPrintCrush.Checked = checkEditPrintOrder.Checked;
        }

        private void checkEditScaleOrder_CheckedChanged(object sender, EventArgs e)
        {
            checkEditScaleMixing.Checked = checkEditScaleIncoming.Checked = checkEditScaleCrush.Checked = checkEditScaleOrder.Checked;
        }

        private void checkEditPrintMixing_CheckedChanged(object sender, EventArgs e)
        {
            checkEditPrintOrder.Checked = checkEditPrintIncoming.Checked = checkEditPrintCrush.Checked = checkEditPrintMixing.Checked;
        }

        private void checkEditScaleMixing_CheckedChanged(object sender, EventArgs e)
        {
            checkEditScaleOrder.Checked = checkEditScaleIncoming.Checked = checkEditScaleCrush.Checked = checkEditScaleMixing.Checked;
        }

        private void checkEditPrintIncoming_CheckedChanged(object sender, EventArgs e)
        {
            checkEditPrintOrder.Checked = checkEditPrintMixing.Checked = checkEditPrintCrush.Checked = checkEditPrintIncoming.Checked;
        }

        private void checkEditScaleIncoming_CheckedChanged(object sender, EventArgs e)
        {
            checkEditScaleOrder.Checked = checkEditScaleMixing.Checked = checkEditScaleCrush.Checked = checkEditScaleIncoming.Checked;
        }

        private void checkEditPrintCrush_CheckedChanged(object sender, EventArgs e)
        {
            checkEditPrintOrder.Checked = checkEditPrintMixing.Checked = checkEditPrintIncoming.Checked = checkEditPrintCrush.Checked;
        }

        private void checkEditScaleCrush_CheckedChanged(object sender, EventArgs e)
        {
            checkEditScaleOrder.Checked = checkEditScaleMixing.Checked = checkEditScaleIncoming.Checked = checkEditScaleCrush.Checked;
        }
    }
}