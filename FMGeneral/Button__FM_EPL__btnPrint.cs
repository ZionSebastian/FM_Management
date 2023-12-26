using B1WizardBase;
using SAPbouiCOM;
using SBOHelper.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FMGeneral
{
    using Class_Files;
    public class Button__FM_EPL__btnPrint:B1Item
    {
        public Button__FM_EPL__btnPrint()
        {
            FormType = "FM_EPL";
            ItemUID = "btnPrint";
        }

        [B1Listener(BoEventTypes.et_ITEM_PRESSED, true)]
        public virtual bool OnBeforeItemPressed(ItemEvent pVal)
        {
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("btnPrint");

            Button button = ((Button)(item.Specific));

            // ADD YOUR ACTION CODE HERE ...
            try
            {
                if (form.Mode == BoFormMode.fm_OK_MODE)
                {
                    var _with = form.DataSources.DBDataSources.Item("@FM_OEPL");
                    var _with1 = form.DataSources.DBDataSources.Item("@FM_EPL1");
                    SAPbouiCOM.Matrix matrix = (SAPbouiCOM.Matrix)form.Items.Item("0_U_G").Specific;

                    //string printtype = _with.GetValue("U_PrintType", 0).ToString().Trim();
                    //string ItemNo = _with.GetValue("U_ItemNo", 0).ToString().Trim();
                    //string DocNo = _with.GetValue("U_DocNo", 0).ToString().Trim();
                    //string PalletNo = _with.GetValue("U_PalletNo", 0).ToString().Trim();
                    //string Type = _with.GetValue("U_Type", 0).ToString().Trim();
                    
                     
                    matrix.FlushToDataSource();
                    if (matrix.VisualRowCount == 0)
                    {
                        if (form.Mode == BoFormMode.fm_ADD_MODE)
                        {
                            TNotification.StatusBarError("Item details cannot be empty");
                            return false;
                        }
                    }
                    else if (matrix.RowCount > 0)
                    {
                        
                        //for (int i = 0; i < matrix.RowCount; i++)
                        //{
                        //    string ItemCode = _with1.GetValue("U_ItemCode", i).ToString().Trim();
                        //    if (ItemCode=="")
                        //    {
                        //        TNotification.StatusBarError("Please select any row");
                        //        return false;
                        //    }
                        //}
                        
                    }
                }
                else
                {
                    TNotification.StatusBarError("Please Add or Update the form");
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
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("btnPrint");
            Button button = ((Button)(item.Specific));
            // ADD YOUR ACTION CODE HERE ...
            try
            {
                int code = 0;
                var _with = form.DataSources.DBDataSources.Item("@FM_OEPL");
                var _with1 = form.DataSources.DBDataSources.Item("@FM_EPL1");

                string sDocEntry = _with.GetValue("DocEntry", 0).ToString().Trim();
                globalvariables.EPLPrintDocEntry = sDocEntry;
               // globalvariables.StickDocEntry = sDocEntry;

                //form.Mode=BoFormMode.fm_ADD_MODE;
                // form.Items.Item("1").Click(BoCellClickType.ct_Regular);

                string printDocEntry = globalvariables.EPLPrintDocEntry;

                globalvariables.CRSPrint = true;
                globalvariables.PrintType = "FM_EPL";

                SAPbobsCOM.Recordset RS = null;

                string MenuUID = TSQL.GetSingleRecord("Select \"MenuUID\"  from OCMN where \"ObjectKey\" = (Select \"U_Values\" From \"@CCS_EPLSETT\" Where \"Code\" = '1')");
                
                
                B1Connections.theAppl.ActivateMenuItem(MenuUID);

            }
            catch (Exception ex)
            {

            }
        }

    }
}
