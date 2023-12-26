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

	internal class TValidate
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pForm"></param>
		/// <param name="pUidItem"></param>
		/// <param name="pUidLabel"></param>
		/// <param name="pType"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static bool IsNullOrEmpty(SAPbouiCOM.Form pForm, string pUidItem, string pUidLabel, SAPbouiCOM.BoFormItemTypes pType)
		{

			SAPbouiCOM.EditText oEditText = default(SAPbouiCOM.EditText);
			SAPbouiCOM.ComboBox oComboBox = default(SAPbouiCOM.ComboBox);
			SAPbouiCOM.StaticText oStatic = default(SAPbouiCOM.StaticText);
			try {
                oStatic = (SAPbouiCOM.StaticText)pForm.Items.Item(pUidLabel).Specific;
				switch (pType) {
					case BoFormItemTypes.it_EDIT:
                        oEditText = (EditText)pForm.Items.Item(pUidItem).Specific;
						if (string.IsNullOrEmpty(oEditText.Value.Trim())) {
							//TNotification.StatusBarError(oStatic.Caption + " should not be left blank.");
                           TGeneric.ErrorMessage = oStatic.Caption + " should not be left blank.";
                           // TNotification.MessageBox(oStatic.Caption + " should not be left blank.");
							oEditText.Active = true;
							return false;
						}
						break;
                    case BoFormItemTypes.it_EXTEDIT:
                        oEditText = (EditText)pForm.Items.Item(pUidItem).Specific;
                        if (string.IsNullOrEmpty(oEditText.Value.Trim()))
                        {
                            //TNotification.StatusBarError(oStatic.Caption + " should not be left blank.");
                            TGeneric.ErrorMessage = oStatic.Caption + " should not be left blank.";
                            // TNotification.MessageBox(oStatic.Caption + " should not be left blank.");
                            oEditText.Active = true;
                            return false;
                        }
                        break;
					case BoFormItemTypes.it_COMBO_BOX:
                        oComboBox = (ComboBox)pForm.Items.Item(pUidItem).Specific;
						if (string.IsNullOrEmpty(oComboBox.Value.Trim()) | oComboBox.Value == null) {
							//TNotification.StatusBarError(oStatic.Caption + " should not be left blank.");
                            TGeneric.ErrorMessage = oStatic.Caption + " should not be left blank.";
                            //TNotification.MessageBox(oStatic.Caption + " should not be left blank.");
							oComboBox.Active = true;
							return false;
						}
						break;
				}
				return true;
			} catch (Exception ex) {
				throw ex;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="pForm"></param>
		/// <param name="pUidItem"></param>
		/// <param name="pType"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static bool IsNullOrEmpty(SAPbouiCOM.Form pForm, string pUidItem, SAPbouiCOM.BoFormItemTypes pType)
		{

			SAPbouiCOM.EditText oEditText = default(SAPbouiCOM.EditText);
			SAPbouiCOM.ComboBox oComboBox = default(SAPbouiCOM.ComboBox);
			try {
				switch (pType) {
					case BoFormItemTypes.it_EDIT:
                        oEditText = (EditText)pForm.Items.Item(pUidItem).Specific;
						if (string.IsNullOrEmpty(oEditText.Value.Trim())) {
							oEditText.Active = true;
							return false;
						}
						break;
					case BoFormItemTypes.it_COMBO_BOX:
                        oComboBox = (ComboBox)pForm.Items.Item(pUidItem).Specific;
						if (string.IsNullOrEmpty(oComboBox.Value.Trim()) | oComboBox.Value == null) {
							oComboBox.Active = true;
							return false;
						}
						break;
				}
				return true;
			} catch (Exception ex) {
				throw ex;
			}
		}

	}

}

