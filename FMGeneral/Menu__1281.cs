

namespace FMGeneral
{
    using SAPbobsCOM;
    using SAPbouiCOM;
    using B1WizardBase;
    using System;
    using SBOHelper.Utils;
    using Class_Files;


    public class Menu__1281 : B1Menu
    {

        public Menu__1281()
        {
            MenuUID = "1281";
        }


        [B1Listener(BoEventTypes.et_MENU_CLICK, true)]
        public virtual bool OnBeforeMenuClick(MenuEvent pVal)
        {
            // ADD YOUR ACTION CODE HERE ...
            SAPbouiCOM.Form oForm = default(SAPbouiCOM.Form);
            SAPbouiCOM.Matrix oMatrix = default(SAPbouiCOM.Matrix);
            try
            {
                oForm = B1Connections.theAppl.Forms.ActiveForm;

                switch (oForm.TypeEx)
                {
                    #region zion
                    case "FM_EPL":
                        oForm.Freeze(true);
                        oForm.Mode = BoFormMode.fm_FIND_MODE;

                        clsFMGeneral.FindMode(oForm);

                        oForm.Freeze(false);
                        return false;
                        break;
                    case "FM_BER":
                        oForm.Freeze(true);
                        oForm.Mode = BoFormMode.fm_FIND_MODE;

                        clsFMGeneral.FindMode(oForm);

                        oForm.Freeze(false);
                        return false;
                        break;
                    case "FM_PBD":
                        oForm.Freeze(true);
                        oForm.Mode = BoFormMode.fm_FIND_MODE;

                        clsFMGeneral.FindMode(oForm);

                        oForm.Freeze(false);
                        return false;
                        break;
                        #endregion

                }

            }
            catch (Exception ex)
            {
                oForm.Freeze(false);
                TNotification.StatusBarError(ex.Message);
            }
            finally
            {

            }
            return true;
        }
    }
}
