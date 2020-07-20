namespace RecycledManagement
{
    partial class userControlBookingOrders_List
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
            this.grcBookingOrder = new DevExpress.XtraGrid.GridControl();
            this.grvBookingOrder = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.grcBookingOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvBookingOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // grcBookingOrder
            // 
            this.grcBookingOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcBookingOrder.Location = new System.Drawing.Point(0, 0);
            this.grcBookingOrder.MainView = this.grvBookingOrder;
            this.grcBookingOrder.Name = "grcBookingOrder";
            this.grcBookingOrder.Size = new System.Drawing.Size(896, 460);
            this.grcBookingOrder.TabIndex = 0;
            this.grcBookingOrder.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grvBookingOrder});
            // 
            // grvBookingOrder
            // 
            this.grvBookingOrder.GridControl = this.grcBookingOrder;
            this.grvBookingOrder.Name = "grvBookingOrder";
            this.grvBookingOrder.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
            this.grvBookingOrder.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.grvBookingOrder_ShowingEditor);
            // 
            // userControlBookingOrders_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grcBookingOrder);
            this.Name = "userControlBookingOrders_List";
            this.Size = new System.Drawing.Size(896, 460);
            this.Load += new System.EventHandler(this.userControlBookingOrders_List_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grcBookingOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvBookingOrder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grcBookingOrder;
        private DevExpress.XtraGrid.Views.Grid.GridView grvBookingOrder;
    }
}
