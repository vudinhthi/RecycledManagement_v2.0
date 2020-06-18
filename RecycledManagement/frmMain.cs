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
using RecycledManagement.Common;
using System.Diagnostics;

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
                    barBtnAddNew.Enabled = false;
                    shiftListUserControl = new userControlShifts_List();
                    shiftListUserControl.Text = "Shifts";
                    tabbedView.AddDocument(shiftListUserControl);
                    tabbedView.ActivateDocument(shiftListUserControl);                    
                    break;
                case "Other Sources":
                    barBtnAddNew.Enabled = false;
                    otherSourcesListUserControl = new userControlOtherSources_List();
                    otherSourcesListUserControl.Text = "Other Source";
                    tabbedView.AddDocument(otherSourcesListUserControl);
                    tabbedView.ActivateDocument(otherSourcesListUserControl);
                    break;
                case "Reasons":
                    barBtnAddNew.Enabled = false;
                    reasonListUserControl = new userControlReasons_List();
                    reasonListUserControl.Text = "Reason";
                    tabbedView.AddDocument(reasonListUserControl);
                    tabbedView.ActivateDocument(reasonListUserControl);
                    break;
                case "Loss Type":
                    barBtnAddNew.Enabled = false;
                    lossTypesListUserControl = new userControlLossTypes_List();
                    lossTypesListUserControl.Text = "Loss Type";
                    tabbedView.AddDocument(lossTypesListUserControl);
                    tabbedView.ActivateDocument(lossTypesListUserControl);
                    break;
                case "Operators":
                    barBtnAddNew.Enabled = false;
                    operatorListUserControl = new userControlOperators_List();
                    operatorListUserControl.Text = "Operator";
                    tabbedView.AddDocument(operatorListUserControl);
                    tabbedView.ActivateDocument(operatorListUserControl);
                    break;
                case "Booking Materials":
                    barBtnAddNew.Enabled = true;
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

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DbHistoryLogin.Instance.UpdateCmd($"logoutDate='{DateTime.Now}'", $"userId='{GlobalVariable.userId}' and loginDate='{GlobalVariable.loginDate}'");
            frmLogin nf = new frmLogin();
            nf.TextUser = "";
            nf.TextPass = "";
            nf.TextCombo = "Operator";
            this.Hide();
            nf.ShowDialog();
        }

        private void shiftsControlElement_Click(object sender, EventArgs e)
        {

        }

        private void barBtnAddNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            //get tên cua form đang được mở trên tableView
            switch (tabbedView.ActiveDocument.Caption)
            {
                case "Shifts":
                    shiftUserControl = new userControlShifts();
                    shiftUserControl.Text = "Add New Shifts";
                    tabbedView.AddDocument(shiftUserControl);
                    tabbedView.ActivateDocument(shiftUserControl);
                    break;
                case "Other Sources":
                    break;
                case "Reasons":
                    break;
                case "Loss Type":
                    break;
                case "Operators":
                    break;
                case "Booking Materials":
                    break;
                default:
                    break;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            barBtnAddNew.Enabled = false;
        }
    }
}