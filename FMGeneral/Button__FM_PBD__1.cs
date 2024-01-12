using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using B1WizardBase;
using FMGeneral.Class_Files;
using SAPbobsCOM;
using SAPbouiCOM;
using SBOHelper.Utils;

namespace FMGeneral
{
    public class Button__FM_PBD__1 : B1Item
    {
        public Button__FM_PBD__1()
        {
            FormType = "FM_PBD";
            ItemUID = "1";
        }

        [B1Listener(BoEventTypes.et_ITEM_PRESSED, true)]
        public virtual bool OnBeforeItemPressed(ItemEvent pVal)
        {
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("1");
            Button button = ((Button)(item.Specific));
            // ADD YOUR ACTION CODE HERE ...
            try
            {
                SAPbobsCOM.Recordset oRS= (SAPbobsCOM.Recordset)B1Connections.diCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                var _with = form.DataSources.DBDataSources.Item("@FM_OPBD");
                if (form.Mode == BoFormMode.fm_ADD_MODE)
                {
                    string Code = _with.GetValue("U_Year", 0).ToString().Trim();
                    string sqlCode = "SELECT * FROM [@FM_OPBD] WHERE U_Year='" + Code + "'";
                    oRS=TSQL.GetRecords(sqlCode);
                    int sqlCodeCount = oRS.RecordCount;
                    if (sqlCodeCount>0)
                    {
                        TNotification.MessageBox("The document for this year ("+ Code + ") has already been submitted.");
                        return false;
                    }
                    else
                    {
                        _with.SetValue("Code", 0, Code);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        [B1Listener(BoEventTypes.et_ITEM_PRESSED, false)]
        public virtual void OnAfterItemPressed(ItemEvent pVal)
        {
            bool ActionSuccess = pVal.ActionSuccess;
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("1");
            Button button = ((Button)(item.Specific));
            // ADD YOUR ACTION CODE HERE ...
            try
            {
                form.Freeze(true);
                if (pVal.ActionSuccess == true & form.Mode == BoFormMode.fm_ADD_MODE)
                {
                    clsFMGeneral.AddMode(form);
                }

                form.Freeze(false);
            }
            catch (Exception ex)
            {
                TNotification.StatusBarError(ex.Message);
            }
        }
    }
}
