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
    public class EditText__FM_MHF__txtVchle: B1Item
    {
        public EditText__FM_MHF__txtVchle()
        {
            FormType = "FM_MHF";
            ItemUID = "txtVchle";
        }

        [B1Listener(BoEventTypes.et_CHOOSE_FROM_LIST, true)]
        public virtual bool OnBeforeChooseFromList(ItemEvent pVal)
        {
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("txtVchle");
            EditText edittext = ((EditText)(item.Specific));
            // ADD YOUR ACTION CODE HERE ...
            try
            {
                form.Freeze(true);
                SAPbouiCOM.Conditions Conds = default(SAPbouiCOM.Conditions);
                Conds = TConditions.Create("DimCode", "3", BoConditionOperation.co_EQUAL);
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
            Item item = form.Items.Item("txtVchle");
            EditText edittext = ((EditText)(item.Specific));
            // ADD YOUR ACTION CODE HERE ...

            try
            {
                if (pVal.ActionSuccess)
                {
                    SAPbouiCOM.DataTable dataTableCFL = null;
                    var _with_FM_OMHF = form.DataSources.DBDataSources.Item("@FM_OMHF");
                    dataTableCFL = TChooseFromList.GetValue(pVal, form);

                    string Vehicle = dataTableCFL.GetValue("OcrCode", 0).ToString().Trim();
                    string Month = _with_FM_OMHF.GetValue("U_Month", 0).ToString().Trim();
                    string SupplierCode = _with_FM_OMHF.GetValue("U_SplrCode", 0).ToString().Trim();
                    int VNcount = Convert.ToInt16(TSQL.GetSingleRecord("select count(DocNum) from [@FM_OMHF] WHERE U_VhcleCode='" + Vehicle + "' and U_Month='" + Month + "' and year(U_DocDate)=year(Getdate()) and U_SplrCode='" + SupplierCode + "' and Status='O'").ToString().Trim());
                    //int VNcount = Convert.ToInt16(TSQL.GetSingleRecord("select count(DocNum) from [@FM_OMHF] WHERE lower(REPLACE(U_Matricula, ' ', ''))='" + ModVhclNum + "' and U_Month='" + Month + "' and U_SplrCode='"+ SupplierCode + "' ").ToString().Trim());
                    if (VNcount > 0)
                    {
                        TNotification.StatusBarError("Vehicle already exist!");
                        
                    }
                    else if (dataTableCFL != null && VNcount==0)
                    //if (dataTableCFL != null)
                    {
                        _with_FM_OMHF.SetValue("U_Vehicle", 0, dataTableCFL.GetValue("OcrName", 0).ToString().Trim());
                        _with_FM_OMHF.SetValue("U_VhcleCode", 0, dataTableCFL.GetValue("OcrCode", 0).ToString().Trim());

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
