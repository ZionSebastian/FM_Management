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
    using System;
    using SBOHelper.Utils;
    using System.Globalization;

    class Matrix__FM_RPD__0_U_G:B1Item
    {
        public Matrix__FM_RPD__0_U_G()
        {
            FormType = "FM_RPD";
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
                if (pVal.InnerEvent == false)
                {
                    form.Freeze(true);
                    double unitPrice, qty, discPrcnt,docTotal = 0;
                    double lineTotal1, lineTotal = 0;
                    string vatGroup = String.Empty;
                    double vatRate = 0;
                    double taxTotal = 0;

                    var with = form.DataSources.DBDataSources.Item("@FM_ORPD");
                    var with1 = form.DataSources.DBDataSources.Item("@FM_RPD1");

                    SAPbouiCOM.Matrix oMat = (SAPbouiCOM.Matrix)form.Items.Item("0_U_G").Specific;
                    SAPbobsCOM.Recordset oRes = null;
                    oRes = (SAPbobsCOM.Recordset)B1Connections.diCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                    if (pVal.ActionSuccess)
                    {
                        double TotalUseQty = 0;
                        switch (pVal.ColUID)
                        {
                            case "C_0_2":
                                {
                                    matrix.FlushToDataSource();
                                    int lastrow = matrix.VisualRowCount;
                                    string LastItem = with1.GetValue("U_Dscription", lastrow - 1).ToString().Trim();
                                    if (LastItem != "")
                                    {
                                        TMatrix.addRow(form, "0_U_G", "#", "@FM_RPD1");
                                        TMatrix.RefreshRowNo(form, "0_U_G", "#");
                                        matrix.Columns.Item("C_0_2").Cells.Item(pVal.Row).Click();
                                    }
                                    break;
                                    //TMatrix.addRow(form, "0_U_G", "#", "@FM_PRD1");
                                    //TMatrix.RefreshRowNo(form, "0_U_G", "#");
                                    //break;
                                }
                            case "Col_0":
                                matrix.FlushToDataSource();
                                int SelectRow = pVal.Row;
                                qty = Convert.ToDouble(with1.GetValue("U_Quantity", pVal.Row - 1).ToString().Trim());
                                unitPrice = Convert.ToDouble(with1.GetValue("U_UnitPrice", pVal.Row - 1).ToString().Trim());
                                if (qty > 0 && unitPrice > 0)
                                {
                                    Double TotalPrice = qty * unitPrice;
                                    matrix.FlushToDataSource();
                                    with1.SetValue("U_LineTotal", pVal.Row - 1, TotalPrice.ToString().Trim());
                                    matrix.LoadFromDataSourceEx(false);
                                }

                                //Line Total after Discount

                                discPrcnt = Convert.ToDouble(with1.GetValue("U_DiscPrcnt", pVal.Row - 1).ToString().Trim());
                                if (qty > 0 && unitPrice > 0 && discPrcnt > 0)
                                {
                                    Double TotalPrice = qty * unitPrice * ((100 - discPrcnt) / 100);
                                    matrix.FlushToDataSource();
                                    with1.SetValue("U_LineTotal", pVal.Row - 1, TotalPrice.ToString().Trim());
                                    matrix.LoadFromDataSourceEx(false);
                                }

                                //
                                //Total Before Discount
                                lineTotal = 0;
                                for (int i = 0; i < matrix.VisualRowCount - 1; i++)
                                {
                                    lineTotal += Convert.ToDouble(with1.GetValue("U_LineTotal", i).ToString().Trim());
                                }
                                with.SetValue("U_TotlBfrDsc", 0, lineTotal.ToString().Trim());
                                //

                                //TaxAmount Updation
                                for (int i = 0; i < matrix.RowCount; i++)
                                {
                                    lineTotal1 = Convert.ToDouble(with1.GetValue("U_LineTotal", i).ToString().Trim());
                                    vatGroup = with1.GetValue("U_VATGroup", i).ToString().Trim();
                                    vatRate = Convert.ToDouble(TSQL.GetSingleRecord("Select Rate from OVTG where Code='" + vatGroup + "'"));

                                    taxTotal += lineTotal1 * (vatRate / 100);


                                }
                                with.SetValue("U_TaxAmount", 0, taxTotal.ToString().Trim());
                                //

                                //Document Total
                                docTotal = taxTotal + lineTotal;
                                with.SetValue("U_DocTotal", 0, docTotal.ToString().Trim());
                                //
                                matrix.LoadFromDataSourceEx();
                                break;
                            case "Col_3":
                                matrix.FlushToDataSource();
                                qty = 1;
                                unitPrice = Convert.ToDouble(with1.GetValue("U_Price", pVal.Row - 1).ToString().Trim());
                                if (qty > 0 && unitPrice > 0)
                                {
                                    Double TotalPrice = qty * unitPrice;
                                    matrix.FlushToDataSource();
                                    with1.SetValue("U_LineTotal", pVal.Row - 1, TotalPrice.ToString().Trim());
                                    matrix.LoadFromDataSourceEx(false);
                                }

                                //Line Total after Discount

                                discPrcnt = Convert.ToDouble(with1.GetValue("U_DiscPrcnt", pVal.Row - 1).ToString().Trim());
                                if (qty > 0 && unitPrice > 0 && discPrcnt > 0)
                                {
                                    Double TotalPrice = qty * unitPrice * ((100 - discPrcnt) / 100);
                                    matrix.FlushToDataSource();
                                    with1.SetValue("U_LineTotal", pVal.Row - 1, TotalPrice.ToString().Trim());
                                    matrix.LoadFromDataSourceEx(false);
                                }

                                //
                                //Total Before Discount
                                lineTotal = 0;
                                for (int i = 0; i < matrix.VisualRowCount - 1; i++)
                                {
                                    lineTotal += Convert.ToDouble(with1.GetValue("U_LineTotal", i).ToString().Trim());
                                }
                                with.SetValue("U_TotlBfrDsc", 0, lineTotal.ToString().Trim());
                                //

                                //TaxAmount Updation
                                for (int i = 0; i < matrix.RowCount; i++)
                                {
                                    lineTotal1 = Convert.ToDouble(with1.GetValue("U_LineTotal", i).ToString().Trim());
                                    vatGroup = with1.GetValue("U_VATGroup", i).ToString().Trim();
                                    vatRate = Convert.ToDouble(TSQL.GetSingleRecord("Select Rate from OVTG where Code='" + vatGroup + "'"));

                                    taxTotal += lineTotal1 * (vatRate / 100);


                                }
                                with.SetValue("U_TaxAmount", 0, taxTotal.ToString().Trim());
                                //

                                //Document Total
                                docTotal = taxTotal + lineTotal;
                                with.SetValue("U_DocTotal", 0, docTotal.ToString().Trim());
                                //
                                matrix.LoadFromDataSourceEx();
                                break;
                            case "C_0_3":
                                matrix.FlushToDataSource();

                                
                                    qty = Convert.ToDouble(with1.GetValue("U_Quantity", pVal.Row - 1).ToString().Trim());
                                    unitPrice = Convert.ToDouble(with1.GetValue("U_UnitPrice", pVal.Row - 1).ToString().Trim());
                                    if (qty > 0 && unitPrice > 0)
                                    {
                                        Double TotalPrice = Convert.ToDouble(with1.GetValue("U_Quantity", pVal.Row - 1).ToString().Trim()) * Convert.ToDouble(with1.GetValue("U_UnitPrice", pVal.Row - 1).ToString().Trim());
                                        matrix.FlushToDataSource();
                                        with1.SetValue("U_LineTotal", pVal.Row - 1, TotalPrice.ToString().Trim());
                                        matrix.LoadFromDataSourceEx(false);
                                    }

                                    //Line Total after Discount

                                    discPrcnt = Convert.ToDouble(with1.GetValue("U_DiscPrcnt", pVal.Row - 1).ToString().Trim());
                                    if (qty > 0 && unitPrice > 0 && discPrcnt > 0)
                                    {
                                        Double TotalPrice = qty * unitPrice * ((100 - discPrcnt) / 100);
                                        matrix.FlushToDataSource();
                                        with1.SetValue("U_LineTotal", pVal.Row - 1, TotalPrice.ToString().Trim());
                                        matrix.LoadFromDataSourceEx(false);
                                    }
                                
                                
                                //



                                //Total Before Discount
                                lineTotal = 0;
                                for (int i = 0; i < matrix.VisualRowCount - 1; i++)
                                {
                                    lineTotal += Convert.ToDouble(with1.GetValue("U_LineTotal", i).ToString().Trim());
                                }
                                with.SetValue("U_TotlBfrDsc", 0, lineTotal.ToString().Trim());
                                //

                                //TaxAmount Updation
                                for (int i = 0; i < matrix.RowCount; i++)
                                {
                                    lineTotal1 = Convert.ToDouble(with1.GetValue("U_LineTotal", i).ToString().Trim());
                                    vatGroup = with1.GetValue("U_VATGroup", i).ToString().Trim();
                                    vatRate = Convert.ToDouble(TSQL.GetSingleRecord("Select Rate from OVTG where Code='" + vatGroup + "'"));

                                    taxTotal += lineTotal1 * (vatRate / 100);


                                }
                                with.SetValue("U_TaxAmount", 0, taxTotal.ToString().Trim());
                                //

                                //Document Total
                                docTotal = taxTotal + lineTotal;
                                with.SetValue("U_DocTotal", 0, docTotal.ToString().Trim());
                                //
                                matrix.LoadFromDataSourceEx();
                                break;
                            case "C_0_4":
                                matrix.FlushToDataSource();
                                if(with.GetValue("U_DocType", 0).ToString().Trim() == "S")
                                {
                                    qty = 1;
                                    unitPrice = Convert.ToDouble(with1.GetValue("U_Price", pVal.Row - 1).ToString().Trim());
                                }
                                else
                                {
                                    qty = Convert.ToDouble(with1.GetValue("U_Quantity", pVal.Row - 1).ToString().Trim());
                                    unitPrice = Convert.ToDouble(with1.GetValue("U_UnitPrice", pVal.Row - 1).ToString().Trim());
                                }
                                //qty = Convert.ToDouble(with1.GetValue("U_Quantity", pVal.Row - 1).ToString().Trim());
                                //unitPrice = Convert.ToDouble(with1.GetValue("U_UnitPrice", pVal.Row - 1).ToString().Trim());
                                discPrcnt = Convert.ToDouble(with1.GetValue("U_DiscPrcnt", pVal.Row - 1).ToString().Trim());
                                if (qty > 0 && unitPrice > 0 && discPrcnt>0)
                                {
                                    Double TotalPrice = qty * unitPrice*((100- discPrcnt)/100);
                                    matrix.FlushToDataSource();
                                    with1.SetValue("U_LineTotal", pVal.Row - 1, TotalPrice.ToString().Trim());
                                    matrix.LoadFromDataSourceEx(false);
                                }
                                lineTotal = 0;
                                for (int i=0;i<matrix.VisualRowCount-1;i++)
                                {
                                    lineTotal+= Convert.ToDouble(with1.GetValue("U_LineTotal", i).ToString().Trim());
                                }
                                with.SetValue("U_TotlBfrDsc", 0, lineTotal.ToString().Trim());

                                //TaxAmount Updation
                                for (int i = 0; i < matrix.RowCount; i++)
                                {
                                    lineTotal1 = Convert.ToDouble(with1.GetValue("U_LineTotal", i).ToString().Trim());
                                    vatGroup = with1.GetValue("U_VATGroup", i).ToString().Trim();
                                    vatRate = Convert.ToDouble(TSQL.GetSingleRecord("Select Rate from OVTG where Code='" + vatGroup + "'"));

                                    taxTotal += lineTotal1 * (vatRate / 100);


                                }
                                with.SetValue("U_TaxAmount", 0, taxTotal.ToString().Trim());
                                //

                                //Document Total
                                docTotal = taxTotal + lineTotal;
                                with.SetValue("U_DocTotal", 0, docTotal.ToString().Trim());
                                //
                                matrix.LoadFromDataSourceEx();

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


        /// <summary>
        /// For selecting Item,supervisor,warehouse from CFL
        /// </summary>
        /// <param name="pVal"></param>
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
                    double lineTotal = 0;
                    string vatGroup = String.Empty;
                    double vatRate = 0;
                    double taxTotal = 0;
                    double totlBfrDs, docTotal = 0;
                    SAPbouiCOM.DataTable dataTableCFL = null;
                    dataTableCFL = TChooseFromList.GetValue(pVal, form);
                    if (dataTableCFL != null)
                    {
                        form.Freeze(true);
                        var _with_FM_RPD1 = form.DataSources.DBDataSources.Item("@FM_RPD1");
                        var _with_FM_ORPD = form.DataSources.DBDataSources.Item("@FM_ORPD");
                        _with_FM_RPD1.Clear();
                        matrix.FlushToDataSource();

                        switch (pVal.ColUID)
                        {
                            
                            case "C_0_1":

                                _with_FM_RPD1.SetValue("U_Dscription", pVal.Row - 1, dataTableCFL.GetValue("ItemName", 0).ToString().Trim());
                                _with_FM_RPD1.SetValue("U_ItemCode", pVal.Row - 1, dataTableCFL.GetValue("ItemCode", 0).ToString().Trim());

                                string sqlAcctCode = TSQL.GetSingleRecord("select BalInvntAc from OITM T0 INNER JOIN OITB T1 ON T0.ItmsGrpCod=T1.ItmsGrpCod Where ItemCode='"+ dataTableCFL.GetValue("ItemCode", 0).ToString().Trim() + "'").ToString().Trim();
                                string sqlAcctName = TSQL.GetSingleRecord("select AcctName from OACT Where AcctCode='" + sqlAcctCode.ToString().Trim() + "'").ToString().Trim();

                                _with_FM_RPD1.SetValue("U_AcctName", pVal.Row - 1, sqlAcctName.ToString().Trim());
                                _with_FM_RPD1.SetValue("U_AcctCode", pVal.Row - 1, sqlAcctCode.ToString().Trim());

                                

                                matrix.LoadFromDataSourceEx();

                                matrix.FlushToDataSource();
                                int lastrow = matrix.VisualRowCount;
                                string LastItem = _with_FM_RPD1.GetValue("U_Dscription", lastrow - 1).ToString().Trim();
                                if (LastItem != "")
                                {
                                    TMatrix.addRow(form, "0_U_G", "#", "@FM_RPD1");
                                    TMatrix.RefreshRowNo(form, "0_U_G", "#");
                                    matrix.Columns.Item("C_0_2").Cells.Item(pVal.Row).Click();
                                }


                                break;
                            case "C_0_8":

                                _with_FM_RPD1.SetValue("U_AcctName", pVal.Row - 1, dataTableCFL.GetValue("AcctName", 0).ToString().Trim());
                                _with_FM_RPD1.SetValue("U_AcctCode", pVal.Row - 1, dataTableCFL.GetValue("AcctCode", 0).ToString().Trim());
                                matrix.LoadFromDataSourceEx();
                                break;
                            case "C_0_5":

                                _with_FM_RPD1.SetValue("U_VatGroup", pVal.Row - 1, dataTableCFL.GetValue("Code", 0).ToString().Trim());

                                //VAT Amount

                                lineTotal = Convert.ToDouble(_with_FM_RPD1.GetValue("U_LineTotal", pVal.Row-1).ToString().Trim());
                                vatGroup = _with_FM_RPD1.GetValue("U_VATGroup", pVal.Row - 1).ToString().Trim();
                                vatRate = Convert.ToDouble(TSQL.GetSingleRecord("Select Rate from OVTG where Code='" + vatGroup + "'"));
                                taxTotal = lineTotal * (vatRate / 100);
                                _with_FM_RPD1.SetValue("U_VATAmount", pVal.Row - 1, taxTotal.ToString().Trim());
                                //


                                //TaxCode
                                string sqlVATAcct = TSQL.GetSingleRecord("select T0.Account from OVTG T0  WHERE T0.Code='" + dataTableCFL.GetValue("Code", 0).ToString().Trim() + "'").ToString().Trim();

                                _with_FM_RPD1.SetValue("U_VATAcct", pVal.Row - 1, sqlVATAcct.ToString().Trim());

                                //Total TAX Amount
                                taxTotal = 0;

                                for (int i=0;i<matrix.RowCount;i++)
                                {
                                    lineTotal = Convert.ToDouble(_with_FM_RPD1.GetValue("U_LineTotal", i).ToString().Trim());
                                    vatGroup = _with_FM_RPD1.GetValue("U_VATGroup", i).ToString().Trim();
                                    vatRate = Convert.ToDouble(TSQL.GetSingleRecord("Select Rate from OVTG where Code='"+ vatGroup + "'"));

                                    taxTotal += lineTotal * (vatRate / 100);


                                }
                                _with_FM_ORPD.SetValue("U_TaxAmount", 0, taxTotal.ToString().Trim());



                                //Document Total
                                totlBfrDs = Convert.ToDouble(_with_FM_ORPD.GetValue("U_TotlBfrDsc", 0).ToString().Trim());
                                docTotal = taxTotal + totlBfrDs;
                                _with_FM_ORPD.SetValue("U_DocTotal", 0, docTotal.ToString().Trim());
                                //
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

        [B1Listener(BoEventTypes.et_CHOOSE_FROM_LIST, true)]
        public virtual bool OnBeforeChooseFromList(ItemEvent pVal)
        {
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("0_U_G");
            Matrix matrix = ((Matrix)(item.Specific));
            // ADD YOUR ACTION CODE HERE ...
            try
            {
                
                var _With_OMHF = form.DataSources.DBDataSources.Item("@FM_ORPD");
                var _With_MHF1 = form.DataSources.DBDataSources.Item("@FM_RPD1");
                SAPbouiCOM.Conditions oConditions = null;
                
                switch (pVal.ColUID)
                {
                    case "C_0_5":
                        oConditions = TConditions.Create("Inactive", "N", BoConditionOperation.co_EQUAL);
                        TChooseFromList.SetCondition(pVal, form, oConditions);
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


    }
}
