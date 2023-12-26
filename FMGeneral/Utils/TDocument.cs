using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;
////using SAPbouiCOM.Framework;

namespace SBOHelper.Utils
{

	internal class TDocument
	{

		/// <summary>
		/// To get the next documnet no
		/// </summary>
		/// <param name="_form">The Form, for which the next docnum to be generated</param>
		/// <param name="_series">The Series</param>
		/// <returns></returns>
		/// <remarks></remarks>
        public static int GetNextDocNo(SAPbouiCOM.Form _form, string _series)
        {
            int iRetVal = -1;
            try {
                if (!string.IsNullOrEmpty(_series.Trim())) {
                    iRetVal = _form.BusinessObject.GetNextSerialNumber(_series.Trim(), _form.BusinessObject.Type.ToString().Trim());
                }
                return iRetVal;
            } catch (Exception ex) {
                throw ex;
            }
        }

		public static int GetNextDocNo(string _objType, string _series)
		{
			int iRetVal = -1;
			string sSQL = string.Empty;
			SAPbobsCOM.Recordset rsNxtNo = null;
			try {
				if (!string.IsNullOrEmpty(_series.Trim())) {
					sSQL = "SELECT ISNULL(NextNumber,0) NxtNo FROM NNM1 WHERE ObjectCode ='{0}' AND Series ='{1}'";
					sSQL = string.Format(sSQL, _objType.Trim(), _series.Trim());
					rsNxtNo = TSQL.GetRecords(sSQL);
					rsNxtNo.MoveFirst();
					if (rsNxtNo.RecordCount > 0) {
						iRetVal = Convert.ToInt32(rsNxtNo.Fields.Item("NxtNo").Value);
					}
				}
				return iRetVal;
			} catch (Exception ex) {
				throw ex;
			}
		}


		public static Hashtable GetSeries(string docObj)
		{

			SAPbobsCOM.CompanyService oCompanyService = default(SAPbobsCOM.CompanyService);
			SAPbobsCOM.SeriesService oSeriesService = default(SAPbobsCOM.SeriesService);
			SeriesCollection oSeriesCollection = default(SeriesCollection);
			DocumentTypeParams oDocumentTypeParams = default(DocumentTypeParams);
			int iSeriesCntr = 0;
			Hashtable htSeries = new Hashtable();
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
           // company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            company =B1Connections.diCompany;
			try {
				//get company service
                oCompanyService = company.GetCompanyService();

				//get series service
                oSeriesService = (SAPbobsCOM.SeriesService)oCompanyService.GetBusinessService(ServiceTypes.SeriesService);

				//get series collection
                oSeriesCollection = (SeriesCollection)oSeriesService.GetDataInterface(SeriesServiceDataInterfaces.ssdiSeriesCollection);

				//get Document Type Params
                oDocumentTypeParams = (DocumentTypeParams)oSeriesService.GetDataInterface(SeriesServiceDataInterfaces.ssdiDocumentTypeParams);

				//set the document type
				//(e.g. SaleInvoice=13 , BoObjectTypes has all document types)
				oDocumentTypeParams.Document = docObj;

				//get series collection
				oSeriesCollection = oSeriesService.GetDocumentSeries(oDocumentTypeParams);
				// htSeries = Nothing


				for (iSeriesCntr = 0; iSeriesCntr <= oSeriesCollection.Count - 1; iSeriesCntr++) {
					//print the series name

					if (oSeriesCollection.Item(iSeriesCntr).Locked == BoYesNoEnum.tNO) {
						if (!htSeries.ContainsKey(oSeriesCollection.Item(iSeriesCntr).Series)) {
							htSeries.Add(oSeriesCollection.Item(iSeriesCntr).Series, oSeriesCollection.Item(iSeriesCntr).Name.Trim());
						}

					}

				}
				return htSeries;

			} catch (Exception ex) {
				throw ex;
			}

		}
		public static string GetDocEntry(BusinessObjectInfo pVal)
		{
			string sDocEntry = string.Empty;
			try {
				sDocEntry = "-1";
				System.Xml.XmlDocument oXmlDoc = new System.Xml.XmlDocument();
				oXmlDoc.LoadXml(pVal.ObjectKey);
				if (!string.IsNullOrEmpty(oXmlDoc.SelectSingleNode("/DocumentParams/DocEntry").InnerText.Trim())) {
					sDocEntry = oXmlDoc.SelectSingleNode("/DocumentParams/DocEntry").InnerText.Trim();
				}
				return sDocEntry;
			} catch (Exception ex) {
				return sDocEntry;
			}
		}

	}

}
