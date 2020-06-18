namespace RecycledManagement
{
    partial class userControlReasons_List
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
            this.grcReason = new DevExpress.XtraGrid.GridControl();
            this.grvReason = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.grcReason)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvReason)).BeginInit();
            this.SuspendLayout();
            // 
            // grcReason
            // 
            this.grcReason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcReason.Location = new System.Drawing.Point(0, 0);
            this.grcReason.MainView = this.grvReason;
            this.grcReason.Name = "grcReason";
            this.grcReason.Size = new System.Drawing.Size(640, 336);
            this.grcReason.TabIndex = 0;
            this.grcReason.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvReason});
            // 
            // grvReason
            // 
            this.grvReason.GridControl = this.grcReason;
            this.grvReason.Name = "grvReason";
            // 
            // userControlReasons_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grcReason);
            this.Name = "userControlReasons_List";
            this.Size = new System.Drawing.Size(640, 336);
            this.Load += new System.EventHandler(this.userControlReasons_List_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grcReason)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvReason)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grcReason;
        private DevExpress.XtraGrid.Views.Grid.GridView grvReason;
    }
}
