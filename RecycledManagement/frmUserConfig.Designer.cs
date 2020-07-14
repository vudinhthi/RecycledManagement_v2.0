namespace RecycledManagement
{
    partial class frmUserConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserConfig));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtToMailAddress = new DevExpress.XtraEditors.TextEdit();
            this.txtCCMailAddress = new DevExpress.XtraEditors.TextEdit();
            this.txtMaterial = new DevExpress.XtraEditors.TextEdit();
            this.txtRecycle = new DevExpress.XtraEditors.TextEdit();
            this.txtIncoming = new DevExpress.XtraEditors.TextEdit();
            this.txtCrushing = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.fs = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtToMailAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCCMailAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaterial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRecycle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIncoming.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCrushing.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.txtToMailAddress);
            this.layoutControl1.Controls.Add(this.txtCCMailAddress);
            this.layoutControl1.Controls.Add(this.txtMaterial);
            this.layoutControl1.Controls.Add(this.txtRecycle);
            this.layoutControl1.Controls.Add(this.txtIncoming);
            this.layoutControl1.Controls.Add(this.txtCrushing);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(903, 599);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnSave.Location = new System.Drawing.Point(12, 551);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(879, 36);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtToMailAddress
            // 
            this.txtToMailAddress.Location = new System.Drawing.Point(24, 66);
            this.txtToMailAddress.Margin = new System.Windows.Forms.Padding(4);
            this.txtToMailAddress.Name = "txtToMailAddress";
            this.txtToMailAddress.Properties.Mask.EditMask = "\\s*([a-zA-Z0-9_%+~=$&*!#?\\-\\\'](\\.)?)*[a-zA-Z0-9_%+~=$&*!#?\\-\\\']@(framas\\.\\com)\\s*" +
    "((,|;)\\s*([a-zA-Z0-9_%+~=$&*!#?\\-\\\'](\\.)?)*[a-zA-Z0-9_%+~=$&*!#?\\-\\\']@(framas\\.\\" +
    "com)\\s*)*";
            this.txtToMailAddress.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtToMailAddress.Size = new System.Drawing.Size(855, 24);
            this.txtToMailAddress.StyleController = this.layoutControl1;
            this.txtToMailAddress.TabIndex = 1;
            // 
            // txtCCMailAddress
            // 
            this.txtCCMailAddress.Location = new System.Drawing.Point(24, 115);
            this.txtCCMailAddress.Margin = new System.Windows.Forms.Padding(4);
            this.txtCCMailAddress.Name = "txtCCMailAddress";
            this.txtCCMailAddress.Properties.Mask.EditMask = "\\s*([a-zA-Z0-9_%+~=$&*!#?\\-\\\'](\\.)?)*[a-zA-Z0-9_%+~=$&*!#?\\-\\\']@(framas\\.\\com)\\s*" +
    "((,|;)\\s*([a-zA-Z0-9_%+~=$&*!#?\\-\\\'](\\.)?)*[a-zA-Z0-9_%+~=$&*!#?\\-\\\']@(framas\\.\\" +
    "com)\\s*)*";
            this.txtCCMailAddress.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtCCMailAddress.Size = new System.Drawing.Size(855, 24);
            this.txtCCMailAddress.StyleController = this.layoutControl1;
            this.txtCCMailAddress.TabIndex = 1;
            // 
            // txtMaterial
            // 
            this.txtMaterial.Location = new System.Drawing.Point(36, 242);
            this.txtMaterial.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaterial.Name = "txtMaterial";
            this.txtMaterial.Properties.Mask.EditMask = "n3";
            this.txtMaterial.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtMaterial.Size = new System.Drawing.Size(268, 24);
            this.txtMaterial.StyleController = this.layoutControl1;
            this.txtMaterial.TabIndex = 1;
            // 
            // txtRecycle
            // 
            this.txtRecycle.Location = new System.Drawing.Point(36, 291);
            this.txtRecycle.Margin = new System.Windows.Forms.Padding(4);
            this.txtRecycle.Name = "txtRecycle";
            this.txtRecycle.Properties.Mask.EditMask = "n3";
            this.txtRecycle.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtRecycle.Size = new System.Drawing.Size(268, 24);
            this.txtRecycle.StyleController = this.layoutControl1;
            this.txtRecycle.TabIndex = 1;
            // 
            // txtIncoming
            // 
            this.txtIncoming.Location = new System.Drawing.Point(332, 242);
            this.txtIncoming.Margin = new System.Windows.Forms.Padding(4);
            this.txtIncoming.Name = "txtIncoming";
            this.txtIncoming.Properties.Mask.EditMask = "n4";
            this.txtIncoming.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtIncoming.Size = new System.Drawing.Size(257, 24);
            this.txtIncoming.StyleController = this.layoutControl1;
            this.txtIncoming.TabIndex = 1;
            // 
            // txtCrushing
            // 
            this.txtCrushing.Location = new System.Drawing.Point(617, 242);
            this.txtCrushing.Margin = new System.Windows.Forms.Padding(4);
            this.txtCrushing.Name = "txtCrushing";
            this.txtCrushing.Properties.Mask.EditMask = "n3";
            this.txtCrushing.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCrushing.Size = new System.Drawing.Size(250, 24);
            this.txtCrushing.StyleController = this.layoutControl1;
            this.txtCrushing.TabIndex = 1;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup4,
            this.layoutControlGroup2,
            this.layoutControlItem3,
            this.emptySpaceItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(903, 599);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "DATABASE";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7,
            this.layoutControlItem1});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(883, 143);
            this.layoutControlGroup4.Text = "MAILS";
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.txtToMailAddress;
            this.layoutControlItem7.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem7.CustomizationFormText = "Server name:";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(859, 49);
            this.layoutControlItem7.Text = "To:";
            this.layoutControlItem7.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(84, 18);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtCCMailAddress;
            this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem1.CustomizationFormText = "Server name:";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 49);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(859, 49);
            this.layoutControlItem1.Text = "CC:";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(84, 18);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "DATABASE";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.layoutControlGroup5,
            this.layoutControlGroup6});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 143);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(883, 188);
            this.layoutControlGroup2.Text = "BOX WEIGHT";
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "DATABASE";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.fs});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(296, 143);
            this.layoutControlGroup3.Text = "Mixing";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtMaterial;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "Server name:";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(272, 49);
            this.layoutControlItem2.Text = "Material (Kg):";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(84, 18);
            // 
            // fs
            // 
            this.fs.Control = this.txtRecycle;
            this.fs.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.fs.CustomizationFormText = "Server name:";
            this.fs.Location = new System.Drawing.Point(0, 49);
            this.fs.Name = "fs";
            this.fs.Size = new System.Drawing.Size(272, 49);
            this.fs.Text = "Recycle (Kg):";
            this.fs.TextLocation = DevExpress.Utils.Locations.Top;
            this.fs.TextSize = new System.Drawing.Size(84, 18);
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "DATABASE";
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup5.Location = new System.Drawing.Point(296, 0);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Size = new System.Drawing.Size(285, 143);
            this.layoutControlGroup5.Text = "Incoming";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtIncoming;
            this.layoutControlItem4.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem4.CustomizationFormText = "Server name:";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(261, 98);
            this.layoutControlItem4.Text = "Incoming (Kg):";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(84, 18);
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.CustomizationFormText = "DATABASE";
            this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.layoutControlGroup6.Location = new System.Drawing.Point(581, 0);
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.Size = new System.Drawing.Size(278, 143);
            this.layoutControlGroup6.Text = "Crushing";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtCrushing;
            this.layoutControlItem5.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem5.CustomizationFormText = "Server name:";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(254, 98);
            this.layoutControlItem5.Text = "Crushing (Kg):";
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(84, 18);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnSave;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 539);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(883, 40);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 331);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(883, 208);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmUserConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 599);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.Image = global::RecycledManagement.Properties.Resources.framas_mini__black_;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUserConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Config";
            this.Load += new System.EventHandler(this.Userconfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtToMailAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCCMailAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaterial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRecycle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIncoming.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCrushing.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit txtToMailAddress;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.TextEdit txtCCMailAddress;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraEditors.TextEdit txtMaterial;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit txtRecycle;
        private DevExpress.XtraLayout.LayoutControlItem fs;
        private DevExpress.XtraEditors.TextEdit txtIncoming;
        private DevExpress.XtraEditors.TextEdit txtCrushing;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}