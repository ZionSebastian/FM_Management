using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;
using SBOHelper.Utils;
using System;

namespace FMGeneral
{
    public class EditText__FM_MHF__txtMtrcla : B1Item
    {
        public EditText__FM_MHF__txtMtrcla()
        {
            FormType = "FM_MHF";
            ItemUID = "txtMtrcla";
        }

        [B1Listener(BoEventTypes.et_VALIDATE, true)]
        public virtual bool OnBeforeValidate(ItemEvent pVal)
        {
            bool ActionSuccess = pVal.ActionSuccess;
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("txtMtrcla");
            EditText ccod = (SAPbouiCOM.EditText)item.Specific;
            // ADD YOUR ACTION CODE HERE ...            
             var with_MHF = form.DataSources.DBDataSources.Item("@FM_OMHF");
            SAPbouiCOM.EditText edtEPE = default(SAPbouiCOM.EditText);

            try
            {
                string VehicleNumber = with_MHF.GetValue("U_Matricula", 0).ToString().Trim();
                string ModVhclNum = VehicleNumber.ToLower().Replace(" ","");
                string Month= with_MHF.GetValue("U_Month", 0).ToString().Trim();
                string SupplierCode=with_MHF.GetValue("U_SplrCode", 0).ToString().Trim();
                int VNcount = Convert.ToInt16(TSQL.GetSingleRecord("select count(DocNum) from [@FM_OMHF] WHERE lower(REPLACE(U_Matricula, ' ', ''))='" + ModVhclNum + "' and U_Month='"+ Month + "' and year(U_DocDate)=year(Getdate()) and U_SplrCode='"+ SupplierCode + "' and Status='O'").ToString().Trim());
                //int VNcount = Convert.ToInt16(TSQL.GetSingleRecord("select count(DocNum) from [@FM_OMHF] WHERE lower(REPLACE(U_Matricula, ' ', ''))='" + ModVhclNum + "' and U_Month='" + Month + "' and U_SplrCode='"+ SupplierCode + "' ").ToString().Trim());
                if (VNcount>0)
                {
                    TNotification.StatusBarError("Vehicle number already exist!");
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
    }
}
