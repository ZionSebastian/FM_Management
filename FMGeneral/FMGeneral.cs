namespace FMGeneral
{

    using SAPbobsCOM;
    using SAPbouiCOM;
    using B1WizardBase;
    using SBOHelper.Utils;

    using System.IO;
    using System;

    public class FMGeneral : B1AddOn
    {

        public FMGeneral()
        {
            // ADD YOUR INITIALIZATION CODE HERE	...
        }

        public override void OnShutDown()
        {
            // ADD YOUR TERMINATION CODE HERE	...
        }

        public override void OnCompanyChanged()
        {
            // ADD YOUR COMPANY CHANGE CODE HERE	...
        }

        public override void OnLanguageChanged(BoLanguages language)
        {
            InitializeMenus();
            // ADD YOUR LANGUAGE CHANGE CODE HERE	...
        }

        public override void OnStatusBarErrorMessage(string txt)
        {
            // ADD YOUR CODE HERE	...
            try
            {
                if (txt.Contains("UI_API -7780") || txt.Contains("[66000-18]"))
                {
                    if (string.IsNullOrEmpty(TGeneric.ErrorMessage))
                    {
                        B1Connections.theAppl.StatusBar.SetText(string.Empty, 0, BoStatusBarMessageType.smt_None);
                    }
                    else
                    {
                        B1Connections.theAppl.StatusBar.SetText(TGeneric.ErrorMessage, BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                    }
                }
                else if (txt.Contains("Enter valid currency"))
                {
                    B1Connections.theAppl.StatusBar.SetText(string.Empty, 0, BoStatusBarMessageType.smt_None);
                }
            }
            catch
            {
            }
        }

        public override void OnStatusBarSuccessMessage(string txt)
        {
            // ADD YOUR CODE HERE	...
        }

        public override void OnStatusBarWarningMessage(string txt)
        {
            // ADD YOUR CODE HERE	...
        }

        public override void OnStatusBarNoTypedMessage(string txt)
        {
            // ADD YOUR CODE HERE	...
        }

        public override bool OnBeforeProgressBarCreated()
        {
            // ADD YOUR CODE HERE	...
            return true;
        }

        public override bool OnAfterProgressBarCreated()
        {
            // ADD YOUR CODE HERE	...
            return true;
        }

        public override bool OnBeforeProgressBarStopped(bool success)
        {
            // ADD YOUR CODE HERE	...
            return true;
        }

        public override bool OnAfterProgressBarStopped(bool success)
        {
            // ADD YOUR CODE HERE	...
            return true;
        }

        public override bool OnProgressBarReleased()
        {
            // ADD YOUR CODE HERE	...
            return true;
        }
        //public static StreamWriter log;
        public static void Main()
        {
            int retCode = 0;
            string connStr = "";
            B1WizardBase.B1Connections.ConnectionType cnxType = B1WizardBase.B1Connections.ConnectionType.SSO;
            // CHANGE ADDON IDENTIFIER BEFORE RELEASING TO CUSTOMER (Solution Identifier)
            string addOnIdentifierStr = null;
            if ((System.Environment.GetCommandLineArgs().Length == 1))
            {
                connStr = B1Connections.connStr;
            }
            else
            {
                connStr = System.Environment.GetCommandLineArgs().GetValue(1).ToString();
            }
            try
            {
                // INIT CONNECTIONS
                retCode = B1Connections.Init(connStr, addOnIdentifierStr, cnxType);
                // CONNECTION FAILED
                if ((retCode != 0))
                {
                    System.Windows.Forms.MessageBox.Show("ERROR - Connection failed: " + B1Connections.diCompany.GetLastErrorDescription());
                    return;
                }
                // CREATE DB
                // MANAGE COCKPITS
                if (((cnxType == B1WizardBase.B1Connections.ConnectionType.SSO)
                            || (cnxType == B1WizardBase.B1Connections.ConnectionType.MultipleAddOns)))
                {
                    SAPbobsCOM.Recordset oRs = null;


                    FMGeneral_Db addOnDb = new FMGeneral_Db();
                    addOnDb.Add(B1Connections.diCompany);
                    FMGeneral_Cockpits addOnCockpit = new FMGeneral_Cockpits();
                    addOnCockpit.Manage(B1Connections.theAppl, B1Connections.diCompany);



                }
                // CREATE ADD-ON
                FMGeneral addOn = new FMGeneral();
                string suser = B1Connections.diCompany.UserName;
                //log = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + "\\log_Rubfila_"+DateTime.Now.ToString("yyyy-MM-dd") + suser + ".log");
                //log.AutoFlush = true;
                TNotification.MessageBox("FM Addon connected successfully!");
                System.Windows.Forms.Application.Run();
            }
            catch (System.Runtime.InteropServices.COMException com_err)
            {
                // HANDLE ANY COMException HERE
                System.Windows.Forms.MessageBox.Show("ERROR - Connection failed: " + com_err.Message);
            }
        }
    }
}
