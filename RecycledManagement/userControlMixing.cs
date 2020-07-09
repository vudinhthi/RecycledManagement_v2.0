using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using RecycledManagement.Common;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace RecycledManagement
{
    public partial class userControlMixing : EditFormUserControl
    {
        private DataTable dt;
        public userControlMixing()
        {
            InitializeComponent();            
            //this.SetBoundFieldName(lueOrderId, "ItemCode");
        }
        private void UserControlMixing_Load(object sender, EventArgs e)
        {
            LoadItems();
            LoadShifts();
            LoadOprators();
            LoadReasons();
            SetEnableRecycled();            
        }
        //Use for check Recycled Using
        private void CheckUsingRecycled_CheckedChanged(object sender, EventArgs e)
        {
            SetEnableRecycled();
        }
        //Set enable or not for Recycle Lots if check using Recycled is True
        private void SetEnableRecycled()
        {
            if (checkUsingRecycled.Checked)
            {
                lueReasonMixing.Enabled = false;
                lueRecycled1.Enabled = true;
                lueRecycled2.Enabled = true;
                lueCompound.Enabled = true;
                lueClearRecycled.Enabled = true;
                lueFramapur.Enabled = true;
                lueLeftover.Enabled = true;

                LoadRecycled();
                LoadCompound();
                LoadClearRecycled();
                LoadFramafur();
                LoadLeftOver();
            }
            else
            {
                lueReasonMixing.Enabled = true;
                lueRecycled1.Enabled = false;
                lueRecycled2.Enabled = false;
                lueCompound.Enabled = false;
                lueClearRecycled.Enabled = false;
                lueFramapur.Enabled = false;
                lueLeftover.Enabled = false;
            }
        }
        //Load list of Shifts show to form
        private void LoadShifts()
        {
            dt = DbMixing.Instance.GetShifts();
            lueShifts.Properties.DataSource = dt;
            lueShifts.Properties.DisplayMember = "ShiftName";
            lueShifts.Properties.ValueMember = "ShiftId";
            lueShifts.Properties.Columns.Add(new LookUpColumnInfo("ShiftName", "ShiftName"));                      
        }
        //Load list of Operator for Mixing Station
        private void LoadOprators()
        {
            dt = DbMixing.Instance.GetOperators(1);
            lueOperators.Properties.DataSource = dt;
            lueOperators.Properties.DisplayMember = "OperatorName";
            lueOperators.Properties.ValueMember = "OperatorId";
            lueOperators.Properties.Columns.Add(new LookUpColumnInfo("OperatorName", "OperatorName"));
        }
        //Load list of Reasons
        private void LoadReasons()
        {
            dt = DbMixing.Instance.GetReasons(1);
            lueReasonMixing.Properties.DataSource = dt;
            lueReasonMixing.Properties.DisplayMember = "ReasonName";
            lueReasonMixing.Properties.ValueMember = "ReasonId";
            lueReasonMixing.Properties.Columns.Add(new LookUpColumnInfo("ReasonName", "ReasonName"));
        }
        //Load List of Items
        private void LoadItems()
        {
            dt = DbMixing.Instance.GetItems();
            lueOrderId.Properties.DataSource = dt;
            lueOrderId.Properties.DisplayMember = "c002";
            lueOrderId.Properties.ValueMember = "c002";
            lueOrderId.Properties.Columns.Add(new LookUpColumnInfo("c002", "ProductCode", 100));
            lueOrderId.Properties.Columns.Add(new LookUpColumnInfo("c003", "ProductName", 300));            
        }        
        //Load list of Recycled
        private void LoadRecycled()
        {
            dt = DbMixing.Instance.GetRecycledByLossType(1);
            lueRecycled1.Properties.DataSource = dt;
            lueRecycled1.Properties.DisplayMember = "CrushedCode";
            lueRecycled1.Properties.ValueMember = "CrushedId";
            lueRecycled1.Properties.Columns.Add(new LookUpColumnInfo("CrushedId", "CrushedId"));
            lueRecycled1.Properties.Columns.Add(new LookUpColumnInfo("CrushedCode", "CrushedCode"));

            lueRecycled2.Properties.DataSource = dt;
            lueRecycled2.Properties.DisplayMember = "CrushedCode";
            lueRecycled2.Properties.ValueMember = "CrushedId";
            lueRecycled2.Properties.Columns.Add(new LookUpColumnInfo("CrushedId", "CrushedId"));
            lueRecycled2.Properties.Columns.Add(new LookUpColumnInfo("CrushedCode", "CrushedCode"));
        }

        private void LoadCompound()
        {
            dt = DbMixing.Instance.GetRecycledByLossType(13);
            lueCompound.Properties.DataSource = dt;
            lueCompound.Properties.DisplayMember = "CrushedCode";
            lueCompound.Properties.ValueMember = "CrushedId";
            lueCompound.Properties.Columns.Add(new LookUpColumnInfo("CrushedId", "CrushedId"));
            lueCompound.Properties.Columns.Add(new LookUpColumnInfo("CrushedCode", "CrushedCode"));
        }

        private void LoadClearRecycled()
        {
            dt = DbMixing.Instance.GetRecycledByLossType(14);
            lueClearRecycled.Properties.DataSource = dt;
            lueClearRecycled.Properties.DisplayMember = "CrushedCode";
            lueClearRecycled.Properties.ValueMember = "CrushedId";
            lueClearRecycled.Properties.Columns.Add(new LookUpColumnInfo("CrushedId", "CrushedId"));
            lueClearRecycled.Properties.Columns.Add(new LookUpColumnInfo("CrushedCode", "CrushedCode"));
        }

        private void LoadFramafur()
        {
            dt = DbMixing.Instance.GetRecycledByLossType(2);
            lueFramapur.Properties.DataSource = dt;
            lueFramapur.Properties.DisplayMember = "CrushedCode";
            lueFramapur.Properties.ValueMember = "CrushedId";
            lueFramapur.Properties.Columns.Add(new LookUpColumnInfo("CrushedId", "CrushedId"));
            lueFramapur.Properties.Columns.Add(new LookUpColumnInfo("CrushedCode", "CrushedCode"));
        }

        private void LoadLeftOver()
        {
            dt = DbMixing.Instance.GetRecycledByLossType(15);
            lueLeftover.Properties.DataSource = dt;
            lueLeftover.Properties.DisplayMember = "CrushedCode";
            lueLeftover.Properties.ValueMember = "CrushedId";
            lueLeftover.Properties.Columns.Add(new LookUpColumnInfo("CrushedId", "CrushedId"));
            lueLeftover.Properties.Columns.Add(new LookUpColumnInfo("CrushedCode", "CrushedCode"));
        }        

        public static Control FindFocusedControl(Control control)
        {
            var container = control as IContainerControl;
            while (container != null)
            {
                control = container.ActiveControl;
                container = control as IContainerControl;
            }
            return control;
        }

        private void tedWeightMaterial_EditValueChanged(object sender, EventArgs e)
        {
            LoadGridMaterial(lueOrderId.GetColumnValue("c002").ToString(), tedWeightMaterial.EditValue.ToString());
        }

        private void LoadGridMaterial(string orderId, string weightMaterialConsumption)
        {
            dt = DbMixing.Instance.GetMaterialByItems(orderId);
            gridMaterialConsumption.DataSource = dt;

            gridViewMaterialConsumption.PopulateColumns();
            gridViewMaterialConsumption.Columns["ID"].VisibleIndex = -1;
            gridViewMaterialConsumption.Columns["productcode"].VisibleIndex = -1;

            gridViewMaterialConsumption.Columns["materialcode"].VisibleIndex = 1;
            gridViewMaterialConsumption.Columns["materialname"].VisibleIndex = 2;
            gridViewMaterialConsumption.Columns["Quantity"].VisibleIndex = 3;

            gridViewMaterialConsumption.Columns["materialcode"].Caption = "Material Code";
            gridViewMaterialConsumption.Columns["materialname"].Caption = "Material Name";

            gridViewMaterialConsumption.Columns["materialcode"].Width = 80;
            gridViewMaterialConsumption.Columns["materialname"].Width = 150;
            gridViewMaterialConsumption.Columns["Quantity"].Width = 60;

            gridViewMaterialConsumption.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridViewMaterialConsumption.Columns["Quantity"].DisplayFormat.FormatString = "{0:n3}";

            // Calculated Total column: 
            GridColumn columnTotal = new GridColumn();
            columnTotal.Caption = "Total";
            columnTotal.FieldName = "Total";
            columnTotal.OptionsColumn.AllowEdit = false;
            columnTotal.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            columnTotal.UnboundExpression = weightMaterialConsumption + "*[Quantity]";

            gridViewMaterialConsumption.Columns.Add(columnTotal);

            columnTotal.VisibleIndex = gridViewMaterialConsumption.VisibleColumns.Count;
            columnTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            columnTotal.DisplayFormat.FormatString = "{0:n3}";

            gridViewMaterialConsumption.OptionsBehavior.Editable = false;
            gridViewMaterialConsumption.OptionsView.ColumnAutoWidth = true;

            GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Total", "{0:n3}");
            gridViewMaterialConsumption.Columns["Total"].Summary.Add(item1);
        }

        private void lueOrderId_EditValueChanged(object sender, EventArgs e)
        {            
            tedItemName.Text = lueOrderId.GetColumnValue("c003").ToString();
            tedColorName.Text = lueOrderId.GetColumnValue("ColorName").ToString();
            LoadGridMaterial(lueOrderId.GetColumnValue("c002").ToString(), tedWeightMaterial.EditValue.ToString());
        }        
    }
}
