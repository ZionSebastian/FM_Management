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

	internal class TDataSorces
	{

		/// <summary>
		/// Adds a user data source to form 
		/// </summary>
		/// <param name="pForm"></param>
		/// <param name="pUID"></param>
		/// <param name="pBoDataType"></param>
		/// <param name="pLength"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static UserDataSource Add(SAPbouiCOM.Form pForm, string pUID, SAPbouiCOM.BoDataType pBoDataType, int pLength)
		{

			SAPbouiCOM.UserDataSource udsRetVal = null;
			try {
				udsRetVal = pForm.DataSources.UserDataSources.Add(pUID, pBoDataType, pLength);
			} catch (Exception ex) {
				throw ex;
			}

			return udsRetVal;
		}

	}

}
