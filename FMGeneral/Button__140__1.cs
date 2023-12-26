using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;
using System;
using SBOHelper.Utils;

namespace FMGeneral
{
    using Class_Files;
    public class Button__140__1: B1Item
    {
        public Button__140__1()
        {
            FormType = "140";
            ItemUID = "1";
        }

        //[B1Listener(BoEventTypes.et_ITEM_PRESSED, false)]
        //public virtual void OnAfterItemPressed(ItemEvent pVal)
        //{
        //    bool ActionSuccess = pVal.ActionSuccess;
        //    Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
        //    Item item = form.Items.Item("1");
        //    Button button = (Button)item.Specific;
        //    //ADD YOUR ACTION CODE HERE ...
        //    //SAPbouiCOM.Matrix oMatrix, oMatrix1;
        //    SAPbouiCOM.EditText oEdit;
        //    SAPbouiCOM.EditText oEdit1;
        //    try
        //    {

        //        var _with_ODLN = form.DataSources.DBDataSources.Item("ODLN");

        //        if(_with_ODLN.GetValue("U_EPLEntry",0).ToString().Trim()=="")
        //        {
        //            TNotification.StatusBarError("Please select the Export Packing Entry");

        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        //oForm.Freeze(false);
        //    }
        //    finally
        //    {
                
        //    }
        //}

        [B1Listener(BoEventTypes.et_ITEM_PRESSED, true)]
        public virtual bool OnBeforeItemPressed(ItemEvent pVal)
        {
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("1");
            //Button button = (Button)(item.Specific);
            // ADD YOUR ACTION CODE HERE ...
            try
            { 

                SAPbouiCOM.EditText edtEPE1 = default(SAPbouiCOM.EditText);
                String EPEntry1 = "";

                var _with_ODLN = form.DataSources.DBDataSources.Item("ODLN");

                globalvariables.DlvryCustCode = _with_ODLN.GetValue("CardCode", 0).ToString().Trim();

                edtEPE1 = (SAPbouiCOM.EditText)form.Items.Item("txtEPE").Specific;
                EPEntry1 = edtEPE1.Value.ToString().Trim();
                globalvariables.EPLEntry = EPEntry1;

                globalvariables.TransFormType = "140";

                SAPbouiCOM.EditText edtEPE = default(SAPbouiCOM.EditText);
                String EPEntry = "";
                edtEPE = (SAPbouiCOM.EditText)form.Items.Item("txtEPE").Specific;
                EPEntry = edtEPE.Value.ToString().Trim();
                if (EPEntry == "")
                {
                    //TNotification.StatusBarError("Please select the Export Packing Entry");
                    //return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                B1Connections.theAppl.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
                return false;
            }

            finally
            {
                form.Freeze(false);
            }
        }
    }
}
