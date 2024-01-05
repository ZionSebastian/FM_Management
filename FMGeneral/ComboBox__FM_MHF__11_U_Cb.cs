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

    public class ComboBox__FM_MHF__11_U_Cb:B1Item
    {
        public ComboBox__FM_MHF__11_U_Cb()
        {
            FormType = "FM_MHF";
            ItemUID = "11_U_Cb";
        }
        [B1Listener(BoEventTypes.et_COMBO_SELECT, false)]
        public virtual void OnAfterComboSelect(ItemEvent pVal)
        {
            bool ActionSuccess = pVal.ActionSuccess;
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("11_U_Cb");

            // ADD YOUR ACTION CODE HERE ...
            try
            {
                form.Freeze(true);
                var _with = form.DataSources.DBDataSources.Item("@FM_OMHF");

                string status = item.Specific.Value.ToString().Trim();
                if (_with.GetValue("Status", 0).ToString().Trim() == "C")
                {
                    form.Items.Item("0_U_G").Enabled = false;
                    form.Items.Item("txtSpCode").Enabled = false;
                    form.Items.Item("22_U_E").Enabled = false;
                    form.Items.Item("23_U_E").Enabled = false;
                    form.Items.Item("24_U_E").Enabled = false;
                    form.Items.Item("25_U_E").Enabled = false;
                    form.Items.Item("txtMtrcla").Enabled = false;
                    form.Items.Item("txtVchle").Enabled = false;
                    form.Items.Item("txtWTCode").Enabled = false;
                    form.Items.Item("cmbMonth").Enabled = false;
                    form.Items.Item("Item_7").Enabled = false;
                    form.Items.Item("Item_0").Enabled = false;
                    form.Items.Item("Item_21").Enabled = false;
                }
                else
                {
                    form.Items.Item("0_U_G").Enabled = true;
                    form.Items.Item("txtSpCode").Enabled = true;
                    form.Items.Item("22_U_E").Enabled = true;
                    form.Items.Item("23_U_E").Enabled = true;
                    form.Items.Item("24_U_E").Enabled = true;
                    form.Items.Item("25_U_E").Enabled = true;
                    form.Items.Item("txtMtrcla").Enabled = true;
                    form.Items.Item("txtVchle").Enabled = true;
                    form.Items.Item("txtWTCode").Enabled = true;
                    form.Items.Item("cmbMonth").Enabled = true;
                    form.Items.Item("Item_7").Enabled = true;
                    form.Items.Item("Item_0").Enabled = true;
                    form.Items.Item("Item_21").Enabled = true;
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
