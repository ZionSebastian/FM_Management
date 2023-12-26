using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;

namespace SBOHelper.Utils
{

	internal class TChooseFromList
	{

		/// <summary>
		/// Returns the first entry of a choosefromlist object by the choosefromlist event.
		/// </summary>
		/// <param name="_pVal">The eventarguments of a choosefromlist event. </param>
		/// <param name="_form">The form, where the event occured. </param>
		/// <param name="_column">The column of the datatable as columnname (string) or index (integer). </param>
		/// <returns>Returns the first entry of the choosefromlist datatable, if successful, else NOTHING. </returns>
		public static string GetValue(SAPbouiCOM.ItemEvent _pVal, SAPbouiCOM.Form _form, string _column)
		{

			SAPbouiCOM.IChooseFromListEvent CFLEvent = null;
			string sCFL_ID = null;
			SAPbouiCOM.Form oForm = default(SAPbouiCOM.Form);
			SAPbouiCOM.ChooseFromList oCFL = null;
			SAPbouiCOM.DataTable oDataTable = default(SAPbouiCOM.DataTable);
			string sRetval = string.Empty;

			try {

				if (_pVal.EventType == SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST) {
					CFLEvent = (SAPbouiCOM.IChooseFromListEvent)_pVal;
					sCFL_ID = CFLEvent.ChooseFromListUID;
					oForm = _form;
					oCFL = oForm.ChooseFromLists.Item(sCFL_ID);

					if (CFLEvent.BeforeAction == false) {
						oDataTable = CFLEvent.SelectedObjects;
						if (oDataTable == null) {
							return sRetval;
						}
						sRetval = oDataTable.GetValue(_column, 0).ToString();
						return sRetval;
					}

				}
				return null;

			} catch (Exception ex) {
				throw ex;
			}

		}

		/// <summary>
		/// Returns the first row entry of a choosefromlist object by the choosefromlist event.
		/// </summary>
		/// <param name="_pVal">The eventarguments of a choosefromlist event. </param>
		/// <param name="_form">The form, where the event occured. </param>
		/// <returns>Returns the whole datatable with the selected rows in the choosefromlist object. </returns>
		public static SAPbouiCOM.DataTable GetValue(SAPbouiCOM.ItemEvent _pVal, SAPbouiCOM.Form _form)
		{

			SAPbouiCOM.IChooseFromListEvent oChooseFromListEvent = null;
			SAPbouiCOM.ChooseFromList oChooseFromList = null;
			SAPbouiCOM.DataTable oDataTable = default(SAPbouiCOM.DataTable);
			string sCFL_ID = null;

			try {

				if (_pVal.EventType == SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST) {
					oChooseFromListEvent = (SAPbouiCOM.IChooseFromListEvent)_pVal;
					sCFL_ID = oChooseFromListEvent.ChooseFromListUID;
					oChooseFromList = _form.ChooseFromLists.Item(sCFL_ID);
					if (oChooseFromListEvent.BeforeAction == false) {
						oDataTable = oChooseFromListEvent.SelectedObjects;
						if (oDataTable == null)
							return null;
						return oDataTable;
					}
				}
				return null;
			} catch  {
                return null;
			}

		}



		public static void SetCondition(SAPbouiCOM.ItemEvent _pVal,SAPbouiCOM.Form _form, SAPbouiCOM.Conditions _Conditions)
		{
			SAPbouiCOM.ChooseFromList oChooseFromList = default(SAPbouiCOM.ChooseFromList);
			string sIdCFL = string.Empty;
			SAPbouiCOM.IChooseFromListEvent oCFLEvent = null;
			try {
				if (_pVal.EventType == SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST) {
					oCFLEvent = (SAPbouiCOM.IChooseFromListEvent)_pVal;
					oChooseFromList = _form.ChooseFromLists.Item(oCFLEvent.ChooseFromListUID);
					if (oCFLEvent.BeforeAction == true) {
						oChooseFromList.SetConditions(null);
						oChooseFromList.SetConditions(_Conditions);
					}
				}

			} catch (Exception ex) {
				throw ex;
			}

		}


     

		public static void SetCondition(string _cflId, SAPbouiCOM.Form _form, SAPbouiCOM.Conditions _Conditions)
		{
			SAPbouiCOM.ChooseFromList oChooseFromList = default(SAPbouiCOM.ChooseFromList);
			string sIdCFL = string.Empty;
			SAPbouiCOM.IChooseFromListEvent oCFLEvent = null;
			try {
				oChooseFromList = _form.ChooseFromLists.Item(_cflId);
				oChooseFromList.SetConditions(null);
				oChooseFromList.SetConditions(_Conditions);              
			} catch (Exception ex) {
				throw ex;
			}

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_form"></param>
		/// <param name="_UID"></param>
		/// <param name="_isMultiSelection"></param>
		/// <param name="_objectType"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static SAPbouiCOM.ChooseFromList Add(SAPbouiCOM.Form _form, string _UID, bool _isMultiSelection, string _objectType)
		{
			try {
				ChooseFromListCreationParams cflCreator = default(ChooseFromListCreationParams);
				cflCreator = (SAPbouiCOM.ChooseFromListCreationParams)B1Connections.theAppl.CreateObject(BoCreatableObjectType.cot_ChooseFromListCreationParams);
				cflCreator.UniqueID = _UID;
				cflCreator.MultiSelection = _isMultiSelection;
				cflCreator.ObjectType = _objectType;
				return _form.ChooseFromLists.Add(cflCreator);
			} catch (Exception ex) {
				throw ex;
			}
		}


		/// <summary>
		/// To Enable Choose from List
		/// </summary>
		/// <param name="_form"></param>
		/// <param name="_matrixUID"></param>
		/// <param name="_columnName"></param>
		/// <param name="_alias"></param>
		/// <remarks></remarks>
		public static void Enable(SAPbouiCOM.Form _form, string _matrixUID, string _columnName, string _alias)
		{
			try {
                SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)_form.Items.Item(_matrixUID).Specific;
				oMatrix.Columns.Item(_columnName).ChooseFromListUID = oMatrix.Columns.Item(_columnName).ChooseFromListUID;
				oMatrix.Columns.Item(_columnName).ChooseFromListAlias = _alias;
			} catch (Exception ex) {
				throw ex;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_form"></param>
		/// <param name="_item"></param>
		/// <param name="_alias"></param>
		/// <remarks></remarks>
		public static void Enable(SAPbouiCOM.Form _form, string _item, string _alias)
		{
			try {
				SAPbouiCOM.EditText oEditText = (SAPbouiCOM.EditText) _form.Items.Item(_item).Specific;
				oEditText.ChooseFromListUID = oEditText.ChooseFromListUID;
				oEditText.ChooseFromListAlias = _alias;
			} catch (Exception ex) {
				throw ex;
			}
		}


	}

}
