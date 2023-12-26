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

    public class Form__410000100:B1Item
    {

        Boolean blnClose = false;
        public Form__410000100()
        {
            FormType = "410000100";
        }


        [B1Listener(BoEventTypes.et_FORM_LOAD, true)]
        public virtual bool OnBeforeFormLoad(ItemEvent pVal)
        {
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            // ADD YOUR ACTION CODE HERE ...

            form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            SAPbouiCOM.EditText oEditDocEntry = (SAPbouiCOM.EditText)form.Items.Item("1000003").Specific;



            if (globalvariables.CRSPrint)
            {

                if (globalvariables.PrintType == "FM_EPL")
                {

                    SAPbouiCOM.EditText oEditUniqueLId = (SAPbouiCOM.EditText)form.Items.Item("1000003").Specific;
                    oEditUniqueLId.Value = globalvariables.EPLPrintDocEntry.ToString().Trim();

                }
                form.Visible = false;
                blnClose = true;
                form.Items.Item("1").Click(BoCellClickType.ct_Regular);
                blnClose = true;
                globalvariables.CRSPrint = false;
                globalvariables.PrintType = "";
                return true;
            }
            else
            {
                blnClose = false;
            }
            return true;

        }


        [B1Listener(BoEventTypes.et_FORM_ACTIVATE, false)]
        public virtual void OnAfterFormActivate(ItemEvent pVal)
        {
            bool ActionSuccess = pVal.ActionSuccess;
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            //Form form = B1Connections.theAppl.Forms.GetForm("410000100", 0);
            // ADD YOUR ACTION CODE HERE ...

            if (pVal.Action_Success)
            {
                if (blnClose)
                {
                    // blnClose = false;
                    form.Close();
                    //Forerun.HelperFunc.globalvariables.NVBPrint = false;
                }

            }
        }
    }
}
