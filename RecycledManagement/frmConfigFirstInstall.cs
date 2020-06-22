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
using System.IO;
using System.Reflection;
using System.Globalization;

namespace RecycledManagement
{
    public partial class frmConfigFirstInstall : DevExpress.XtraEditors.XtraForm
    {
        string pathApp;
        string[] dataArr = new string[5];
        string serverName, dbName, userName, password, configFirstInstall;

        public frmConfigFirstInstall()
        {
            InitializeComponent();
        }


        private void frmConfigFirstInstall_Load(object sender, EventArgs e)
        {
            try
            {
                #region đọc textFile để lấy thông số cấu hình
                pathApp = $"{ Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Files\\DbServerParametter.txt";//get path
                // Debug.WriteLine(pathApp);

                dataArr = ReadFile(pathApp).Split('|');

                //giải mã MD5 các thồng số với pass giải mã là "ITFramasBDVN"
                serverName = txtServerName.Text = EncodeMD5.DecryptString(dataArr[0].Split(':')[1], "ITFramasBDVN");
                dbName = txtDbName.Text = EncodeMD5.DecryptString(dataArr[1].Split(':')[1], "ITFramasBDVN");
                userName = txtUserName.Text = EncodeMD5.DecryptString(dataArr[2].Split(':')[1], "ITFramasBDVN");
                password = txtPassword.Text = EncodeMD5.DecryptString(dataArr[3].Split(':')[1], "ITFramasBDVN");

                configFirstInstall = dataArr[4].Split(':')[1];//kiểm tra biến này nếu =False là cài đặt đầu tiên, =True là đã cài đặt rồi cho vào thẳng form Login
                #endregion

                //gắn connection 
                DataProvider.Instance.connectionStr = $"Data Source={serverName}" +
                    $";Database={dbName};UID={userName}" +
                    $";Password={password}; Min Pool Size=0;Max Pool Size=1000;Pooling=true; Connect Timeout=100;";

                txtDbName.Enabled = false;
                txtServerName.Enabled = false;
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
                btnSave.Enabled = false;

                //khi cài chương trình chạy dầu tiên thì vào cấu hình DB server 
                if (configFirstInstall == "False")
                {
                    txtDbName.Enabled = true;
                    txtServerName.Enabled = true;
                    txtUserName.Enabled = true;
                    txtPassword.Enabled = true;
                    btnSave.Enabled = true;
                }
                else//
                {
                    SplashScreenManager.ShowForm(this, typeof(frmProcessing), true, true, false);
                    SplashScreenManager.Default.SetWaitFormCaption("Check database server...");
                    var checkConnectDB = DataProvider.Instance.ExecuteScalar("select count(*) from tblAccount");//kiểm tra kết nối đến DB server
                    Thread.Sleep(1000);//thời gian chờ cho tiến trình chạy
                    if (checkConnectDB != null && (int)checkConnectDB > 0)//kết nối thành công
                    {
                        SplashScreenManager.Default.SetWaitFormCaption("Connect databases successful...");
                        Thread.Sleep(1000);
                        SplashScreenManager.CloseForm();

                        //vao form Login
                        frmLogin newForm = new frmLogin();
                        this.Hide();
                        newForm.ShowDialog();
                        this.Close();
                    }
                    else//kết nối thất bại
                    {
                        SplashScreenManager.Default.SetWaitFormCaption("Connect databases fail...");
                        Thread.Sleep(1000);
                        SplashScreenManager.CloseForm();
                    }
                }
            }
            catch { }
        }

        //lưu thông tin server vào lại settings
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                serverName = txtServerName.Text.Trim();
                dbName = txtDbName.Text.Trim();
                userName = txtUserName.Text;
                password = txtPassword.Text;

                DataProvider.Instance.connectionStr = $"Data Source={serverName}" +
                    $";Database={dbName};UID={userName}" +
                    $";Password={password}; Min Pool Size=0;Max Pool Size=1000;Pooling=true; Connect Timeout=100;";


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
                    //mã hóa MD5 rồi mới lưu xuống textFile
                    configFirstInstall = "True";
                    WriteFile(pathApp, $"serverName:{EncodeMD5.EncryptString(serverName, "ITFramasBDVN")}|dbName:{EncodeMD5.EncryptString(dbName, "ITFramasBDVN")}" +
                        $"|userName:{EncodeMD5.EncryptString(userName, "ITFramasBDVN")}|password:{EncodeMD5.EncryptString(password, "ITFramasBDVN")}|configFirstInstall:{configFirstInstall}");

                    //vao form Login
                    frmLogin newForm = new frmLogin();
                    this.Hide();
                    newForm.ShowDialog();
                    this.Close();
                }
                else//kết nối thất bại
                {
                    SplashScreenManager.Default.SetWaitFormCaption("Connect databases fail...");
                    Thread.Sleep(1000);
                    SplashScreenManager.CloseForm();
                }
            }
            catch
            {

            }
        }

        #region method đọc ghi text file
        private string ReadFile(string _path)
        {
            String line = null;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                using (StreamReader sr = new StreamReader(_path))
                {

                    //Read the first line of text
                    line = sr.ReadLine();

                    //close the file
                    sr.Close();
                    sr.Dispose();
                }
                return line;
            }
            catch
            {
                return line;
            }
        }

        private bool WriteFile(string _path, string content)
        {
            bool status = false;
            //try
            {
                using (StreamWriter sw = new StreamWriter(_path))
                {
                    //Write a line of text
                    sw.WriteLine(content);

                    //Close the file
                    sw.Close();
                    sw.Dispose();

                    status = true;
                }
                return status;
            }
            // catch { return status; }
        }
        #endregion
    }
}