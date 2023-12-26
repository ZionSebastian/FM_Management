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

	internal class TConditions
	{

        public static SAPbouiCOM.Conditions Create(string pAlias, string pCondVal, SAPbouiCOM.BoConditionOperation pOprtn)
        {

            SAPbouiCOM.Condition oCondition = null;
            SAPbouiCOM.Conditions oConditions = null;
            string[] sAllias = null;
            string[] sCondVal = null;
            try
            {
                oConditions = (SAPbouiCOM.Conditions)B1Connections.theAppl.CreateObject(BoCreatableObjectType.cot_Conditions);
                sAllias = pAlias.Split(',');
                sCondVal = pCondVal.Split(',');
                if (sAllias.Length == sCondVal.Length)
                {
                    for (int iOffset = 0; iOffset <= sAllias.Length - 1; iOffset++)
                    {
                        oCondition = oConditions.Add();
                        oCondition.BracketOpenNum = 1;
                        oCondition.Alias = sAllias[iOffset];
                        oCondition.Operation = pOprtn;
                        oCondition.CondVal = sCondVal[iOffset];
                        oCondition.BracketCloseNum = 1;
                        if (iOffset < sAllias.Length - 1)
                            oCondition.Relationship = BoConditionRelationship.cr_AND;
                    }
                }

                else
                    if (sAllias.Length < sCondVal.Length)
                    {
                        for (int iOffset = 0; iOffset <= sCondVal.Length - 1; iOffset++)
                        {
                            oCondition = oConditions.Add();
                            oCondition.BracketOpenNum = 1;
                            oCondition.Alias = sAllias[0];
                            oCondition.Operation = pOprtn;
                            oCondition.CondVal = sCondVal[iOffset];
                            oCondition.BracketCloseNum = 1;
                            if (iOffset < sCondVal.Length - 1)
                                oCondition.Relationship = BoConditionRelationship.cr_OR;
                        }
                    }

                    else
                    {
                        if (sAllias.Length > 0 & sCondVal.Length <= 0)
                        {
                            for (int iOffset = 0; iOffset <= sAllias.Length - 1; iOffset++)
                            {
                                oCondition = oConditions.Add();
                                oCondition.BracketOpenNum = 1;
                                oCondition.Alias = sAllias[iOffset];
                                oCondition.Operation = pOprtn;
                                oCondition.CondVal = string.Empty;
                                oCondition.BracketCloseNum = 1;
                                if (iOffset < sAllias.Length - 1)
                                    oCondition.Relationship = BoConditionRelationship.cr_AND;
                            }
                        }

                    }
                return oConditions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public static SAPbouiCOM.Conditions Create(string pAlias, string pCondVal, SAPbouiCOM.BoConditionOperation pOprtn1, SAPbouiCOM.BoConditionOperation pOprtn2)
        {

            SAPbouiCOM.Condition oCondition = null;
            SAPbouiCOM.Conditions oConditions = null;
            string[] sAllias = null;
            string[] sCondVal = null;
            try
            {
                oConditions = (SAPbouiCOM.Conditions)B1Connections.theAppl.CreateObject(BoCreatableObjectType.cot_Conditions);
                sAllias = pAlias.Split(',');
                sCondVal = pCondVal.Split(',');
                if (sAllias.Length == sCondVal.Length)
                {
                    for (int iOffset = 0; iOffset <= sAllias.Length - 1; iOffset++)
                    {
                        if (iOffset == 0)
                        {
                            oCondition = oConditions.Add();
                            oCondition.BracketOpenNum = 1;
                            oCondition.Alias = sAllias[iOffset];
                            oCondition.Operation = pOprtn1;
                            oCondition.CondVal = sCondVal[iOffset];
                            oCondition.BracketCloseNum = 1;
                        }
                        if (iOffset == 1)
                        {
                            oCondition = oConditions.Add();
                            oCondition.BracketOpenNum = 1;
                            oCondition.Alias = sAllias[iOffset];
                            oCondition.Operation = pOprtn2;
                            oCondition.CondVal = sCondVal[iOffset];
                            oCondition.BracketCloseNum = 1;
                        }
                        if (iOffset < sAllias.Length - 1)
                            oCondition.Relationship = BoConditionRelationship.cr_AND;
                    }
                }
                               
                return oConditions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static SAPbouiCOM.Conditions Create(string pAlias, string pQuery)
        {

            SAPbouiCOM.Condition oCondition = null;
            SAPbouiCOM.Conditions oConditions = null;
            string sAllias = null;
            string sCondVal = null;
            try
            {
                oConditions = (SAPbouiCOM.Conditions)B1Connections.theAppl.CreateObject(BoCreatableObjectType.cot_Conditions);

                SAPbobsCOM.Recordset RecSet = ( SAPbobsCOM.Recordset)B1Connections.diCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                RecSet.DoQuery(pQuery);

                sAllias = pAlias;
                //sCondVal = pCondVal.Split(',');
                while (!RecSet.EoF)
                {
                    oCondition = oConditions.Add();
                    oCondition.Alias = sAllias;
                    oCondition.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                    oCondition.CondVal = RecSet.Fields.Item(0).Value.ToString().Trim();
                    RecSet.MoveNext();
                    if ((RecSet.EoF == false))
                    {
                        oCondition.Relationship = SAPbouiCOM.BoConditionRelationship.cr_OR;
                    }
                }
                return oConditions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static SAPbouiCOM.Conditions Create_New(string pAlias, string pCondVal, SAPbouiCOM.BoConditionOperation pOprtn1, SAPbouiCOM.BoConditionOperation pOprtn2)
        {

            SAPbouiCOM.Condition oCondition = null;
            SAPbouiCOM.Conditions oConditions = null;
            string[] sAllias = null;
            string[] sCondVal = null;
            try
            {
                oConditions = (SAPbouiCOM.Conditions)B1Connections.theAppl.CreateObject(BoCreatableObjectType.cot_Conditions);
                sAllias = pAlias.Split(',');
                sCondVal = pCondVal.Split(',');

                    for (int iOffset = 0; iOffset <= sAllias.Length - 1; iOffset++)
                    {
                        if (iOffset == 0)
                        {
                            oCondition = oConditions.Add();
                            oCondition.BracketOpenNum = 1;
                            oCondition.Alias = sAllias[0];
                            oCondition.Operation = pOprtn1;
                            oCondition.CondVal = sCondVal[iOffset];
                            oCondition.BracketCloseNum = 1;
                            oCondition.Relationship = BoConditionRelationship.cr_AND;
                        }
                        if (iOffset == 1)
                        {
                            for (int iOffset_1 = 1; iOffset_1 <= sCondVal.Length - 1; iOffset_1++)
                            {
                                oCondition = oConditions.Add();
                                if (iOffset_1 == 1)
                                    oCondition.BracketOpenNum = 2;
                                else
                                    oCondition.BracketOpenNum = 1;

                                oCondition.Alias = sAllias[iOffset];
                                oCondition.Operation = pOprtn2;
                                oCondition.CondVal = sCondVal[iOffset_1];
                                if (sCondVal.Length - 1 != iOffset_1)
                                    oCondition.BracketCloseNum = 1;
                                else
                                    oCondition.BracketCloseNum = 2;

                                if (iOffset_1 < sCondVal.Length - 1)
                                oCondition.Relationship = BoConditionRelationship.cr_OR;
                            }
                        }
                    }

                return oConditions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

	}

}
