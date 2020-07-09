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

namespace RecycledManagement
{
    public partial class frmProfile : DevExpress.XtraEditors.XtraForm
    {
        public frmProfile()
        {
            InitializeComponent();
        }

        string oldPass, newPass;

        private void frmProfile_Load(object sender, EventArgs e)
        {
            txtFullName.ReadOnly = true;

            DataTable _data = DbAccount.Instance.GetUser(GlobalVariable.userId.ToString());

            if (_data != null && _data.Rows.Count > 0)
            {
                txtFullName.Text = _data.Rows[0]["fullName"].ToString();
                txtUserName.Text = _data.Rows[0]["useName"].ToString();

                oldPass = _data.Rows[0]["password"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //string _pass = Encryptor.MD5Hash(password);//1 cach ma hoa MD5 ko có pass
            string _pass = EncodeMD5.EncryptString(txtOldPassword.Text, "ITFramasBDVN");//mã hóa MD5 có pass

            if (oldPass==_pass)
            {
                if (EncodeMD5.EncryptString(txtNewPassword.Text,"ITFramasBDVN")== EncodeMD5.EncryptString(txtReNewPass.Text,"ITFramasBDVN"))
                {
                    if(DbAccount.Instance.UpdateAccountUser(GlobalVariable.userId.ToString(), txtUserName.Text, EncodeMD5.EncryptString(txtNewPassword.Text, "ITFramasBDVN")) > 0)
                    {
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show($"Fail! New passwords do not match.");
                }
            }
            else
            {
                MessageBox.Show($"Fail! Old password wrong!");
            }
        }
    }
}