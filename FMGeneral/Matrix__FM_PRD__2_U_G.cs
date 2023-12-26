

namespace FMGeneral
{
    using SAPbobsCOM;
    using SAPbouiCOM;
    using B1WizardBase;
    using System;
    using SBOHelper.Utils;
    using System.Globalization;

    public class Matrix__FM_PRD__2_U_G : B1Item
    {

        public Matrix__FM_PRD__2_U_G()
        {
            FormType = "FM_PRD";
            ItemUID = "2_U_G";
        }
        [B1Listener(BoEventTypes.et_RIGHT_CLICK, true)]
        public virtual bool OnBeforeRightClick(ContextMenuInfo pVal)
        {
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("2_U_G");
            Matrix matrix = ((Matrix)(item.Specific));
            // ADD YOUR ACTION CODE HERE ...
            SAPbouiCOM.Form oForm = default(SAPbouiCOM.Form);

            try
            {
                oForm = B1Connections.theAppl.Forms.ActiveForm;
                switch (pVal.ColUID)
                {
                    case "#":
                        oForm.EnableMenu("1292", true);
                        oForm.EnableMenu("1293", true);

                        break;
                    default:
                        oForm.EnableMenu("1292", true);
                        oForm.EnableMenu("1293", true);
                        break;
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
            return true;
        }


        [B1Listener(BoEventTypes.et_VALIDATE, false)]
        public virtual void OnAfterValidate(ItemEvent pVal)
        {
            bool ActionSuccess = pVal.ActionSuccess;
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("2_U_G");
            var matrix = (Matrix)item.Specific;
            // ADD YOUR ACTION CODE HERE ...
            try
            {
                if (pVal.InnerEvent == false)
                {
                    form.Freeze(true);
                    var with = form.DataSources.DBDataSources.Item("@FM_OPRD");
                    var with3 = form.DataSources.DBDataSources.Item("@FM_PRD3");

                    SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)form.Items.Item("2_U_G").Specific;
                    SAPbobsCOM.Recordset oRes = null;
                    oRes = (SAPbobsCOM.Recordset)B1Connections.diCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                    if (pVal.ActionSuccess)
                    {
                        double TotalUseQty = 0;
                        switch (pVal.ColUID)
                        {
                            case "C_2_1":
                                {
                                    matrix.FlushToDataSource();
                                    int lastrow = matrix.VisualRowCount;
                                    string LastItem = with3.GetValue("U_SMMonth", lastrow - 1).ToString().Trim();
                                    if (LastItem != "")
                                    {
                                        TMatrix.addRow(form, "2_U_G", "#", "@FM_PRD3");
                                        TMatrix.RefreshRowNo(form, "2_U_G", "#");
                                        matrix.Columns.Item("C_2_2").Cells.Item(pVal.Row).Click();
                                    }
                                    break;
                                    break;
                                }

                            case "C_2_2":
                                {
                                    double SMNetPrd = 0;
                                    matrix.FlushToDataSource();
                                    for (int i = 0; i < oMat.RowCount; i++)
                                    {
                                        SMNetPrd += Convert.ToDouble(with3.GetValue("U_NetPrdtn", i).ToString().Trim());

                                        with.SetValue("U_SMNetPrd", 0, SMNetPrd.ToString().Trim());

                                        matrix.LoadFromDataSourceEx(false);
                                        //matrix.AutoResizeColumns();
                                    }
                                    break;
                                }
                            case "C_2_3":
                                {
                                    double SMGrsBlt = 0;
                                    matrix.FlushToDataSource();
                                    for (int i = 0; i < oMat.RowCount; i++)
                                    {
                                        SMGrsBlt += Convert.ToDouble(with3.GetValue("U_GrossBilt", i).ToString().Trim());

                                        with.SetValue("U_SMGrsBlt", 0, SMGrsBlt.ToString().Trim());

                                        matrix.LoadFromDataSourceEx(false);
                                        //matrix.AutoResizeColumns();
                                    }
                                    break;
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                form.Freeze(false);
                TNotification.StatusBarError(ex.Message);
            }
            finally
            {
                form.Freeze(false);
            }
        }



    }
}
