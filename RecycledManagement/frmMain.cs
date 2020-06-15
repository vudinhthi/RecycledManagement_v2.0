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
        XtraUserControl shiftsUserControl;
        XtraUserControl reasonsUserControl;
        XtraUserControl operatorsUserControl;
        XtraUserControl otherSourcesUserControl;
        XtraUserControl lossTypesUserControl;

        public frmMain()
        {
            InitializeComponent();
            shiftsUserControl = CreateUserControl("Shifts");
            reasonsUserControl = CreateUserControl("Reasons");
            operatorsUserControl = CreateUserControl("Operators");
            otherSourcesUserControl = CreateUserControl("Other Sources");
            lossTypesUserControl = CreateUserControl("Loss Type");            
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
                    tabbedView.AddDocument(shiftsUserControl);
                    tabbedView.ActivateDocument(shiftsUserControl);
                    break;
                case "Other Sources":
                    tabbedView.AddDocument(otherSourcesUserControl);
                    tabbedView.ActivateDocument(otherSourcesUserControl);
                    break;
                case "Reasons":
                    tabbedView.AddDocument(reasonsUserControl);
                    tabbedView.ActivateDocument(reasonsUserControl);
                    break;
                case "Loss Type":
                    tabbedView.AddDocument(lossTypesUserControl);
                    tabbedView.ActivateDocument(lossTypesUserControl);
                    break;
                case "Operators":
                    tabbedView.AddDocument(operatorsUserControl);
                    tabbedView.ActivateDocument(operatorsUserControl);
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
            switch (e.Document.Caption)
            {
                case "Shifts":
                    shiftsUserControl = CreateUserControl("Shifts");
                    break;
                case "Other Sources":
                    otherSourcesUserControl = CreateUserControl("Other Sources");
                    break;
                case "Reasons":
                    reasonsUserControl = CreateUserControl("Reasons");
                    break;
                case "Loss Type":
                    lossTypesUserControl = CreateUserControl("Loss Type");
                    break;
                case "Operators":
                    operatorsUserControl = CreateUserControl("Operators");
                    break;
            }
        }

        private void accordionControlElement1_Click(object sender, EventArgs e)
        {

        }
    }
}