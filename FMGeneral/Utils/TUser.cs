using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;
using System.Text;
using FMGeneral.Class_Files;

namespace SBOHelper.Utils
{

	internal class TUser
	{

		public static string UserCode {
			get { return GetUserDetails(B1Connections.diCompany, UserDetails.UserCode); }
		}

		public static string UserName {
			get { return GetUserDetails(B1Connections.diCompany, UserDetails.UserName); }
		}
     
		public static string EMail {
			get { return GetUserDetails(B1Connections.diCompany, UserDetails.EMail); }
		}

		public static string Mobile {
			get { return GetUserDetails(B1Connections.diCompany, UserDetails.Mobile); }
		}

		public static string Fax {
			get { return GetUserDetails(B1Connections.diCompany, UserDetails.Fax); }
		}

		public static string Defaults {
			get { return GetUserDetails(B1Connections.diCompany, UserDetails.Defaults); }
		}

		public static string Branch {
			get { return GetUserDetails(B1Connections.diCompany, UserDetails.Branch); }
		}

		public static string Department {
			get { return GetUserDetails(B1Connections.diCompany, UserDetails.Department); }
		}

		public static string isLocked {
			get { return GetUserDetails(B1Connections.diCompany, UserDetails.Locked); }
		}

		/// <summary>
		/// Returns the user type
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public static bool IsSuperUser {
			get { return GetUserType(B1Connections.diCompany); }
		}

		/// <summary>
		/// Returns the user type
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public static bool IsPOSUser {
			get { return GetPOSUserType(B1Connections.diCompany); }
		}

		public static bool ShowPurchase {
			get { return IsEnablePurchase(B1Connections.diCompany); }
		}

		public static bool ShowSales {
			get { return IsEnableSale(B1Connections.diCompany); }
		}


