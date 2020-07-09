using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using RecycledManagement.Common;
using System;
using System.Data;
using System.Linq;

namespace RecycledManagement
{
    public partial class userControlMixing_List : DevExpress.XtraEditors.XtraUserControl
    {
        private DataTable dt;

        public userControlMixing_List()
        {
            InitializeComponent();

        }
        private void userControlMixing_List_Load(object sender, EventArgs e)
        {
            LoadListMixed();
        }
        private void LoadListMixed()
        {
            dt = DbMixing.Instance.GetMixeds();
            gridMixed.DataSource = dt;

            gridViewMixed.OptionsView.ColumnAutoWidth = true;
            gridViewMixed.Columns["MixId"].VisibleIndex = -1;
            gridViewMixed.Columns["ShiftId"].VisibleIndex = -1;
            gridViewMixed.Columns["OperatorId"].VisibleIndex = -1;
            gridViewMixed.Columns["OrderId"].VisibleIndex = -1;

            gridViewMixed.Columns["MixId"].VisibleIndex = 1;
            gridViewMixed.Columns["CreatedDate"].VisibleIndex = 2;
            gridViewMixed.Columns["ShiftName"].VisibleIndex = 3;
            gridViewMixed.Columns["OperatorName"].VisibleIndex = 4;
            gridViewMixed.Columns["Machine"].VisibleIndex = 5;
            gridViewMixed.Columns["OrderCode"].VisibleIndex = 6;
            gridViewMixed.Columns["ItemCode"].VisibleIndex = 7;
            gridViewMixed.Columns["ItemName"].VisibleIndex = 8;
            gridViewMixed.Columns["WeightMaterialTotal"].VisibleIndex = 9;
            gridViewMixed.Columns["WeightRecycledTotal"].VisibleIndex = 10;
            gridViewMixed.Columns["WeightMixTotal"].VisibleIndex = 11;

            gridViewMixed.Columns["MixId"].Width = 20;
            gridViewMixed.Columns["CreatedDate"].Width = 50;
            gridViewMixed.Columns["ShiftName"].Width = 30;
            gridViewMixed.Columns["OperatorName"].Width = 40;
            gridViewMixed.Columns["Machine"].Width = 40;
            gridViewMixed.Columns["WeightMaterialTotal"].Width = 40;
            gridViewMixed.Columns["WeightRecycledTotal"].Width = 40;
            gridViewMixed.Columns["WeightMixTotal"].Width = 40;

            gridViewMixed.Columns["WeightMaterialTotal"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridViewMixed.Columns["WeightMaterialTotal"].DisplayFormat.FormatString = "{0:n2}";
            gridViewMixed.Columns["WeightRecycledTotal"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridViewMixed.Columns["WeightRecycledTotal"].DisplayFormat.FormatString = "{0:n2}";
            gridViewMixed.Columns["WeightMixTotal"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridViewMixed.Columns["WeightMixTotal"].DisplayFormat.FormatString = "{0:n2}";

            //Create total of 3 columns
            GridColumnSummaryItem totalWeightMaterial = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                "WeightMaterialTotal", "{0:n2}");
            gridViewMixed.Columns["WeightMaterialTotal"].Summary.Add(totalWeightMaterial);
            GridColumnSummaryItem totalWeightRecycled = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                "WeightRecycledTotal", "{0:n2}");
            gridViewMixed.Columns["WeightRecycledTotal"].Summary.Add(totalWeightRecycled);
            GridColumnSummaryItem totalWeightTotal = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum,
                "WeightMixTotal", "{0:n2}");
            gridViewMixed.Columns["WeightMixTotal"].Summary.Add(totalWeightTotal);

            //Display format for column
            gridViewMixed.Columns["CreatedDate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridViewMixed.Columns["CreatedDate"].DisplayFormat.FormatString = "MM/dd/yyyy HH:mm:ss";

            gridViewMixed.MoveFirst();

            gridViewMixed.OptionsBehavior.EditingMode = GridEditingMode.EditForm;
            gridViewMixed.OptionsEditForm.CustomEditFormLayout = new userControlMixing();
            gridViewMixed.OptionsEditForm.PopupEditFormWidth = 1200;
            gridViewMixed.OptionsEditForm.FormCaptionFormat = "{ItemCode} - Editing Mixing";
        }
    }
}
