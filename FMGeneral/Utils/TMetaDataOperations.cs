using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using SAPbobsCOM;
using SAPbouiCOM;
////using SAPbouiCOM.Framework;

namespace SBOHelper.Utils
{

	internal class TMetaDataOperations
	{


		public static void AddUserTable(SAPbobsCOM.Company oCompany, string TableName, string Description, BoUTBTableType type)
		{
			UserTablesMD oUserTables = null;
			int errCode = 0;
			string errMsg = null;

			try {
				if (!TMetaDataOperations.DoesUserTableExist(oCompany, TableName)) {
					oUserTables = (UserTablesMD)oCompany.GetBusinessObject(BoObjectTypes.oUserTables);
					oUserTables.TableName = TableName;
					oUserTables.TableDescription = Description;
					oUserTables.TableType = type;
					if ((oUserTables.Add() != 0)) {
                        oCompany.GetLastError(out errCode, out errMsg);
						TNotification.StatusBarError(Convert.ToString(errCode) + " " + errMsg);
					}
				}
			} catch (Exception exception) {
				throw exception;
			} finally {
				TGeneric.ReleaseComObject((object)oUserTables);
				TGeneric.CollectGabage();
			}
		}


		public static void AddUserField(SAPbobsCOM.Company oCompany, string TableName, string FieldName, string Description, BoFieldTypes FieldType, BoFldSubTypes SubType, int FieldLength, string DefaultValue, BoYesNoEnum Mandatory)
		{
			int errCode = 0;
			string errMsg = null;
			UserFieldsMD fieldsMD = null;

			try {
				fieldsMD = (UserFieldsMD)oCompany.GetBusinessObject(BoObjectTypes.oUserFields);
				fieldsMD.TableName = TableName;
				fieldsMD.Name = FieldName;
				fieldsMD.Description = Description;
				fieldsMD.Type = FieldType;
				fieldsMD.SubType = SubType;
				if ((FieldType != BoFieldTypes.db_Float)) {
					fieldsMD.EditSize = FieldLength;
				} else {
					fieldsMD.EditSize = 0;
				}
				if (((DefaultValue != null))) {
					fieldsMD.DefaultValue = DefaultValue;
				}
				if (((Mandatory == BoYesNoEnum.tYES) || (Mandatory == BoYesNoEnum.tNO))) {
					fieldsMD.Mandatory = Mandatory;
				}
				if ((fieldsMD.Add() != 0)) {
					oCompany.GetLastError( out errCode,out errMsg);
					TNotification.StatusBarError(Convert.ToString(errCode) + " " + errMsg);
				}
			} catch (Exception exception) {
				throw exception;
			} finally {
				TGeneric.ReleaseComObject((object)fieldsMD);
				TGeneric.CollectGabage();
			}

		}


		public static void AddUserField(SAPbobsCOM.Company oCompany, string TableName, string FieldName, string Description, BoFieldTypes FieldType, BoFldSubTypes SubType, int FieldLength, string DefaultValue, BoYesNoEnum Mandatory, Hashtable htValidValues)
		{
			int errCode = 0;
			string errMsg = null;
			UserFieldsMD oUserFields = null;
			IDictionaryEnumerator enumerator = null;
			int iOffset = 0;
			try {
				if (!DoesUserFieldExist(oCompany, TableName, FieldName)) {
					oUserFields = (UserFieldsMD)oCompany.GetBusinessObject(BoObjectTypes.oUserFields);
					oUserFields.TableName = TableName;
					oUserFields.Name = FieldName;
					oUserFields.Description = Description;
					oUserFields.Type = FieldType;
					oUserFields.SubType = SubType;
					if ((FieldType != BoFieldTypes.db_Float)) {
						oUserFields.EditSize = FieldLength;
					} else {
						oUserFields.EditSize = 0;
					}
					if (((DefaultValue != null))) {
						oUserFields.DefaultValue = DefaultValue;
					}
					if (((Mandatory == BoYesNoEnum.tYES) || (Mandatory == BoYesNoEnum.tNO))) {
						oUserFields.Mandatory = Mandatory;
					}
					enumerator = htValidValues.GetEnumerator();
					while (enumerator.MoveNext()) {
						oUserFields.ValidValues.SetCurrentLine(iOffset);
						oUserFields.ValidValues.Value = enumerator.Key.ToString();
						oUserFields.ValidValues.Description = enumerator.Value.ToString();
						oUserFields.ValidValues.Add();
						iOffset += 1;
					}

					if ((oUserFields.Add() != 0)) {
                        oCompany.GetLastError(out errCode, out errMsg);
						TNotification.StatusBarError(Convert.ToString(errCode) + " " + errMsg);
					}
				}

			} catch (Exception exception) {
				throw exception;
			} finally {
				TGeneric.ReleaseComObject((object)oUserFields);
				TGeneric.CollectGabage();
			}

		}


