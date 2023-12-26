using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using SAPbobsCOM;
using SAPbouiCOM;
//using SAPbouiCOM.Framework;
using B1WizardBase;

namespace SBOHelper.Utils
{

	internal class TNotification
	{

		/// <summary>
		/// To generate statusbar success message
		/// </summary>
		/// <param name="_ValueToSet">Message to display</param>
		/// <remarks></remarks>

		public static void StatusbarSuccess(string _ValueToSet)
		{
			if (!string.IsNullOrEmpty(_ValueToSet.Trim())) {
				B1Connections.theAppl.StatusBar.SetText(_ValueToSet.Trim(), BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Success);
			}

		}

		/// <summary>
		/// To generate statusbar error message
		/// </summary>
		/// <param name="_ValueToSet">Message to display</param>
		/// <remarks></remarks>

		public static void StatusBarError(string _ValueToSet)
		{
			if (!string.IsNullOrEmpty(_ValueToSet.Trim())) {
                B1Connections.theAppl.StatusBar.SetText(string.Format("Error : {0}", _ValueToSet.Trim()), BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
            }

		}

		/// <summary>
		/// To generate statusbar warning message
		/// </summary>
		/// <param name="_ValueToSet">Message to display</param>
		/// <remarks></remarks>

		public static void StatusBarWarning(string _ValueToSet)
		{
			if (!string.IsNullOrEmpty(_ValueToSet.Trim())) {
				B1Connections.theAppl.StatusBar.SetText(_ValueToSet.Trim(), BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Warning);
			}

		}

		/// <summary>
		/// To generate statusbar None Type message
		/// </summary>
		/// <param name="_ValueToSet">Message to display</param>
		/// <remarks></remarks>

		public static void StatusBarNoTyped(string _ValueToSet)
		{
			if (!string.IsNullOrEmpty(_ValueToSet.Trim())) {
				B1Connections.theAppl.StatusBar.SetText(_ValueToSet.Trim(), BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Warning);
			}

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_ValueToSet"></param>
		/// <remarks></remarks>
		public static void MessageBox(string _ValueToSet)
		{
			if (!string.IsNullOrEmpty(_ValueToSet.Trim())) {
                B1Connections.theAppl.MessageBox(_ValueToSet, 1, "OK", "", "");
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_ValueToSet"></param>
		/// <remarks></remarks>
		public static bool Prompt(string _ValueToSet)
		{
			try {
				if ((B1Connections.theAppl.MessageBox(_ValueToSet, 1, "Yes", "No","")) == 1) {
					return true;
				} else {
					return false;
				}
			} catch (Exception ex) {
				throw ex;
			}

		}

	}

}

