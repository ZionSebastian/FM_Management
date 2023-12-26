using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;
using System;
using SBOHelper.Utils;

namespace FMGeneral
{
    using Class_Files;
    public class Button__42__btnBatch : B1Item
    {

        public Button__42__btnBatch()
        {
            FormType = "42";
            ItemUID = "btnBatch";
        }

        /*
        [B1Listener(BoEventTypes.et_ITEM_PRESSED, false)]
        public virtual void OnAfterItemPressed(ItemEvent pVal)
        {
            bool ActionSuccess = pVal.ActionSuccess;
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("btnBatch");
            Button button = (Button)item.Specific;
            //ADD YOUR ACTION CODE HERE ...

            SAPbouiCOM.EditText oEdit;
            SAPbouiCOM.EditText oEdit1;
            try
            {
                SAPbobsCOM.Recordset oRs = (SAPbobsCOM.Recordset)B1Connections.diCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                String EPEntry = "";
                Matrix oMartrix1= ((Matrix)(form.Items.Item("3").Specific));
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
                //if (pVal.InnerEvent == false)
                {
                    //if (with_ORCT.GetValue("PayNoDoc", 0).ToString().Trim() == "Y")
                    {
                        
                        EPEntry = globalvariables.EPLEntry;
                        string SQLbatch = "select T1.\"U_BatchNum\",T1.\"U_ItemCode\",T1.\"U_QtyUse\" from [@FM_OEPL] T0 inner join [@FM_EPL1] T1 ON T1.\"DocEntry\"=T0.\"DocEntry\" WHERE T0.\"DocNum\"='" + EPEntry + "' and T1.\"U_BatchNum\" is not null";
                        
                        oRs = TSQL.GetRecords(SQLbatch);
                        oRs.MoveFirst();


                        SAPbouiCOM.Column oColumn;
                        SAPbouiCOM.Column oColumnQty;
                        oColumn = oMatrix.Columns.Item("0");
                        oColumnQty = oMatrix.Columns.Item("234000059");
                        for (int iRow = 0; iRow < oRs.RecordCount; iRow++)
                        {
                            string num = "1";
                            string EPEBatch = oRs.Fields.Item("U_BatchNum").Value.ToString().Trim();
                            string EPEBatchQty = oRs.Fields.Item("U_QtyUse").Value.ToString().Trim();
                            for (int i = 1; i <= oMatrix.RowCount; i++)
                            {
                                oEditText = (SAPbouiCOM.EditText)oColumn.Cells.Item(i).Specific;
                                oEditTextQty = (SAPbouiCOM.EditText)oColumnQty.Cells.Item(i).Specific;
                                if (EPEBatch == oEditText.Value.ToString().Trim())
                                {
                                    oEditTextQty.Value = EPEBatchQty;
                                    form.Items.Item("48").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                                }
                            }
                            oRs.MoveNext();
                        }
                        if (ActionSuccess)
                        {
                            form.Items.Item("1").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                            //form.Items.Item("1").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                            TNotification.StatusBarWarning("Batch Created Successfully");
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                form.Freeze(false);
                globalvariables.TransFormType = "";
                globalvariables.EPLEntry = "";
                globalvariables.RefFormType = "";
            }
        }

        */

