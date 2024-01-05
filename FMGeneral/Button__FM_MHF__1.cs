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
