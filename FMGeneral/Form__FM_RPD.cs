using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;
using System;
using SBOHelper.Utils;

namespace FMGeneral
{
    public class Form__FM_RPD:B1Form
    {
        public Form__FM_RPD()
        {
            FormType = "FM_RPD";
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
                var _with = form.DataSources.DBDataSources.Item("@FM_ORPD");
                oMatrix = (SAPbouiCOM.Matrix)form.Items.Item("0_U_G").Specific;
                if (form.Mode == BoFormMode.fm_OK_MODE)
                {

                }

                if (_with.GetValue("U_DocType", 0).ToString().Trim() == "S")
                {
                    oMatrix.Columns.Item("C_0_1").Visible = false;
                    oMatrix.Columns.Item("C_0_3").Visible = false;
                    oMatrix.Columns.Item("Col_0").Visible = false;
                    oMatrix.Columns.Item("Col_3").Visible = true;
                }
                else
                {
                    oMatrix.Columns.Item("C_0_1").Visible = true;
                    oMatrix.Columns.Item("C_0_3").Visible = true;
                    oMatrix.Columns.Item("Col_0").Visible = true;
                    oMatrix.Columns.Item("Col_3").Visible = false;
                }
            }
            catch (Exception ex)
            {
                TNotification.StatusBarError(ex.Message);
            }
        }
    }
}
