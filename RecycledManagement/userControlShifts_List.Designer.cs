namespace RecycledManagement
{
    partial class userControlShifts_List
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
            this.gridControlShift = new DevExpress.XtraGrid.GridControl();
            this.gridViewShift = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewShift)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlShift
            // 
            this.gridControlShift.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlShift.Location = new System.Drawing.Point(0, 0);
            this.gridControlShift.MainView = this.gridViewShift;
            this.gridControlShift.Name = "gridControlShift";
            this.gridControlShift.Size = new System.Drawing.Size(640, 336);
            this.gridControlShift.TabIndex = 0;
            this.gridControlShift.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewShift});
            // 
            // gridViewShift
            // 
            this.gridViewShift.GridControl = this.gridControlShift;
            this.gridViewShift.Name = "gridViewShift";
            this.gridViewShift.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewShift_RowClick);
            // 
            // userControlShifts_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlShift);
            this.Name = "userControlShifts_List";
            this.Size = new System.Drawing.Size(640, 336);
            this.Load += new System.EventHandler(this.userControlShifts_List_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewShift)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlShift;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewShift;
    }
}
