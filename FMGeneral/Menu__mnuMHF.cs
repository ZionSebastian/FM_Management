using B1WizardBase;
using SAPbouiCOM;
using SBOHelper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMGeneral.Class_Files;


namespace FMGeneral
{
    
    public class Menu__mnuMHF : B1XmlFormMenu
    {
        public Menu__mnuMHF()
        {
            MenuUID = "mnuMHF";
            LoadXml("FM_MHFForm.srf");
        }
        [B1Listener(BoEventTypes.et_MENU_CLICK, false)]
        public virtual void OnAfterMenuClick(MenuEvent pVal)
        {
            // ADD YOUR ACTION CODE HERE ...

            this.LoadForm();
            SAPbouiCOM.Form oForm = B1Connections.theAppl.Forms.ActiveForm;
            try
            {
                oForm.Freeze(true);
                oForm.Mode = BoFormMode.fm_ADD_MODE;
                clsFMGeneral.AddMode(oForm);
                oForm.Freeze(false);
            }
            catch (Exception ex)
            {
                oForm.Freeze(false);
                TNotification.StatusBarError(ex.Message);
            }


        }
    }
}
