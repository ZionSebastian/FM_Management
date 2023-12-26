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

    public class EditText__FM_MHF__txtWTCode : B1Item
    {
        public EditText__FM_MHF__txtWTCode()
        {
            FormType = "FM_MHF";
            ItemUID = "txtWTCode";
        }
        [B1Listener(BoEventTypes.et_CHOOSE_FROM_LIST, true)]
        public virtual bool OnBeforeChooseFromList(ItemEvent pVal)
        {
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("txtWTCode");
            EditText edittext = ((EditText)(item.Specific));
            // ADD YOUR ACTION CODE HERE ...
            try
            {
                form.Freeze(true);
                var _with_FM_OMHF = form.DataSources.DBDataSources.Item("@FM_OMHF");
                string SplrCode = _with_FM_OMHF.GetValue("U_SplrCode", 0).ToString().Trim();
                if (!String.IsNullOrEmpty(SplrCode))
                {
                    SAPbouiCOM.Conditions Conds = default(SAPbouiCOM.Conditions);
                    string WTSql = "select WTCode from CRD4 T0  where T0.CardCode='" + SplrCode + "'";
                    int Rwcount = TSQL.GetRecords(WTSql).RecordCount;
                    if (Rwcount>0)
                    {
                        Conds = TConditions.Create("WTCode", WTSql);
                    }
                    else
                    {
                        Conds = TConditions.Create("WTCode", "", SAPbouiCOM.BoConditionOperation.co_EQUAL);
                    }
                    TChooseFromList.SetCondition(pVal, form, Conds);
                }
                else
                {

                    TNotification.StatusBarError("Please select a Supplier!");
                    return false;
                }
                
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
            Item item = form.Items.Item("txtWTCode");
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
                        _with_FM_OMHF.SetValue("U_WTCode", 0, dataTableCFL.GetValue("WTCode", 0).ToString().Trim());
                        //_with_FM_OMHF.SetValue("U_CustName", 0, dataTableCFL.GetValue("CardName", 0).ToString().Trim());

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