		public static void AddUserDefinedObject(SAPbobsCOM.Company Company, string UDOName, string UDODescription, string HeadrTable, string ChildTable, Hashtable htColumns)
		{
			UserObjectsMD oUserObjects = null;
			int errCode = 0;
			string errMsg = null;
			IDictionaryEnumerator enumerator = null;

			try {
				oUserObjects = (UserObjectsMD)Company.GetBusinessObject(BoObjectTypes.oUserObjectsMD);

				if (!oUserObjects.GetByKey(UDOName)) {
					oUserObjects.CanCancel = BoYesNoEnum.tNO;
					oUserObjects.CanClose = BoYesNoEnum.tNO;
					oUserObjects.CanCreateDefaultForm = BoYesNoEnum.tNO;
					oUserObjects.CanDelete = BoYesNoEnum.tNO;
					oUserObjects.CanFind = BoYesNoEnum.tYES;
					if ((htColumns != null)) {
						enumerator = htColumns.GetEnumerator();
						while (enumerator.MoveNext()) {
							oUserObjects.FormColumns.FormColumnAlias = enumerator.Key.ToString();
							oUserObjects.FormColumns.FormColumnDescription = enumerator.Value.ToString();
							oUserObjects.FormColumns.Add();
						}
					}
					//oUserObjects.CanLog = BoYesNoEnum.tYES
					//oUserObjects.LogTableName = ""
					//oUserObjects.CanYearTransfer = BoYesNoEnum.tNO
					//oUserObjects.ExtensionName = ""
					oUserObjects.ManageSeries = BoYesNoEnum.tYES;
					oUserObjects.Code = UDOName;
					oUserObjects.Name = UDODescription;
					oUserObjects.ObjectType = BoUDOObjType.boud_Document;
					oUserObjects.TableName = HeadrTable;
					oUserObjects.ChildTables.TableName = ChildTable;

					if ((oUserObjects.Add() != 0)) {
                        Company.GetLastError(out errCode, out errMsg);
						TNotification.StatusBarError(Convert.ToString(errCode) + " " + errMsg);
					}
				}
			} catch (Exception exception) {
				throw exception;
			} finally {
				TGeneric.ReleaseComObject((object)oUserObjects);
				TGeneric.CollectGabage();
			}
		}

		public static bool DoesUserFieldExist(SAPbobsCOM.Company Company, string TableName, string FieldName)
		{

			bool bExist = false;
			Recordset oRecordset = null;
			string sSQL = null;
			try {
				oRecordset = (Recordset)Company.GetBusinessObject(BoObjectTypes.BoRecordset);
				sSQL = string.Concat(new string[] {
					"SELECT FieldID FROM [CUFD] WHERE TABLEID = '",
					TableName,
					"' AND ALIASID = '",
					FieldName,
					"'"
				});
				oRecordset.DoQuery(sSQL);
				if (!oRecordset.EoF) {
					bExist = true;
				} else {
					bExist = false;
				}
			} catch (Exception ex) {
				throw ex;
			} finally {
				TGeneric.ReleaseComObject((object)oRecordset);
				TGeneric.CollectGabage();
			}
			return bExist;
		}

		public static bool DoesUserTableExist(SAPbobsCOM.Company oCompany, string TableName)
		{

			SAPbobsCOM.UserTablesMD oUserTablesMD = null;
			bool bExist = false;

			try {
                oUserTablesMD = (SAPbobsCOM.UserTablesMD)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables);
				bExist = oUserTablesMD.GetByKey(TableName);
			} catch (Exception ex) {
				throw ex;
			} finally {
				TGeneric.ReleaseComObject((object)oUserTablesMD);
				TGeneric.CollectGabage();
			}
			return bExist;
		}

	}

}


