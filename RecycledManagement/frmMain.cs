using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;

namespace RecycledManagement
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        XtraUserControl shiftUserControl;
        XtraUserControl reasonUserControl;
        XtraUserControl operatorUserControl;
        XtraUserControl lossTypesUserControl;
        XtraUserControl otherSourcesUserControl;
        XtraUserControl bookingOrderUserControl;
        XtraUserControl shiftListUserControl;
        XtraUserControl reasonListUserControl;
        XtraUserControl operatorListUserControl;
        XtraUserControl lossTypesListUserControl;
        XtraUserControl otherSourcesListUserControl;
        XtraUserControl bookingOrderListUserControl;

        public frmMain()
        {
            InitializeComponent();            
        }
        XtraUserControl CreateUserControl(string text)
        {
            XtraUserControl result = new XtraUserControl();
            result.Name = text.ToLower() + "UserControl";
            result.Text = text;            
            LabelControl label = new LabelControl();
            label.Parent = result;
            label.Appearance.Font = new Font("Tahoma", 25.25F);
            label.Appearance.ForeColor = Color.Gray;
            label.Dock = System.Windows.Forms.DockStyle.Fill;
            label.AutoSizeMode = LabelAutoSizeMode.None;
            label.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            label.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            label.Text = text;
            return result;
        }
        void accordionControl_SelectedElementChanged(object sender, SelectedElementChangedEventArgs e)
        {            
            if (e.Element == null) return;            
            switch (e.Element.Text)
            {
                case "Shifts":
                    shiftListUserControl = new userControlShifts_List();
                    shiftListUserControl.Text = "Shifts";
                    tabbedView.AddDocument(shiftListUserControl);
                    tabbedView.ActivateDocument(shiftListUserControl);                    
                    break;
                case "Other Sources":
                    otherSourcesListUserControl = new userControlOtherSources_List();
                    otherSourcesListUserControl.Text = "Other Source";
                    tabbedView.AddDocument(otherSourcesListUserControl);
                    tabbedView.ActivateDocument(otherSourcesListUserControl);
                    break;
                case "Reasons":
                    reasonListUserControl = new userControlReasons_List();
                    reasonListUserControl.Text = "Reason";
                    tabbedView.AddDocument(reasonListUserControl);
                    tabbedView.ActivateDocument(reasonListUserControl);
                    break;
                case "Loss Type":
                    lossTypesListUserControl = new userControlLossTypes_List();
                    lossTypesListUserControl.Text = "Loss Type";
                    tabbedView.AddDocument(lossTypesListUserControl);
                    tabbedView.ActivateDocument(lossTypesListUserControl);
                    break;
                case "Operators":
                    operatorListUserControl = new userControlOperators_List();
                    operatorListUserControl.Text = "Operator";
                    tabbedView.AddDocument(operatorListUserControl);
                    tabbedView.ActivateDocument(operatorListUserControl);
                    break;
                case "Booking Materials":
                    bookingOrderListUserControl = new userControlBookingOrders_List();
                    bookingOrderListUserControl.Text = "Booking Material";
                    tabbedView.AddDocument(bookingOrderListUserControl);
                    tabbedView.ActivateDocument(bookingOrderListUserControl);
                    break;
            }            
        }
        void barButtonNavigation_ItemClick(object sender, ItemClickEventArgs e)
        {
            int barItemIndex = barSubItemNavigation.ItemLinks.IndexOf(e.Link);
            accordionControl.SelectedElement = mainAccordionGroup.Elements[barItemIndex];
        }
        void tabbedView_DocumentClosed(object sender, DocumentEventArgs e)
        {
            RecreateUserControls(e);
            SetAccordionSelectedElement(e);
        }
        void SetAccordionSelectedElement(DocumentEventArgs e)
        {
            if (tabbedView.Documents.Count != 0)
            {
            }
            else
            {
                accordionControl.SelectedElement = null;
            }
        }
        void RecreateUserControls(DocumentEventArgs e)
        {
            //if (e.Document.Caption == "Employees") employeesUserControl = CreateUserControl("Employees");
            //else customersUserControl = CreateUserControl("Customers");
           
        }             
    }
}