using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMGeneral
{
    using SAPbobsCOM;
    using SAPbouiCOM;
    using B1WizardBase;
    using SBOHelper.Utils;
    using System;

    class EditText__FM_RPD__txtPayTrm : B1Item
    {

        public EditText__FM_RPD__txtPayTrm()
        {
            FormType = "FM_RPD";
            ItemUID = "txtPayTrm";
        }

        [B1Listener(BoEventTypes.et_CHOOSE_FROM_LIST, false)]
        public virtual void OnAfterChooseFromList(ItemEvent pVal)
        {
            bool ActionSuccess = pVal.ActionSuccess;
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("txtPayTrm");
            EditText edittext = ((EditText)(item.Specific));
            // ADD YOUR ACTION CODE HERE ...

            try
            {
                if (pVal.ActionSuccess)
                {
                    SAPbouiCOM.DataTable dataTableCFL = null;
                    var _with_FM_OMHF = form.DataSources.DBDataSources.Item("@FM_ORPD");
                    dataTableCFL = TChooseFromList.GetValue(pVal, form);

                    if (dataTableCFL != null)
                    {
                        _with_FM_OMHF.SetValue("U_GroupNum", 0, dataTableCFL.GetValue("GroupNum", 0).ToString().Trim());
                        _with_FM_OMHF.SetValue("U_PymntGrp", 0, dataTableCFL.GetValue("PymntGroup", 0).ToString().Trim());
                    }
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
