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

	internal class TProgressBar
	{

		public static SAPbouiCOM.ProgressBar Create(string sText, int iLimit)
		{
			SAPbouiCOM.ProgressBar oProgressBar = null;
			try {
                //oProgressBar = B1Connections.theAppl.StatusBar.CreateProgressBar(sText.Trim(), iLimit, true);
                //oProgressBar.Value = 0;
				return oProgressBar;
			} catch (Exception ex) {
				throw ex;
			}
		}

		public static void MoveForward(SAPbouiCOM.ProgressBar oProgressBar, string sText)
		{
			int iPos = 0;
			try {
				iPos = oProgressBar.Value;
				oProgressBar.Value = iPos + 1;
				oProgressBar.Text = string.Empty;
				oProgressBar.Text = sText;
			} catch (Exception ex) {
				throw ex;
			}
		}

	}

}

