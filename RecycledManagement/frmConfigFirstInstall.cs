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
using DevExpress.XtraSplashScreen;
using RecycledManagement.Common;
using System.Threading;
using System.Diagnostics;
using System.Configuration;

namespace RecycledManagement
{
    public partial class frmConfigFirstInstall : DevExpress.XtraEditors.XtraForm
    {
        public frmConfigFirstInstall()
        {
            InitializeComponent();
        }


        private void frmConfigFirstInstall_Load(object sender, EventArgs e)
        {
            txtServerName.Text = Properties.Settings.Default["serverName"].ToString();
            txtDbName.Text = Properties.Settings.Default["dbName"].ToString();
            txtUserName.Text = Properties.Settings.Default["user"].ToString();
            txtPassword.Text = Properties.Settings.Default["password"].ToString();

            Debug.WriteLine(Properties.Settings.Default["configFirstInstall"].ToString());

            txtDbName.Enabled = false;
            txtServerName.Enabled = false;
            txtUserName.Enabled = false;
            txtPassword.Enabled = false;
            btnSave.Enabled = false;

            //khi cài chuowgn trình chạy dầu tiên thì vào cấu hình DB server 
            if (Properties.Settings.Default["configFirstInstall"].ToString() == "False")
            {
                //txtDbName.Enabled = true;
                //txtServerName.Enabled = true;
                //txtUserName.Enabled = true;
                //txtPassword.Enabled = true;
                //btnSave.Enabled = true;
                Properties.Settings.Default["configFirstInstall"] = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Debug.WriteLine(Properties.Settings.Default["configFirstInstall"]);
                ////demo processing data ...
                //SplashScreenManager.ShowForm(this, typeof(frmProcessing), true, true, false);
                //SplashScreenManager.Default.SetWaitFormCaption("Check database server...");
                //var checkConnectDB = DataProvider.Instance.ExecuteScalar("select count(*) from tblAccount");//kiểm tra kết nối đến DB server
                //Thread.Sleep(1000);//thời gian chờ cho tiến trình chạy
                //if (checkConnectDB != null && (int)checkConnectDB > 0)//kết nối thành công
                //{
                //    SplashScreenManager.Default.SetWaitFormCaption("Connect databases successful...");
                //    Thread.Sleep(1000);
                //    SplashScreenManager.CloseForm();

                //    //vao form Login
                //    frmLogin newForm = new frmLogin();
                //    this.Hide();
                //    newForm.ShowDialog();
                //    this.Close();
                //}
                //else//kết nối thất bại
                //{
                //    SplashScreenManager.Default.SetWaitFormCaption("Connect databases fail...");
                //    Thread.Sleep(1000);
                //    SplashScreenManager.CloseForm();
                //}
            }
        }

        //lưu thông tin server vào lại settings
        private void btnSave_Click(object sender, EventArgs e)
        {
            DataProvider.Instance.connectionStr= $"Data Source={txtServerName.Text.Trim()}" +
                $";Database={txtDbName.Text.Trim()};UID={txtUserName.Text}" +
                $";Password={txtPassword.Text}; Min Pool Size=0;Max Pool Size=1000;Pooling=true; Connect Timeout=65535;";
           

            //cai đặt thông tin xong thì kiểm tra kết nối đến DB server, OK thì vào form Login
            //lỗi thì nằm yên tại chỗ
            SplashScreenManager.ShowForm(this, typeof(frmProcessing), true, true, false);
            SplashScreenManager.Default.SetWaitFormCaption("Check database server...");
            var checkConnectDB = DataProvider.Instance.ExecuteScalar("select count(*) from tblAccount");//kiểm tra kết nối đến DB server
            Thread.Sleep(1000);//thời gian chờ cho tiến trình chạy
            if (checkConnectDB != null && (int)checkConnectDB > 0)//kết nối thành công
            {
                SplashScreenManager.Default.SetWaitFormCaption("Connect databases successful...");
                Thread.Sleep(1000);
                SplashScreenManager.CloseForm();

                //lưu lại trạng thái cấu hình là đã cấu hình rồi vào settings
                //RecycledManagement.Properties.Settings.Default.serverName = txtServerName.Text.Trim();
                //RecycledManagement.Properties.Settings.Default.dbName = txtDbName.Text.Trim();
                //RecycledManagement.Properties.Settings.Default.userName = txtUserName.Text;
                //RecycledManagement.Properties.Settings.Default.password = txtPassword.Text;
                //RecycledManagement.Properties.Settings.Default.configFirstInstall = true;

                //RecycledManagement.Properties.Settings.Default.Save();

                //vao form Login
                frmLogin newForm = new frmLogin();
                this.Hide();
                newForm.ShowDialog();
                this.Show();
            }
            else//kết nối thất bại
            {
                SplashScreenManager.Default.SetWaitFormCaption("Connect databases fail...");
                Thread.Sleep(1000);
                SplashScreenManager.CloseForm();
            }

        }
    }
}