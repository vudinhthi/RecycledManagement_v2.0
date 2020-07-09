namespace RecycledManagement
{
    partial class userControlOtherSources_List
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
            this.grcOtherSource = new DevExpress.XtraGrid.GridControl();
            this.grvOtherSource = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.grcOtherSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvOtherSource)).BeginInit();
            this.SuspendLayout();
            // 
            // grcOtherSource
            // 
            this.grcOtherSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcOtherSource.Location = new System.Drawing.Point(0, 0);
            this.grcOtherSource.MainView = this.grvOtherSource;
            this.grcOtherSource.Name = "grcOtherSource";
            this.grcOtherSource.Size = new System.Drawing.Size(640, 336);
            this.grcOtherSource.TabIndex = 0;
            this.grcOtherSource.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvOtherSource});
            // 
            // grvOtherSource
            // 
            this.grvOtherSource.GridControl = this.grcOtherSource;
            this.grvOtherSource.Name = "grvOtherSource";
            // 
            // userControlOtherSources_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grcOtherSource);
            this.Name = "userControlOtherSources_List";
            this.Size = new System.Drawing.Size(640, 336);
            this.Load += new System.EventHandler(this.userControlOtherSources_List_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grcOtherSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvOtherSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grcOtherSource;
        private DevExpress.XtraGrid.Views.Grid.GridView grvOtherSource;
    }
}
