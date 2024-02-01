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


    public class Menu__1293 : B1Menu
    {

        public Menu__1293()
        {
            MenuUID = "1293";
        }


        [B1Listener(BoEventTypes.et_MENU_CLICK, true)]
        public virtual bool OnBeforeMenuClick(MenuEvent pVal)
        {
            // ADD YOUR ACTION CODE HERE ...
            SAPbouiCOM.Form oForm = default(SAPbouiCOM.Form);
            SAPbouiCOM.Matrix oMatrix = default(SAPbouiCOM.Matrix);
            bool retVale = true;

            try
            {
                oForm = B1Connections.theAppl.Forms.ActiveForm;
                string sSQL = string.Empty;

                switch (oForm.TypeEx)
                {
                   

                    #region Zion
                    case "FM_MHF":
                        if (oForm.PaneLevel == 1)
                        {
                            oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("0_U_G").Specific;
                            if (oMatrix.RowCount > 0)
                            {
                                oForm.Freeze(true);
                                TMatrix.deleteRow(oForm, "0_U_G");
                                TMatrix.RefreshRowNo(oForm, "0_U_G", "#");

                                oForm.Freeze(false);
                                return false;
                            }
                        }
                        break;

                    case "FM_RPD":
                        if (oForm.PaneLevel == 1)
                        {
                            oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("0_U_G").Specific;
                            if (oMatrix.RowCount > 0)
                            {
                                oForm.Freeze(true);
                                TMatrix.deleteRow(oForm, "0_U_G");
                                TMatrix.RefreshRowNo(oForm, "0_U_G", "#");

                                oForm.Freeze(false);
                                return false;
                            }
                        }
                        break;
                    case "FM_EPL":
                        if (oForm.PaneLevel == 1)
                        {
                            oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("0_U_G").Specific;
                            if (oMatrix.RowCount > 0)
                            {
                                oForm.Freeze(true);
                                TMatrix.deleteRow(oForm, "0_U_G");
                                TMatrix.RefreshRowNo(oForm, "0_U_G", "#");

                                oForm.Freeze(false);
                                return false;
                            }
                        }
                        break;

                    case "FM_BER":
                        if (oForm.PaneLevel == 1)
                        {
                            oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("0_U_G").Specific;
                            if (oMatrix.RowCount > 0)
                            {
                                oForm.Freeze(true);
                                TMatrix.deleteRow(oForm, "0_U_G");
                                TMatrix.RefreshRowNo(oForm, "0_U_G", "#");

                                oForm.Freeze(false);
                                return false;
                            }
                        }
                        if (oForm.PaneLevel == 2)
                        {
                            oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("1_U_G").Specific;
                            if (oMatrix.RowCount > 0)
                            {
                                oForm.Freeze(true);
                                TMatrix.deleteRow(oForm, "1_U_G");
                                TMatrix.RefreshRowNo(oForm, "1_U_G", "#");

                                oForm.Freeze(false);
                                return false;
                            }
                        }
                        break;

                    case "FM_PRD":
                        oForm.Freeze(true);
                        if (oForm.PaneLevel == 1)
                        {
                            oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("0_U_G").Specific;
                            if (oMatrix.RowCount > 0)
                            {
                                oForm.Freeze(true);
                                TMatrix.deleteRow(oForm, "0_U_G");
                                TMatrix.RefreshRowNo(oForm, "0_U_G", "#");

                                oForm.Freeze(false);
                                return false;
                            }
                        }
                        if (oForm.PaneLevel == 2)
                        {
                            oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("1_U_G").Specific;
                            if (oMatrix.RowCount > 0)
                            {
                                oForm.Freeze(true);
                                TMatrix.deleteRow(oForm, "1_U_G");
                                TMatrix.RefreshRowNo(oForm, "1_U_G", "#");

                                oForm.Freeze(false);
                                return false;
                            }

                        }
                        if (oForm.PaneLevel == 3)
                        {
                            oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("2_U_G").Specific;
                            if (oMatrix.RowCount > 0)
                            {
                                oForm.Freeze(true);
                                TMatrix.deleteRow(oForm, "2_U_G");
                                TMatrix.RefreshRowNo(oForm, "2_U_G", "#");

                                oForm.Freeze(false);
                                return false;
                            }
                        }
                        oForm.Freeze(false);
                        return false;
                        break;


                    case "FM_PBD":
                        if (oForm.PaneLevel == 1)
                        {
                            oMatrix = (SAPbouiCOM.Matrix)oForm.Items.Item("0_U_G").Specific;
                            if (oMatrix.RowCount > 0)
                            {
                                oForm.Freeze(true);
                                TMatrix.deleteRow(oForm, "0_U_G");
                                TMatrix.RefreshRowNo(oForm, "0_U_G", "#");

                                oForm.Freeze(false);
                                return false;
                            }
                        }
                        oForm.Freeze(false);
                        return false;
                        break;
                        #endregion

                }

            }
            catch (Exception ex)
            {
                TNotification.StatusBarError(ex.Message);
            }
            finally
            {

            }
            return true;
        }

    }


}
