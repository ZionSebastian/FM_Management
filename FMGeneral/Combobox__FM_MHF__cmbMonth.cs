using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;
using System;
using SBOHelper.Utils;

namespace FMGeneral
{
    public class ComboBox__FM_MHF__cmbMonth : B1Item
    {
        public ComboBox__FM_MHF__cmbMonth()
        {
            FormType = "FM_MHF";
            ItemUID = "cmbMonth";
        }

        public virtual bool OnBeforeComboSelect(ItemEvent pVal)
        {
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("cmbMonth");
            try
            {
                //SAPbouiCOM.ComboBox oCombo = default(SAPbouiCOM.ComboBox);
                //oCombo = (SAPbouiCOM.ComboBox)form.Items.Item("cmbMonth");
                //TComboBox.RemoveValidValues(oCombo);
                //string sSQL = "";
                //TComboBox.Fill(oCombo, sSQL, false);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        [B1Listener(BoEventTypes.et_COMBO_SELECT, false)]
        public virtual void OnAfterComboSelect(ItemEvent pVal)
        {
            bool ActionSuccess = pVal.ActionSuccess;
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("cmbMonth");

            // ADD YOUR ACTION CODE HERE ...
            try
            {
                form.Freeze(true);

                //ButtonCombo BtnCombPrint = ((ButtonCombo)(form.Items.Item("Btn_Print").Specific));
                //if (form.Mode == BoFormMode.fm_UPDATE_MODE)
                //    form.Mode = BoFormMode.fm_OK_MODE;
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
