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
using DevExpress.XtraReports.UI;
using RecycledManagement.LablesPrint;

namespace RecycledManagement
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int value = 10;

        //XtraUserControl shiftUserControl;
        //XtraUserControl reasonUserControl;
        //XtraUserControl operatorUserControl;
        //XtraUserControl lossTypesUserControl;
        //XtraUserControl otherSourcesUserControl;
        //XtraUserControl bookingOrderUserControl;
        //XtraUserControl incomingUserControl;

        XtraUserControl shiftListUserControl;
        XtraUserControl reasonListUserControl;
        XtraUserControl operatorListUserControl;
        XtraUserControl lossTypesListUserControl;
        XtraUserControl otherSourcesListUserControl;
        XtraUserControl bookingOrderListUserControl;
        XtraUserControl incomingListUserControl;
        XtraUserControl crushListUserControl;
        XtraUserControl mixingListUserControl;

        public frmMain()
        {
            InitializeComponent();
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


        private void frmMain_Load(object sender, EventArgs e)
        {
            #region Xet Role
            //quyen admin
            if (GlobalVariable.userId == 1)
            {
                navBarMain.ActiveGroup = navBarGroupCategories;//cho hien thi group nao khi bat len

                barbtnScale.Enabled = false;
                barBtnPrintLables.Enabled = false;

                //tat navbarItem
                navBarItemBookOrder.Enabled = true;
                navBarItemMixing.Enabled = true;
                navBarItemIncoming.Enabled = true;
                navBarItemCrush.Enabled = true;

                GlobalVariable.importOrder = GlobalVariable.importMixing = GlobalVariable.importIncoming = GlobalVariable.importCrush = false;
            }
            else
            {
                navBarMain.ActiveGroup = navBarGroupStation;

                //check Roe
                DataTable _data = DbOperatorRole.Instance.GetOperatorRole(GlobalVariable.userId.ToString());
                if (_data.Rows.Count > 0)
                {                   
                    GlobalVariable.importOrder = Convert.ToBoolean(_data.Rows[0]["booking"].ToString().Split('|')[0]);

                    
                    GlobalVariable.importMixing = Convert.ToBoolean(_data.Rows[0]["mixing"].ToString().Split('|')[0]);

                    
                    GlobalVariable.importIncoming = Convert.ToBoolean(_data.Rows[0]["incoming"].ToString().Split('|')[0]);

                    
                    GlobalVariable.importCrush = Convert.ToBoolean(_data.Rows[0]["crushing"].ToString().Split('|')[0]);

                    GlobalVariable.print = Convert.ToBoolean(_data.Rows[0]["crushing"].ToString().Split('|')[1]);
                    GlobalVariable.scales = Convert.ToBoolean(_data.Rows[0]["crushing"].ToString().Split('|')[2]);

                    if (GlobalVariable.print)
                    {
                        barBtnPrintLables.Enabled = true;
                    }
                    else
                    {
                        barBtnPrintLables.Enabled = false;
                    }

                    if (GlobalVariable.scales)
                    {
                        barbtnScale.Enabled = true;
                    }
                    else
                    {
                        barbtnScale.Enabled = false;
                    }
                }

                navBarItemShifts.Enabled = false;
                navBarItemLossType.Enabled = false;
                navBarItemOperator.Enabled = false;
                navBarItemOtherSource.Enabled = false;
                navBarItemReason.Enabled = false;

                navBarItemAddNewUser.Enabled = false;
                navBarItemDatabase.Enabled = false;
            }
            #endregion

            GlobalVariable.myEvent.ShowMixingEditorChanged += MyEvent_ShowMixingEditorChanged;//đăng ký event để MixingEditor
        }

        private void MyEvent_ShowMixingEditorChanged(object sender, ScaleValueChangedEventArgs e)
        {
            if (e.ShowMixingEditor==true)
            {
                frmMixing nf = new frmMixing();
                tabbedView.AddDocument(nf);
            }
        }

        private void barbtnScale_ItemClick(object sender, ItemClickEventArgs e)
        {
            value = value + 1;
            GlobalVariable.myEvent.ScaleValue = value;
            Debug.WriteLine($"Main write ScaleValue={GlobalVariable.myEvent.ScaleValue} | Select Scale: {GlobalVariable.selectScale}");
        }


        #region sự kiện bấm chọn item navBar

        #region Stations
        private void barBtnPrintLables_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Debug.WriteLine($"Element : {tabbedView.ActiveDocument.Caption}");//get caption Element
                if (tabbedView.ActiveDocument.Caption == "Incoming")
                {
                    DataTable ds = new DataTable();

                    ds = DbIncomingCrush.Instance.GetLable(GlobalVariable.idSelect.ToString());

                    if (GlobalVariable.selectLabel == "Runner")
                    {
                        var rptRe = new rptRunner();

                        rptRe.DataSource = ds;
                        rptRe.CreateDocument();
                        ReportPrintTool printToolCrush = new ReportPrintTool(rptRe);
                        printToolCrush.PrintDialog();
                    }
                    else if (GlobalVariable.selectLabel == "Defect")
                    {
                        var rptRe = new rptDefect();

                        rptRe.DataSource = ds;
                        rptRe.CreateDocument();
                        ReportPrintTool printToolCrush = new ReportPrintTool(rptRe);
                        printToolCrush.PrintDialog();
                    }
                    else if (GlobalVariable.selectLabel == "Contaminated")
                    {
                        var rptRe = new rptContaminated();

                        rptRe.DataSource = ds;
                        rptRe.CreateDocument();
                        ReportPrintTool printToolCrush = new ReportPrintTool(rptRe);
                        printToolCrush.PrintDialog();
                    }
                    else if (GlobalVariable.selectLabel == "Leftover")
                    {
                        var rptRe = new rptLeftover();

                        rptRe.DataSource = ds;
                        rptRe.CreateDocument();
                        ReportPrintTool printToolCrush = new ReportPrintTool(rptRe);
                        printToolCrush.PrintDialog();
                    }
                    else if (GlobalVariable.selectLabel == "Other Source")
                    {
                        var rptRe = new rptOtherSource();

                        rptRe.DataSource = ds;
                        rptRe.CreateDocument();
                        ReportPrintTool printToolCrush = new ReportPrintTool(rptRe);
                        printToolCrush.PrintDialog();
                    }
                }
                else if (tabbedView.ActiveDocument.Caption == "Crushing")
                {
                    DataTable ds = new DataTable();

                    //ds = DbCrushing.Instance.GetLable("6");
                    //var rptRe = new rptCrushed();

                    ds = DbCrushing.Instance.GetLable(GlobalVariable.idSelect.ToString());
                    if (ds.Rows[0]["CrushedType"].ToString() == "0")
                    {
                        var rptRe = new rptCrushed0();
                        rptRe.DataSource = ds;
                        rptRe.CreateDocument();
                        ReportPrintTool printToolCrush = new ReportPrintTool(rptRe);
                        printToolCrush.PrintDialog();
                    }
                    else if (ds.Rows[0]["CrushedType"].ToString() == "1")
                    {
                        var rptRe = new rptCrushed1();
                        rptRe.DataSource = ds;
                        rptRe.CreateDocument();
                        ReportPrintTool printToolCrush = new ReportPrintTool(rptRe);
                        printToolCrush.PrintDialog();
                    }



                }
            }
            catch { }



        }
        private void navBarItemBookOrder_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            bookingOrderListUserControl = new userControlBookingOrders_List();
            bookingOrderListUserControl.Text = "Booking Material";
            tabbedView.AddDocument(bookingOrderListUserControl);
            tabbedView.ActivateDocument(bookingOrderListUserControl);
        }

        private void navBarItemMixing_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            mixingListUserControl = new userControlMixing_List();
            mixingListUserControl.Text = "Mixing";
            tabbedView.AddDocument(mixingListUserControl);
            tabbedView.ActivateDocument(mixingListUserControl);
        }

        private void navBarItemIncoming_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            incomingListUserControl = new IncomingListUserControl();
            incomingListUserControl.Text = "Incoming";
            tabbedView.AddDocument(incomingListUserControl);
            tabbedView.ActivateDocument(incomingListUserControl);
        }

        private void navBarItemCrush_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            crushListUserControl = new CrushListUserControl();
            crushListUserControl.Text = "Crushing";
            tabbedView.AddDocument(crushListUserControl);
            tabbedView.ActivateDocument(crushListUserControl);
        }
        #endregion

        #region Categories
        private void navBarItemShifts_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            shiftListUserControl = new userControlShifts_List();
            shiftListUserControl.Text = "Shifts";
            tabbedView.AddDocument(shiftListUserControl);
            tabbedView.ActivateDocument(shiftListUserControl);
        }

        private void navBarItemOtherSource_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            otherSourcesListUserControl = new userControlOtherSources_List();
            otherSourcesListUserControl.Text = "Other Source";
            tabbedView.AddDocument(otherSourcesListUserControl);
            tabbedView.ActivateDocument(otherSourcesListUserControl);
        }

        private void navBarItemReason_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            reasonListUserControl = new userControlReasons_List();
            reasonListUserControl.Text = "Reason";
            tabbedView.AddDocument(reasonListUserControl);
            tabbedView.ActivateDocument(reasonListUserControl);
        }

        private void navBarItemLossType_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            lossTypesListUserControl = new userControlLossTypes_List();
            lossTypesListUserControl.Text = "Loss Type";
            tabbedView.AddDocument(lossTypesListUserControl);
            tabbedView.ActivateDocument(lossTypesListUserControl);
        }

        private void navBarItemOperator_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            operatorListUserControl = new userControlOperators_List();
            operatorListUserControl.Text = "Operator";
            tabbedView.AddDocument(operatorListUserControl);
            tabbedView.ActivateDocument(operatorListUserControl);
        }
        #endregion

        #region Settings
        private void navBarItemDatabase_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

        }

        private void navBarItemAddNewUser_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            frmAdminUser nf = new frmAdminUser();
            nf.ShowDialog();
        }

        private void navBarItemProfile_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            frmProfile nf = new frmProfile();
            nf.ShowDialog();
        }
        #endregion

        #endregion
    }
}