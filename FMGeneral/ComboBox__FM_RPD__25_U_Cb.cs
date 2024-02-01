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

    public class ComboBox__FM_RPD__25_U_Cb : B1Item
    {
        public ComboBox__FM_RPD__25_U_Cb()
        {
            FormType = "FM_RPD";
            ItemUID = "25_U_Cb";
        }

        [B1Listener(BoEventTypes.et_COMBO_SELECT, false)]
        public virtual void OnAfterComboSelect(ItemEvent pVal)
        {
            bool ActionSuccess = pVal.ActionSuccess;
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("25_U_Cb");

            // ADD YOUR ACTION CODE HERE ...
            try
            {
                form.Freeze(true);

                var _with = form.DataSources.DBDataSources.Item("@FM_ORPD");

                SAPbouiCOM.Matrix oMatrx;
                oMatrx = (SAPbouiCOM.Matrix)form.Items.Item("0_U_G").Specific;

                string status = item.Specific.Value.ToString().Trim();
                if (_with.GetValue("U_DocType", 0).ToString().Trim() == "S")
                {
                    oMatrx.Columns.Item("C_0_1").Visible = false;
                    oMatrx.Columns.Item("C_0_3").Visible = false;
                    oMatrx.Columns.Item("Col_0").Visible = false;
                    oMatrx.Columns.Item("Col_3").Visible = true;
                }
                else
                {
                    oMatrx.Columns.Item("C_0_1").Visible = true;
                    oMatrx.Columns.Item("C_0_3").Visible = true;
                    oMatrx.Columns.Item("Col_0").Visible = true;
                    oMatrx.Columns.Item("Col_3").Visible = false;
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
