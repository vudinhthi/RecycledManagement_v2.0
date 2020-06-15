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
using DevExpress.XtraSplashScreen;
using System.Threading;
using RecycledManagement.Common;

namespace RecycledManagement
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var result = DbAccount.Instance.Login(txtUserName.Text, txtPasswrod.Text, comboRole.Text);
            if (result)
            {
                frmMain newForm = new frmMain();
                this.Hide();
                newForm.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Login Fail, Plesea try again !");
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            
        }
    }
}