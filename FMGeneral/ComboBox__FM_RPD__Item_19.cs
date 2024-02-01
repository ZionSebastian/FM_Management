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

    public class ComboBox__FM_RPD__Item_19:B1Item
    {
        public ComboBox__FM_RPD__Item_19()
        {
            FormType = "FM_RPD";
            ItemUID = "Item_19";
        }

        [B1Listener(BoEventTypes.et_COMBO_SELECT, false)]
        public virtual void OnAfterComboSelect(ItemEvent pVal)
        {
            bool ActionSuccess = pVal.ActionSuccess;
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("Item_19");

            // ADD YOUR ACTION CODE HERE ...
            try
            {
                form.Freeze(true);
                var _with = form.DataSources.DBDataSources.Item("@FM_ORPD");
                string docCur= _with.GetValue("U_DocCur", 0).ToString().Trim();
                string docRate = TSQL.GetSingleRecord("select Rate from ORTT WHERE CAST(RateDate AS date)=Cast(GETDATE() as date) and Currency='"+ docCur + "'");
                _with.SetValue("U_DocRate", 0, docRate);
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
