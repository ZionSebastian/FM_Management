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
    class EditText__FM_MHF__txtDprt:B1Item
    {
        public EditText__FM_MHF__txtDprt()
        {
            FormType = "FM_MHF";
            ItemUID = "txtDprt";
        }

        [B1Listener(BoEventTypes.et_CHOOSE_FROM_LIST, true)]
        public virtual bool OnBeforeChooseFromList(ItemEvent pVal)
        {
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("txtDprt");
            EditText edittext = ((EditText)(item.Specific));
            // ADD YOUR ACTION CODE HERE ...
            try
            {
                form.Freeze(true);
                SAPbouiCOM.Conditions Conds = default(SAPbouiCOM.Conditions);
                Conds = TConditions.Create("DimCode", "1", BoConditionOperation.co_EQUAL);
                TChooseFromList.SetCondition(pVal, form, Conds);
                return true;
            }
            catch (Exception ex)
            {
                TNotification.StatusBarError(ex.Message);
                return false;
            }
            finally
            {
                form.Freeze(false);
            }

        }

        [B1Listener(BoEventTypes.et_CHOOSE_FROM_LIST, false)]
        public virtual void OnAfterChooseFromList(ItemEvent pVal)
        {
            bool ActionSuccess = pVal.ActionSuccess;
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("txtDprt");
            EditText edittext = ((EditText)(item.Specific));
            // ADD YOUR ACTION CODE HERE ...

            try
            {
                if (pVal.ActionSuccess)
                {
                    SAPbouiCOM.DataTable dataTableCFL = null;
                    var _with_FM_OMHF = form.DataSources.DBDataSources.Item("@FM_OMHF");
                    dataTableCFL = TChooseFromList.GetValue(pVal, form);

                    if (dataTableCFL != null)
                    {
                        _with_FM_OMHF.SetValue("U_Departmnt", 0, dataTableCFL.GetValue("OcrName", 0).ToString().Trim());
                        _with_FM_OMHF.SetValue("U_DprtCode", 0, dataTableCFL.GetValue("OcrCode", 0).ToString().Trim());

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
