

namespace FMGeneral
{
    using SAPbobsCOM;
    using SAPbouiCOM;
    using B1WizardBase;
    using System;
    using SBOHelper.Utils;
    using System.Globalization;

    public class Matrix__FM_PRD__1_U_G : B1Item
    {

        public Matrix__FM_PRD__1_U_G()
        {
            FormType = "FM_PRD";
            ItemUID = "1_U_G";
        }
        [B1Listener(BoEventTypes.et_RIGHT_CLICK, true)]
        public virtual bool OnBeforeRightClick(ContextMenuInfo pVal)
        {
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("1_U_G");
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
            Item item = form.Items.Item("1_U_G");
            var matrix = (Matrix)item.Specific;
            // ADD YOUR ACTION CODE HERE ...
            try
            {
                if (pVal.InnerEvent == false)
                {
                    form.Freeze(true);
                    var with = form.DataSources.DBDataSources.Item("@FM_OPRD");
                    var with2 = form.DataSources.DBDataSources.Item("@FM_PRD2");

                    SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)form.Items.Item("1_U_G").Specific;
                    SAPbobsCOM.Recordset oRes = null;
                    oRes = (SAPbobsCOM.Recordset)B1Connections.diCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    matrix.FlushToDataSource();
                    if (pVal.ActionSuccess)
                    {
                        double TotalUseQty = 0;
                        switch (pVal.ColUID)
                        {
                            case "C_1_1":
                                {
                                    //matrix.FlushToDataSource();
                                    int lastrow = matrix.VisualRowCount;
                                    string LastItem = with2.GetValue("U_RMMonth", lastrow - 1).ToString().Trim();
                                    if (LastItem != "")
                                    {
                                        TMatrix.addRow(form, "1_U_G", "#", "@FM_PRD2");
                                        TMatrix.RefreshRowNo(form, "1_U_G", "#");
                                        matrix.Columns.Item("C_1_2").Cells.Item(pVal.Row).Click();
                                    }
                                    break;
                                    break;
                                }

                            case "C_1_3":
                                {
                                    double RMNetPrd = 0;
                                    //matrix.FlushToDataSource();
                                    for (int i = 0; i < oMat.RowCount; i++)
                                    {
                                        RMNetPrd += Convert.ToDouble(with2.GetValue("U_NetPrdtn", i).ToString().Trim());

                                        with.SetValue("U_RMNetPrd", 0, RMNetPrd.ToString().Trim());

                                        matrix.LoadFromDataSourceEx(false);
                                        //matrix.AutoResizeColumns();
                                    }
                                    break;
                                }
                            case "C_1_2":
                                {
                                    double RMGrsBlt = 0;
                                    //matrix.FlushToDataSource();
                                    for (int i = 0; i < oMat.RowCount; i++)
                                    {
                                        RMGrsBlt += Convert.ToDouble(with2.GetValue("U_GrossBilt", i).ToString().Trim());

                                        with.SetValue("U_RMGrsBlt", 0, RMGrsBlt.ToString().Trim());

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
