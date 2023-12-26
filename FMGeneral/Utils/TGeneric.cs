using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using SAPbobsCOM;
using SAPbouiCOM;
////using SAPbouiCOM.Framework;
using System.Threading;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Configuration;
using B1WizardBase;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;

namespace SBOHelper.Utils
{

    internal class TGeneric
    {

        public static SAPbobsCOM.Company diCompany { get; set; }
        private static string sOpenFileName = string.Empty;
        private static string sSaveFileName = string.Empty;

        private static string sSelectedPrinter;
        public static readonly Dictionary<string, string> finYearMonths = new Dictionary<string, string>()
	{
	    {"4", "April"},
        {"5", "May"},
        {"6", "June"},
        {"7", "July"},
        {"8", "August"},
        {"9", "September"},
        {"10", "October"},
        {"11", "November"},
        {"12", "December"},
        {"1", "January"},
	    {"2", "February"},
	    {"3", "March"}
	};

        //private static string sErrorMessage = string.Empty;


        //public static string ErrorMessage
        //{
        //    get { return sErrorMessage; }
        //    set { ErrorMessage = value; }
        //}

        public static string ErrorMessage { get; set; }

        //public static string DefaultTaxCode
        //{
        //    get { return GetDefaultTaxCode(sItemCode, type); }
        //}

        public static string DbPassword
        {
            get { return GetDbPassword(); }
        }

        //public static double TaxRate(string sTaxCode) {
        //    get { return GetTaxRate(sTaxCode.Trim()); }
        //}

        ////public static string DueDate {
        ////    get { return GetDueDate(sCardCode.Trim()); }
        ////}

        //public static string DueDate {
        //    get { return GetDueDate(sCardCode.Trim(), PostDate.Trim()); }
        //}

        //Methods 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        //public static bool IsNumeric(string s)
        //{

