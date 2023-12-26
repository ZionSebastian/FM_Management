using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;
//using SAPbouiCOM.Framework;

namespace SBOHelper.Utils
{

    internal class TComboBox
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ComboBox"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetSelectedDescription(SAPbouiCOM.ComboBox _ComboBox)
        {

            string sRetVal = string.Empty;
            string sDescription = string.Empty;

            try
            {
                if ((((_ComboBox.ValidValues.Count > 0) && ((_ComboBox.Selected != null))) && (_ComboBox.Selected.Value != string.Empty)))
                {
                    sDescription = _ComboBox.Selected.Description;
                }
                sRetVal = sDescription.Trim();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return sRetVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ComboBox"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetSelectedValue(SAPbouiCOM.ComboBox _ComboBox)
        {

            string sRetVal = string.Empty;
            string sValue = string.Empty;


            try
            {
                if ((((_ComboBox.ValidValues.Count > 0) && ((_ComboBox.Selected != null))) && (_ComboBox.Selected.Value != string.Empty)))
                {
                    sValue = _ComboBox.Selected.Value;
                }
                sRetVal = sValue;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return sRetVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ComboBox"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetValue(SAPbouiCOM.ComboBox _ComboBox)
        {

            string sRetVal = string.Empty;
            string sValue = string.Empty;

            try
            {
                if ((((_ComboBox.ValidValues.Count > 0) && ((_ComboBox.Value != null))) && (_ComboBox.Value != string.Empty)))
                {
                    sValue = _ComboBox.Value;
                }
                sRetVal = sValue;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return sRetVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_comboBox"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool RemoveValidValues(SAPbouiCOM.ComboBox _comboBox)
        {

            bool bRetVal = false;
            try
            {
                while (_comboBox.ValidValues.Count > 0)
                {
                    _comboBox.ValidValues.Remove(0, SAPbouiCOM.BoSearchKey.psk_Index);
                }
                bRetVal = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bRetVal;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_comboBox"></param>
        /// <param name="_query"></param>
        /// <param name="_firstEntryEmpty"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool Fill(SAPbouiCOM.ComboBox _comboBox, string _query, bool _firstEntryEmpty)
        {

            string sValue = string.Empty;
            string sDescription = string.Empty;
            bool bRetVal = false;
            SAPbobsCOM.Recordset rsResult = null;
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
            //  company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            company = B1Connections.diCompany;
            try
            {
                rsResult = (SAPbobsCOM.Recordset)company.GetBusinessObject(BoObjectTypes.BoRecordset);
                if (!string.IsNullOrEmpty(_query))
                {
                    rsResult.DoQuery(_query.Trim());
                }
                //Removes all values from the combo before filling
                if (RemoveValidValues(_comboBox))
                {
                    if (_firstEntryEmpty == true)
                    {
                        _comboBox.ValidValues.Add(" ", "  ");
                    }
                    rsResult.MoveFirst();
                    while (!rsResult.EoF)
                    {
                        sValue = rsResult.Fields.Item(0).Value.ToString();
                        sDescription = rsResult.Fields.Item(1).Value.ToString();
                        _comboBox.ValidValues.Add(sValue, sDescription);
                        rsResult.MoveNext();
                    }
                    bRetVal = true;
                }
                else
                {
                    bRetVal = false;
                }
                return bRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                TGeneric.ReleaseComObject((object)rsResult);
                TGeneric.CollectGabage();
            }

        }

        public static bool Fill(SAPbouiCOM.ComboBox _comboBox, string _query)
        {

            string sValue = string.Empty;
            string sDescription = string.Empty;
            bool bRetVal = false;
            SAPbobsCOM.Recordset rsResult = null;
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
            // company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            company = B1Connections.diCompany;
            try
            {
                rsResult = (SAPbobsCOM.Recordset)company.GetBusinessObject(BoObjectTypes.BoRecordset);
                if (!string.IsNullOrEmpty(_query))
                {
                    rsResult.DoQuery(_query.Trim());
                }
                rsResult.MoveFirst();
                while (!rsResult.EoF)
                {
                    sValue = rsResult.Fields.Item(0).Value.ToString();
                    sDescription = rsResult.Fields.Item(1).Value.ToString();
                    _comboBox.ValidValues.Add(sValue, sDescription);
                    rsResult.MoveNext();
                }
                bRetVal = true;

                return bRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                TGeneric.ReleaseComObject((object)rsResult);
                TGeneric.CollectGabage();
            }

        }


        public static bool Fill(SAPbouiCOM.ComboBox _comboBox, Hashtable _htSeries, bool _firstEntryEmpty)
        {

            string sValue = string.Empty;
            string sDescription = string.Empty;
            bool bRetVal = false;
            try
            {
                //Removes all values from the combo before filling
                if (RemoveValidValues(_comboBox))
                {
                    if (_firstEntryEmpty == true)
                    {
                        _comboBox.ValidValues.Add("-1", "---Select a value----");
                    }

                    foreach (DictionaryEntry element in _htSeries)
                    {
                        _comboBox.ValidValues.Add(Convert.ToString(element.Key), Convert.ToString(element.Value));
                    }
                    bRetVal = true;
                }
                else
                {
                    bRetVal = false;
                }
                return bRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_form"></param>
        /// <param name="_ComboUid"></param>
        /// <remarks></remarks>
        public static void LoadSeries(SAPbouiCOM.Form _form, string _ComboUid)
        {
            try
            {
                SAPbouiCOM.ComboBox oComboBox = (SAPbouiCOM.ComboBox)_form.Items.Item(_ComboUid.Trim()).Specific;
                oComboBox.ValidValues.LoadSeries(_form.TypeEx, BoSeriesMode.sf_Add);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LoadSeries(SAPbouiCOM.Form _form, string _ComboUid, string _ObjType)
        {
            try
            {
                SAPbouiCOM.ComboBox oComboBox = (SAPbouiCOM.ComboBox)_form.Items.Item(_ComboUid.Trim()).Specific;
                oComboBox.ValidValues.LoadSeries(_ObjType, BoSeriesMode.sf_Add);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool FillSlpCode(SAPbouiCOM.ComboBox _comboBox, string _query, bool _firstEntryEmpty)
        {

            string sValue = string.Empty;
            string sDescription = string.Empty;
            bool bRetVal = false;
            SAPbobsCOM.Recordset rsResult = null;
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
            //  company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            company = B1Connections.diCompany;
            try
            {
                rsResult = (SAPbobsCOM.Recordset)company.GetBusinessObject(BoObjectTypes.BoRecordset);
                if (!string.IsNullOrEmpty(_query))
                {
                    rsResult.DoQuery(_query.Trim());
                }
                //Removes all values from the combo before filling
                if (RemoveValidValues(_comboBox))
                {
                    if (_firstEntryEmpty == true)
                    {
                        _comboBox.ValidValues.Add("-1", "No Employee");
                    }
                    rsResult.MoveFirst();
                    while (!rsResult.EoF)
                    {
                        sValue = rsResult.Fields.Item(0).Value.ToString();
                        sDescription = rsResult.Fields.Item(1).Value.ToString();
                        _comboBox.ValidValues.Add(sValue, sDescription);
                        rsResult.MoveNext();
                    }
                    bRetVal = true;
                }
                else
                {
                    bRetVal = false;
                }
                return bRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                TGeneric.ReleaseComObject((object)rsResult);
                TGeneric.CollectGabage();
            }

        }

        public static bool FillCardType(SAPbouiCOM.ComboBox _comboBox, string _query, bool _firstEntryEmpty)
        {

            string sValue = string.Empty;
            string sDescription = string.Empty;
            bool bRetVal = false;
            SAPbobsCOM.Recordset rsResult = null;
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
            //  company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            company = B1Connections.diCompany;
            try
            {
                rsResult = (SAPbobsCOM.Recordset)company.GetBusinessObject(BoObjectTypes.BoRecordset);
                if (!string.IsNullOrEmpty(_query))
                {
                    rsResult.DoQuery(_query.Trim());
                }
                //Removes all values from the combo before filling
                if (RemoveValidValues(_comboBox))
                {
                    if (_firstEntryEmpty == true)
                    {
                        _comboBox.ValidValues.Add("-1", "No Scheme Card");
                    }
                    rsResult.MoveFirst();
                    while (!rsResult.EoF)
                    {
                        sValue = rsResult.Fields.Item(0).Value.ToString();
                        sDescription = rsResult.Fields.Item(1).Value.ToString();
                        _comboBox.ValidValues.Add(sValue, sDescription);
                        rsResult.MoveNext();
                    }
                    bRetVal = true;
                }
                else
                {
                    bRetVal = false;
                }
                return bRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                TGeneric.ReleaseComObject((object)rsResult);
                TGeneric.CollectGabage();
            }

        }


        public static bool FillPeriod(SAPbouiCOM.ComboBox _comboBox, string _query, bool _firstEntryEmpty)
        {

            string sValue = string.Empty;
            string sDescription = string.Empty;
            bool bRetVal = false;
            SAPbobsCOM.Recordset rsResult = null;
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
            //  company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            company = B1Connections.diCompany;
            try
            {
                rsResult = (SAPbobsCOM.Recordset)company.GetBusinessObject(BoObjectTypes.BoRecordset);
                if (!string.IsNullOrEmpty(_query))
                {
                    rsResult.DoQuery(_query.Trim());
                }
                //Removes all values from the combo before filling
                if (RemoveValidValues(_comboBox))
                {
                    if (_firstEntryEmpty == true)
                    {
                        _comboBox.ValidValues.Add("", "Select Period");
                    }
                    rsResult.MoveFirst();
                    while (!rsResult.EoF)
                    {
                        sValue = rsResult.Fields.Item(0).Value.ToString();
                       // sDescription = rsResult.Fields.Item(1).Value.ToString();
                        _comboBox.ValidValues.Add(sValue,"");
                        rsResult.MoveNext();
                    }
                    bRetVal = true;
                }
                else
                {
                    bRetVal = false;
                }
                return bRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                TGeneric.ReleaseComObject((object)rsResult);
                TGeneric.CollectGabage();
            }

        }

        public static bool FillDocno(SAPbouiCOM.ComboBox _comboBox, string _query, bool _firstEntryEmpty)
        {

            string sValue = string.Empty;
            string sDescription = string.Empty;
            bool bRetVal = false;
            SAPbobsCOM.Recordset rsResult = null;
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
            //  company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            company = B1Connections.diCompany;
            try
            {
                rsResult = (SAPbobsCOM.Recordset)company.GetBusinessObject(BoObjectTypes.BoRecordset);
                if (!string.IsNullOrEmpty(_query))
                {
                    rsResult.DoQuery(_query.Trim());
                }
                //Removes all values from the combo before filling
                if (RemoveValidValues(_comboBox))
                {
                    if (_firstEntryEmpty == true)
                    {
                        _comboBox.ValidValues.Add("0", "Select Invoice No");
                    }
                    rsResult.MoveFirst();
                    while (!rsResult.EoF)
                    {
                        sValue = rsResult.Fields.Item(0).Value.ToString();
                        // sDescription = rsResult.Fields.Item(1).Value.ToString();
                        _comboBox.ValidValues.Add(sValue, "");
                        rsResult.MoveNext();
                    }
                    bRetVal = true;
                }
                else
                {
                    bRetVal = false;
                }
                return bRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                TGeneric.ReleaseComObject((object)rsResult);
                TGeneric.CollectGabage();
            }

        }

        public static bool FillBranch(SAPbouiCOM.ComboBox _comboBox, string _query, bool _firstEntryEmpty)
        {

            string sValue = string.Empty;
            string sDescription = string.Empty;
            bool bRetVal = false;
            SAPbobsCOM.Recordset rsResult = null;
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
            //  company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            company = B1Connections.diCompany;
            try
            {
                rsResult = (SAPbobsCOM.Recordset)company.GetBusinessObject(BoObjectTypes.BoRecordset);
                if (!string.IsNullOrEmpty(_query))
                {
                    rsResult.DoQuery(_query.Trim());
                }
                //Removes all values from the combo before filling
                if (RemoveValidValues(_comboBox))
                {
                    if (_firstEntryEmpty == true)
                    {
                        _comboBox.ValidValues.Add("-1", "Select Branch");
                    }
                    rsResult.MoveFirst();
                    while (!rsResult.EoF)
                    {
                        sValue = rsResult.Fields.Item(0).Value.ToString();
                        sDescription = rsResult.Fields.Item(1).Value.ToString();
                        _comboBox.ValidValues.Add(sValue, sDescription);
                        rsResult.MoveNext();
                    }
                    bRetVal = true;
                }
                else
                {
                    bRetVal = false;
                }
                return bRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                TGeneric.ReleaseComObject((object)rsResult);
                TGeneric.CollectGabage();
            }

        }

        public static bool FillVendType(SAPbouiCOM.ComboBox _comboBox, string _query, bool _firstEntryEmpty)
        {

            string sValue = string.Empty;
            string sDescription = string.Empty;
            bool bRetVal = false;
            SAPbobsCOM.Recordset rsResult = null;
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
            //  company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            company = B1Connections.diCompany;
            try
            {
                rsResult = (SAPbobsCOM.Recordset)company.GetBusinessObject(BoObjectTypes.BoRecordset);
                if (!string.IsNullOrEmpty(_query))
                {
                    rsResult.DoQuery(_query.Trim());
                }
                //Removes all values from the combo before filling
                if (RemoveValidValues(_comboBox))
                {
                    if (_firstEntryEmpty == true)
                    {
                        _comboBox.ValidValues.Add("-1", "No Vendor Type");
                    }
                    rsResult.MoveFirst();
                    while (!rsResult.EoF)
                    {
                        sValue = rsResult.Fields.Item(0).Value.ToString();
                        sDescription = rsResult.Fields.Item(1).Value.ToString();
                        _comboBox.ValidValues.Add(sValue, sDescription);
                        rsResult.MoveNext();
                    }
                    bRetVal = true;
                }
                else
                {
                    bRetVal = false;
                }
                return bRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                TGeneric.ReleaseComObject((object)rsResult);
                TGeneric.CollectGabage();
            }

        }


        public static bool fillMatrixComboBySQL(ref SAPbouiCOM.Column _columnCombo, string _SQLString, string _fieldName1, string _fieldName2, bool _firstEntryEmpty)
        {
            try
            {
                // removeMatrxComboValues(ref _columnCombo);
                Recordset oRecord = (SAPbobsCOM.Recordset)B1Connections.diCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecord.DoQuery(_SQLString);

                if (_firstEntryEmpty == true)
                {
                    _columnCombo.ValidValues.Add("", "");
                }

                oRecord.MoveFirst();
                while (!oRecord.EoF)
                {
                    string s1 = oRecord.Fields.Item(_fieldName1).Value.ToString();
                    string s2 = oRecord.Fields.Item(_fieldName2).Value.ToString();
                    _columnCombo.ValidValues.Add(s1, s2);
                    oRecord.MoveNext();
                }
                return true;
            }
            catch (Exception ex)
            {
                B1Connections.theAppl.SetStatusBarMessage(ex.Message);
                return false;
            }

        }

        public static bool removeMatrxComboValues(ref SAPbouiCOM.Column _combo)
        {

            try
            {
                while (_combo.ValidValues.Count > 1)
                {
                    if (_combo.ValidValues.Item(0).Value != "-1")
                        _combo.ValidValues.Remove(0, SAPbouiCOM.BoSearchKey.psk_Index);
                }
                return true;
            }
            catch (Exception ex)
            {
                B1Connections.theAppl.SetStatusBarMessage(ex.Message);
                return false;
            }

        }



    }

}
