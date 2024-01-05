using B1WizardBase;
using SAPbouiCOM;
using SBOHelper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMGeneral
{
    using Class_Files;
    class Button__FM_MHF__1 : B1Item
    {
        public Button__FM_MHF__1()
        {
            FormType = "FM_MHF";
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
                if (form.Mode != BoFormMode.fm_FIND_MODE)
                {
                    var _with = form.DataSources.DBDataSources.Item("@FM_OMHF");
                    var _with1 = form.DataSources.DBDataSources.Item("@FM_MHF1");

                    if (String.IsNullOrEmpty(_with.GetValue("U_SplrCode", 0).ToString().Trim()))
                    {
                        TNotification.StatusBarError("Supplier Code is Mandatory");
                        return false;
                    }

                    if (String.IsNullOrEmpty(_with.GetValue("U_SplrCode", 0).ToString().Trim()))
                    {
                        TNotification.StatusBarError("Supplier Name is Mandatory");
                        return false;
                    }


                    SAPbouiCOM.Matrix matrix = (SAPbouiCOM.Matrix)form.Items.Item("0_U_G").Specific;
                    matrix.FlushToDataSource();
                    if (matrix.VisualRowCount > 0)
                    {

                        if (form.Mode == BoFormMode.fm_ADD_MODE)
                        {
                            double Totalcost = 0;
                            for(int i=0;i< matrix.VisualRowCount;i++)
                            {
                                Totalcost += Convert.ToDouble(_with1.GetValue("U_TotlCost", i).ToString().Trim());

                            }
                            if (Totalcost == 0)
                            {
                                TNotification.StatusBarError("Total Cost cannot be Zero");
                                return false;
                            }
                        }
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
                if(form.Mode == BoFormMode.fm_OK_MODE)
                {
                    string parentMenuUID = "1304"; // You may need to change this to the desired parent menu ID

                    //oMenuItem = SBO_Application.Menus.Item(parentMenuUID);



                    B1Connections.theAppl.ActivateMenuItem("1304");


                    //SAPbouiCOM.Application oApplication=
                    //oMenuItem = oApplication.Menus.Item(_menuItem);
                    //form.Refresh();
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
