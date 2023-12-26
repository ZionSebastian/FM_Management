using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;
using System;
using SBOHelper.Utils;

namespace FMGeneral
{
    public class Form__FM_MHF : B1Form
    {
        public Form__FM_MHF()
        {
            FormType = "FM_MHF";
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
                var _with = form.DataSources.DBDataSources.Item("@FM_OMHF");

                if (form.Mode == BoFormMode.fm_OK_MODE )
                {
                    string test = _with.GetValue("U_APInvEntry", 0).ToString().Trim();
                    if ( _with.GetValue("U_APInvEntry", 0).ToString().Trim() != "")
                    {
                        form.Items.Item("btnInvGen").Enabled = false;
                    }
                    else
                    {
                        form.Items.Item("btnInvGen").Enabled = true;
                    }
                }
                else
                {
                    form.Items.Item("btnInvGen").Enabled = false;

                }




            }
            catch (Exception ex)
            {
                TNotification.StatusBarError(ex.Message);
            }
        }
    }
}
