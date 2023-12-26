

namespace FMGeneral
{
    using SAPbobsCOM;
    using SAPbouiCOM;
    using B1WizardBase;
    using SBOHelper.Utils;
    using System;
    public class EditText__42__txtEPE : B1Item
    {
        //UserDefinedFunctions.Validations objUserDef = new UserDefinedFunctions.Validations();
        public EditText__42__txtEPE()
        {
            FormType = "42";
            ItemUID = "txtEPE";
        }

        [B1Listener(BoEventTypes.et_VALIDATE, false)]
        public virtual void OnAfterValidate(ItemEvent pVal)
        {
            bool ActionSuccess = pVal.ActionSuccess;
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("txtEPE");
            EditText ccod = (SAPbouiCOM.EditText)item.Specific;
            // ADD YOUR ACTION CODE HERE ...            
           // var with_ODLN = form.DataSources.DBDataSources.Item("ODLN");
            SAPbouiCOM.EditText edtEPE = default(SAPbouiCOM.EditText);
            try
            {
                SAPbobsCOM.Recordset oRs = (SAPbobsCOM.Recordset)B1Connections.diCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                String EPEntry = "";
                Matrix oMatrix = ((Matrix)(form.Items.Item("4").Specific));

                SAPbouiCOM.Item oNewItem = default(SAPbouiCOM.Item);
                SAPbouiCOM.ComboBox combobox = default(SAPbouiCOM.ComboBox);
                SAPbouiCOM.Item oItem = default(SAPbouiCOM.Item);
                SAPbouiCOM.StaticText oStaticText = default(SAPbouiCOM.StaticText);
                SAPbouiCOM.EditText oEditText = default(SAPbouiCOM.EditText);
                SAPbouiCOM.Button oButton = default(SAPbouiCOM.Button);
                SAPbouiCOM.ChooseFromList oCFL = default(SAPbouiCOM.ChooseFromList);
                SAPbouiCOM.CheckBox chkReserve = default(SAPbouiCOM.CheckBox);

                SAPbouiCOM.EditText oEditTextQty = default(SAPbouiCOM.EditText);

                form.Freeze(true);
                //if (form.Mode == BoFormMode.fm_ADD_MODE)
                if (pVal.InnerEvent == false)
                {
                    //if (with_ORCT.GetValue("PayNoDoc", 0).ToString().Trim() == "Y")
                    {
                        edtEPE = (SAPbouiCOM.EditText)form.Items.Item("txtEPE").Specific;
                        EPEntry = edtEPE.Value.ToString().Trim();
                        string SQLbatch = "select T1.\"U_BatchNum\",T1.\"U_ItemCode\",T1.\"U_QtyUse\" from [@FM_OEPL] T0 inner join [@FM_EPL1] T1 ON T1.\"DocEntry\"=T0.\"DocEntry\" WHERE T0.\"DocNum\"='"+ EPEntry + "' and T1.\"U_BatchNum\" is not null";

                        //oRs.DoQuery(SQLbatch);
                        oRs = TSQL.GetRecords(SQLbatch);
                        oRs.MoveFirst();


                        SAPbouiCOM.Column oColumn;
                        SAPbouiCOM.Column oColumnQty;
                        oColumn = oMatrix.Columns.Item("0");
                        oColumnQty = oMatrix.Columns.Item("234000059");
                        //oMatrix.FlushToDataSource();
                        for (int iRow = 0; iRow < oRs.RecordCount; iRow++)
                        {
                            string num = "1";
                            string EPEBatch = oRs.Fields.Item("U_BatchNum").Value.ToString().Trim();
                            string EPEBatchQty = oRs.Fields.Item("U_QtyUse").Value.ToString().Trim();
                            for (int i = 1; i <= oMatrix.RowCount; i++)
                            {
                                oEditText = (SAPbouiCOM.EditText)oColumn.Cells.Item(i).Specific;
                                oEditTextQty = (SAPbouiCOM.EditText)oColumnQty.Cells.Item(i).Specific;
                                if (EPEBatch== oEditText.Value.ToString().Trim())
                                {
                                    oEditTextQty.Value = EPEBatchQty;
                                    form.Items.Item("48").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                                    //oMatrix.SetCellWithoutValidation(1,"0",num);
                                    // _With_RCT1.SetValue("U_BDocEntry", iRow, oRs.Fields.Item("DocEntry").Value.ToString());
                                }
                            }
                            //oMatrix.LoadFromDataSourceEx();
                            oRs.MoveNext();
                        }



                        //for (int iRow = 0; iRow < oRs.RecordCount; iRow++)
                        //{
                        //    if (iRow > 0)
                        //        _With_RCT1.InsertRecord(iRow);

                        //    _With_RCT1.SetValue("U_Select", iRow, "N");
                        //    _With_RCT1.SetValue("U_BDocType", iRow, "IN");
                        //    _With_RCT1.SetValue("U_BDocEntry", iRow, oRs.Fields.Item("DocEntry").Value.ToString());
                        //    _With_RCT1.SetValue("U_BDocNum", iRow, oRs.Fields.Item("DocNum").Value.ToString());
                        //    _With_RCT1.SetValue("U_BDocDate", iRow, oRs.Fields.Item("DocDate").Value.ToString());
                        //    _With_RCT1.SetValue("U_OverDueDays", iRow, oRs.Fields.Item("OverDueDays").Value.ToString());
                        //    _With_RCT1.SetValue("U_DocTotal", iRow, oRs.Fields.Item("DocTotal").Value.ToString());
                        //    _With_RCT1.SetValue("U_BalDue", iRow, oRs.Fields.Item("BalanceDue").Value.ToString());
                        //    _With_RCT1.SetValue("U_Amount", iRow, oRs.Fields.Item("BalanceDue").Value.ToString());
                        //    _With_RCT1.SetValue("U_BCNam", iRow, oRs.Fields.Item("CardName").Value.ToString());
                        //    oMatrix.LoadFromDataSourceEx();
                        //    oRs.MoveNext();
                        //}

                    }
                }
            }
            catch (Exception ex)
            {

                TNotification.StatusBarError(ex.Message);
            }
            finally
            {
                form.Freeze(false);
            }
        }
    }
}
