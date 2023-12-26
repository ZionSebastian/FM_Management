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

	internal class TSQL
	{

		public static string GetSingleRecord(string _query)
		{

			SAPbobsCOM.Recordset rsResult = null;
			string sRetVal = string.Empty;
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
            //company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            company =B1Connections.diCompany;
			try {
                rsResult = (SAPbobsCOM.Recordset) company.GetBusinessObject(BoObjectTypes.BoRecordset);

				if (!string.IsNullOrEmpty(_query)) {
					rsResult.DoQuery(_query.Trim());
					sRetVal = rsResult.Fields.Item(0).Value.ToString();
				}

			} catch (Exception ex) {
				throw ex;
			} finally {
				TGeneric.ReleaseComObject((object)rsResult);
				TGeneric.CollectGabage();
			}

			return sRetVal;

		}


		public static SAPbobsCOM.Recordset GetRecords(string _query)
		{

			SAPbobsCOM.Recordset rsResult = null;
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
           // company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            company =B1Connections.diCompany;
            try {
                rsResult = (SAPbobsCOM.Recordset) company.GetBusinessObject(BoObjectTypes.BoRecordset);
				if (!string.IsNullOrEmpty(_query)) {
					rsResult.DoQuery(_query.Trim());
				}
				return rsResult;
			} catch (Exception ex) {
				TGeneric.ReleaseComObject((object)rsResult);
				TGeneric.CollectGabage();
				throw ex;
			}

		}

		public static bool Update(string _query)
		{

			SAPbobsCOM.Recordset rsResult = null;
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
            //company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            company =B1Connections.diCompany;
            try {
                rsResult = (SAPbobsCOM.Recordset) company.GetBusinessObject(BoObjectTypes.BoRecordset);
				if (!string.IsNullOrEmpty(_query)) {
					rsResult.DoQuery(_query.Trim());
				}
				return true;
			} catch (Exception ex) {
				TGeneric.ReleaseComObject((object)rsResult);
				TGeneric.CollectGabage();
				return false;
			}

		}


	}

}

