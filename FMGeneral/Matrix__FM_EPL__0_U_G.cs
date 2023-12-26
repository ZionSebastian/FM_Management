
namespace FMGeneral
{

    using SAPbobsCOM;
    using SAPbouiCOM;
    using B1WizardBase;
    using System;
    using SBOHelper.Utils;
    using System.Globalization;

    public class Matrix__FM_EPL__0_U_G : B1Item
    {
        public Matrix__FM_EPL__0_U_G()
        {
            FormType = "FM_EPL";
            ItemUID = "0_U_G";
        }
        [B1Listener(BoEventTypes.et_RIGHT_CLICK, true)]
        public virtual bool OnBeforeRightClick(ContextMenuInfo pVal)
        {
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("0_U_G");
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

        [B1Listener(BoEventTypes.et_COMBO_SELECT, true)]
        public virtual bool OnBeforeComboSelect(ItemEvent pVal)
        {
            bool ActionSuccess = pVal.ActionSuccess;
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("0_U_G");
            Matrix matrix = ((Matrix)(item.Specific));

            var _with = form.DataSources.DBDataSources.Item("@FM_OEPL");
            if (_with.GetValue("Status", 0) == "O")
            {
                try
                {
                    form.Freeze(true);
                    matrix.FlushToDataSource();
                    switch (pVal.ColUID)
                    {
                        case "C_0_18":

                            break;

                    }
                    matrix.LoadFromDataSourceEx(false);
                    matrix.AutoResizeColumns();
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

            return true;
        }

        [B1Listener(BoEventTypes.et_CHOOSE_FROM_LIST, true)]
        public virtual bool OnBeforeChooseFromList(ItemEvent pVal)
        {
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("0_U_G");
            Matrix matrix = ((Matrix)(item.Specific));
            // ADD YOUR ACTION CODE HERE ...
            try
            {
                var _With_OEPL = form.DataSources.DBDataSources.Item("@FM_OEPL");
                var _With_EPL1 = form.DataSources.DBDataSources.Item("@FM_EPL1");
                SAPbouiCOM.Conditions oConditions = null;

                //string Dimension1 = _With_EPL1.GetValue("U_Dimnsn", pVal.Row - 1).ToString().Trim();
                switch (pVal.ColUID)
                {
                    case "C_0_1":
                        string EPItem = _With_EPL1.GetValue("U_ItemCode", pVal.Row - 1).ToString().Trim();
                        if (_With_EPL1.GetValue("U_ItemCode", pVal.Row - 1).ToString().Trim() != "")
                        {

                            SAPbouiCOM.Conditions Conds = default(SAPbouiCOM.Conditions);
                            //string SQLBatch = "select \"DistNumber\"+','+\"ItemCode\" [batch] from OBTN where \"ItemCode\" = '" + EPItem + "' and(\"Quantity\" - \"QuantOut\") > 0";
                            //oConditions = TConditions.Create("DistNumber,ItemCode", SQLBatch);
                            string SQLBatch = "select \"AbsEntry\" from OBTN where \"ItemCode\" = '" + EPItem + "' and(\"Quantity\" - \"QuantOut\") > 0";
                            oConditions = TConditions.Create("AbsEntry", SQLBatch );
                            TChooseFromList.SetCondition(pVal, form, oConditions);
                        }
                        else {
                            oConditions = TConditions.Create("DistNumber", "0", BoConditionOperation.co_EQUAL);
                            TChooseFromList.SetCondition(pVal, form, oConditions);
                            TNotification.StatusBarError("Please select an Item.");
                        }
                       
                        break;
                }






            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
            finally
            { form.Freeze(false); }
            return true;
        }


        [B1Listener(BoEventTypes.et_CHOOSE_FROM_LIST, false)]
        public virtual void OnAfterChooseFromList(ItemEvent pVal)
        {
            bool ActionSuccess = pVal.ActionSuccess;
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("0_U_G");
            Matrix matrix = ((Matrix)(item.Specific));
            // ADD YOUR ACTION CODE HERE ...
            try
            {
                if (pVal.ActionSuccess)
                {
                    SAPbouiCOM.DataTable dataTableCFL = null;
                    dataTableCFL = TChooseFromList.GetValue(pVal, form);
                    if (dataTableCFL != null)
                    {
                        form.Freeze(true);
                        var _with_FM_EPL1 = form.DataSources.DBDataSources.Item("@FM_EPL1");
                        _with_FM_EPL1.Clear();
                        matrix.FlushToDataSource();

                        switch (pVal.ColUID)
                        {
                            case "C_0_2":

                                _with_FM_EPL1.SetValue("U_ItemCode", pVal.Row - 1, dataTableCFL.GetValue("ItemCode", 0).ToString().Trim());
                                _with_FM_EPL1.SetValue("U_ItemName", pVal.Row - 1, dataTableCFL.GetValue("ItemName", 0).ToString().Trim());
                                matrix.LoadFromDataSourceEx();

                                //TMatrix.addRow(form, "0_U_G", "#", "@FM_EPL1");
                                //TMatrix.RefreshRowNo(form, "0_U_G", "#");

                                int lastrow = matrix.VisualRowCount;
                                string LastItem= _with_FM_EPL1.GetValue("U_ItemCode", lastrow-1);
                                if (LastItem!="")
                                {
                                    TMatrix.addRow(form, "0_U_G", "#", "@FM_EPL1");
                                    TMatrix.RefreshRowNo(form, "0_U_G", "#");
                                    matrix.Columns.Item("C_0_4").Cells.Item(pVal.Row).Click();
                                }
                                break;
                            case "C_0_1":

                                _with_FM_EPL1.SetValue("U_BatchNum", pVal.Row - 1, dataTableCFL.GetValue("DistNumber", 0).ToString().Trim());

                                string BatchQty = TSQL.GetSingleRecord("select (\"Quantity\" - \"QuantOut\") \"BatchQty\" from OBTN WHERE \"DistNumber\"='"+ dataTableCFL.GetValue("DistNumber", 0).ToString().Trim() + "' and \"ItemCode\"='"+ _with_FM_EPL1.GetValue("U_ItemCode", pVal.Row - 1 ).ToString().Trim()+ "' ");
                                _with_FM_EPL1.SetValue("U_BtchQty", pVal.Row - 1, BatchQty.ToString().Trim());
                                matrix.LoadFromDataSourceEx();

                                break;


                        }

                        matrix.AutoResizeColumns();

                        if (form.Mode == BoFormMode.fm_OK_MODE)
                        {
                            form.Mode = BoFormMode.fm_UPDATE_MODE;
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
        [B1Listener(BoEventTypes.et_VALIDATE, false)]
        public virtual void OnAfterValidate(ItemEvent pVal)
        {
            bool ActionSuccess = pVal.ActionSuccess;
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("0_U_G");
            var matrix = (Matrix)item.Specific;
            // ADD YOUR ACTION CODE HERE ...
            try
            {
                form.Freeze(true);
                var with = form.DataSources.DBDataSources.Item("@FM_OEPL");
                var with1 = form.DataSources.DBDataSources.Item("@FM_EPL1");

                SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)form.Items.Item("0_U_G").Specific;
                SAPbobsCOM.Recordset oRes = null;
                oRes = (SAPbobsCOM.Recordset)B1Connections.diCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                if (pVal.ActionSuccess)
                {
                    double TotalUseQty = 0;
                    switch (pVal.ColUID)
                    {
                        case "C_0_5":
                            {
                                matrix.FlushToDataSource();
                                for (int i = 0; i < oMat.RowCount; i++)
                                {
                                    if (with1.GetValue("U_ItemCode", i).ToString().Trim() != "")
                                    {
                                        TotalUseQty += Convert.ToDouble(with1.GetValue("U_QtyUse", i).ToString().Trim());
                                    }
                                    with.SetValue("U_TotalTon",0, TotalUseQty.ToString().Trim());

                                    matrix.LoadFromDataSourceEx(false);
                                    //matrix.AutoResizeColumns();
                                }
                                break;
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
