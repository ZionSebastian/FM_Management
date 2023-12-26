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
    using System.Globalization;

    public class Button__FM_MHF__btnInvGen:B1Item
    {
        public Button__FM_MHF__btnInvGen()
        {
            FormType = "FM_MHF";
            ItemUID = "btnInvGen";
        }

        [B1Listener(BoEventTypes.et_ITEM_PRESSED, true)]
        public virtual bool OnBeforeItemPressed(ItemEvent pVal)
        {
            Form oForm = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = oForm.Items.Item("btnInvGen");

            Button button = ((Button)(item.Specific));

            // ADD YOUR ACTION CODE HERE ...
            try
            {
                var _with = oForm.DataSources.DBDataSources.Item("@FM_OMHF");
                int SQLCount = TSQL.GetRecords("select T1.U_GLAcct from [@FM_MHF1] T1 WHERE T1.DocEntry='" + _with.GetValue("DocEntry", 0).ToString() + "'  and T1.U_TotlCost>0 and T1.U_GLAcct is null  ").RecordCount;
                
                if (oForm.Mode != BoFormMode.fm_OK_MODE)
                {
                    TNotification.MessageBox("This functionality is only available in OK mode");
                    return false;
                }
                else if(_with.GetValue("U_APInvEntry",0).ToString().Trim()!="")
                {
                    TNotification.MessageBox("AP Invoice Already Generated for this document");
                    return false;
                }
                else if(SQLCount > 0)
                {
                    TNotification.MessageBox("Select an Appropriate G/L Account");
                    return false;
                }
                else if (_with.GetValue("U_APInvDate", 0).ToString().Trim() != "")
                {
                    TNotification.MessageBox("Select a date for Invoice Generation.");
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pVal"></param>
        [B1Listener(BoEventTypes.et_ITEM_PRESSED, false)]
        public virtual void OnAfterItemPressed(ItemEvent pVal)
        {
            bool ActionSuccess = pVal.ActionSuccess;
            Form oForm = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = oForm.Items.Item("btnInvGen");
            Button button = (Button)item.Specific;
            //ADD YOUR ACTION CODE HERE ...

            try
            {
                oForm.Freeze(true);



                //var _with = oForm.DataSources.DBDataSources.Item("@CCS_SOB");

                //string selectedUID = _with.GetValue("DocNum", 0);

                int i = 0;
                if (CreateServiceInv(oForm))
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

        public static bool CreateServiceInv(Form form)
        {
            bool bCreated = false;
            String DocType = "";

            try
            {
                int result = B1Connections.diCompany.Connect();
                if (result != 0)
                {
                    Console.WriteLine("Connection to DI API failed.");
                    Console.WriteLine(B1Connections.diCompany.GetLastErrorDescription());
                    
                }

                SAPbobsCOM.Recordset oRS = (SAPbobsCOM.Recordset)B1Connections.diCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)form.Items.Item("0_U_G").Specific;

                SAPbobsCOM.Documents oServInvoice = (SAPbobsCOM.Documents)B1Connections.diCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseInvoices);
                //oServInvoice.DocObjectCode = SAPbobsCOM.BoObjectTypes.oPurchaseInvoices;

                oServInvoice.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Service;

                var _with = form.DataSources.DBDataSources.Item("@FM_OMHF");
                var _with1 = form.DataSources.DBDataSources.Item("@FM_MHF1");

                oServInvoice.CardCode = _with.GetValue("U_SplrCode", 0).ToString();
                //oServInvoice.DocDate = System.DateTime.Now;
                //oServInvoice.DocDueDate = System.DateTime.Now;
                //oServInvoice.TaxDate = System.DateTime.Now;
                oServInvoice.DocDate = DateTime.ParseExact(_with.GetValue("U_APInvDate", 0).ToString(), "yyyyMMdd", DateTimeFormatInfo.InvariantInfo); 
                oServInvoice.DocDueDate = DateTime.ParseExact(_with.GetValue("U_APInvDate", 0).ToString(), "yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
                oServInvoice.TaxDate = DateTime.ParseExact(_with.GetValue("U_APInvDate", 0).ToString(), "yyyyMMdd", DateTimeFormatInfo.InvariantInfo);

                //oServInvoice.BPL_IDAssignedToInvoice = Convert.ToInt32(sBranch);
                oServInvoice.Comments = "Purchase invoice based on Machine Hiring, DocNum"+ _with.GetValue("DocNum", 0).ToString();
                oServInvoice.UserFields.Fields.Item("U_Type").Value = "2";
                SAPbobsCOM.Document_Lines oSerInvLines = oServInvoice.Lines;


                string MHF1SQL = "select T0.U_WTCode,T0.U_SplrCode,T0.U_SplrName,T1.U_Remark,T1.U_TotlCost,T1.U_GLAcct,T1.U_VATCode,T1.U_CstCrCode,T1.U_DprtCode from [@FM_OMHF] T0 INNER JOIN [@FM_MHF1] T1 ON T1.DocEntry=T0.DocEntry WHERE T0.DocEntry='" + _with.GetValue("DocEntry", 0).ToString() + "'  and  T1.U_TotlCost>0 ";
                oRS = TSQL.GetRecords(MHF1SQL);
                oRS.MoveFirst();
                string WithTax= oRS.Fields.Item("U_WTCode").Value.ToString().Trim();
                for (int i = 0; i < oRS.RecordCount; i++)
                {
                    string test= oRS.Fields.Item("U_Remark").Value.ToString().Trim();
                    double test1 = Convert.ToDouble(oRS.Fields.Item("U_TotlCost").Value.ToString().Trim());
                    oSerInvLines.ItemDescription= oRS.Fields.Item("U_Remark").Value.ToString().Trim();
                    oSerInvLines.AccountCode = oRS.Fields.Item("U_GLAcct").Value.ToString().Trim();
                    
                    oSerInvLines.CostingCode4 = oRS.Fields.Item("U_CstCrCode").Value.ToString().Trim();
                    oSerInvLines.CostingCode = oRS.Fields.Item("U_DprtCode").Value.ToString().Trim();
                    oSerInvLines.LineTotal = Convert.ToDouble(oRS.Fields.Item("U_TotlCost").Value.ToString().Trim());
                    if(oRS.Fields.Item("U_VATCode").Value.ToString().Trim()!="")
                    {
                        oSerInvLines.VatGroup = oRS.Fields.Item("U_VATCode").Value.ToString().Trim();

                    }

                    //oSerInvLines.TaxCode = sTaxCode;
                    //oSerInvLines.SACEntry = int.Parse(sSAC);
                    oSerInvLines.Add();
                    oRS.MoveNext();
                }
                oServInvoice.WithholdingTaxData.WTCode = WithTax;
                oServInvoice.WithholdingTaxData.Add();


                int oRetCode = oServInvoice.Add();
                if (oRetCode == 0)
                {
                    string oDocKey = B1Connections.diCompany.GetNewObjectKey();
                    SAPbobsCOM.Recordset RS = null;
                    string InvDocNum = TSQL.GetSingleRecord("Select DocNum from OPCH where DocEntry='"+ oDocKey + "'").ToString().Trim();
                    _with.SetValue("U_APInvEntry", 0, oDocKey);
                    _with.SetValue("U_APInvNum", 0, InvDocNum);
                    form.Mode = BoFormMode.fm_UPDATE_MODE;
                    form.Items.Item("1").Click(BoCellClickType.ct_Regular);
                    TNotification.StatusbarSuccess("Purchase Service Invoice Created successfully");
                    bCreated = true;

                }
                else
                {
                    bCreated = false;
                    string oErrorMesg = null;
                    int oErrNo = 0;
                    B1Connections.diCompany.GetLastError(out oErrNo, out oErrorMesg);
                    B1Connections.theAppl.MessageBox("ERROR - Copy to  purchase service invoice failed: Error# " + oErrNo + "=> " + oErrorMesg, 1, "Ok", "", "");
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }


        }

        public static bool CreateServiceInvoice(Form form, int irow)
        {
            bool bCreated = false;
            String DocType = "";
            try
            {
                //TNotification.StatusbarSuccess("Service Invoice is in ");

                SAPbouiCOM.Matrix oMatrix_3 = (SAPbouiCOM.Matrix)form.Items.Item("0_U_G").Specific;


                SAPbobsCOM.Documents oServInvoice = (SAPbobsCOM.Documents)B1Connections.diCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices);
                oServInvoice.DocObjectCode = SAPbobsCOM.BoObjectTypes.oInvoices;

                oServInvoice.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Service;

                var _with = form.DataSources.DBDataSources.Item("@FM_OMHF");
                var _with3 = form.DataSources.DBDataSources.Item("@FM_MHF1");

                String DocNum = "";
                String ItemNo = "";
                int lineid = Convert.ToInt32(_with3.GetValue("U_LineId", irow));
                string sEntry = _with3.GetValue("U_Entry", irow).ToString().Trim();

                string sTaxCode = "";

                double amount = Convert.ToDouble(_with3.GetValue("U_Amount", irow));
                DocType = _with.GetValue("U_DocType", 0).ToString().Trim();
                string sCardCode = _with3.GetValue("U_CardCode", irow).ToString().Trim();
                string sBranch = _with.GetValue("U_Branch", 0).ToString().Trim();
                string sAcctCode = TSQL.GetSingleRecord("Select \"U_AcctCode\" From \"@CCS_GLMAP\" Where \"U_DocType\" = '" + DocType + "' And \"U_Objt\" = 'PRI' and \"U_Branch\"='" + sBranch + "'");
                string sSAC = TSQL.GetSingleRecord("SELECT \"AbsEntry\" FROM OSAC Where \"ServCode\" = (Select \"U_SAC\" From \"@CCS_GLMAP\" Where \"U_DocType\" = '" + DocType + "' And \"U_Objt\" = 'PRI' and \"U_Branch\"='" + sBranch + "')");

                oServInvoice.CardCode = sCardCode;
                oServInvoice.DocDate = TDataTime.GetDate(_with3.GetValue("U_InvoiceDate", irow).ToString());
                oServInvoice.DocDueDate = TDataTime.GetDate(_with3.GetValue("U_InvoiceDate", irow).ToString());
                oServInvoice.BPL_IDAssignedToInvoice = Convert.ToInt32(sBranch);


                if (_with3.GetValue("U_DocType", irow).ToString().Trim() == "CCS_LES")
                {
                    DocType = "LA";
                    DocNum = _with3.GetValue("U_DocNum", irow).ToString().Trim();
                    ItemNo = TSQL.GetSingleRecord("Select \"U_ItemCode\" from \"@CCS_LES2\" where \"DocEntry\"='" + sEntry + "' and \"LineId\"='" + lineid + "'");

                    oServInvoice.UserFields.Fields.Item("U_LEntry").Value = sEntry;
                    oServInvoice.UserFields.Fields.Item("U_LESNum").Value = DocNum;
                    oServInvoice.UserFields.Fields.Item("U_LESItem").Value = ItemNo;
                    //oServInvoice.UserFields.Fields.Item("U_BaseType").Value = "CCS_LES";
                    oServInvoice.Comments = "Recurring Transaction based on Lease Agrement";

                }
                if (_with3.GetValue("U_DocType", irow).ToString().Trim() == "CCS_ETT")
                {
                    DocNum = _with3.GetValue("U_DocNum", irow).ToString().Trim();
                    DocType = "EX";
                    ItemNo = TSQL.GetSingleRecord("Select  \"U_ItemCode\" from \"@CCS_ETT2\" where \"DocEntry\"='" + sEntry + "' and \"LineId\"='" + lineid + "'");
                    oServInvoice.UserFields.Fields.Item("U_EXEntry").Value = sEntry;
                    oServInvoice.UserFields.Fields.Item("U_EXNum").Value = DocNum;
                    oServInvoice.UserFields.Fields.Item("U_EXItem").Value = ItemNo;
                    // oServInvoice.UserFields.Fields.Item("U_BaseType").Value = "CCS_ETT";

                    oServInvoice.Comments = "Recurring Transaction based on Exhibition Trial transfer";
                }

                string strSQL = "Select   T1.\"AcctCode\"  from \"@CCS_OCAM\"  T0  Inner Join OACT T1 on T0.\"U_CustAcc\"=T1.\"AcctCode\"   where T0.\"U_Branch\" = '" + sBranch + "' and ifnull(\"U_TypOfBus\",'')=''";
                oServInvoice.ControlAccount = TSQL.GetSingleRecord(strSQL);

                SAPbobsCOM.Document_Lines oSerInvLines = oServInvoice.Lines;
                oServInvoice.ShipToCode =
                oServInvoice.PayToCode =

                    oSerInvLines.AccountCode = sAcctCode;
                oSerInvLines.LineTotal = amount;
                string sBrSt = TSQL.GetSingleRecord("SELECT T0.\"State\" FROM \"OBPL\"  T0  WHERE T0.\"BPLId\" ='" + _with.GetValue("U_Branch", 0).ToString().Trim() + "'").ToString().Trim();
                string sCSt = TSQL.GetSingleRecord("SELECT T0.\"State\" FROM CRD1 T0 WHERE T0.\"CardCode\" ='" + sCardCode + "' and  T0.\"AdresType\" ='B'").ToString().Trim();
                string sTaxrate = TSQL.GetSingleRecord("Select \"U_TaxRate\" From \"@CCS_GLMAP\" Where \"U_DocType\" = '" + DocType + "' And \"U_Objt\" = 'PRI' and \"U_Branch\"='" + sBranch + "'").ToString().Trim();


                if (sCSt == sBrSt)
                {
                    String SQL = " SELECT Distinct T2.\"Code\" FROM \"STC1\"  T1 INNER JOIN OSTC T2 ON T1.\"STCCode\" = T2.\"Code\" INNER JOIN OSTA T3 ON T1.\"STACode\" = T3.\"Code\" ";
                    SQL += " INNER JOIN OACT T4 ON T3.\"SalesTax\" = T4.\"AcctCode\" WHERE T2.\"Rate\" ='" + sTaxrate + "' and T2.\"TfcId\" =6 and T4.\"BPLId\" = '" + sBranch + "' and T3.\"RvsCrgPrc\"=0";

                    sTaxCode = TSQL.GetSingleRecord(SQL);
                }
                else
                {
                    String SQL = " SELECT Distinct T2.\"Code\" FROM \"STC1\"  T1 INNER JOIN OSTC T2 ON T1.\"STCCode\" = T2.\"Code\" INNER JOIN OSTA T3 ON T1.\"STACode\" = T3.\"Code\" ";
                    SQL += " INNER JOIN OACT T4 ON T3.\"SalesTax\" = T4.\"AcctCode\" WHERE T2.\"Rate\" ='" + sTaxrate + "' and T2.\"TfcId\" = 7 and T4.\"BPLId\" = '" + sBranch + "' and T3.\"RvsCrgPrc\"=0";
                    sTaxCode = TSQL.GetSingleRecord(SQL);
                }
                oSerInvLines.TaxCode = sTaxCode;
                oSerInvLines.SACEntry = int.Parse(sSAC);
                oSerInvLines.LocationCode = Convert.ToInt16(TSQL.GetSingleRecord("SELECT T1.\"Location\" FROM OBPL T0  INNER JOIN OWHS T1 ON T0.\"DflWhs\" = T1.\"WhsCode\" WHERE T0.\"BPLId\" ='" + _with.GetValue("U_Branch", 0).ToString().Trim() + "'").ToString().Trim());
                if (TSQL.GetSingleRecord("select \"ActType\" from OACT where \"AcctCode\"='" + sAcctCode + "' ") == "E")
                {
                    oSerInvLines.CostingCode = _with.GetValue("U_OcrCode", 0).ToString();
                    oSerInvLines.CostingCode2 = _with.GetValue("U_OcrCode1", 0).ToString().Trim();
                    oSerInvLines.CostingCode3 = _with.GetValue("U_OcrCode2", 0).ToString().Trim();
                    oSerInvLines.CostingCode4 = _with.GetValue("U_OcrCode3", 0).ToString().Trim();
                    oSerInvLines.CostingCode5 = _with.GetValue("U_OcrCode4", 0).ToString().Trim();
                }
                oSerInvLines.Add();

                int oRetCode = oServInvoice.Add();
                if (oRetCode == 0)
                {
                    SAPbobsCOM.Recordset RS = null;
                    string sql1 = "UPDATE T2 SET T2.\"U_SrvInv\"='Y' FROM \"@CCS_OLES\" T0 Inner Join \"@CCS_LES2\" T2 On T0.\"DocEntry\" = T2.\"DocEntry\" ";
                    sql1 += " WHERE T2.\"U_Pricetype\" = '1' and IFNULL(T2.\"U_SrvInv\",'N')!= 'Y' and T2.\"LineId\" = '" + lineid + "' and T0.\"DocEntry\" = '" + sEntry + "' and T0.\"U_CustCode\" = '" + sCardCode + "' ";
                    //RS.DoQuery(sql1);
                    TSQL.Update(sql1);
                    TNotification.StatusbarSuccess("Service Invoice Created successfully");
                    bCreated = true;

                }
                else
                {
                    bCreated = false;
                    string oErrorMesg = null;
                    int oErrNo = 0;
                    B1Connections.diCompany.GetLastError(out oErrNo, out oErrorMesg);
                    B1Connections.theAppl.MessageBox("ERROR - Copy to service invoice failed: Error# " + oErrNo + "=> " + oErrorMesg, 1, "Ok", "", "");
                }

            }
            catch (Exception ex)
            {
                TNotification.StatusBarError(ex.Message);
            }
            return bCreated;
        }
    }
}
