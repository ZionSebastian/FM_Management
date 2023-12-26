using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;
using SBOHelper.Utils;
using System;

namespace FMGeneral
{
    using Class_Files;
    public class EditText__140__txtEPE : B1Item
    {
        public EditText__140__txtEPE()
        {
            FormType = "140";
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
            SAPbouiCOM.EditText edtEPE = default(SAPbouiCOM.EditText);
            try
            {
                SAPbobsCOM.Recordset oRs = (SAPbobsCOM.Recordset)B1Connections.diCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                String EPEntry = "";
                
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
                if (pVal.InnerEvent == false)
                {
                    edtEPE = (SAPbouiCOM.EditText)form.Items.Item("txtEPE").Specific;
                    EPEntry = edtEPE.Value.ToString().Trim();
                    globalvariables.EPLEntry = EPEntry;
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
