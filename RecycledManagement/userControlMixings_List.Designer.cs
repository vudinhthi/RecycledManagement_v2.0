namespace RecycledManagement
{
    partial class userControlMixing_List
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
            this.gridMixed = new DevExpress.XtraGrid.GridControl();
            this.gridViewMixed = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridMixed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMixed)).BeginInit();
            this.SuspendLayout();
            // 
            // gridMixed
            // 
            this.gridMixed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMixed.Location = new System.Drawing.Point(0, 0);
            this.gridMixed.MainView = this.gridViewMixed;
            this.gridMixed.Name = "gridMixed";
            this.gridMixed.Size = new System.Drawing.Size(1461, 641);
            this.gridMixed.TabIndex = 0;
            this.gridMixed.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMixed});
            // 
            // gridViewMixed
            // 
            this.gridViewMixed.GridControl = this.gridMixed;
            this.gridViewMixed.Name = "gridViewMixed";
            this.gridViewMixed.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewMixed.OptionsBehavior.ReadOnly = true;
            this.gridViewMixed.OptionsPrint.PrintHeader = false;
            this.gridViewMixed.OptionsPrint.PrintPreview = true;
            // 
            // userControlMixing_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridMixed);
            this.Name = "userControlMixing_List";
            this.Size = new System.Drawing.Size(1461, 641);
            this.Load += new System.EventHandler(this.userControlMixing_List_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridMixed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMixed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridMixed;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMixed;
    }
}