        //    try
        //    {
        //        int.Parse(s);
        //    }
        //    catch (Exception Ex)
        //    {
        //        return false;
        //    }
        //    return true;

        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string MonthString(long Value)
        {

            long num = Value;

            try
            {

                if (((num <= 12) && (num >= 1)))
                {
                    switch (Convert.ToInt32((num - 1)))
                    {
                        case 0:
                            return "January";
                        case 1:
                            return "February";
                        case 2:
                            return "March";
                        case 3:
                            return "April";
                        case 4:
                            return "May";
                        case 5:
                            return "June";
                        case 6:
                            return "July";
                        case 7:
                            return "August";
                        case 8:
                            return "September";
                        case 9:
                            return "October";
                        case 10:
                            return "November";
                        case 11:
                            return "December";
                    }

                }

                return string.Empty;


            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Reference"></param>
        /// <remarks></remarks>

        public static void ReleaseComObject(object Reference)
        {
            try
            {
                while (!(System.Runtime.InteropServices.Marshal.ReleaseComObject(Reference) <= 0))
                {

                }
            }
            catch
            {
            }
            finally
            {
                Reference = null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        public static void CollectGabage()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ActCode"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetActFormatCode(string _ActCode)
        {

            string sFormatCode = string.Empty;
            Recordset rsActDts = null;
            string sSQL = string.Empty;

            try
            {
                sSQL = "select EnbSgmnAct from CINF";


                if (TSQL.GetSingleRecord(sSQL).Equals("Y"))
                {
                    sSQL = "select FormatCode from OACT where AcctCode ='{0}'";
                    sSQL = string.Format(sSQL, _ActCode.Trim());
                    sFormatCode = TSQL.GetSingleRecord(sSQL).Trim();
                }
                else
                {
                    sFormatCode = _ActCode;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sFormatCode;
        }


        public static string GetActCode(string _ActCode)
        {

            string sActCode = string.Empty;
            Recordset rsActDts = null;
            string sSQL = string.Empty;

            try
            {
                sSQL = "select EnbSgmnAct from CINF";
                if (TSQL.GetSingleRecord(sSQL).Equals("Y"))
                {
                    sSQL = "select AcctCode from OACT where FormatCode ='{0}'";
                    sSQL = string.Format(sSQL, _ActCode.Trim());
                    sActCode = TSQL.GetSingleRecord(sSQL).Trim();
                }
                else
                {
                    sActCode = _ActCode;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sActCode;
        }
      
        public static int GetCompanyLocation()
        {

           
          
            string sSQL = string.Empty;

            try
            {
                
                    sSQL = "Select T1.Location from OADM T0 Inner Join OWHS T1 on T0.Dfltwhs=T1.WhsCode";
                  
                   
            }
              

            catch (Exception ex)
            {
                throw ex;
            }

            return Convert.ToInt16(TSQL.GetSingleRecord(sSQL));
        }
        /// <summary>
        /// Saves XML representation of a form to the path
        /// <para>Path should end with a slash. Filename is 'UniqueID'.xml </para>
        /// </summary>
        /// <param name="_form">The form to save. </param>
        /// <param name="_path">String with path information e.g. 'c:\temp\' </param>
        /// <returns>Returns the XML string or nothing. </returns>
        public static System.Xml.XmlDocument saveFormAsXML(ref SAPbouiCOM.Form _form, string _path)
        {

            try
            {
                System.Xml.XmlDocument oXMLDoc = new System.Xml.XmlDocument();
                oXMLDoc.LoadXml(_form.GetAsXML());
                oXMLDoc.Save(_path + _form.UniqueID + ".xml");
                return oXMLDoc;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_sSqlQry"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static SAPbobsCOM.Recordset GetRecordset(string _sSqlQry)
        {
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
            // company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            company = B1Connections.diCompany;
            Recordset oRecord = (SAPbobsCOM.Recordset)company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            try
            {
                oRecord.DoQuery(_sSqlQry);
                return oRecord;
            }
            catch (Exception ex)
            {
                TGeneric.ReleaseComObject((object)oRecord);
                TGeneric.CollectGabage();
                throw ex;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_form"></param>
        /// <param name="_sUID"></param>
        /// <param name="_sTableName"></param>
        /// <param name="_sAlias"></param>
        /// <remarks></remarks>

        public static void SetDataBind(SAPbouiCOM.Form _form, SAPbouiCOM.BoFormItemTypes _type, string _sUID, string _sTableName, string _sAlias, DataSourceType _DSType)
        {
            try
            {
                switch (_DSType)
                {
                    case DataSourceType.DBDataSource:
                        switch (_type)
                        {
                            case BoFormItemTypes.it_EDIT:
                                SAPbouiCOM.EditText oEditText = (SAPbouiCOM.EditText)_form.Items.Item(_sUID).Specific;
                                oEditText.DataBind.SetBound(true, _sTableName.Trim(), _sAlias.Trim());
                                break;
                            case BoFormItemTypes.it_COMBO_BOX:
                                SAPbouiCOM.ComboBox oCombo = (SAPbouiCOM.ComboBox)_form.Items.Item(_sUID).Specific;
                                oCombo.DataBind.SetBound(true, _sTableName.Trim(), _sAlias.Trim());
                                break;
                        }
                        break;
                    case DataSourceType.UserDataSource:
                        switch (_type)
                        {
                            case BoFormItemTypes.it_EDIT:
                                SAPbouiCOM.EditText oEditText = (SAPbouiCOM.EditText)_form.Items.Item(_sUID).Specific;
                                oEditText.DataBind.SetBound(true, _sTableName.Trim(), _sAlias.Trim());
                                break;
                            case BoFormItemTypes.it_COMBO_BOX:
                                SAPbouiCOM.ComboBox oCombo = (SAPbouiCOM.ComboBox)_form.Items.Item(_sUID).Specific;
                                oCombo.DataBind.SetBound(true, _sTableName.Trim(), _sAlias.Trim());
                                break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Public Shared Function GetDocEntryFromDocNum(ByVal DocNum As Integer, ByVal DocType As BoObjectTypes, ByVal Company As Company) As Integer
        //    Dim queryStr As String = Nothing
        //    Dim o As Recordset = Nothing
        //    Dim tableNameForObjectType As String = Nothing
        //    Dim num As Integer = 0
        //    num = -1
        //    tableNameForObjectType = ClsDIGeneric.GetTableNameForObjectType(DocType)
        //    If (tableNameForObjectType <> "") Then
        //        queryStr = String.Concat(New Object() {"SELECT DOCENTRY FROM [", tableNameForObjectType, "] WHERE DOCNUM = ", DocNum})
        //        Try
        //            o = DirectCast(Company.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
        //            o.DoQuery(queryStr)
        //        Catch exception As Exception
        //            Return -1
        //        End Try
        //        If Not o.EoF Then
        //            num = Convert.ToInt32(o.Fields.Item(0).Value)
        //        Else
        //            num = -1
        //        End If
        //    End If
        //    Marshal.ReleaseComObject(o)
        //    Return num
        //End Function

        //Public Shared Function GetDocNumFromDocEntry(ByVal DocEntry As Integer, ByVal DocType As BoObjectTypes, ByVal Company As Company) As Integer
        //    Dim queryStr As String = Nothing
        //    Dim o As Recordset = Nothing
        //    Dim tableNameForObjectType As String = Nothing
        //    Dim num As Integer = 0
        //    num = -1
        //    tableNameForObjectType = ClsDIGeneric.GetTableNameForObjectType(DocType)
        //    If (tableNameForObjectType <> "") Then
        //        queryStr = String.Concat(New Object() {"SELECT DOCNUM FROM [", tableNameForObjectType, "] WHERE DOCENTRY = ", DocEntry})
        //        Try
        //            o = DirectCast(Company.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
        //            o.DoQuery(queryStr)
        //            If Not o.EoF Then
        //                num = Convert.ToInt32(o.Fields.Item(0).Value)
        //            End If
        //        Catch exception As Exception
        //            Return num
        //        End Try
        //    End If
        //    Marshal.ReleaseComObject(o)
        //    Return num
        //End Function

        public static int GetColumnPosition(SAPbouiCOM.Matrix oMatrix, string colUID)
        {
            int iRetVal = -1;
            try
            {
                for (int iRows = 0; iRows <= oMatrix.Columns.Count - 1; iRows++)
                {
                    if (oMatrix.Columns.Item(iRows).UniqueID.Equals(colUID.Trim()))
                    {
                        iRetVal = iRows;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
                return iRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetWarehouseDetails(string sWhsCode, WarehouseDetails _ReturnType)
        {
            SAPbobsCOM.Warehouses oWarehouses = default(SAPbobsCOM.Warehouses);
            string sRetVal = string.Empty;
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
            // company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            company = B1Connections.diCompany;
            try
            {
                oWarehouses = (SAPbobsCOM.Warehouses)company.GetBusinessObject(BoObjectTypes.oWarehouses);
                if (oWarehouses.GetByKey(sWhsCode.Trim()))
                {
                    switch (_ReturnType)
                    {
                        case WarehouseDetails.Location:
                            sRetVal = Convert.ToString(oWarehouses.Location);
                            break;
                        case WarehouseDetails.Name:
                            sRetVal = oWarehouses.WarehouseName;
                            break;
                        case WarehouseDetails.State:
                            sRetVal = oWarehouses.State;
                            break;
                    }
                }
                return sRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static double GetItemPrice(string sCardCode, string sItemCode, float amount, string sRefDate)
        {
            SAPbobsCOM.SBObob oSBObob = default(SAPbobsCOM.SBObob);
            SAPbobsCOM.Recordset rsPrice = default(SAPbobsCOM.Recordset);
            string sPrice = string.Empty;
            System.DateTime dtRefDate = default(System.DateTime);
            double dDiscRate = 0;
            double dItemPrice = 0;
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
            //company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            company = B1Connections.diCompany;
            try
            {
                oSBObob = (SAPbobsCOM.SBObob)company.GetBusinessObject(BoObjectTypes.BoBridge);
                rsPrice = (SAPbobsCOM.Recordset)company.GetBusinessObject(BoObjectTypes.BoRecordset);
                dtRefDate = TDataTime.GetDate(sRefDate.Trim());
                rsPrice = oSBObob.GetItemPrice(sCardCode, sItemCode, amount, dtRefDate);
                if (!rsPrice.EoF)
                {
                    dDiscRate = TGeneric.GetDiscountPercentage(sCardCode, sItemCode);
                    dItemPrice = (Convert.ToDouble(rsPrice.Fields.Item(0).Value) / (100 - dDiscRate)) * 100;
                }
                return dItemPrice;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static double GetItemPrice(string sCardCode, string sItemCode)
        {
            string sSQL = string.Empty;
            try
            {
                sSQL = "SELECT ISNULL(Price,0) Price FROM ITM1 WHERE ItemCode ='{0}' AND PriceList = ";
                sSQL += "(SELECT ListNum FROM OCRD WHERE CardCode ='{1}')";
                sSQL = string.Format(sSQL, sItemCode.Trim(), sCardCode.Trim());
                return Convert.ToDouble(TSQL.GetSingleRecord(sSQL).Trim());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static double GetDiscountPercentage(string sCardCode, string sItemCode)
        {

            SAPbobsCOM.SpecialPrices oSpecialPrices = default(SAPbobsCOM.SpecialPrices);
            SAPbobsCOM.BusinessPartners oBusinessPartners = default(SAPbobsCOM.BusinessPartners);
            SAPbobsCOM.DiscountGroups oDiscountGroups = default(SAPbobsCOM.DiscountGroups);
            SAPbobsCOM.Items oItems = default(SAPbobsCOM.Items);
            double dDiscountPercentage = 0;
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
            // company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            company = B1Connections.diCompany;
            try
            {
                oSpecialPrices = (SAPbobsCOM.SpecialPrices)company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oSpecialPrices);
                oBusinessPartners = (SAPbobsCOM.BusinessPartners)company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
                oItems = (SAPbobsCOM.Items)company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems);
                if (oBusinessPartners.GetByKey(sCardCode.Trim()) & oItems.GetByKey(sItemCode.Trim()))
                {
                    if (oSpecialPrices.GetByKey(sItemCode.Trim(), sCardCode.Trim()))
                    {
                        if (oSpecialPrices.DiscountPercent > 0)
                        {
                            dDiscountPercentage = oSpecialPrices.DiscountPercent;
                        }
                    }
                    else
                    {
                        dDiscountPercentage = 0;
                        oDiscountGroups = oBusinessPartners.DiscountGroups;
                        if (oDiscountGroups.BaseObjectType != DiscountGroupBaseObjectEnum.dgboNone)
                        {
                            switch (oDiscountGroups.BaseObjectType)
                            {
                                case DiscountGroupBaseObjectEnum.dgboNone:
                                    dDiscountPercentage = 0;
                                    break;
                                case DiscountGroupBaseObjectEnum.dgboItemGroups:
                                    for (int iRows = 0; iRows <= oDiscountGroups.Count - 1; iRows++)
                                    {
                                        oDiscountGroups.SetCurrentLine(iRows);
                                        if (oDiscountGroups.ObjectEntry.Equals(Convert.ToString(oItems.ItemsGroupCode).Trim()))
                                        {
                                            dDiscountPercentage = oDiscountGroups.DiscountPercentage;
                                        }
                                    }

                                    break;
                                case DiscountGroupBaseObjectEnum.dgboItemProperties:
                                    break;
                                //to be Implimented
                                case DiscountGroupBaseObjectEnum.dgboManufacturer:
                                    break;
                                //to be Implimented
                            }
                        }
                        if (dDiscountPercentage < 1)
                        {
                            if (oSpecialPrices.GetByKeyDiscounts(sItemCode.Trim(), oBusinessPartners.PriceListNum))
                            {
                                dDiscountPercentage = oSpecialPrices.DiscountPercent;
                            }
                        }
                    }
                }

                return dDiscountPercentage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Get the default taxcode 
        /// </summary>
        /// <param name="sItemCode">Item Code</param>
        /// <param name="type">Doc Type ( AR/AP )</param>
        /// <returns>If sucessfull then returns the taxcode</returns>
        /// <remarks></remarks>
        public  static string GetDefaultTaxCode(string sItemCode, string sIGST)
        {

            string sReturnValue = string.Empty;
            string sSQL = string.Empty;
            SAPbobsCOM.Items oItems = null;
            string sItemType = string.Empty;
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
            //company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            company = B1Connections.diCompany;
            try
            {
                oItems = (SAPbobsCOM.Items)company.GetBusinessObject(BoObjectTypes.oItems);
                //Retrieve a User record by its key from the database
                sSQL = "Select U_TaxCode from OITM T_OITM INNER JOIN [@CCS_OTCD] T_OTCD on T_OITM.U_TxRate=T_OTCD.U_TaxRate where ItemCode='"+sItemCode+"' and T_OTCD.U_IGST='"+sIGST+"'";
                
                    sReturnValue = TSQL.GetSingleRecord(sSQL);
                
                return sReturnValue.Trim();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                TGeneric.ReleaseComObject((object)oItems);
                TGeneric.CollectGabage();
            }

        }

        public static double GetTaxRate(string sTaxCode)
        {
            double bTaxRate = 0;
            string sSQL = string.Empty;
            string sRate = string.Empty;
            try
            {
                sSQL = "SELECT U_Rate TaxRate FROM dbo.[@CCS_TAX] WHERE U_TaxCode ='{0}'";
                sSQL = string.Format(sSQL, sTaxCode.Trim());
                sRate = TSQL.GetSingleRecord(sSQL.Trim());
                if (!string.IsNullOrEmpty(sRate.Trim()))
                {
                    bTaxRate = Convert.ToDouble(sRate.Trim());
                }
                return bTaxRate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static double GetTaxRate1(string sTaxCode)
        {
            double bTaxRate = 0;
            string sSQL = string.Empty;
            string sRate = string.Empty;
            try
            {
                sSQL = "SELECT U_Rate TaxRate FROM dbo.[@CCS_TAX] WHERE U_TaxCode ='{0}'";
                sSQL = string.Format(sSQL, sTaxCode.Trim());
                sRate = TSQL.GetSingleRecord(sSQL.Trim());
                if (!string.IsNullOrEmpty(sRate.Trim()))
                {
                    bTaxRate = Convert.ToDouble(sRate.Trim());
                }
                return bTaxRate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string GetDueDate(string sCardCode)
        {
            SAPbobsCOM.SBObob oSBObob = default(SAPbobsCOM.SBObob);
            SAPbobsCOM.Recordset rsDueDate = default(SAPbobsCOM.Recordset);
            string sDate = string.Empty;
            DateTime dtDate = default(DateTime);
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
            // company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            company = B1Connections.diCompany;
            try
            {
                oSBObob = (SAPbobsCOM.SBObob)company.GetBusinessObject(BoObjectTypes.BoBridge);
                rsDueDate = (SAPbobsCOM.Recordset)company.GetBusinessObject(BoObjectTypes.BoRecordset);
                rsDueDate = oSBObob.GetDueDate(sCardCode, System.DateTime.Now);
                dtDate = (DateTime)rsDueDate.Fields.Item(0).Value;
                sDate = TDataTime.GetDateAsStringFormat(dtDate, 0x66);
                return sDate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string GetDueDate(string sCardCode, string PostingDate)
        {
            SAPbobsCOM.SBObob oSBObob = default(SAPbobsCOM.SBObob);
            SAPbobsCOM.Recordset rsDueDate = default(SAPbobsCOM.Recordset);
            string sDate = string.Empty;
            DateTime dtDate = default(DateTime);
            DateTime PostDate = default(DateTime);
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
            // company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            company = B1Connections.diCompany;
            try
            {
                PostDate = TDataTime.GetDate(PostingDate.Trim());
                oSBObob = (SAPbobsCOM.SBObob)company.GetBusinessObject(BoObjectTypes.BoBridge);
                rsDueDate = (SAPbobsCOM.Recordset)company.GetBusinessObject(BoObjectTypes.BoRecordset);
                rsDueDate = oSBObob.GetDueDate(sCardCode, PostDate);
                dtDate = (DateTime)rsDueDate.Fields.Item(0).Value;
                sDate = TDataTime.GetDateAsStringFormat(dtDate, 0x66);
                return sDate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool catchCtrlTab( SAPbouiCOM.Form _form, string _uid1, string _uid2, string _dbDataSource, string _alias)
        {

            string strValue = null;
            SAPbouiCOM.EditText oEditText = (SAPbouiCOM.EditText)_form.Items.Item(_uid1).Specific;
            try
            {
                strValue = oEditText.Value;
                oEditText.Value = "";
                _form.ActiveItem = _uid2;
                var _with1 = _form.DataSources.DBDataSources.Item(_dbDataSource);
                _with1.SetValue(_alias, 0, strValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        private static string GetDbPassword()
        {
            string sSQL = string.Empty;
            string sPassword = string.Empty;
            try
            {
                        sSQL = "SELECT \"U_PasWd\" FROM \"@CCS_ODBSTNGS\"";
                sPassword = TSQL.GetSingleRecord(sSQL);
                return sPassword;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static string GetDbPassword1()
        {
            string sSQL = string.Empty;
            string sPassword = string.Empty;
            try
            {
                sSQL = "SELECT U_PasWd Passwrd FROM dbo.[@CCS_ODBSTNGS] ";
                sPassword = TSQL.GetSingleRecord(sSQL);
                return sPassword;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string showOpenFileDialog()
        {

            System.Threading.Thread ShowFolderBrowserThread = null;
            string sFilePath = string.Empty;
            try
            {
                ShowFolderBrowserThread = new System.Threading.Thread(ShowFolderBrowser);
                if (ShowFolderBrowserThread.ThreadState == System.Threading.ThreadState.Unstarted)
                {
                    ShowFolderBrowserThread.SetApartmentState(System.Threading.ApartmentState.STA);
                    ShowFolderBrowserThread.Start();
                }
                else if (ShowFolderBrowserThread.ThreadState == System.Threading.ThreadState.Stopped)
                {
                    ShowFolderBrowserThread.Start();
                    ShowFolderBrowserThread.Join();
                }
                while (ShowFolderBrowserThread.ThreadState == System.Threading.ThreadState.Running)
                {
                    System.Windows.Forms.Application.DoEvents();
                }
                if (!string.IsNullOrEmpty(sOpenFileName.Trim()))
                {
                    sFilePath = sOpenFileName.Trim();
                }
                else
                {
                    sFilePath = string.Empty;
                }
                return sFilePath.Trim();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string SaveFileDialog()
        {

            System.Threading.Thread ShowFolderBrowserThread = null;
            string sFilePath = string.Empty;
            try
            {
                ShowFolderBrowserThread = new System.Threading.Thread(ShowSaveDialog);
                if (ShowFolderBrowserThread.ThreadState == System.Threading.ThreadState.Unstarted)
                {
                    ShowFolderBrowserThread.SetApartmentState(System.Threading.ApartmentState.STA);
                    ShowFolderBrowserThread.Start();
                }
                else if (ShowFolderBrowserThread.ThreadState == System.Threading.ThreadState.Stopped)
                {
                    ShowFolderBrowserThread.Start();
                    ShowFolderBrowserThread.Join();
                }
                while (ShowFolderBrowserThread.ThreadState == System.Threading.ThreadState.Running)
                {
                    System.Windows.Forms.Application.DoEvents();
                }
                if (!string.IsNullOrEmpty(sSaveFileName.Trim()))
                {
                    sFilePath = sSaveFileName.Trim();
                }
                else
                {
                    sFilePath = string.Empty;
                }
                return sFilePath.Trim();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void ShowSaveDialog()
        {
            System.Diagnostics.Process[] MyProcs = null;
            SaveFileDialog oSaveFile = new SaveFileDialog();
            int filterindex = 0;
            WindowWrapper MyWindow = null;
            DialogResult ret = default(DialogResult);
            try
            {
                sOpenFileName = string.Empty;
                oSaveFile.AddExtension = true;
                oSaveFile.CheckPathExists = true;
                oSaveFile.DefaultExt = "xls";
                oSaveFile.FileName = "Template.xls";
                // Put here the filename
                oSaveFile.Filter = "Microsoft Excel 97-2003(.xls)|*.xls|Microsoft Excel 2007/2010(.xlsx)|*.xlsx";
                oSaveFile.Title = "Save an Excel File";
                //oSaveFile.InitialDirectory = "<set initial folder if needed>"
                oSaveFile.OverwritePrompt = true;
                MyProcs = System.Diagnostics.Process.GetProcessesByName("SAP Business One");
                if (MyProcs.Length == 1)
                {
                    for (int iProcs = 0; iProcs <= MyProcs.Length - 1; iProcs++)
                    {
                        MyWindow = new WindowWrapper(MyProcs[iProcs].MainWindowHandle);
                        ret = oSaveFile.ShowDialog(MyWindow);
                        if (ret == DialogResult.OK)
                        {
                            sSaveFileName = oSaveFile.FileName.Trim();
                            oSaveFile.Dispose();
                        }
                        else
                        {
                            sSaveFileName = string.Empty;
                            System.Windows.Forms.Application.ExitThread();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sOpenFileName = string.Empty;
                sSaveFileName = string.Empty;
                throw ex;
            }
            finally
            {
                oSaveFile.Dispose();
            }
        }

        //Public Shared Sub ShowFolderBrowser()

        //    Dim MyProcs() As System.Diagnostics.Process
        //    Dim OpenFile As New OpenFileDialog
        //    Dim filterindex As Integer = 0
        //    Dim MyWindow As WindowWrapper
        //    Dim ret As DialogResult
        //    Try
        //        sOpenFileName = String.Empty
        //        OpenFile.Multiselect = False
        //        OpenFile.Filter = "All files(*.xls)|*.xls"
        //        OpenFile.FilterIndex = filterindex
        //        OpenFile.RestoreDirectory = True
        //        MyProcs = System.Diagnostics.Process.GetProcessesByName("SAP Business One")
        //        If MyProcs.Length = 1 Then
        //            For iProcs As Integer = 0 To MyProcs.Length - 1
        //                MyWindow = New WindowWrapper(MyProcs(iProcs).MainWindowHandle)
        //                ret = OpenFile.ShowDialog(MyWindow)
        //                If ret = DialogResult.OK Then
        //                    sOpenFileName = OpenFile.FileName
        //                    OpenFile.Dispose()
        //                Else
        //                    System.Windows.Forms.Application.ExitThread()
        //                End If
        //            Next
        //        End If
        //    Catch ex As Exception
        //        sOpenFileName = String.Empty
        //        Throw ex
        //    Finally
        //        OpenFile.Dispose()
        //    End Try
        //End Sub

        public class WindowWrapper : System.Windows.Forms.IWin32Window
        {

            public System.IntPtr _hwnd;
            public WindowWrapper(System.IntPtr handle)
            {
                _hwnd = handle;
            }

            public System.IntPtr Handle
            {
                get { return _hwnd; }
            }
        }

        //public static string FindFile()
        //{

        //    string sFilePath = string.Empty;
        //    System.Threading.Thread ShowFolderBrowserThread = null;
        //    int iProLength = 0;
        //    try
        //    {
        //        iProLength = GetRunningProcessInfo.Utils.GetProcessLength("SAP Business One");

        //        ShowFolderBrowserThread = new System.Threading.Thread(ShowFolderBrowser);
        //        if (ShowFolderBrowserThread.ThreadState == System.Threading.ThreadState.Unstarted)
        //        {
        //            ShowFolderBrowserThread.SetApartmentState(System.Threading.ApartmentState.STA);
        //            ShowFolderBrowserThread.Start(iProLength);
        //        }
        //        else if (ShowFolderBrowserThread.ThreadState == System.Threading.ThreadState.Stopped)
        //        {
        //            ShowFolderBrowserThread.Start(iProLength);
        //            ShowFolderBrowserThread.Join();
        //        }
        //        while (ShowFolderBrowserThread.ThreadState == System.Threading.ThreadState.Running)
        //        {
        //            System.Windows.Forms.Application.DoEvents();
        //        }
        //        if (!string.IsNullOrEmpty(sOpenFileName.Trim()))
        //        {
        //            sFilePath = sOpenFileName.Trim();
        //        }
        //        else
        //        {
        //            sFilePath = string.Empty;
        //        }
        //        return sFilePath.Trim();
        //    }
        //    catch (Exception ex)
        //    {
        //        TNotification.StatusBarError(ex.Message);
        //        return string.Empty;
        //    }
        //}

        //Public Shared Function FindFile() As String
        //    Dim sFilePath As String = String.Empty
        //    Dim ShowFolderBrowserThread As Threading.Thread
        //    Dim iProLength As Integer
        //    Try
        //        iProLength = GetProcessLength("SAP Business One")
        //        'If iProLength = 1 Then
        //        ShowFolderBrowserThread = New Threading.Thread(AddressOf ShowFolderBrowser)
        //        If ShowFolderBrowserThread.ThreadState = System.Threading.ThreadState.Unstarted Then
        //            ShowFolderBrowserThread.SetApartmentState(System.Threading.ApartmentState.STA)
        //            ShowFolderBrowserThread.Start(iProLength)
        //        ElseIf ShowFolderBrowserThread.ThreadState = System.Threading.ThreadState.Stopped Then
        //            ShowFolderBrowserThread.Start(iProLength)
        //            ShowFolderBrowserThread.Join()
        //        End If
        //        While ShowFolderBrowserThread.ThreadState = Threading.ThreadState.Running
        //            Windows.Forms.Application.DoEvents()
        //        End While
        //        If Not String.IsNullOrEmpty(sOpenFileName.Trim) Then
        //            sFilePath = sOpenFileName.Trim
        //        Else
        //            sFilePath = String.Empty
        //        End If
        //        'Else
        //        'TNotification.StatusBarError("More than one SAP Business One process running.")
        //        'End If
        //        Return sFilePath.Trim
        //    Catch ex As Exception
        //        TNotification.StatusBarError(ex.Message)
        //        Return String.Empty
        //    End Try

        //End Function


        public static void ShowFolderBrowser(object oObj)
        {
            System.Diagnostics.Process[] MyProcs = null;
            OpenFileDialog OpenFile = new OpenFileDialog();
            int iVal = 0;
            try
            {
                sOpenFileName = string.Empty;
                OpenFile.Multiselect = false;
                OpenFile.Filter = "All files(*.)|*.*";
                int filterindex = 0;
                try
                {
                    filterindex = 0;
                }
                catch (Exception ex)
                {
                }
                OpenFile.FilterIndex = filterindex;
                OpenFile.RestoreDirectory = true;
                MyProcs = System.Diagnostics.Process.GetProcessesByName("SAP Business One");
                iVal = Convert.ToInt32(oObj);
                // For i As Integer = 0 To iVal - 1
                WindowWrapper MyWindow = new WindowWrapper(MyProcs[iVal].MainWindowHandle);
                DialogResult ret = OpenFile.ShowDialog(MyWindow);
                if (ret == DialogResult.OK)
                {
                    sOpenFileName = OpenFile.FileName.Trim();
                    OpenFile.Dispose();
                }
                else
                {
                    System.Windows.Forms.Application.ExitThread();
                }
                //Next
            }
            catch (Exception ex)
            {
                sOpenFileName = string.Empty;
                TNotification.StatusBarError(ex.Message);
            }
            finally
            {
                OpenFile.Dispose();
                GC.Collect();
            }
        }


        public static System.Boolean IsNumeric(System.Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                    long.Parse(Expression as string);
                else
                    long.Parse(Expression.ToString());
                return true;
            }
            catch { } // just dismiss errors but return false
            return false;
        }

/// <summary>
/// HICABF Nimmy
/// </summary>
/// <returns></returns>
        public static string FindFile()
        {

            string sFilePath = string.Empty;
            System.Threading.Thread ShowFolderBrowserThread = null;
            int iProLength = 0;
            try
            {
                //iProLength = GetRunningProcessInfo.Utils.GetProcessLength("SAP Business One");

                //ShowFolderBrowserThread = new System.Threading.Thread(ShowFolderBrowser);
                //if (ShowFolderBrowserThread.ThreadState == System.Threading.ThreadState.Unstarted)
                //{
                //    ShowFolderBrowserThread.SetApartmentState(System.Threading.ApartmentState.STA);
                //    ShowFolderBrowserThread.Start(iProLength);
                //}
                //else if (ShowFolderBrowserThread.ThreadState == System.Threading.ThreadState.Stopped)
                //{
                //    ShowFolderBrowserThread.Start(iProLength);
                //    ShowFolderBrowserThread.Join();
                //}
                //while (ShowFolderBrowserThread.ThreadState == System.Threading.ThreadState.Running)
                //{
                //    System.Windows.Forms.Application.DoEvents();
                //}
                //if (!string.IsNullOrEmpty(sOpenFileName.Trim()))
                //{
                //    sFilePath = sOpenFileName.Trim();
                //}
                //else
                //{
                //    sFilePath = string.Empty;
                //}
                return "";
            }
            catch (Exception ex)
            {
                TNotification.StatusBarError(ex.Message);
                return string.Empty;
            }
        }
        /// <summary>
        /// Save uploaded data and a uniqueId to a temporary table
        /// </summary>
        /// <param name="excelFilePath"></param>
        /// <param name="uniqueID"></param>
        /// <param name="excelDataQuery"></param>
        /// <param name="sqlTableName"></param>
        /// <returns></returns>
        public static System.Data.DataTable SSUImportDataFromExcel(string excelFilePath, string uniqueID, string excelDataQuery, string sqlTableName)
        {
            OleDbConnection excelConnection = null;
            SqlConnection myConnection = null;
            try
            {
                //make sure your sheet name is correct, here sheet name is sheet1, so you can change your sheet name if have different
                //Create connection string to SQL Server

                var sqlConnectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

                //Create connection string to Excel work book

                string excelConnectionString = string.Empty;

                //string excelDataQuery = ConfigurationManager.AppSettings["SOEIMPORTQUERY"];
                //string sqlTableName = ConfigurationManager.AppSettings["SOEIMPORT"];

                string CommandText = String.Format("DELETE FROM dbo.[{0}] WHERE ID='{1}'", sqlTableName, uniqueID);

                //Getting the extension of the selected file 

                string Extension = Path.GetExtension(excelFilePath);

                switch (Extension)
                {
                    case ".xls": //Excel 97-03
                        excelConnectionString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07
                        excelConnectionString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }

                excelConnectionString = String.Format(excelConnectionString, excelFilePath);
               // sqlConnectionString = String.Format(sqlConnectionString, B1Connections.diCompany.Server, B1Connections.diCompany.CompanyDB, TGeneric.GetDbPassword());
                sqlConnectionString = String.Format(sqlConnectionString, B1Connections.diCompany.Server, B1Connections.diCompany.CompanyDB, TGeneric.GetDbPassword());
                //Create Connection to Excel work book
                excelConnection = new OleDbConnection(excelConnectionString);
                //Create OleDbCommand to fetch data from Excel
                OleDbCommand cmd = new OleDbCommand(excelDataQuery, excelConnection);
                // OleDbCommand cmd = new OleDbCommand("Select [Date],Debit,Credit,Particulars from [BSP$]", excelConnection);

                excelConnection.Open();
                DataSet ds = new DataSet();
                System.Data.DataTable dt = new System.Data.DataTable();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(ds);
                dt = ds.Tables[0];

                myConnection = new SqlConnection(sqlConnectionString);
                myConnection.Open();
                SqlCommand cmdProc = new SqlCommand("[dbo].[uspPopulateSSUImportTable]", myConnection);
                cmdProc.CommandType = CommandType.StoredProcedure;
                cmdProc.Parameters.AddWithValue("@GUID", uniqueID);
                cmdProc.Parameters.AddWithValue("@Details", dt);
                cmdProc.ExecuteNonQuery();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                excelConnection.Close();
                myConnection.Close();
            }
        }

        /// <summary>
        /// Save the secondary sales upload data to a pivottable
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool SaveSSUDataToDB(System.Data.DataTable dt)
        {
            SqlConnection myConnection = null;
            try
            {
                var sqlConnectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
               // sqlConnectionString = String.Format(sqlConnectionString, B1Connections.diCompany.Server, B1Connections.diCompany.CompanyDB, TGeneric.GetDbPassword());
                sqlConnectionString = String.Format(sqlConnectionString, B1Connections.diCompany.Server, B1Connections.diCompany.CompanyDB, TGeneric.GetDbPassword());
                myConnection = new SqlConnection(sqlConnectionString);
                myConnection.Open();
                myConnection = new SqlConnection(sqlConnectionString);
                myConnection.Open();
                // Query for Save the secondary sales upload data to a pivottable
                SqlCommand cmdProc = new SqlCommand("[dbo].[uspDataImportSSU]", myConnection);
                cmdProc.CommandType = CommandType.StoredProcedure;
                cmdProc.Parameters.AddWithValue("@Details", dt);
                cmdProc.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;                
            }
            finally
            {
                myConnection.Close();
            }
        }
        /// <summary>
        /// Saving secondary sales details to the database
        /// </summary>
        /// <param name="Guid"></param>
        /// <param name="DocId"></param>
        /// <param name="Uplddate"></param>
        /// <returns></returns>
        public static bool SaveSSUDataToDATADB( Int64 Guid,Int32 DocId,string Uplddate)
        {
            SqlConnection myConnection = null;
            try
            {
                var sqlConnectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
                //sqlConnectionString = String.Format(sqlConnectionString, B1Connections.diCompany.Server, B1Connections.diCompany.CompanyDB, TGeneric.GetDbPassword());
                sqlConnectionString = String.Format(sqlConnectionString, B1Connections.diCompany.Server, B1Connections.diCompany.CompanyDB, TGeneric.GetDbPassword());

                myConnection = new SqlConnection(sqlConnectionString);
                myConnection.Open();

                myConnection = new SqlConnection(sqlConnectionString);
                myConnection.Open();
                //Query for  selecting  data from the pivot table and save to the secondary database
                //Parameters are UniqueId,DocEntry,Date
                SqlCommand cmdProc = new SqlCommand("[dbo].[uspSaveSSUDataToTable]", myConnection);
                cmdProc.CommandTimeout = 120;
                cmdProc.CommandType = CommandType.StoredProcedure;
                cmdProc.Parameters.AddWithValue("@Guid", Guid);
                cmdProc.Parameters.AddWithValue("@DocId", DocId);
                cmdProc.Parameters.AddWithValue("@Date", Uplddate);
                cmdProc.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;

            }
            finally
            {
                myConnection.Close();
            }
        }

        public static string FindPrinter()
        {
            string sPrinterName = string.Empty;
            System.Threading.Thread ShowFolderBrowserThread = default(System.Threading.Thread);
            int iProLength = 0;
            try
            {
                //iProLength = GetRunningProcessInfo.Utils.GetProcessLength("SAP Business One");
                ////If iProLength = 1 Then
                //ShowFolderBrowserThread = new System.Threading.Thread(ShowPrinterDialog);
                //if (ShowFolderBrowserThread.ThreadState == System.Threading.ThreadState.Unstarted)
                //{
                //    ShowFolderBrowserThread.SetApartmentState(System.Threading.ApartmentState.STA);
                //    ShowFolderBrowserThread.Start(iProLength);
                //}
                //else if (ShowFolderBrowserThread.ThreadState == System.Threading.ThreadState.Stopped)
                //{
                //    ShowFolderBrowserThread.Start(iProLength);
                //    ShowFolderBrowserThread.Join();
                //}
                //while (ShowFolderBrowserThread.ThreadState == System.Threading.ThreadState.Running)
                //{
                //    System.Windows.Forms.Application.DoEvents();
                //}
                //if (!string.IsNullOrEmpty(sSelectedPrinter.Trim()))
                //{
                //    sPrinterName = sSelectedPrinter.Trim();
                //}
                //else
                //{
                //    sPrinterName = string.Empty;
                //}
                //Else
                //TNotification.StatusBarError("More than one SAP Business One process running.")
                //End If
                return sPrinterName.Trim();
            }
            catch (Exception ex)
            {
                TNotification.StatusBarError(ex.Message);
                return string.Empty;
            }

        }

        public static void ShowPrinterDialog(object oObj)
        {
            System.Diagnostics.Process[] MyProcs = null;
            PrintDialog PrintDialog1 = new PrintDialog();
            int iVal = 0;
            try
            {
                PrintDialog1.UseEXDialog = true;
                MyProcs = System.Diagnostics.Process.GetProcessesByName("SAP Business One");
                iVal = Convert.ToInt32(oObj);
                // For i As Integer = 0 To iVal - 1
                WindowWrapper MyWindow = new WindowWrapper(MyProcs[iVal].MainWindowHandle);
                DialogResult ret = PrintDialog1.ShowDialog(MyWindow);
                if (ret == DialogResult.OK)
                {
                    //sSelectedPrinter = PrintDialog1.PrinterSettings.PrinterName;

                }
                else
                {
                    sSelectedPrinter = string.Empty;
                    System.Windows.Forms.Application.ExitThread();
                }
                //Next
            }
            catch (Exception ex)
            {
                sOpenFileName = string.Empty;
                TNotification.StatusBarError(ex.Message);
            }
            finally
            {
                GC.Collect();
            }
        }

        //public static double GetTaxRate(string sTaxCode)
        //{
        //    double bTaxRate = 0;
        //    string sSQL = string.Empty;
        //    string sRate = string.Empty;
        //    try
        //    {
        //        sSQL = "SELECT U_Rate TaxRate FROM dbo.[@CCS_TAX] WHERE U_TaxCode ='{0}'";
        //        sSQL = string.Format(sSQL, sTaxCode.Trim());
        //        sRate = TSQL.GetSingleRecord(sSQL.Trim());
        //        if (!string.IsNullOrEmpty(sRate.Trim()))
        //        {
        //            bTaxRate = Convert.ToDouble(sRate.Trim());
        //        }
        //        return bTaxRate;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        //public static string GetLayoutType(string formType)
        //{
        //    string type;
        //    try
        //    {
        //        type = ConfigurationManager.AppSettings[formType];
        //    }
        //    catch
        //    {
        //        return string.Empty;
        //    }
        //    return type;
        //}

        //public static void ImportDataFromExcel(string excelFilePath,string uniqueID)
        //{
        //    OleDbConnection excelConnection = null;
        //    SqlConnection myConnection = null;
        //    try
        //    {
        //        // make sure your sheet name is correct, here sheet name is sheet1, so you can change your sheet name if have different
        //        string excelDataQuery = ConfigurationManager.AppSettings["SOEIMPORTQUERY"];
        //        //Create connection string to SQL Server
        //        var sqlConnectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        //        //Create connection string to Excel work book
        //        string excelConnectionString = string.Empty;

        //        string sqlTableName = ConfigurationManager.AppSettings["SOEIMPORT"];

        //        string CommandText = String.Format("TRUNCATE TABLE [dbo].[{0}]", sqlTableName);

        //        //Getting the extension of the selected file 
        //        string Extension = Path.GetExtension(excelFilePath);

        //        switch (Extension)
        //        {
        //            case ".xls": //Excel 97-03
        //                excelConnectionString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
        //                break;
        //            case ".xlsx": //Excel 07
        //                excelConnectionString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
        //                break;
        //        }

        //        excelConnectionString = String.Format(excelConnectionString, excelFilePath);

        //        sqlConnectionString = String.Format(sqlConnectionString, B1Connections.diCompany.Server, B1Connections.diCompany.CompanyDB,TGeneric.GetDbPassword());

        //        //Create Connection to Excel work book
        //        excelConnection = new OleDbConnection(excelConnectionString);

        //        //Create OleDbCommand to fetch data from Excel
        //        OleDbCommand cmd = new OleDbCommand(excelDataQuery, excelConnection);
        //        excelConnection.Open();
        //        DataSet ds = new DataSet();
        //        System.Data.DataTable dt = new System.Data.DataTable();
        //        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        //        da.Fill(ds);
        //        dt = ds.Tables[0];

        //        myConnection = new SqlConnection(sqlConnectionString);
        //        myConnection.Open();
        //        SqlCommand cmdProc = new SqlCommand("[dbo].[uspSample]", myConnection);
        //        cmdProc.CommandType = CommandType.StoredProcedure;
        //        cmdProc.Parameters.AddWithValue("@GUID", uniqueID);
        //        cmdProc.Parameters.AddWithValue("@Details", dt);
        //        cmdProc.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        excelConnection.Close();
        //        myConnection.Close();
        //    }
        //}

        //public static System.Data.DataTable ImportDataFromExcel(string excelFilePath, string uniqueID)
        //{
        //    OleDbConnection excelConnection = null;
        //    SqlConnection myConnection = null;
        //    try
        //    {
        //        // make sure your sheet name is correct, here sheet name is sheet1, so you can change your sheet name if have different
        //        string excelDataQuery = ConfigurationManager.AppSettings["SOEIMPORTQUERY"];
        //        //Create connection string to SQL Server
        //        var sqlConnectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        //        //Create connection string to Excel work book
        //        string excelConnectionString = string.Empty;

        //        string sqlTableName = ConfigurationManager.AppSettings["SOEIMPORT"];

        //        string CommandText = String.Format("TRUNCATE TABLE [dbo].[{0}]", sqlTableName);

        //        //Getting the extension of the selected file 
        //        string Extension = Path.GetExtension(excelFilePath);

        //        switch (Extension)
        //        {
        //            case ".xls": //Excel 97-03
        //                excelConnectionString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
        //                break;
        //            case ".xlsx": //Excel 07
        //                excelConnectionString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
        //                break;
        //        }

        //        excelConnectionString = String.Format(excelConnectionString, excelFilePath);

        //        sqlConnectionString = String.Format(sqlConnectionString, B1Connections.diCompany.Server, B1Connections.diCompany.CompanyDB, TGeneric.GetDbPassword());

        //        //Create Connection to Excel work book
        //        excelConnection = new OleDbConnection(excelConnectionString);

        //        //Create OleDbCommand to fetch data from Excel
        //        OleDbCommand cmd = new OleDbCommand(excelDataQuery, excelConnection);
        //        excelConnection.Open();
        //        DataSet ds = new DataSet();
        //        System.Data.DataTable dt = new System.Data.DataTable();
        //        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        //        da.Fill(ds);
        //        dt = ds.Tables[0];

        //        myConnection = new SqlConnection(sqlConnectionString);
        //        myConnection.Open();
        //        SqlCommand cmdProc = new SqlCommand("[dbo].[uspSample]", myConnection);
        //        cmdProc.CommandType = CommandType.StoredProcedure;
        //        cmdProc.Parameters.AddWithValue("@GUID", uniqueID);
        //        cmdProc.Parameters.AddWithValue("@Details", dt);
        //        cmdProc.ExecuteNonQuery();

        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        excelConnection.Close();
        //        myConnection.Close();
        //    }
        //}


        //public static System.Data.DataTable ImportDataFromExcel(string excelFilePath, string uniqueID, string excelDataQuery, string sqlTableName)
        //{
        //    OleDbConnection excelConnection = null;
        //    SqlConnection myConnection = null;
        //    try
        //    {
        //        // make sure your sheet name is correct, here sheet name is sheet1, so you can change your sheet name if have different

        //        //Create connection string to SQL Server
        //        var sqlConnectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        //        //Create connection string to Excel work book
        //        string excelConnectionString = string.Empty;

        //        //   string excelDataQuery = ConfigurationManager.AppSettings["SOEIMPORTQUERY"];
        //        //  string sqlTableName = ConfigurationManager.AppSettings["SOEIMPORT"];

        //        string CommandText = String.Format("DELETE FROM dbo.[{0}] WHERE ID='{1}'", sqlTableName, uniqueID);

        //        //Getting the extension of the selected file 
        //        string Extension = Path.GetExtension(excelFilePath);

        //        switch (Extension)
        //        {
        //            case ".xls": //Excel 97-03
        //                excelConnectionString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
        //                break;
        //            case ".xlsx": //Excel 07
        //                excelConnectionString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
        //                break;
        //        }

        //        excelConnectionString = String.Format(excelConnectionString, excelFilePath);

        //        sqlConnectionString = String.Format(sqlConnectionString, B1Connections.diCompany.Server, B1Connections.diCompany.CompanyDB, TGeneric.GetDbPassword());

        //        //Create Connection to Excel work book
        //        excelConnection = new OleDbConnection(excelConnectionString);

        //        //Create OleDbCommand to fetch data from Excel
        //        OleDbCommand cmd = new OleDbCommand(excelDataQuery, excelConnection);
        //        excelConnection.Open();
        //        DataSet ds = new DataSet();
        //        System.Data.DataTable dt = new System.Data.DataTable();

        //        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        //        da.Fill(ds);
        //        dt = ds.Tables[0];

        //        myConnection = new SqlConnection(sqlConnectionString);
        //        myConnection.Open();
        //        SqlCommand cmdProc = new SqlCommand("[dbo].[uspPopulateImportTempTable]", myConnection);
        //        cmdProc.CommandType = CommandType.StoredProcedure;
        //        cmdProc.Parameters.AddWithValue("@GUID", uniqueID);
        //        cmdProc.Parameters.AddWithValue("@Details", dt);
        //        cmdProc.ExecuteNonQuery();

        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        excelConnection.Close();
        //        myConnection.Close();
        //    }
        //}

        //public static System.Data.DataTable GetExcelImportData(string uniqueID, string type)
        //{
        //    SqlConnection myConnection = null;
        //    try
        //    {
        //        //Create connection string to SQL Server
        //        var sqlConnectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        //        sqlConnectionString = String.Format(sqlConnectionString, B1Connections.diCompany.Server, B1Connections.diCompany.CompanyDB, TGeneric.GetDbPassword());

        //        myConnection = new SqlConnection(sqlConnectionString);
        //        myConnection.Open();
        //        SqlCommand cmdProc = new SqlCommand("[dbo].[uspGetExcelImportData]", myConnection);
        //        cmdProc.CommandType = CommandType.StoredProcedure;
        //        cmdProc.Parameters.AddWithValue("@GUID", uniqueID);
        //        cmdProc.Parameters.AddWithValue("@Type", type);
        //        //cmdProc.ExecuteNonQuery();
        //        SqlDataAdapter da = new SqlDataAdapter(cmdProc);
        //        System.Data.DataTable dt = new System.Data.DataTable();
        //        da.Fill(dt);
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        myConnection.Close();
        //    }
        //}

        //public static bool ValidateExcelData(string uniqueID, string type,string fromdate , string Todate)
        //{
        //    SqlConnection myConnection = null;
        //    try
        //    {
        //        //Create connection string to SQL Server
        //        var sqlConnectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        //        sqlConnectionString = String.Format(sqlConnectionString, B1Connections.diCompany.Server, B1Connections.diCompany.CompanyDB, TGeneric.GetDbPassword());

                
        //        myConnection = new SqlConnection(sqlConnectionString);
        //        myConnection.Open();
        //        SqlCommand cmdProc = new SqlCommand("[dbo].[uspValidateExcelData]", myConnection);
        //        cmdProc.CommandType = CommandType.StoredProcedure;
        //        cmdProc.Parameters.AddWithValue("@GUID", uniqueID);
        //        cmdProc.Parameters.AddWithValue("@Type", type);
        //        cmdProc.Parameters.AddWithValue("@fromdate", fromdate);
        //        cmdProc.Parameters.AddWithValue("@todate", Todate);
        //        //cmdProc.ExecuteNonQuery();
        //        SqlDataReader rdr = cmdProc.ExecuteReader();

        //        while (!rdr.IsClosed)
        //        {
        //            while (rdr.Read())
        //            {
        //                // Console.WriteLine("First name: {0}, Last name: {1}", rdr["fname"], rdr["lname"]);
        //            }
        //            if (!rdr.NextResult())
        //            {
        //                rdr.Close();
        //            }
        //        }
        //    }
        //    catch (SqlException se)
        //    {
        //        throw se;
        //    }
        //    finally
        //    {
        //        myConnection.Close();
        //    }
        //    return true;
        //}
    }
}


