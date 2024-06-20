using B1WizardBase;
using SAPbouiCOM;
using SBOHelper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMGeneral
{
    public class Menu__1292 : B1Menu
    {
        public Menu__1292()
        {
            MenuUID = "1292";
        }


        [B1Listener(BoEventTypes.et_MENU_CLICK, true)]
        public virtual bool OnBeforeMenuClick(MenuEvent pVal)
        {
            // ADD YOUR ACTION CODE HERE ...
            SAPbouiCOM.Form oForm = default(SAPbouiCOM.Form);
            SAPbouiCOM.Matrix oMatrix = default(SAPbouiCOM.Matrix);
            SAPbouiCOM.ComboBox oCombo = default(SAPbouiCOM.ComboBox);

            bool retVale = true;
            string sSQL = string.Empty;
            SAPbobsCOM.Recordset rs = null;

            try
            {
                oForm = B1Connections.theAppl.Forms.ActiveForm;

                switch (oForm.TypeEx)
                {
                        
                    #region Zion
                    

                    case "FM_MHF":
                        oForm.Freeze(true);
                        if (oForm.PaneLevel == 1)
                        {
                            TMatrix.addRow(oForm, "0_U_G", "#", "@FM_MHF1");
                            TMatrix.RefreshRowNo(oForm, "0_U_G", "#");

                            //SAPbouiCOM.Matrix oMatrx;
                            //oMatrx = (SAPbouiCOM.Matrix)oForm.Items.Item("0_U_G").Specific;
                            //oCombo = (SAPbouiCOM.ComboBox)oMatrx.GetCellSpecific("C_0_18", oMatrx.RowCount);
                            //TComboBox.RemoveValidValues(oCombo);
                            //sSQL = "select \"DimCode\",\"DimDesc\" from ODIM where DimCode in ('1','3','4')";
                            //TComboBox.Fill(oCombo, sSQL, false);

                        }
                        oForm.Freeze(false);
                        return false;
                        break;

                    case "FM_RPD":
                        oForm.Freeze(true);
                        if (oForm.PaneLevel == 1)
                        {
                            TMatrix.addRow(oForm, "0_U_G", "#", "@FM_RPD1");
                            TMatrix.RefreshRowNo(oForm, "0_U_G", "#");
                            
                        }
                        oForm.Freeze(false);
                        return false;
                        break;


                    case "FM_EPL":
                        oForm.Freeze(true);
                        if (oForm.PaneLevel == 1)
                        {
                            TMatrix.addRow(oForm, "0_U_G", "#", "@FM_EPL1");
                            TMatrix.RefreshRowNo(oForm, "0_U_G", "#");
                            

                        }
                        oForm.Freeze(false);
                        return false;
                        break;

                    case "FM_BER":
                        oForm.Freeze(true);
                        if (oForm.PaneLevel == 1)
                        {
                            TMatrix.addRow(oForm, "0_U_G", "#", "@FM_BER1");
                            TMatrix.RefreshRowNo(oForm, "0_U_G", "#");


                        }
                        if (oForm.PaneLevel == 2)
                        {
                            TMatrix.addRow(oForm, "1_U_G", "#", "@FM_BER2");
                            TMatrix.RefreshRowNo(oForm, "1_U_G", "#");


                        }
                        oForm.Freeze(false);
                        return false;
                        break;

                    case "FM_SBL":
                        oForm.Freeze(true);
                        if (oForm.PaneLevel == 1)
                        {
                            TMatrix.addRow(oForm, "0_U_G", "#", "@FM_SBL1");
                            TMatrix.RefreshRowNo(oForm, "0_U_G", "#");
                        }
                        if (oForm.PaneLevel == 2)
                        {
                            TMatrix.addRow(oForm, "1_U_G", "#", "@FM_SBL2");
                            TMatrix.RefreshRowNo(oForm, "1_U_G", "#");
                        }
                        if (oForm.PaneLevel == 3)
                        {
                            TMatrix.addRow(oForm, "2_U_G", "#", "@FM_SBL3");
                            TMatrix.RefreshRowNo(oForm, "2_U_G", "#");
                        }
                        if (oForm.PaneLevel == 4)
                        {
                            TMatrix.addRow(oForm, "3_U_G", "#", "@FM_SBL4");
                            TMatrix.RefreshRowNo(oForm, "3_U_G", "#");
                        }
                        if (oForm.PaneLevel == 5)
                        {
                            TMatrix.addRow(oForm, "4_U_G", "#", "@FM_SBL5");
                            TMatrix.RefreshRowNo(oForm, "4_U_G", "#");
                        }
                        oForm.Freeze(false);
                        return false;
                        break;

                    case "FM_PRD":
                        oForm.Freeze(true);
                        if (oForm.PaneLevel == 1)
                        {
                            TMatrix.addRow(oForm, "0_U_G", "#", "@FM_PRD1");
                            TMatrix.RefreshRowNo(oForm, "0_U_G", "#");


                        }
                        if (oForm.PaneLevel == 2)
                        {
                            TMatrix.addRow(oForm, "1_U_G", "#", "@FM_PRD2");
                            TMatrix.RefreshRowNo(oForm, "1_U_G", "#");

                        }
                        if (oForm.PaneLevel == 3)
                        {
                            TMatrix.addRow(oForm, "2_U_G", "#", "@FM_PRD3");
                            TMatrix.RefreshRowNo(oForm, "2_U_G", "#");

                        }
                        oForm.Freeze(false);
                        return false;
                        break;

                    case "FM_PBD":
                        oForm.Freeze(true);
                        if (oForm.PaneLevel == 1)
                        {
                            TMatrix.addRow(oForm, "0_U_G", "#", "@FM_PBD1");
                            TMatrix.RefreshRowNo(oForm, "0_U_G", "#");


                        }
                        oForm.Freeze(false);
                        return false;
                        break;

                    case "FM_FPC":
                        oForm.Freeze(true);
                        if (oForm.PaneLevel == 1)
                        {
                            TMatrix.addRow(oForm, "0_U_G", "#", "@FM_FPC1");
                            TMatrix.RefreshRowNo(oForm, "0_U_G", "#");
                        }
                        oForm.Freeze(false);
                        return false;
                        break;
                        #endregion

                }
                return false;
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
