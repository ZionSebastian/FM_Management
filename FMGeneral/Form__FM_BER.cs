using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;
using System;
using SBOHelper.Utils;

namespace FMGeneral
{
    public class Form__FM_BER: B1Item
    {
        public Form__FM_BER()
        {
            FormType = "FM_BER";
        }

        [B1Listener(BoEventTypes.et_FORM_DATA_LOAD, false)]
        public virtual void OnAfterFormDataLoad(BusinessObjectInfo pVal)
        {
            bool ActionSuccess = pVal.ActionSuccess;
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Matrix oMatrix;
            ComboBox oComboBox;
            // ADD YOUR ACTION CODE HERE ...
            try
            {
                var _with = form.DataSources.DBDataSources.Item("@FM_OBER");

                string systemDate = System.DateTime.Today.ToString("yyyyMMdd");
                string documentDate = TDataTime.GetDate(_with.GetValue("U_DocDate", 0)).ToString("yyyyMMdd").Trim();

                if (form.Mode == BoFormMode.fm_OK_MODE && systemDate!= documentDate)
                {
                    TForm.Disable(form);
                    form.Items.Item("0_U_G").Enabled = false;
                    form.Items.Item("1_U_G").Enabled = false;
                }
                else
                {
                    TForm.Enable(form);
                    form.Items.Item("0_U_G").Enabled = true;
                    form.Items.Item("1_U_G").Enabled = true;
                }




            }
            catch (Exception ex)
            {
                TNotification.StatusBarError(ex.Message);
            }
        }
    }
}
