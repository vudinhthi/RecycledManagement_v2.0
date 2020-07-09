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
using System.Globalization;

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
                //this.Show();
            }
            else
            {
                MessageBox.Show("Login Fail, Plesea try again !");
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            comboRole.Text = "Operator";
        }

        public string TextUser
        {
            get { return txtUserName.Text; }
            set { txtUserName.Text = value; }
        }
        public string TextPass
        {
            get { return txtPasswrod.Text; }
            set { txtPasswrod.Text = value; }
        }
        public string TextCombo
        {
            get { return comboRole.Text; }
            set { comboRole.Text = value; }
        }
    }
}