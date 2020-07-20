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
using System.IO;
using Newtonsoft.Json;
using RecycledManagement.Common;
using RecycledManagement.Models;

namespace RecycledManagement
{
    public partial class frmUserConfig : DevExpress.XtraEditors.XtraForm
    {
        string UserConfigPath;
        UserConfigModel userconfig;
        public frmUserConfig()
        {
            InitializeComponent();
        }

        private void Userconfig_Load(object sender, EventArgs e)
        {
            UserConfigPath = @"./Files/UserConfig.json";
            if (File.Exists(UserConfigPath))
            {
                userconfig = JsonConvert.DeserializeObject<UserConfigModel>(File.ReadAllText(UserConfigPath));
                txtToMailAddress.Text = userconfig.ToMailAddress = EncodeMD5.DecryptString(userconfig.ToMailAddress, "ITFramasBDVN");
                txtCCMailAddress.Text = userconfig.CCMailAddress = EncodeMD5.DecryptString(userconfig.CCMailAddress, "ITFramasBDVN");
                txtMaterial.Text = EncodeMD5.DecryptString(userconfig.Mixing_Material_BoxWeight.ToString(), "ITFramasBDVN");
                userconfig.Mixing_Material_BoxWeight = txtMaterial.Text;
                txtRecycle.Text = EncodeMD5.DecryptString(userconfig.Mixing_Recycle_BoxWeight.ToString(), "ITFramasBDVN");
                userconfig.Mixing_Recycle_BoxWeight = txtRecycle.Text;
                txtIncoming.Text = EncodeMD5.DecryptString(userconfig.Incoming_BoxWeight.ToString(), "ITFramasBDVN");
                userconfig.Incoming_BoxWeight =txtIncoming.Text;
                txtCrushing.Text = EncodeMD5.DecryptString(userconfig.Crushing_BoxWeight.ToString(), "ITFramasBDVN");
                userconfig.Crushing_BoxWeight = txtCrushing.Text;
            }
            else
            {
                userconfig = new UserConfigModel();
                txtToMailAddress.Text = userconfig.ToMailAddress = "sang.nguyen@framas.com";
                txtCCMailAddress.Text = userconfig.CCMailAddress = "sang.nguyen@framas.com";
                txtMaterial.Text = userconfig.Mixing_Material_BoxWeight= "0.16";
                txtRecycle.Text =   userconfig.Mixing_Recycle_BoxWeight ="1.14";
                txtIncoming.Text =  userconfig.Incoming_BoxWeight = "2.1966";
                txtCrushing.Text =  userconfig.Crushing_BoxWeight = "1.14";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveUserConfigConfig())
            {
                XtraMessageBox.Show("Saved changes");
            }
            else
            {
                XtraMessageBox.Show("Save failed!");
            }

        }
        private bool SaveUserConfigConfig()
        {
            try
            {
                userconfig.ToMailAddress = EncodeMD5.EncryptString(txtToMailAddress.Text, "ITFramasBDVN");
                userconfig.CCMailAddress = EncodeMD5.EncryptString(txtCCMailAddress.Text, "ITFramasBDVN");
                userconfig.Mixing_Material_BoxWeight =EncodeMD5.EncryptString(txtMaterial.Text, "ITFramasBDVN");
                userconfig.Mixing_Recycle_BoxWeight = EncodeMD5.EncryptString(txtRecycle.Text, "ITFramasBDVN");
                userconfig.Incoming_BoxWeight = EncodeMD5.EncryptString(txtIncoming.Text, "ITFramasBDVN");
                userconfig.Crushing_BoxWeight = EncodeMD5.EncryptString(txtCrushing.Text, "ITFramasBDVN");
                string json = JsonConvert.SerializeObject(userconfig, Formatting.Indented);
                File.WriteAllText(UserConfigPath, json);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}