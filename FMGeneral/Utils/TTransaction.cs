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

	internal class TTransaction
	{

		/// <summary>
		/// Starts a transation
		/// </summary>
		/// <remarks></remarks>

		public static void Start()
		{
            SAPbobsCOM.Company company = new SAPbobsCOM.Company(); 
          //  company =(SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            company =B1Connections.diCompany;
            if (!company.InTransaction)
            {
                company.StartTransaction();
			}
          
		}
		/// <summary>
		/// Rollbacks a transaction
		/// </summary>
		/// <remarks></remarks>

		public static void RollBack()
		{
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
            //company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            company =B1Connections.diCompany;
            if (company.InTransaction)
            {
                company.EndTransaction(BoWfTransOpt.wf_RollBack);
			}

		}

		/// <summary>
		/// commit the changes 
		/// </summary>
		/// <remarks></remarks>

		public static void Commit()
		{
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
            //company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            company =B1Connections.diCompany;
            if (company.InTransaction)
            {
                company.EndTransaction(BoWfTransOpt.wf_Commit);
			}

		}
	}

}

