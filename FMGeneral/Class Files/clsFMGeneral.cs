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
            SAPbouiCOM.Matrix oMatrx1;
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
                        var _withBER2 = _form.DataSources.DBDataSources.Item("@FM_BER2");

                        oMatrx = (SAPbouiCOM.Matrix)_form.Items.Item("0_U_G").Specific;

                        TComboBox.LoadSeries(_form, "Item_0", "FM_BER");
                        //@TABLE is the name of the DBDataSource the form's connect to 
                        //_withBER.SetValue("Series", 0, TUser.GetDefaultSeriesBranch("FM_BER"));
                        string series1 = TUser.GetDefaultSeries("FM_BER", SeriesReturnType.Series);
                        _withBER.SetValue("Series", 0, TUser.GetDefaultSeries("FM_BER", SeriesReturnType.Series));
                        _withBER.SetValue("DocNum", 0, Convert.ToString(TDocument.GetNextDocNo(_form, TUser.GetDefaultSeriesBranch("FM_BER"))));
                        _withBER.SetValue("U_DocDate", 0, System.DateTime.Today.ToString("yyyyMMdd"));


                        oMatrx = (SAPbouiCOM.Matrix)_form.Items.Item("0_U_G").Specific;
                        //oMatrx.FlushToDataSource();

                        for (int i = 1; i <= 2; i++)
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


                        oMatrx1 = (SAPbouiCOM.Matrix)_form.Items.Item("1_U_G").Specific;
                        SAPbobsCOM.Recordset oRS = (SAPbobsCOM.Recordset)B1Connections.diCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                        string BankSQL = "select * from( select distinct CASE WHEN T1.AcctCode='124101' OR T1.AcctCode= '124102' OR T1.AcctCode= '124104' THEN 'CASH IN HAND' ";
                        BankSQL += "WHEN T1.AcctCode = '124201' OR T1.AcctCode = '124500' OR T1.AcctCode = '124708' THEN 'BFA'  WHEN T1.AcctCode = '124202' THEN 'BIC' ";
                        BankSQL += "WHEN T1.AcctCode = '124204' THEN 'BPA' WHEN T1.AcctCode = '124205' OR T1.AcctCode = '124507' THEN 'BCGA' WHEN T1.AcctCode = '124206' OR T1.AcctCode = '124516' THEN 'SBA' ";
                        BankSQL += "WHEN T1.AcctCode = '124207' THEN 'BAI' WHEN T1.AcctCode = '124211' THEN 'BANCO VALOR' WHEN T1.AcctCode = '124213' THEN 'YETU'  ";
                        BankSQL += "WHEN T1.AcctCode = '124214' OR T1.AcctCode = '124521' THEN 'BIR' WHEN T1.AcctCode = '124215'  THEN 'BCS' WHEN T1.AcctCode = '124216' THEN 'KEVE' WHEN T1.AcctCode = '124404' THEN 'BCI' ";
                        BankSQL += "WHEN T1.AcctCode='125216' THEN 'DEBIT CARD-02-MEDICAL USE' WHEN T1.AcctCode='125217' THEN 'DEBIT CARD-03-OPERATION USE' WHEN T1.AcctCode='125218' THEN 'DEBIT CARD-04-OFFICE USE' ";
                        BankSQL += "WHEN T1.AcctCode = '125219' THEN 'DEBIT CARD-15.001 BENGUELA' WHEN T1.AcctCode = '125220' THEN 'DEBIT CARD-15.002 OFFICE' WHEN T1.AcctCode = '124510' THEN 'BFA SHORT TERM LOAN-OD FACILTY' WHEN T1.AcctCode = '124514' THEN 'CAIXA SHORT TERM LOAN-OD FACILTY'  WHEN T1.AcctCode='124519' THEN 'CAIXA RESERVE' WHEN T1.AcctCode ='124522' THEN 'Banco VLR - Kz : 91001840001 (OD)' END AS[AccountName] ";

                        BankSQL += ",CASE WHEN T1.AcctCode='124101' OR T1.AcctCode= '124102' OR T1.AcctCode= '124104' THEN '1' ";
                        BankSQL += "WHEN T1.AcctCode = '124201' OR T1.AcctCode = '124500' OR T1.AcctCode = '124708' THEN '2'  WHEN T1.AcctCode = '124202' THEN '3' ";
                        BankSQL += "WHEN T1.AcctCode = '124204' THEN '4' WHEN T1.AcctCode = '124205' OR T1.AcctCode = '124507' THEN '5' WHEN T1.AcctCode = '124206' OR T1.AcctCode = '124516' THEN '6' ";
                        BankSQL += "WHEN T1.AcctCode = '124207' THEN '7' WHEN T1.AcctCode = '124211' THEN '8' WHEN T1.AcctCode = '124213' THEN '9'  ";
                        BankSQL += "WHEN T1.AcctCode = '124214' OR T1.AcctCode = '124521' THEN '10' WHEN T1.AcctCode = '124215'  THEN '11' WHEN T1.AcctCode = '124216' THEN '12' WHEN T1.AcctCode = '124404' THEN '13' ";
                        BankSQL += "WHEN T1.AcctCode='125216' THEN '14' WHEN T1.AcctCode='125217' THEN '15' WHEN T1.AcctCode='125218' THEN '16' ";
                        BankSQL += "WHEN T1.AcctCode = '125219' THEN '17' WHEN T1.AcctCode = '125220' THEN '18' WHEN T1.AcctCode = '124510' THEN '19' WHEN T1.AcctCode = '124514' THEN '20'  WHEN T1.AcctCode='124519' THEN '21' WHEN T1.AcctCode='124522' THEN '22'  END AS[AccountOrder] ";

                        BankSQL += " FROM OACT T1 WHERE T1.AcctCode in ('124101', '124201', '124202', '124204', '124205', '124206', '124207', '124211', '124404', '124213', '124214', '124215', '124216', '124102', '124500', '124507', '124516', '124521', '124104', '124708','125216','125217','125218','125219','125220', '124510', '124514', '124519','124522') )A order by cast([AccountOrder] as numeric)";
                        oRS = TSQL.GetRecords(BankSQL);

                        oMatrx1.FlushToDataSource();
                        oRS.MoveFirst();
                        for (int i = 0; i < oRS.RecordCount; i++)
                        {

                            TMatrix.addRow(_form, "1_U_G", "#", "@FM_BER2");
                            TMatrix.RefreshRowNo(_form, "1_U_G", "#");

                            string AccountName = oRS.Fields.Item("AccountName").Value.ToString().Trim();
                            _withBER2.SetValue("U_Bank", i, AccountName);
                            //_withBER2.SetValue("U_Bank", i, "MM");

                            oMatrx1.LoadFromDataSource();
                            oRS.MoveNext();
                        }


                        break;
                    #endregion

                    #region RPD
                    case "FM_RPD":
                        var _withRPD = _form.DataSources.DBDataSources.Item("@FM_ORPD");

                        oMatrx = (SAPbouiCOM.Matrix)_form.Items.Item("0_U_G").Specific;

                        TComboBox.LoadSeries(_form, "Item_1", "FM_RPD");
                        //@TABLE is the name of the DBDataSource the form's connect to 
                        string seriesPRD = TUser.GetDefaultSeries("FM_RPD", SeriesReturnType.Series);
                        _withRPD.SetValue("Series", 0, TUser.GetDefaultSeries("FM_RPD", SeriesReturnType.Series));
                        _withRPD.SetValue("DocNum", 0, Convert.ToString(TDocument.GetNextDocNo(_form, TUser.GetDefaultSeriesBranch("FM_RPD"))));

                        //_withQAT.SetValue("Series", 0, TUser.GetDefaultSeriesBranch("FM_MHF"));
                        //_withQAT.SetValue("DocNum", 0, Convert.ToString(TDocument.GetNextDocNo(_form, TUser.GetDefaultSeriesBranch("FM_MHF"))));
                        _withRPD.SetValue("U_DocDate", 0, System.DateTime.Today.ToString("yyyyMMdd"));
                        _withRPD.SetValue("U_DocDueDate", 0, System.DateTime.Today.ToString("yyyyMMdd"));

                        _withRPD.SetValue("U_DocType", 0, "I");
                        _withRPD.SetValue("U_DocCur", 0, "AOA");
                        string docRate = TSQL.GetSingleRecord("select Rate from ORTT WHERE CAST(RateDate AS date)=Cast(GETDATE() as date) and Currency='AOA'");
                        _withRPD.SetValue("U_DocRate", 0, docRate);

                        //
                        
                        
                        if (_withRPD.GetValue("U_DocType", 0).ToString().Trim() == "S")
                        {
                            oMatrx.Columns.Item("C_0_1").Visible = false;
                            oMatrx.Columns.Item("C_0_3").Visible = false;
                            oMatrx.Columns.Item("Col_0").Visible = false;
                            oMatrx.Columns.Item("Col_3").Visible = true;
                        }
                        else
                        {
                            oMatrx.Columns.Item("C_0_1").Visible = true;
                            oMatrx.Columns.Item("C_0_3").Visible = true;
                            oMatrx.Columns.Item("Col_0").Visible = true;
                            oMatrx.Columns.Item("Col_3").Visible = false;
                        }

                        //

                        TMatrix.addRow(_form, "0_U_G", "#", "@FM_RPD1");
                        TMatrix.RefreshRowNo(_form, "0_U_G", "#");




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

                        string userCode = TUser.UserCode;
                        string sSQL1 = "Select \"Series\" from OUSR T0   Inner Join OUDG  T1 on T0.\"DfltsGroup\"=T1.\"Code\"	Inner Join NNM1 T2 on T2.\"BPLId\"=T1.\"BPLId\" inner join OFPR T3 on T3.\"Indicator\" = T2.\"Indicator\" and T3.\"PeriodStat\" = 'N' and T2.\"Locked\" = 'N' where \"USER_CODE\"='" + TUser.UserCode + "' and \"ObjectCode\"='FM_MHF' and T2.\"Remark\"='Y'";
                        sSQL1 = "Select    \"Series\" from  NNM1 T2 inner join OFPR T3 on T3.\"Indicator\" = T2.\"Indicator\" and T3.\"PeriodStat\" = 'N' and T2.\"Locked\"= 'N' where \"ObjectCode\"='FM_MHF' and T2.\"BPLId\" ='" + TUser.GetDefaultBranch() + "'";

                        string docNum = Convert.ToString(TDocument.GetNextDocNo(_form, TUser.GetDefaultSeriesBranch("FM_MHF")));

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

                        string signeduser = TUser.UserCode;
                        string InvAccess = TSQL.GetSingleRecord("select U_MHFAccess from OUSR WHERE USER_CODE='" + signeduser + "'").ToString().Trim();
                        if (InvAccess == "Y")
                        {

                            _form.Items.Item("txtWTCode").Enabled = true;
                            _form.Items.Item("11_U_Cb").Enabled = true;
                        }
                        else
                        {
                            _form.Items.Item("txtWTCode").Enabled = false;
                            _form.Items.Item("11_U_Cb").Enabled = false;

                        }

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

                        for (int i = 1; i <= 12; i++)
                        {
                            // string MonthSql = TSQL.GetSingleRecord("select DateName(month, "+i+")").ToString().Trim();
                            string MonthSql = TSQL.GetSingleRecord("Select DateName(month , DateAdd(month," + i + " , -1 ) )+'-'+ cast(year(getdate()) as varchar)").ToString().Trim();
                            _withPRD1.SetValue("U_FPMonth", i - 1, MonthSql);
                            _withPRD2.SetValue("U_RMMonth", i - 1, MonthSql);
                            _withPRD3.SetValue("U_SMMonth", i - 1, MonthSql);
                            if (i == 12)
                            {
                                _withPRD1.SetValue("U_FPMonth", i, "GrandTotal");
                                _withPRD2.SetValue("U_RMMonth", i, "GrandTotal");
                                _withPRD3.SetValue("U_SMMonth", i, "GrandTotal");

                            }

                            oMatrix1.LoadFromDataSourceEx();
                            oMatrix2.LoadFromDataSourceEx();
                            oMatrix3.LoadFromDataSourceEx();

                            if (i != 12)
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

                    #region PBD
                    case "FM_PBD":
                        var _withPDB = _form.DataSources.DBDataSources.Item("@FM_OPBD");
                        var _withPDB1 = _form.DataSources.DBDataSources.Item("@FM_PBD1");

                        oMatrx = (SAPbouiCOM.Matrix)_form.Items.Item("0_U_G").Specific;

                        //TComboBox.LoadSeries(_form, "Item_1", "FM_EPL");
                        ////@TABLE is the name of the DBDataSource the form's connect to 
                        //_withEPL.SetValue("Series", 0, TUser.GetDefaultSeriesBranch("FM_EPL"));
                        //_withEPL.SetValue("DocNum", 0, Convert.ToString(TDocument.GetNextDocNo(_form, TUser.GetDefaultSeriesBranch("FM_EPL"))));
                        _withPDB.SetValue("U_DocDate", 0, System.DateTime.Today.ToString("yyyyMMdd"));
                        _withPDB.SetValue("U_Year", 0, System.DateTime.Today.ToString("yyyy"));

                        //TMatrix.addRow(_form, "0_U_G", "#", "@FM_EPL1");
                        //TMatrix.RefreshRowNo(_form, "0_U_G", "#");
                        //_form.Items.Item("txtCusCode").Click(BoCellClickType.ct_Regular);


                        oMatrx1 = (SAPbouiCOM.Matrix)_form.Items.Item("0_U_G").Specific;
                        SAPbobsCOM.Recordset oRS1 = (SAPbobsCOM.Recordset)B1Connections.diCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                        string sqlCategory = "select Code,U_Category from [@FM_OBCT] order by Cast(Code as int)";
                        oRS1 = TSQL.GetRecords(sqlCategory);

                        oMatrx1.FlushToDataSource();
                        oRS1.MoveFirst();
                        for (int i = 0; i < oRS1.RecordCount; i++)
                        {

                            TMatrix.addRow(_form, "0_U_G", "#", "@FM_PBD1");
                            TMatrix.RefreshRowNo(_form, "0_U_G", "#");

                            string categoryName = oRS1.Fields.Item("U_Category").Value.ToString().Trim();
                            string categoryCode = oRS1.Fields.Item("Code").Value.ToString().Trim();
                            _withPDB1.SetValue("U_CtgryCode", i, categoryCode);
                            _withPDB1.SetValue("U_Category", i, categoryName);
                            //_withBER2.SetValue("U_Bank", i, "MM");

                            oMatrx1.LoadFromDataSource();
                            oRS1.MoveNext();
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
                    TItem.Enable(oForm, "1_U_E", true);
                    break;
                    #endregion


            }
        }

    }

}
