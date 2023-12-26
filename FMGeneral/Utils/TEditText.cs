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

	internal class TEditText
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_EditText"></param>
		/// <param name="_value"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static bool SetValue(EditText _EditText, string _value)
		{

			try {
				_EditText.String = _value;

				return true;
			} catch (Exception generatedExceptionName) {
				return true;
			}

		}


        public static double GetValue(EditText _EditText)
        {
            string[] strArr ;
            double value = 0;
            try
            {
                // strArr = _EditText.Value.Trim.Split(" ")
                strArr = _EditText.Value.Trim().Split(' ');
                if (strArr.Length == 1)
                {
                    value = 0;
                }
                else
                {
                    if (B1Connections.diCompany.GetCompanyService().GetAdminInfo().DisplayCurrencyontheRight == BoYesNoEnum.tYES)
                    {
                        value = Convert.ToDouble(strArr[0]);
                    }
                    else
                    {
                        value = Convert.ToDouble(strArr[1]);
                    }
                }
                return value;
            }
            catch (Exception generatedExceptionName)
            {
                return value;
            }

        }

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
			SAPbouiCOM.EditText oEditText = default(SAPbouiCOM.EditText);

			try {
				oEditText = (SAPbouiCOM.EditText)_Form.Items.Item(_ItemUID).Specific;
				sRetVal = oEditText.Value;
				return sRetVal;
			} catch (Exception ex) {
				throw ex;
			}

		}


		///' <summary>
		///' 
		///' </summary>
		///' <param name="_form"></param>
		///' <param name="_EditTextUid"></param>
		///' <returns></returns>
		///' <remarks></remarks>
		//Public Shared Function SetValue(ByRef _form As SAPbouiCOM.Form, ByVal _EditTextUid As String) As Boolean

		//    Try
		//        Dim oEditText As SAPbouiCOM.EditText = _form.Items.Item(_EditTextUid).Specific

		//        If oEditText.DataBind.DataBound Then
		//            With _form.DataSources.DBDataSources.Item(oEditText.DataBind.TableName)
		//                .SetValue(oEditText.DataBind.Alias, 0, Format(System.DateTime.Today, "yyyyMMdd").Trim)
		//            End With
		//        End If

		//        Return True
		//    Catch ex As Exception
		//        Throw ex
		//    End Try

		//End Function


		///' <summary>
		///' 
		///' </summary>
		///' <param name="_form"></param>
		///' <param name="_EditTextUid"></param>
		///' <param name="_Value"></param>
		///' <returns></returns>
		///' <remarks></remarks>
		//Public Shared Function SetValue(ByRef _form As SAPbouiCOM.Form, ByVal _EditTextUid As String, ByVal _Value As String) As Boolean

		//    Try
		//        Dim oEditText As SAPbouiCOM.EditText = _form.Items.Item(_EditTextUid).Specific

		//        If oEditText.DataBind.DataBound Then
		//            With _form.DataSources.DBDataSources.Item(oEditText.DataBind.TableName)
		//                .SetValue(oEditText.DataBind.Alias, 0, _Value.Trim)
		//            End With
		//        End If

		//        Return True
		//    Catch ex As Exception
		//        Throw ex
		//    End Try

		//End Function

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_Form"></param>
		/// <param name="_Uid"></param>
		/// <param name="_Cfl"></param>
		/// <param name="_Alias"></param>
		/// <remarks></remarks>
		public static void SetChooseFromList(SAPbouiCOM.Form _Form, string _Uid, string _Cfl, string _Alias)
		{
			SAPbouiCOM.EditText oEditText = default(SAPbouiCOM.EditText);
			try {
				oEditText = (EditText)_Form.Items.Item(_Uid).Specific;
				oEditText.ChooseFromListUID = _Cfl;
				oEditText.ChooseFromListAlias = _Alias;
			} catch (Exception ex) {
				throw ex;
			}
		}

	}

}

