using B1WizardBase;
using SAPbouiCOM;
using SBOHelper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMGeneral
{
    using Class_Files;
    using SAPbobsCOM;
    using System.Globalization;

    public class Button__FM_RPD__btnJEGen : B1Item
    {
        public Button__FM_RPD__btnJEGen()
        {
            FormType = "FM_RPD";
            ItemUID = "btnJEGen";
        }

        [B1Listener(BoEventTypes.et_ITEM_PRESSED, true)]
        public virtual bool OnBeforeItemPressed(ItemEvent pVal)
        {
            Form oForm = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = oForm.Items.Item("btnJEGen");

            Button button = ((Button)(item.Specific));

            // ADD YOUR ACTION CODE HERE ...
            try
            {
                var _with = oForm.DataSources.DBDataSources.Item("@FM_ORPD");
                int SQLCount = TSQL.GetRecords("select T1.U_AcctCode from [@FM_RPD1] T1 WHERE T1.DocEntry='" + _with.GetValue("DocEntry", 0).ToString() + "'  and T1.U_LineTotal>0 and T1.U_AcctCode is null  ").RecordCount;

                //if (oForm.Mode != BoFormMode.fm_OK_MODE)
                //{
                //    TNotification.MessageBox("This functionality is only available in OK mode");
                //    return false;
                //}
                if (_with.GetValue("U_JrnlEntry", 0).ToString().Trim() != "")
                {
                    TNotification.MessageBox("Journal Entry already generated for this document");
                    return false;
                }
                else if (SQLCount > 0)
                //else if (_with.GetValue("U_GLAcct", 0).ToString().Trim() == "")
                {
                    TNotification.MessageBox("Select an Appropriate G/L Account");
                    return false;
                }
                else if (_with.GetValue("U_DocDate", 0).ToString().Trim() == "")
                {
                    TNotification.MessageBox("Select the Documunt Date.");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                TNotification.StatusBarError(ex.Message);
                return false;
            }
        }

        [B1Listener(BoEventTypes.et_ITEM_PRESSED, false)]
        public virtual void OnAfterItemPressed(ItemEvent pVal)
        {
            bool ActionSuccess = pVal.ActionSuccess;
            Form oForm = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = oForm.Items.Item("btnJEGen");
            Button button = (Button)item.Specific;
            //ADD YOUR ACTION CODE HERE ...

            try
            {
                oForm.Freeze(true);



                //var _with = oForm.DataSources.DBDataSources.Item("@CCS_SOB");

                //string selectedUID = _with.GetValue("DocNum", 0);

                int i = 0;
                if (Create_JournalEntry(oForm))
                {
                    string oDocKey = B1Connections.diCompany.GetNewObjectKey();

                    B1Connections.theAppl.OpenForm((BoFormObjectEnum)112, "", oDocKey);
                }


            }
            catch (Exception ex)
            {
                TNotification.StatusBarError(ex.Message);
                oForm.Freeze(false);
            }
            finally
            {
                oForm.Freeze(false);
            }
        }


        static bool Create_JournalEntry(SAPbouiCOM.Form oForm)
        {
            try
            {
                String GLAccount;
                String query;
                String queryDebit;
                string value;
                SAPbobsCOM.Recordset oRecordSet = null;
                var _with = oForm.DataSources.DBDataSources.Item("@FM_ORPD");
                var _with1 = oForm.DataSources.DBDataSources.Item("@FM_RPD1");

                string docEntry = _with.GetValue("DocEntry", 0).ToString().Trim();

                queryDebit = "SELECT SUM(U_LineTotal)[Amount],U_AcctCode [Account] from [@FM_RPD1] WHERE DocEntry='" + docEntry + "' and U_Dscription is not null  GROUP BY U_AcctCode union all SELECT SUM(U_VATAmount)[Amount],U_VATAcct [Account] from [@FM_RPD1] WHERE DocEntry='" + docEntry + "' and U_Dscription is not null  GROUP BY U_VATAcct";

                oRecordSet = TSQL.GetRecords(queryDebit);
                if (oRecordSet.RecordCount != 0)
                {
                    TNotification.StatusBarWarning("Initiating Journal Entry");
                    SAPbobsCOM.JournalEntries oJournalEntry = default(SAPbobsCOM.JournalEntries);
                    oJournalEntry = (SAPbobsCOM.JournalEntries)B1Connections.diCompany.GetBusinessObject(BoObjectTypes.oJournalEntries);

                    oJournalEntry.ReferenceDate = DateTime.ParseExact(_with.GetValue("U_DocDate", 0).ToString().Trim(), "yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
                    oJournalEntry.DueDate = DateTime.ParseExact(_with.GetValue("U_DocDate", 0).ToString().Trim(), "yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
                    oJournalEntry.TaxDate = DateTime.ParseExact(_with.GetValue("U_DocDate", 0).ToString().Trim(), "yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
                    oJournalEntry.Memo = "Based On AP Debit Memo Doc No.- " + _with.GetValue("DocNum", 0).ToString().Trim();
                    //oJournalEntry.ProjectCode = _with.GetValue("U_PrjCode", 0);

                    query = "SELECT DebPayAcct from OCRD WHERE CardCode='" + _with.GetValue("U_CardCode", 0).ToString().Trim()+"'";
                    GLAccount = TSQL.GetSingleRecord(query);

                    oJournalEntry.Lines.AccountCode = GLAccount;
                    //oJournalEntry.Lines.ContraAccount = GLAccount;
                    oJournalEntry.Lines.ShortName = _with.GetValue("U_CardCode", 0).ToString().Trim();
                    oJournalEntry.Lines.Debit = 0;
                    query = "SELECT \"U_DocTotal\" from [@FM_ORPD] WHERE \"DocEntry\"='" + _with.GetValue("DocEntry", 0).ToString().Trim() + "' ";
                    value = TSQL.GetSingleRecord(query);
                    oJournalEntry.Lines.Credit = Convert.ToDouble(value);
                    //oJournalEntry.Lines.BPLID = Convert.ToInt16(_with.GetValue("U_Branch", 0).ToString().Trim());
                    //oJournalEntry.Lines.ProjectCode = _with.GetValue("U_PrjCode", 0);
                    oJournalEntry.Lines.Add();

                    oRecordSet.MoveFirst();
                    for (int i = 0; i < oRecordSet.RecordCount; i++)
                    {
                        oJournalEntry.Lines.AccountCode = oRecordSet.Fields.Item("Account").Value;
                        oJournalEntry.Lines.ShortName = oRecordSet.Fields.Item("Account").Value;
                        oJournalEntry.Lines.Debit = Convert.ToDouble(oRecordSet.Fields.Item("Amount").Value);
                        oJournalEntry.Lines.Credit = 0;
                        //oJournalEntry.Lines.BPLID = Convert.ToInt16(_with.GetValue("U_Branch", 0).ToString().Trim());
                        //oJournalEntry.Lines.ProjectCode = _with.GetValue("U_PrjCode", 0);
                        oJournalEntry.Lines.Add();
                        oRecordSet.MoveNext();
                    }
                    int iRetCode = oJournalEntry.Add();
                    if (iRetCode != 0)
                    {
                        int lErrorCode = 0;
                        string sErrMsg = "";
                        B1Connections.diCompany.GetLastError(out lErrorCode, out sErrMsg);
                        B1Connections.theAppl.MessageBox(lErrorCode.ToString() + "-" + sErrMsg);
                        return false;
                    }
                    else
                    {
                        string DocKey = B1Connections.diCompany.GetNewObjectKey();
                    }
                    query = "UPDATE \"@FM_ORPD\"  SET \"U_JrnlEntry\"='" + B1Connections.diCompany.GetNewObjectKey() + "' WHERE \"DocEntry\" = '" + _with.GetValue("DocEntry", 0).ToString().Trim() + "'";
                    SAPbobsCOM.Recordset oRs = (SAPbobsCOM.Recordset)B1Connections.diCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRs.DoQuery(query);
                    TNotification.StatusbarSuccess("Completed Successfully.");
                    B1Connections.theAppl.ActivateMenuItem("1304");
                }
                // return true;

            }
            catch (Exception ex)
            {
                TNotification.StatusBarError(ex.Message);
                return false;
            }


            return true;
        }

    }
}
