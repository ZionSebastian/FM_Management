using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SBOHelper.Utils;
using System.Collections;
using B1WizardBase;
using SAPbobsCOM;
using SAPbouiCOM;
using System.IO;

namespace FMGeneral.Class_Files
{
    public class clsFMGeneral
    {
        private static bool bMaxMin;
        private static bool bFlagItemPress;
        private static string sReEntrys;
        private static string sErrorMsg;
        private static int iLineNo;
        private static bool iDoc_SalesEmpSelect;
        private static bool bDOPrint;
        private static bool bPrintPartNo;
        public static bool bCopy_To = false;

        public static void AddMode(SAPbouiCOM.Form _form)
        {

            SAPbouiCOM.Matrix oMatrix = default(SAPbouiCOM.Matrix);
            SAPbouiCOM.ComboBox oCombo = default(SAPbouiCOM.ComboBox);

            string sDateNow = string.Empty;
            string sSQL = string.Empty;
            SAPbouiCOM.Matrix oMatrx;
            SAPbouiCOM.Column oColType = default(SAPbouiCOM.Column);
            try
            {
                switch (_form.TypeEx)
                {
                    #region Zion
                    #region Bank Exchange Rate
                    case "FM_BER":
                        var _withBER = _form.DataSources.DBDataSources.Item("@FM_OBER");
                        var _withBER1 = _form.DataSources.DBDataSources.Item("@FM_BER1");

                        oMatrx = (SAPbouiCOM.Matrix)_form.Items.Item("0_U_G").Specific;

                        TComboBox.LoadSeries(_form, "Item_0", "FM_BER");
                        //@TABLE is the name of the DBDataSource the form's connect to 
                        _withBER.SetValue("Series", 0, TUser.GetDefaultSeriesBranch("FM_BER"));
                        _withBER.SetValue("DocNum", 0, Convert.ToString(TDocument.GetNextDocNo(_form, TUser.GetDefaultSeriesBranch("FM_BER"))));
                        _withBER.SetValue("U_DocDate", 0, System.DateTime.Today.ToString("yyyyMMdd"));


                        oMatrx = (SAPbouiCOM.Matrix)_form.Items.Item("0_U_G").Specific;
                        //oMatrx.FlushToDataSource();
                        
                        for (int i=1;i<=2;i++)
                         {
                            oMatrx.FlushToDataSource();
                            TMatrix.addRow(_form, "0_U_G", "#", "@FM_BER1");
                            TMatrix.RefreshRowNo(_form, "0_U_G", "#");
                            //if (i == 1)
                            //{
                            //    _withBER1.SetValue("U_ExCurr", i, "USD");
                            //}
                            //if (i == 2)
                            //{
                            //    _withBER1.SetValue("U_ExCurr", i, "EUR");
                            //}
                            oMatrx.LoadFromDataSource();

                        }
                        


                        break;
                    #endregion
                    #region MHF
                    case "FM_MHF":
                        var _withQAT = _form.DataSources.DBDataSources.Item("@FM_OMHF");

                        oMatrx = (SAPbouiCOM.Matrix)_form.Items.Item("0_U_G").Specific;

                        TComboBox.LoadSeries(_form, "Item_0", "FM_MHF");
                        //@TABLE is the name of the DBDataSource the form's connect to 
                        string series = TUser.GetDefaultSeries("FM_MHF", SeriesReturnType.Series);
                        _withQAT.SetValue("Series", 0, TUser.GetDefaultSeries("FM_MHF", SeriesReturnType.Series));
                        _withQAT.SetValue("DocNum", 0, Convert.ToString(TDocument.GetNextDocNo(_form, TUser.GetDefaultSeriesBranch("FM_MHF"))));

                        //_withQAT.SetValue("Series", 0, TUser.GetDefaultSeriesBranch("FM_MHF"));
                        //_withQAT.SetValue("DocNum", 0, Convert.ToString(TDocument.GetNextDocNo(_form, TUser.GetDefaultSeriesBranch("FM_MHF"))));
                        _withQAT.SetValue("U_DocDate", 0, System.DateTime.Today.ToString("yyyyMMdd"));
                        _withQAT.SetValue("U_APInvDate", 0, System.DateTime.Today.ToString("yyyyMMdd"));

                        TMatrix.addRow(_form, "0_U_G", "#", "@FM_MHF1");
                        TMatrix.RefreshRowNo(_form, "0_U_G", "#");
                        oCombo = (SAPbouiCOM.ComboBox)oMatrx.GetCellSpecific("C_0_18", 1);
                        TComboBox.RemoveValidValues(oCombo);
                        sSQL = "select \"DimCode\",\"DimDesc\" from ODIM where DimCode in ('1','3','4')";
                        TComboBox.Fill(oCombo, sSQL, false);

                        _form.Items.Item("btnInvGen").Enabled = false;

                        string month = TSQL.GetSingleRecord("select Month(GETDATE())");
                        _withQAT.SetValue("U_Month", 0, month);
                        _form.Items.Item("txtSpCode").Click(BoCellClickType.ct_Regular);
                        break;
                    #endregion

                    #region EPL
                    case "FM_EPL":
                        var _withEPL = _form.DataSources.DBDataSources.Item("@FM_OEPL");

                        oMatrx = (SAPbouiCOM.Matrix)_form.Items.Item("0_U_G").Specific;

                        TComboBox.LoadSeries(_form, "Item_1", "FM_EPL");
                        //@TABLE is the name of the DBDataSource the form's connect to 
                        _withEPL.SetValue("Series", 0, TUser.GetDefaultSeriesBranch("FM_EPL"));
                        _withEPL.SetValue("DocNum", 0, Convert.ToString(TDocument.GetNextDocNo(_form, TUser.GetDefaultSeriesBranch("FM_EPL"))));
                        _withEPL.SetValue("U_DocDate", 0, System.DateTime.Today.ToString("yyyyMMdd"));

                        TMatrix.addRow(_form, "0_U_G", "#", "@FM_EPL1");
                        TMatrix.RefreshRowNo(_form, "0_U_G", "#");
                        _form.Items.Item("txtCusCode").Click(BoCellClickType.ct_Regular);
                        //oCombo = (SAPbouiCOM.ComboBox)oMatrx.GetCellSpecific("C_0_18", 1);
                        //TComboBox.RemoveValidValues(oCombo);
                        //sSQL = "select \"DimCode\",\"DimDesc\" from ODIM where DimCode in ('1','3','4')";
                        //TComboBox.Fill(oCombo, sSQL, false);
                        break;
                    #endregion

                    #region PRD
                    case "FM_PRD":
                        SAPbouiCOM.Matrix oMatrix1 = default(SAPbouiCOM.Matrix);
                        SAPbouiCOM.Matrix oMatrix2 = default(SAPbouiCOM.Matrix);
                        SAPbouiCOM.Matrix oMatrix3 = default(SAPbouiCOM.Matrix);
                        var _withPRD = _form.DataSources.DBDataSources.Item("@FM_OPRD");
                        var _withPRD1 = _form.DataSources.DBDataSources.Item("@FM_PRD1");
                        var _withPRD2 = _form.DataSources.DBDataSources.Item("@FM_PRD2");
                        var _withPRD3 = _form.DataSources.DBDataSources.Item("@FM_PRD3");

                        oMatrix1 = (SAPbouiCOM.Matrix)_form.Items.Item("0_U_G").Specific;
                        oMatrix2 = (SAPbouiCOM.Matrix)_form.Items.Item("1_U_G").Specific;
                        oMatrix3 = (SAPbouiCOM.Matrix)_form.Items.Item("2_U_G").Specific;

                        TComboBox.LoadSeries(_form, "Item_0", "FM_PRD");
                        //@TABLE is the name of the DBDataSource the form's connect to 
                        _withPRD.SetValue("Series", 0, TUser.GetDefaultSeriesBranch("FM_PRD"));
                        _withPRD.SetValue("DocNum", 0, Convert.ToString(TDocument.GetNextDocNo(_form, TUser.GetDefaultSeriesBranch("FM_PRD"))));
                        _withPRD.SetValue("U_DocDate", 0, System.DateTime.Today.ToString("yyyyMMdd"));

                        TMatrix.addRow(_form, "0_U_G", "#", "@FM_PRD1");
                        TMatrix.RefreshRowNo(_form, "0_U_G", "#");

                        TMatrix.addRow(_form, "1_U_G", "#", "@FM_PRD2");
                        TMatrix.RefreshRowNo(_form, "1_U_G", "#");

                        TMatrix.addRow(_form, "2_U_G", "#", "@FM_PRD3");
                        TMatrix.RefreshRowNo(_form, "2_U_G", "#");

                        //_form.Items.Item("txtCusCode").Click(BoCellClickType.ct_Regular);
                        //oCombo = (SAPbouiCOM.ComboBox)oMatrx.GetCellSpecific("C_0_18", 1);
                        //TComboBox.RemoveValidValues(oCombo);
                        //sSQL = "select \"DimCode\",\"DimDesc\" from ODIM where DimCode in ('1','3','4')";
                        //TComboBox.Fill(oCombo, sSQL, false);

                        oMatrix1.FlushToDataSource();
                        oMatrix2.FlushToDataSource();
                        oMatrix3.FlushToDataSource();

                        oMatrix3.Columns.Item("C_2_3").Editable = true;

                        for (int i=1;i<=12;i++)
                        {
                           // string MonthSql = TSQL.GetSingleRecord("select DateName(month, "+i+")").ToString().Trim();
                            string MonthSql = TSQL.GetSingleRecord("Select DateName(month , DateAdd(month," + i + " , -1 ) )+'-'+ cast(year(getdate()) as varchar)").ToString().Trim();
                            _withPRD1.SetValue("U_FPMonth", i-1, MonthSql);
                            _withPRD2.SetValue("U_RMMonth", i-1, MonthSql);
                            _withPRD3.SetValue("U_SMMonth", i-1, MonthSql);
                            if(i==12)
                            {
                                _withPRD1.SetValue("U_FPMonth", i, "GrandTotal");
                                _withPRD2.SetValue("U_RMMonth", i, "GrandTotal");
                                _withPRD3.SetValue("U_SMMonth", i, "GrandTotal");

                            }

                            oMatrix1.LoadFromDataSourceEx();
                            oMatrix2.LoadFromDataSourceEx();
                            oMatrix3.LoadFromDataSourceEx();

                            if (i!=12)
                            {
                                TMatrix.addRow(_form, "0_U_G", "#", "@FM_PRD1");
                                TMatrix.RefreshRowNo(_form, "0_U_G", "#");

                                TMatrix.addRow(_form, "1_U_G", "#", "@FM_PRD2");
                                TMatrix.RefreshRowNo(_form, "1_U_G", "#");

                                TMatrix.addRow(_form, "2_U_G", "#", "@FM_PRD3");
                                TMatrix.RefreshRowNo(_form, "2_U_G", "#");
                            }
                            //oMatrix1.LoadFromDataSourceEx();
                            //oMatrix2.LoadFromDataSourceEx();
                            //oMatrix3.LoadFromDataSourceEx();
                        }
                        
                        break;
                        #endregion
                        #endregion
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void FindMode(SAPbouiCOM.Form oForm)
        {
            switch (oForm.TypeEx)
            {
                #region zion
                case "FM_EPL":
                    TItem.Enable(oForm, "1_U_E", true);
                    break;
                case "FM_BER":
                    //TItem.Enable(oForm, "1_U_E", true);
                    break;
                    #endregion


            }
        }

    }

}
