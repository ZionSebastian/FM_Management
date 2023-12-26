using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using SAPbobsCOM;
using SAPbouiCOM;
//using SAPbouiCOM.Framework;

namespace SBOHelper.Utils
{

	internal class TStaticText
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_Form"></param>
		/// <param name="_ItemUID"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string GetValue(SAPbouiCOM.Form _Form, string _ItemUID)
		{

			string sRetVal = string.Empty;
			SAPbouiCOM.StaticText oStaticText = default(SAPbouiCOM.StaticText);

			try {
				oStaticText = (SAPbouiCOM.StaticText)_Form.Items.Item(_ItemUID).Specific;
				sRetVal = oStaticText.Caption;
				return sRetVal;
			} catch (Exception ex) {
				throw ex;
			}

		}

        public static bool SetValue(StaticText _StaticText, string _value)
        {
            try
            {
                _StaticText.Caption = _value;
                return true;
            }
            catch (Exception generatedExceptionName)
            {
                return true;
            }
        }
	}

}