        //******************// Batch number Auto filling
        /// <summary>
        /// Create batch automatically
        /// </summary>
        /// <param name="pVal"></param>
        [B1Listener(BoEventTypes.et_ITEM_PRESSED, false)]
        public virtual void OnAfterItemPressed(ItemEvent pVal)
        {
            bool ActionSuccess = pVal.ActionSuccess;
            SAPbouiCOM.Form oForm = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = oForm.Items.Item("btnBatch");
            SAPbouiCOM.Button button = (SAPbouiCOM.Button)item.Specific;
            //ADD YOUR ACTION CODE HERE ...
            SAPbouiCOM.Matrix oMatrix, oMatrix1;
            SAPbouiCOM.EditText oEdit;
            SAPbouiCOM.EditText oEdit22;

            SAPbouiCOM.Item oNewItem = default(SAPbouiCOM.Item);
            SAPbouiCOM.ComboBox combobox = default(SAPbouiCOM.ComboBox);
            SAPbouiCOM.Item oItem = default(SAPbouiCOM.Item);
            SAPbouiCOM.StaticText oStaticText = default(SAPbouiCOM.StaticText);
            SAPbouiCOM.EditText oEditText = default(SAPbouiCOM.EditText);
            SAPbouiCOM.Button oButton = default(SAPbouiCOM.Button);
            SAPbouiCOM.ChooseFromList oCFL = default(SAPbouiCOM.ChooseFromList);
            SAPbouiCOM.CheckBox chkReserve = default(SAPbouiCOM.CheckBox);

            SAPbouiCOM.EditText oEditTextQty = default(SAPbouiCOM.EditText);

            SAPbouiCOM.EditText oEditTextTotalQty = default(SAPbouiCOM.EditText);

            SAPbouiCOM.EditText oEditQty, oEditQty1;
            int Selrow;
            try
            {
                oEdit22 = oForm.Items.Item("14").Specific;

                oMatrix1 = (SAPbouiCOM.Matrix)oForm.Items.Item("3").Specific;
                oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("4").Specific;

                SAPbobsCOM.Recordset recSet = B1Connections.diCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                string EPEntry = globalvariables.EPLEntry;
                string EPCardCode = globalvariables.DlvryCustCode;
                for (int iSlRows = 0; iSlRows < oMatrix1.RowCount; iSlRows++)
                {
                    if (oForm.Mode == BoFormMode.fm_UPDATE_MODE)
                        oForm.Items.Item("1").Click(BoCellClickType.ct_Regular);
                    oMatrix1.Columns.Item("3").Cells.Item(iSlRows + 1).Click(SAPbouiCOM.BoCellClickType.ct_Regular, 1);

                    oEdit = (SAPbouiCOM.EditText)(oMatrix1.Columns.Item("1").Cells.Item(iSlRows + 1).Specific);

                    String sSQL = "select T1.\"U_BatchNum\",T1.\"U_ItemCode\",T1.\"U_QtyUse\" from [@FM_OEPL] T0 inner join [@FM_EPL1] T1 ON T1.\"DocEntry\"=T0.\"DocEntry\" WHERE T0.\"DocNum\"='" + EPEntry + "' and  T0.\"U_CustCode\"='" + EPCardCode + "' and T1.\"U_ItemCode\"='" + oEdit.Value.ToString().Trim() + "' and T1.\"U_BatchNum\" is not null";
                    recSet = TSQL.GetRecords(sSQL);
                    recSet.MoveFirst();

                    recSet.MoveFirst();


                    SAPbouiCOM.Column oColumn;
                    SAPbouiCOM.Column oColumnQty;
                    SAPbouiCOM.Column oColumnTotalQty;
                    oColumn = oMatrix.Columns.Item("0");
                    oColumnQty = oMatrix.Columns.Item("234000059");
                    oColumnTotalQty = oMatrix.Columns.Item("234000058");
                    for (int iRow = 0; iRow < recSet.RecordCount; iRow++)
                    {
                        string num = "1";
                        string EPEBatch = recSet.Fields.Item("U_BatchNum").Value.ToString().Trim();
                        string EPEBatchQty = recSet.Fields.Item("U_QtyUse").Value.ToString().Trim();
                        int MtxRow = oMatrix.RowCount;
                        //for (int i = 1; i <= oMatrix.RowCount; i++)
                        for (int i = 1; i <= MtxRow; i++)
                        {
                            if (i==19)
                            {
                                oEditText = (SAPbouiCOM.EditText)oColumn.Cells.Item(i).Specific;
                                oEditTextQty = (SAPbouiCOM.EditText)oColumnQty.Cells.Item(i).Specific;
                            }

                            oEditText = (SAPbouiCOM.EditText)oColumn.Cells.Item(i).Specific;
                            oEditTextQty = (SAPbouiCOM.EditText)oColumnQty.Cells.Item(i).Specific;
                            oEditTextTotalQty = (SAPbouiCOM.EditText)oColumnTotalQty.Cells.Item(i).Specific;
                            if (EPEBatch == oEditText.Value.ToString().Trim())
                            {
                                oEditTextQty.Value = EPEBatchQty;
                                oForm.Items.Item("48").Click(SAPbouiCOM.BoCellClickType.ct_Regular);

                                if (Convert.ToDouble(oEditTextTotalQty.Value) == Convert.ToDouble(EPEBatchQty))
                                {
                                    MtxRow = oMatrix.RowCount;
                                    MtxRow = MtxRow-1;
                                }
                            }
                        }
                        recSet.MoveNext();
                    }

                    /*
                    for (int i = 0; i < recSet.RecordCount; i++)
                    {

                        oEdit22.Value = recSet.Fields.Item("U_BatchNum").Value;

                        //  B1Connections.theAppl.SendKeys("{TAB}");


                        Selrow = oMatrix.GetNextSelectedRow();

                        oEditQty = oMatrix.GetCellSpecific("3", Selrow);
                        oEditQty1 = oMatrix.GetCellSpecific("4", Selrow);
                        if (!String.IsNullOrEmpty(oEditQty.Value.ToString()))
                        {
                            if (Convert.ToDouble(recSet.Fields.Item("U_Qty").Value) <= Convert.ToDouble(oEditQty.Value.ToString()))
                            {
                                oEditQty1.Value = recSet.Fields.Item("U_Qty").Value.ToString();
                                oForm.Items.Item("48").Click(BoCellClickType.ct_Regular);
                            }
                            else if (Convert.ToDouble(recSet.Fields.Item("U_Qty").Value) > Convert.ToDouble(oEditQty.Value.ToString()))
                            {
                                oEditQty1.Value = oEditQty.Value.ToString();
                                oForm.Items.Item("48").Click(BoCellClickType.ct_Regular);
                            }

                        }

                        //B1Connections.theAppl.SendKeys("{TAB}");
                        //oForm.Items.Item("8").Enabled = true;


                        recSet.MoveNext();
                    }
                    */
                    if (oForm.Items.Item("48").Enabled)
                        TItem.Enable(oForm, "16", false);

                }
                if (oForm.Mode == BoFormMode.fm_UPDATE_MODE)
                {
                    oForm.Freeze(false);
                    oForm.Items.Item("1").Click(BoCellClickType.ct_Regular);
                   // oForm.Items.Item("1").Click(BoCellClickType.ct_Regular);
                   // TNotification.StatusBarWarning("Batch Created Successfully");

                }

               // SharedVariables.SharedVariables.TransFormType = "0";


            }
            catch (Exception ex)
            {
                oForm.Freeze(false);
            }
            finally
            {
                //oForm.Freeze(false);
                oForm.Freeze(false);
                globalvariables.TransFormType = "";
                globalvariables.EPLEntry = "";
                globalvariables.RefFormType = "";
                globalvariables.DlvryCustCode = "";
            }
        }
        //*****************//
    }
}
