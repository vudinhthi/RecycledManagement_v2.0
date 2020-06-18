namespace RecycledManagement
{
    partial class userControlOperators_List
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
            this.grcOperator = new DevExpress.XtraGrid.GridControl();
            this.grvOperator = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.grcOperator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvOperator)).BeginInit();
            this.SuspendLayout();
            // 
            // grcOperator
            // 
            this.grcOperator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcOperator.Location = new System.Drawing.Point(0, 0);
            this.grcOperator.MainView = this.grvOperator;
            this.grcOperator.Name = "grcOperator";
            this.grcOperator.Size = new System.Drawing.Size(640, 336);
            this.grcOperator.TabIndex = 0;
            this.grcOperator.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvOperator});
            // 
            // grvOperator
            // 
            this.grvOperator.GridControl = this.grcOperator;
            this.grvOperator.Name = "grvOperator";
            // 
            // userControlOperators_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grcOperator);
            this.Name = "userControlOperators_List";
            this.Size = new System.Drawing.Size(640, 336);
            this.Load += new System.EventHandler(this.userControlOperators_List_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grcOperator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvOperator)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grcOperator;
        private DevExpress.XtraGrid.Views.Grid.GridView grvOperator;
    }
}