		/// <summary>
		/// 
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string DefaultColor {
			get { return GetUserDefaults(B1Connections.diCompany, GetDefaults.Color); }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string DefaultSalesEmployee {
			get { return GetUserDefaults(B1Connections.diCompany, GetDefaults.SelesEmployee); }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string DefaultCashAccount {
			get { return GetUserDefaults(B1Connections.diCompany, GetDefaults.CashOnHand); }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string DefaultCheckAccount {
			get { return GetUserDefaults(B1Connections.diCompany, GetDefaults.ChecksRcvd); }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string DefaultWarehouse {
			get { return GetUserDefaults(B1Connections.diCompany, GetDefaults.Warehouse); }
		}

        //public static string DefaultWarehouse
        //{
        //    get { return GetUserDefaults(B1Connections.diCompany, GetDefaults.Warehouse, sItemCode); }
        //}


		/// <summary>
		/// 
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string DefaultTaxCode {
			get { return GetUserDefaults(B1Connections.diCompany, GetDefaults.TaxCode); }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string DefaultBP {
			get { return GetUserDefaults(B1Connections.diCompany, GetDefaults.DftCustAR); }
		}

		/// <summary>
		/// Check  whether the logged in user is super user or not
		/// </summary>
		/// <param name="oCompany">Company object</param>
		/// <returns></returns>
		/// <remarks></remarks>
		private static bool GetUserType(SAPbobsCOM.Company oCompany)
		{

			string sSQL = string.Empty;
			SAPbobsCOM.Users oUsers = null;
			try {
				oUsers =(SAPbobsCOM.Users ) oCompany.GetBusinessObject(BoObjectTypes.oUsers);
				//Retrieve a record by its key from the database
				if (oUsers.GetByKey(oCompany.UserSignature)) {
					//Checking whether it is super user
					if (oUsers.Superuser == BoYesNoEnum.tNO) {
						return false;
					}
				}
				return true;
			} catch (Exception ex) {
				throw ex;
			} finally {
				TGeneric.ReleaseComObject((object)oUsers);
				TGeneric.CollectGabage();
			}

		}

		private static bool GetPOSUserType(SAPbobsCOM.Company oCompany)
		{

			string sSQL = string.Empty;
			SAPbobsCOM.Users oUsers = null;
			try {
				oUsers =(SAPbobsCOM.Users ) oCompany.GetBusinessObject(BoObjectTypes.oUsers);
				//Retrieve a record by its key from the database
				if (oUsers.GetByKey(oCompany.UserSignature)) {
					if (!oUsers.UserFields.Fields.Item("U_IsPosUsr").Value.ToString().Trim().Equals("Y")) {
						return false;
					}
				}
				return true;
			} catch (Exception ex) {
				throw ex;
			} finally {
				TGeneric.ReleaseComObject((object)oUsers);
				TGeneric.CollectGabage();
			}

		}


		private static bool IsEnableSale(SAPbobsCOM.Company oCompany)
		{

			string sSQL = string.Empty;
			SAPbobsCOM.Users oUsers = null;
			try {
				oUsers = (SAPbobsCOM.Users )oCompany.GetBusinessObject(BoObjectTypes.oUsers);
				//Retrieve a record by its key from the database
				if (oUsers.GetByKey(oCompany.UserSignature)) {
					if (!oUsers.UserFields.Fields.Item("U_EblSales").Value.ToString().Trim().Equals("Y")) {
						return false;
					}
				}
				return true;
			} catch (Exception ex) {
				throw ex;
			} finally {
				TGeneric.ReleaseComObject((object)oUsers);
				TGeneric.CollectGabage();
			}

		}

		private static bool IsEnablePurchase(SAPbobsCOM.Company oCompany)
		{

			string sSQL = string.Empty;
			SAPbobsCOM.Users oUsers = null;
			try {
                oUsers = (SAPbobsCOM.Users)oCompany.GetBusinessObject(BoObjectTypes.oUsers);
				//Retrieve a record by its key from the database
				if (oUsers.GetByKey(oCompany.UserSignature)) {
					if (!oUsers.UserFields.Fields.Item("U_EblPurchs").Value.ToString().Trim().Equals("Y")) {
						return false;
					}
				}
				return true;
			} catch (Exception ex) {
				throw ex;
			} finally {
				TGeneric.ReleaseComObject((object)oUsers);
				TGeneric.CollectGabage();
			}

		}

		/// <summary>
		/// To Retrive the default setting assigned to a user
		/// </summary>
		/// <param name="oCompany"></param>
		/// <param name="_ReturnType"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		private static string GetUserDefaults(SAPbobsCOM.Company oCompany, GetDefaults _ReturnType, string sItemCode )
		{

			string sReturnValue = string.Empty;
			SAPbobsCOM.Users oUsers = default(SAPbobsCOM.Users);
			SAPbobsCOM.UserDefaultGroups oUserDefaultGroups = default(SAPbobsCOM.UserDefaultGroups);
			SAPbobsCOM.CompanyService oCompanyService = default(SAPbobsCOM.CompanyService);
			SAPbobsCOM.AdminInfo oAdminInfo = default(SAPbobsCOM.AdminInfo);
			SAPbobsCOM.Items oItems = default(SAPbobsCOM.Items);
			try {
				oUsers =(SAPbobsCOM.Users) oCompany.GetBusinessObject(BoObjectTypes.oUsers);
				oUserDefaultGroups =(SAPbobsCOM.UserDefaultGroups) oCompany.GetBusinessObject(BoObjectTypes.oUserDefaultGroups);
				//get company service
				oCompanyService = oCompany.GetCompanyService();
				//get admin info
				oAdminInfo =oCompanyService.GetAdminInfo();
				oItems = (SAPbobsCOM.Items)oCompany.GetBusinessObject(BoObjectTypes.oItems);

				//Retrieve a User record by its key from the database
				if (oUsers.GetByKey(oCompany.UserSignature)) {
					//Retrieve a user defaults record by its key from the database
					if (oUserDefaultGroups.GetByKey(oUsers.Defaults.Trim())) {
						switch (_ReturnType) {
							case GetDefaults.Color:
								sReturnValue = Convert.ToString(oUserDefaultGroups.WindowsColor);
								break;
							case GetDefaults.SelesEmployee:
								sReturnValue =Convert.ToString( oUserDefaultGroups.SalesEmployee);
								break;
							case GetDefaults.CashOnHand:
								sReturnValue = TGeneric.GetActFormatCode(oUserDefaultGroups.CashAccount.Trim());
								break;
							case GetDefaults.ChecksRcvd:
								sReturnValue = TGeneric.GetActFormatCode(oUserDefaultGroups.CheckingAcct.Trim());
								break;
							case GetDefaults.Warehouse:
								if (string.IsNullOrEmpty(sItemCode.Trim())) {
									sReturnValue = oUserDefaultGroups.Warehouse;
								} else {
									if (oItems.GetByKey(sItemCode.Trim())) {
										if (!string.IsNullOrEmpty(oItems.DefaultWarehouse)) {
											sReturnValue = oItems.DefaultWarehouse;
										} else {
											if (!string.IsNullOrEmpty(oUserDefaultGroups.Warehouse)) {
												sReturnValue = oUserDefaultGroups.Warehouse;
											} else {
												sReturnValue = oAdminInfo.DefaultWarehouse;
											}
										}
									} else {
										if (!string.IsNullOrEmpty(oUserDefaultGroups.Warehouse)) {
											sReturnValue = oUserDefaultGroups.Warehouse;
										} else {
											sReturnValue = oAdminInfo.DefaultWarehouse;
										}
									}
								}

								break;
							case GetDefaults.TaxCode:
								sReturnValue = oUserDefaultGroups.DefaultTaxCode;
								break;
							case GetDefaults.DftCustAR:
								sReturnValue = oUserDefaultGroups.BPforInvoicePayment;
								break;
						}
					} else {
						//Return sReturnValue = "" 'String.Empty
						sReturnValue = oAdminInfo.DefaultWarehouse;
					}
				} else {
					return sReturnValue = string.Empty;
				}
				return sReturnValue.Trim();
			} catch (Exception ex) {
				throw ex;
			} finally {
				TGeneric.ReleaseComObject((object)oUserDefaultGroups);
				TGeneric.ReleaseComObject((object)oUsers);
				TGeneric.CollectGabage();
			}

		}


        private static string GetUserDefaults(SAPbobsCOM.Company oCompany, GetDefaults _ReturnType)
        {

            string sReturnValue = string.Empty;
            SAPbobsCOM.Users oUsers = default(SAPbobsCOM.Users);
            SAPbobsCOM.UserDefaultGroups oUserDefaultGroups = default(SAPbobsCOM.UserDefaultGroups);
            SAPbobsCOM.CompanyService oCompanyService = default(SAPbobsCOM.CompanyService);
            SAPbobsCOM.AdminInfo oAdminInfo = default(SAPbobsCOM.AdminInfo);
            SAPbobsCOM.Items oItems = default(SAPbobsCOM.Items);
            try
            {
                oUsers = (SAPbobsCOM.Users)oCompany.GetBusinessObject(BoObjectTypes.oUsers);
                oUserDefaultGroups = (SAPbobsCOM.UserDefaultGroups)oCompany.GetBusinessObject(BoObjectTypes.oUserDefaultGroups);
                //get company service
                oCompanyService = oCompany.GetCompanyService();
                //get admin info
                oAdminInfo = oCompanyService.GetAdminInfo();
                oItems = (SAPbobsCOM.Items)oCompany.GetBusinessObject(BoObjectTypes.oItems);

                //Retrieve a User record by its key from the database
                if (oUsers.GetByKey(oCompany.UserSignature))
                {
                    //Retrieve a user defaults record by its key from the database
                    if (oUserDefaultGroups.GetByKey(oUsers.Defaults.Trim()))
                    {
                        switch (_ReturnType)
                        {
                            case GetDefaults.Color:
                                sReturnValue = Convert.ToString(oUserDefaultGroups.WindowsColor);
                                break;
                            case GetDefaults.SelesEmployee:
                                sReturnValue = Convert.ToString(oUserDefaultGroups.SalesEmployee);
                                break;
                            case GetDefaults.CashOnHand:
                                sReturnValue = TGeneric.GetActFormatCode(oUserDefaultGroups.CashAccount.Trim());
                                break;
                            case GetDefaults.ChecksRcvd:
                                sReturnValue = TGeneric.GetActFormatCode(oUserDefaultGroups.CheckingAcct.Trim());
                                break;
                            
                            case GetDefaults.TaxCode:
                                sReturnValue = oUserDefaultGroups.DefaultTaxCode;
                                break;
                            case GetDefaults.DftCustAR:
                                sReturnValue = oUserDefaultGroups.BPforInvoicePayment;
                                break;
                            case GetDefaults.Warehouse:
                                sReturnValue = oUserDefaultGroups.Warehouse;
                                break;
                        }
                    }
                    else
                    {
                        //Return sReturnValue = "" 'String.Empty
                        sReturnValue = oAdminInfo.DefaultWarehouse;
                    }
                }
                else
                {
                    return sReturnValue = string.Empty;
                }
                return sReturnValue.Trim();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                TGeneric.ReleaseComObject((object)oUserDefaultGroups);
                TGeneric.ReleaseComObject((object)oUsers);
                TGeneric.CollectGabage();
            }

        }

		/// <summary>
		/// Returns the default documrnt series assigned to the loged in user 
		/// </summary>
		/// <param name="_DocType"></param>
		/// <param name="_ReturnType"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string GetDefaultSeries(string _DocType, SeriesReturnType _ReturnType)
		{

			string strReturnValue = string.Empty;
			try {
				SAPbobsCOM.CompanyService oCompanyService = default(SAPbobsCOM.CompanyService);
				SAPbobsCOM.SeriesService oSeriesService = default(SAPbobsCOM.SeriesService);
				Series oSeries = default(Series);
				DocumentTypeParams oDocumentTypeParams = default(DocumentTypeParams);

				//get company service
				oCompanyService = B1Connections.diCompany.GetCompanyService();

				//get series service
				oSeriesService =(SAPbobsCOM.SeriesService) oCompanyService.GetBusinessService(ServiceTypes.SeriesService);

				//get new series
				oSeries =(Series) oSeriesService.GetDataInterface(SeriesServiceDataInterfaces.ssdiSeries);

				//get DocumentTypeParams for filling the document type
				oDocumentTypeParams =(DocumentTypeParams) oSeriesService.GetDataInterface(SeriesServiceDataInterfaces.ssdiDocumentTypeParams);

				//////set the document type (e.g. A/R Invoice=13)
				oDocumentTypeParams.Document = _DocType;

				//get the default series of the SaleOrder documentset the document type
				oSeries = oSeriesService.GetDefaultSeries(oDocumentTypeParams);

				switch (_ReturnType) {

					case SeriesReturnType.Series:
                        
						strReturnValue = Convert.ToString(oSeries.Series);

						break;
					case SeriesReturnType.Name:

						strReturnValue = oSeries.Name;

						break;
					case SeriesReturnType.InitialNumber:

						strReturnValue = Convert.ToString(oSeries.InitialNumber);

						break;
				}

				return strReturnValue.Trim();

			} catch (Exception ex) {
				throw ex;
			}

		}


        public static string GetDefaultSeriesBranch(string _DocType)
        {

            string sSQL = string.Empty;
            string sSeries = string.Empty;

            try
            {
                if (_DocType == "CCS_PRP")
                {
                    sSQL = " Select \"Series\" from NNM1 where \"ObjectCode\"='" + _DocType + "'";
                    sSQL = "Select \"Series\" from OUSR T0   Inner Join OUDG  T1 on T0.\"DfltsGroup\"=T1.\"Code\"	Inner Join NNM1 T2 on T2.\"BPLId\"=T1.\"BPLId\" where \"USER_CODE\"='" + TUser.UserCode + "' and \"ObjectCode\"='" + _DocType + "' and T2.\"Remark\"='Y'";
                    sSQL = "Select    \"Series\" from  NNM1 T2 inner join OFPR T3 on T3.\"Indicator\" = T2.\"Indicator\" and T3.\"PeriodStat\" = 'N' and T2.\"Locked\"= 'N' where \"ObjectCode\"='" + _DocType + "' and T2.\"BPLId\" ='" + TUser.GetDefaultBranch() + "'";

                }
                else if (_DocType == "CCS_RCT" || _DocType == "CCS_VPM" || _DocType == "CCS_TGET")
                {
                    sSQL = "Select \"Series\" from OUSR T0   Inner Join OUDG  T1 on T0.\"DfltsGroup\"=T1.\"Code\"	Inner Join NNM1 T2 on T2.\"BPLId\"=T1.\"BPLId\" inner join OFPR T3 on T3.\"Indicator\" = T2.\"Indicator\" and T3.\"PeriodStat\" = 'N' and T2.\"Locked\" = 'N' where \"USER_CODE\"='" + TUser.UserCode + "' and \"ObjectCode\"='" + _DocType + "'";
                    sSQL = "Select    \"Series\" from  NNM1 T2 inner join OFPR T3 on T3.\"Indicator\" = T2.\"Indicator\" and T3.\"PeriodStat\" = 'N' and T2.\"Locked\"= 'N' where \"ObjectCode\"='" + _DocType + "' and T2.\"BPLId\" ='" + TUser.GetDefaultBranch() + "'";

                }
                else
                {
                    sSQL = "Select \"Series\" from OUSR T0   Inner Join OUDG  T1 on T0.\"DfltsGroup\"=T1.\"Code\"	Inner Join NNM1 T2 on T2.\"BPLId\"=T1.\"BPLId\" inner join OFPR T3 on T3.\"Indicator\" = T2.\"Indicator\" and T3.\"PeriodStat\" = 'N' and T2.\"Locked\" = 'N' where \"USER_CODE\"='" + TUser.UserCode + "' and \"ObjectCode\"='" + _DocType + "' and T2.\"Remark\"='Y'";
                    sSQL = "Select    \"Series\" from  NNM1 T2 inner join OFPR T3 on T3.\"Indicator\" = T2.\"Indicator\" and T3.\"PeriodStat\" = 'N' and T2.\"Locked\"= 'N' where \"ObjectCode\"='" + _DocType + "' and T2.\"BPLId\" ='" + TUser.GetDefaultBranch() + "'";

                }
                sSeries = TSQL.GetSingleRecord(sSQL);
                return sSeries;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public static string GetDefaultBranch()
        {

            string sSQL = string.Empty;
            string sBranch = string.Empty;

            try
            {
				//sBranch=TUser.Branch;
                if (globalvariables.DftBranch != "")
                {
                    sBranch = globalvariables.DftBranch.ToString().Trim();
					
                }
                else
                {
                    sSQL = "Select T1.\"BPLId\" from OUSR T0    Inner Join OUDG  T1 on T0.\"DfltsGroup\"=T1.\"Code\"	  where \"USER_CODE\"='" + TUser.UserCode + "'";

                    sBranch = TSQL.GetSingleRecord(sSQL);
                }
                return sBranch;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_UserCode"></param>
        /// <param name="_DocType"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetDefaultSeries(string _UserCode, string _DocType)
		{

			StringBuilder sbSQL = new StringBuilder();
			string sSeries = string.Empty;
			SAPbobsCOM.Recordset rsSeries = null;
			try {
				sbSQL.AppendFormat("SELECT Series FROM NNM2 WHERE ObjectCode ='{0}' AND UserSign ='{1}'", _DocType, _UserCode.Trim());
				rsSeries = TSQL.GetRecords(sbSQL.ToString().Trim());
				if (rsSeries.RecordCount > 0) {
					sSeries = rsSeries.Fields.Item("Series").Value.ToString().Trim();
				} else {
					sbSQL.Replace(sbSQL.ToString(), string.Empty);
					sbSQL.AppendFormat("SELECT DfltSeries FROM ONNM  WHERE ObjectCode='{0}'", _DocType);
					rsSeries = TSQL.GetRecords(sbSQL.ToString().Trim());
					if (rsSeries.RecordCount > 0) {
						sSeries = rsSeries.Fields.Item("DfltSeries").Value.ToString().Trim();
					}
				}
				return sSeries.Trim();
			} catch (Exception ex) {
				throw ex;
			}
		}

		private static string GetUserDetails(SAPbobsCOM.Company oCompany, UserDetails _ReturnType)
		{

			string sReturnValue = string.Empty;
			SAPbobsCOM.Users oUsers = (SAPbobsCOM.Users)oCompany.GetBusinessObject(BoObjectTypes.oUsers);
			SAPbobsCOM.UserDefaultGroups oUserDefaultGroups = (SAPbobsCOM.UserDefaultGroups)oCompany.GetBusinessObject(BoObjectTypes.oUserDefaultGroups);

			try {
				//Retrieve a User record by its key from the database

				if (oUsers.GetByKey(oCompany.UserSignature)) {
					switch (_ReturnType) {

						case UserDetails.UserCode:
							sReturnValue = Convert.ToString(oUsers.UserCode);
							break;
						case UserDetails.UserName:
							sReturnValue = Convert.ToString(oUsers.UserName);
							break;
						case UserDetails.Mobile:
							sReturnValue = Convert.ToString(oUsers.MobilePhoneNumber);
							break;
						case UserDetails.SuperUser:
							if (oUsers.Superuser == BoYesNoEnum.tYES) {
								sReturnValue = "Y";
							} else {
								sReturnValue = "N";
							}
							break;
						case UserDetails.EMail:
							sReturnValue = Convert.ToString(oUsers.eMail);
							break;
						case UserDetails.Fax:
							sReturnValue = Convert.ToString(oUsers.FaxNumber);
							break;
						case UserDetails.Defaults:
							sReturnValue = Convert.ToString(oUsers.Defaults);
							break;
						case UserDetails.Branch:
							sReturnValue = Convert.ToString(oUsers.Branch);
							break;
						case UserDetails.Department:
							sReturnValue = Convert.ToString(oUsers.Department);
							break;
						case UserDetails.Locked:
							if (oUsers.Locked == BoYesNoEnum.tYES) {
								sReturnValue = "Y";
							} else {
								sReturnValue = "N";
							}
							break;
					}
				} else {
					return sReturnValue =string.Empty;
				}

				return sReturnValue.Trim();

			} catch (Exception ex) {
				throw ex;
			} finally {
				TGeneric.ReleaseComObject((object)oUserDefaultGroups);
				TGeneric.ReleaseComObject((object)oUsers);
				TGeneric.CollectGabage();
			}

		}

		public static string GetUserAuthType(SAPbobsCOM.Company oCompany)
		{

			string sSQL = string.Empty;
			SAPbobsCOM.Users oUsers = null;
			string sRetVal = string.Empty;
			try {
				oUsers = (SAPbobsCOM.Users )oCompany.GetBusinessObject(BoObjectTypes.oUsers);
				//Retrieve a record by its key from the database
				if (oUsers.GetByKey(oCompany.UserSignature)) {
					sRetVal = Convert.ToString(oUsers.UserFields.Fields.Item("U_AuthType").Value);
				}
				return sRetVal;
			} catch (Exception ex) {
				throw ex;
			} finally {
				TGeneric.ReleaseComObject((object)oUsers);
				TGeneric.CollectGabage();
			}

		}


	}

}



