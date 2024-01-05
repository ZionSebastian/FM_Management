using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;
using System;
using SBOHelper.Utils;
using System.Globalization;

namespace FMGeneral
{
    public class Matrix__FM_BER__1_U_G : B1Item
    {
        public Matrix__FM_BER__1_U_G()
        {
            FormType = "FM_BER";
            ItemUID = "1_U_G";
        }
        [B1Listener(BoEventTypes.et_RIGHT_CLICK, true)]
        public virtual bool OnBeforeRightClick(ContextMenuInfo pVal)
        {
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("1_U_G");
            Matrix matrix = ((Matrix)(item.Specific));
            // ADD YOUR ACTION CODE HERE ...
            SAPbouiCOM.Form oForm = default(SAPbouiCOM.Form);

            try
            {
                oForm = B1Connections.theAppl.Forms.ActiveForm;
                switch (pVal.ColUID)
                {
                    case "#":
                        oForm.EnableMenu("1292", true);
                        oForm.EnableMenu("1293", true);

                        break;
                    default:
                        oForm.EnableMenu("1292", true);
                        oForm.EnableMenu("1293", true);
                        break;
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
            return true;
        }

    }
}
