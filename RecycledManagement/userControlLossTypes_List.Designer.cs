namespace RecycledManagement
{
    partial class userControlLossTypes_List
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
            this.grcLossType = new DevExpress.XtraGrid.GridControl();
            this.grvLossType = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.grcLossType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvLossType)).BeginInit();
            this.SuspendLayout();
            // 
            // grcLossType
            // 
            this.grcLossType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcLossType.Location = new System.Drawing.Point(0, 0);
            this.grcLossType.MainView = this.grvLossType;
            this.grcLossType.Name = "grcLossType";
            this.grcLossType.Size = new System.Drawing.Size(640, 336);
            this.grcLossType.TabIndex = 0;
            this.grcLossType.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvLossType});
            // 
            // grvLossType
            // 
            this.grvLossType.GridControl = this.grcLossType;
            this.grvLossType.Name = "grvLossType";
            // 
            // userControlLossTypes_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grcLossType);
            this.Name = "userControlLossTypes_List";
            this.Size = new System.Drawing.Size(640, 336);
            this.Load += new System.EventHandler(this.userControlLossTypes_List_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grcLossType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvLossType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grcLossType;
        private DevExpress.XtraGrid.Views.Grid.GridView grvLossType;
    }
}
