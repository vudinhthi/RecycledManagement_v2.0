namespace RecycledManagement
{
    partial class IncomingUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IncomingUserControl));
            this.lookUpShift = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtNetWeight = new DevExpress.XtraEditors.TextEdit();
            this.txtWeight = new DevExpress.XtraEditors.TextEdit();
            this.lookUpReason = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpOtherSource = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpMaterial = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpMixCode = new DevExpress.XtraEditors.LookUpEdit();
            this.radLossType = new DevExpress.XtraEditors.RadioGroup();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.lookUpShift.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNetWeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpReason.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpOtherSource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpMaterial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpMixCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLossType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            this.SuspendLayout();
            // 
            // lookUpShift
            // 
            this.SetBoundPropertyName(this.lookUpShift, "");
            this.lookUpShift.Location = new System.Drawing.Point(153, 89);
            this.lookUpShift.Name = "lookUpShift";
            this.lookUpShift.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpShift.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lookUpShift.Size = new System.Drawing.Size(868, 24);
            this.lookUpShift.StyleController = this.layoutControl1;
            this.lookUpShift.TabIndex = 1;
            // 
            // layoutControl1
            // 
            this.SetBoundPropertyName(this.layoutControl1, "");
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.txtNetWeight);
            this.layoutControl1.Controls.Add(this.txtWeight);
            this.layoutControl1.Controls.Add(this.lookUpReason);
            this.layoutControl1.Controls.Add(this.lookUpOtherSource);
            this.layoutControl1.Controls.Add(this.lookUpMaterial);
            this.layoutControl1.Controls.Add(this.lookUpMixCode);
            this.layoutControl1.Controls.Add(this.radLossType);
            this.layoutControl1.Controls.Add(this.lookUpShift);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1045, 351);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Appearance.Options.UseFont = true;
            this.SetBoundPropertyName(this.btnSave, "");
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.Location = new System.Drawing.Point(524, 303);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(509, 36);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtNetWeight
            // 
            this.SetBoundPropertyName(this.txtNetWeight, "");
            this.txtNetWeight.EditValue = "0";
            this.txtNetWeight.Location = new System.Drawing.Point(657, 263);
            this.txtNetWeight.Name = "txtNetWeight";
            this.txtNetWeight.Properties.Appearance.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtNetWeight.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtNetWeight.Properties.Appearance.Options.UseFont = true;
            this.txtNetWeight.Properties.Appearance.Options.UseForeColor = true;
            this.txtNetWeight.Properties.Appearance.Options.UseTextOptions = true;
            this.txtNetWeight.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtNetWeight.Properties.Mask.EditMask = "n2";
            this.txtNetWeight.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtNetWeight.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtNetWeight.Size = new System.Drawing.Size(364, 24);
            this.txtNetWeight.StyleController = this.layoutControl1;
            this.txtNetWeight.TabIndex = 8;
            // 
            // txtWeight
            // 
            this.SetBoundPropertyName(this.txtWeight, "");
            this.txtWeight.EditValue = "0";
            this.txtWeight.Location = new System.Drawing.Point(153, 263);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Properties.Appearance.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtWeight.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtWeight.Properties.Appearance.Options.UseFont = true;
            this.txtWeight.Properties.Appearance.Options.UseForeColor = true;
            this.txtWeight.Properties.Appearance.Options.UseTextOptions = true;
            this.txtWeight.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtWeight.Properties.Mask.EditMask = "n2";
            this.txtWeight.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtWeight.Properties.ReadOnly = true;
            this.txtWeight.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtWeight.Size = new System.Drawing.Size(371, 24);
            this.txtWeight.StyleController = this.layoutControl1;
            this.txtWeight.TabIndex = 7;
            this.txtWeight.EditValueChanged += new System.EventHandler(this.txtWeight_EditValueChanged);
            // 
            // lookUpReason
            // 
            this.SetBoundPropertyName(this.lookUpReason, "");
            this.lookUpReason.Enabled = false;
            this.lookUpReason.Location = new System.Drawing.Point(665, 190);
            this.lookUpReason.Name = "lookUpReason";
            this.lookUpReason.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpReason.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lookUpReason.Size = new System.Drawing.Size(356, 24);
            this.lookUpReason.StyleController = this.layoutControl1;
            this.lookUpReason.TabIndex = 6;
            // 
            // lookUpOtherSource
            // 
            this.SetBoundPropertyName(this.lookUpOtherSource, "");
            this.lookUpOtherSource.Enabled = false;
            this.lookUpOtherSource.Location = new System.Drawing.Point(665, 162);
            this.lookUpOtherSource.Name = "lookUpOtherSource";
            this.lookUpOtherSource.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpOtherSource.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lookUpOtherSource.Size = new System.Drawing.Size(356, 24);
            this.lookUpOtherSource.StyleController = this.layoutControl1;
            this.lookUpOtherSource.TabIndex = 5;
            // 
            // lookUpMaterial
            // 
            this.SetBoundPropertyName(this.lookUpMaterial, "");
            this.lookUpMaterial.Enabled = false;
            this.lookUpMaterial.Location = new System.Drawing.Point(153, 190);
            this.lookUpMaterial.Name = "lookUpMaterial";
            this.lookUpMaterial.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpMaterial.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.lookUpMaterial.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoSuggest;
            this.lookUpMaterial.Size = new System.Drawing.Size(355, 24);
            this.lookUpMaterial.StyleController = this.layoutControl1;
            this.lookUpMaterial.TabIndex = 4;
            // 
            // lookUpMixCode
            // 
            this.SetBoundPropertyName(this.lookUpMixCode, "");
            this.lookUpMixCode.Enabled = false;
            this.lookUpMixCode.Location = new System.Drawing.Point(153, 162);
            this.lookUpMixCode.Name = "lookUpMixCode";
            this.lookUpMixCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpMixCode.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.lookUpMixCode.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoSuggest;
            this.lookUpMixCode.Size = new System.Drawing.Size(355, 24);
            this.lookUpMixCode.StyleController = this.layoutControl1;
            this.lookUpMixCode.TabIndex = 3;
            // 
            // radLossType
            // 
            this.SetBoundPropertyName(this.radLossType, "");
            this.radLossType.Location = new System.Drawing.Point(153, 45);
            this.radLossType.Name = "radLossType";
            this.radLossType.Size = new System.Drawing.Size(868, 40);
            this.radLossType.StyleController = this.layoutControl1;
            this.radLossType.TabIndex = 2;
            this.radLossType.SelectedIndexChanged += new System.EventHandler(this.radLossType_SelectedIndexChanged);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlGroup1,
            this.layoutControlGroup2,
            this.layoutControlGroup4,
            this.layoutControlGroup3,
            this.layoutControlItem9});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1045, 351);
            this.Root.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 291);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(512, 40);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1025, 117);
            this.layoutControlGroup1.Text = "Incoming Info";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.radLossType;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1001, 44);
            this.layoutControlItem2.Text = "Loss Type";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(126, 18);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.lookUpShift;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 44);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1001, 28);
            this.layoutControlItem1.Text = "Shift";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(126, 18);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 117);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(512, 101);
            this.layoutControlGroup2.Text = "From Booking Order";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.lookUpMixCode;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(488, 28);
            this.layoutControlItem3.Text = "MixCode";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(126, 18);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lookUpMaterial;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(488, 28);
            this.layoutControlItem4.Text = "Material Code";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(126, 18);
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7,
            this.layoutControlItem8});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 218);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(1025, 73);
            this.layoutControlGroup4.Text = "Weight Recycled";
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.txtWeight;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(504, 28);
            this.layoutControlItem7.Text = "Weight Recycled (Kg)";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(126, 18);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.txtNetWeight;
            this.layoutControlItem8.Location = new System.Drawing.Point(504, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(497, 28);
            this.layoutControlItem8.Text = "Net Weight (-2.1966)";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(126, 18);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup3.Location = new System.Drawing.Point(512, 117);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(513, 101);
            this.layoutControlGroup3.Text = "Other Source And Reason";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.lookUpOtherSource;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(489, 28);
            this.layoutControlItem5.Text = "Other Source";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(126, 18);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.lookUpReason;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(489, 28);
            this.layoutControlItem6.Text = "Reason";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(126, 18);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnSave;
            this.layoutControlItem9.Location = new System.Drawing.Point(512, 291);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(513, 40);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // IncomingUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "IncomingUserControl";
            this.Size = new System.Drawing.Size(1045, 351);
            this.Load += new System.EventHandler(this.IncomingUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lookUpShift.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNetWeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpReason.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpOtherSource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpMaterial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpMixCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLossType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit lookUpShift;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit txtNetWeight;
        private DevExpress.XtraEditors.TextEdit txtWeight;
        private DevExpress.XtraEditors.LookUpEdit lookUpReason;
        private DevExpress.XtraEditors.LookUpEdit lookUpOtherSource;
        private DevExpress.XtraEditors.LookUpEdit lookUpMaterial;
        private DevExpress.XtraEditors.LookUpEdit lookUpMixCode;
        private DevExpress.XtraEditors.RadioGroup radLossType;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private System.Windows.Forms.Timer timer1;
    }
}
