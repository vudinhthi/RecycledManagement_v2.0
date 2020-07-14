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
using System.IO.Ports;
using Newtonsoft.Json;
using System.Net.Mail;
using DevExpress.Utils.Menu;

namespace RecycledManagement
{
    public partial class frmConfigFirstInstall : DevExpress.XtraEditors.XtraForm
    {
        string pathApp, pathComConfig, pathMailserverConfig;
        string[] dataArr = new string[5];
        string serverName, dbName, userName, password, configFirstInstall;
        MailConfig mailConfig;
        public frmConfigFirstInstall()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            MailHelper mailHelper = new MailHelper()
            {
                FromMailAddress =new MailAddress(txtFromMailAddress.Text),
                Host = txtHost.Text,
                Password = txtMailPassword.Text,
                Port = txtPort.Text,
                ToMailAddress = "cong.nguyen@framas.com,thi.vu@framas.com",
                CCMailAddress = "tuan.le@framas.com,sang.nguyen@framas.com",
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnalbleSsl = true,
                isBodyHtml = false,
                UseDefaultCredentials = false,
                Subject = "Test",
                Body = "This is email to test"
            };
            Task<bool> task = new Task<bool>(()=>mailHelper.SendEmail()
            );
            task.Start();
            task.ContinueWith(t => XtraMessageBox.Show(t.Result.ToString()));
        }

        private void frmConfigFirstInstall_Load(object sender, EventArgs e)
        {
            //try
            {
                GetComPort();
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
                btnSend.Enabled = false;
                //Cấu hình Mail server
                pathMailserverConfig = @"./Files/MailserverConfig.json";
                if (File.Exists(pathMailserverConfig))
                {
                    mailConfig = JsonConvert.DeserializeObject<MailConfig>(File.ReadAllText(pathMailserverConfig));
                    txtFromMailAddress.Text = mailConfig.FromMailAddress = EncodeMD5.DecryptString(mailConfig.FromMailAddress, "ITFramasBDVN");
                    txtMailPassword.Text = mailConfig.Password = EncodeMD5.DecryptString(mailConfig.Password, "ITFramasBDVN");
                    txtHost.Text = mailConfig.Host = EncodeMD5.DecryptString(mailConfig.Host, "ITFramasBDVN");
                    txtPort.Text = mailConfig.Port = EncodeMD5.DecryptString(mailConfig.Port, "ITFramasBDVN");
                }
                else
                {
                    mailConfig = new MailConfig();
                    mailConfig.FromMailAddress = txtFromMailAddress.Text = "sang.nguyen@framas.com";
                    mailConfig.Password = txtMailPassword.Text = "san48Ngu#";
                    mailConfig.Host = txtHost.Text = "smtp.office365.com";
                    mailConfig.Port = txtPort.Text = "587";

                }
                //khi cài chương trình chạy dầu tiên thì vào cấu hình DB server 
                if (configFirstInstall == "False")
                {
                    txtDbName.Enabled = true;
                    txtServerName.Enabled = true;
                    txtUserName.Enabled = true;
                    txtPassword.Enabled = true;
                    btnSend.Enabled = true;
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
            //catch { }
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
                    configFirstInstall = "False";
                    WriteFile(pathApp, $"serverName:{EncodeMD5.EncryptString(serverName, "ITFramasBDVN")}|dbName:{EncodeMD5.EncryptString(dbName, "ITFramasBDVN")}" +
                        $"|userName:{EncodeMD5.EncryptString(userName, "ITFramasBDVN")}|password:{EncodeMD5.EncryptString(password, "ITFramasBDVN")}|configFirstInstall:{configFirstInstall}");
                    //Save Mail Config
                    SaveMailConfig();
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
        #region Sang's Method
        private void GetComPort()
        {
            string[] portNames = SerialPort.GetPortNames();     //<-- Reads all available comPorts
            foreach (var portName in portNames)
            {
                cbbComPort.Properties.Items.Add(portName);                  //<-- Adds Ports to combobox
            }
            cbbComPort.SelectedIndex = 0;
        }
        
        
        private void SaveMailConfig()
        {
            mailConfig.FromMailAddress = EncodeMD5.EncryptString(txtFromMailAddress.Text, "ITFramasBDVN");
            mailConfig.Password = EncodeMD5.EncryptString(txtMailPassword.Text, "ITFramasBDVN");
            mailConfig.Host = EncodeMD5.EncryptString(txtHost.Text, "ITFramasBDVN");
            mailConfig.Port = EncodeMD5.EncryptString(txtPort.Text, "ITFramasBDVN");
            string json = JsonConvert.SerializeObject(mailConfig, Formatting.Indented);
            File.WriteAllText(pathMailserverConfig, json);
        }
        #endregion
    }
    class MailConfig
    {
        public string FromMailAddress { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
       

    }
}