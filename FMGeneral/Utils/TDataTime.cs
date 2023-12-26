using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace SBOHelper.Utils
{

	internal class TDataTime
	{


		/// <summary>
		/// Returns current date in yyyyMMdd format
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string GetCurrentDate {
			get { return System.DateTime.Today.ToString("yyyyMMdd"); }
		}

		// Methods

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pDate"></param>
		/// <param name="pFormat"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string ConvertString(string pDate, int pFormat)
		{

			if ((pFormat != 0x65)) {
				return string.Empty;
			}
			if ((((pDate == null) || (pDate == string.Empty)) || ((pDate.Length != 6) && (pDate.Length != 8)))) {
				return string.Empty;
			}
			if ((pDate.Length == 6)) {
				return string.Concat(new string[] {
					"Convert(datetime, N'",
					pDate.Substring(2, 2),
					"/",
					pDate.Substring(4, 2),
					"/",
					pDate.Substring(0, 2),
					"', ",
					pFormat.ToString(),
					")"
				});
			}
			return string.Concat(new string[] {
				"Convert(datetime, N'",
				pDate.Substring(4, 2),
				"/",
				pDate.Substring(6, 2),
				"/",
				pDate.Substring(0, 4),
				"', ",
				pFormat.ToString(),
				")"
			});

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pDateTime"></param>
		/// <param name="pFormat"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string GetDateAsStringFormat(DateTime pDateTime, int pFormat)
		{

			string sYear = string.Empty;
			string sMonth = string.Empty;
			string sDay = string.Empty;
			try {
				if ((pFormat == 0x65)) {
					return string.Concat(new object[] {
						pDateTime.Month.ToString(),
						"/",
						pDateTime.Day,
						"/",
						pDateTime.Year.ToString()
					});
				}
				if ((pFormat == 0x67)) {
					sYear = pDateTime.Year.ToString();
					if (pDateTime.Month.ToString().Trim().Length == 1) {
						sMonth = "0" + pDateTime.Month.ToString();
					} else {
						sMonth = pDateTime.Month.ToString();
					}
					if (pDateTime.Day.ToString().Trim().Length == 1) {
						sDay = "0" + pDateTime.Day.ToString();
					} else {
						sDay = pDateTime.Day.ToString();
					}
					return string.Concat(new object[] {
						sMonth,
						"/",
						sDay,
						"/",
						sYear
					});
				}

				if ((pFormat == 0x66)) {
					sYear = pDateTime.Year.ToString();
					if (pDateTime.Month.ToString().Trim().Length == 1) {
						sMonth = "0" + pDateTime.Month.ToString();
					} else {
						sMonth = pDateTime.Month.ToString();
					}
					if (pDateTime.Day.ToString().Trim().Length == 1) {
						sDay = "0" + pDateTime.Day.ToString();
					} else {
						sDay = pDateTime.Day.ToString();
					}

					return string.Concat(new object[] {
						sYear,
						sMonth,
						sDay
					});
				}
				return string.Empty;
			} catch (Exception ex) {
				throw ex;
			}

		}

		///' <summary>
		///' 
		///' </summary>
		///' <param name="pDate"></param>
		///' <returns></returns>
		///' <remarks></remarks>
		//Public Shared Function GetDate(ByVal pDate As String) As DateTime

		//    Dim dtReturn As DateTime
		//    Dim iYear As Integer
		//    Dim iMonth As Integer
		//    Dim iDay As Integer
		//    Try
		//        If pDate.Length = 8 Then
		//            iYear = System.Convert.ToInt32(pDate.Substring(0, 4))
		//            iMonth = System.Convert.ToInt32(pDate.Substring(4, 2))
		//            iDay = System.Convert.ToInt32(pDate.Substring(6, 2))
		//            dtReturn = New DateTime(iYear, iMonth, iDay)
		//        ElseIf pDate.Length = 10 Then
		//            iYear = System.Convert.ToInt32(pDate.Substring(6, 4))
		//            iMonth = System.Convert.ToInt32(pDate.Substring(3, 2))
		//            iDay = System.Convert.ToInt32(pDate.Substring(0, 2))
		//            dtReturn = New DateTime(iYear, iMonth, iDay)
		//        End If
		//        Return dtReturn
		//    Catch ex As Exception
		//        Throw ex
		//    End Try

		//End Function

		public static System.DateTime GetDate(string pDate)
		{

			System.DateTime dtReturn = default(System.DateTime);
			int iYear = 0;
			int iMonth = 0;
			int iDay = 0;
			try {
				if (pDate.Length == 8) {
					iYear = System.Convert.ToInt32(pDate.Substring(0, 4));
					iMonth = System.Convert.ToInt32(pDate.Substring(4, 2));
					iDay = System.Convert.ToInt32(pDate.Substring(6, 2));
					dtReturn = new DateTime(iYear, iMonth, iDay);
				} else if (pDate.Length == 10) {
					iYear = System.Convert.ToInt32(pDate.Substring(6, 4));
					iMonth = System.Convert.ToInt32(pDate.Substring(3, 2));
					iDay = System.Convert.ToInt32(pDate.Substring(0, 2));
					dtReturn = new System.DateTime(iYear, iMonth, iDay);
				}
				return dtReturn;
			} catch (Exception ex) {
				throw ex;
			}

		}

		public static int DateDifference(string FromDate, string ToDate, DateDiffTypes RetType)
		{
			System.DateTime dtFromDate = default(System.DateTime);
			System.DateTime dtToDate = default(System.DateTime);
			TimeSpan tsTimeSpan = default(TimeSpan);
			int iRetunVal = 0;
			try {
				dtFromDate = GetDate(FromDate.Trim());
				dtToDate = GetDate(ToDate.Trim());
				tsTimeSpan = dtToDate.Subtract(dtFromDate);
				switch (RetType) {

					case DateDiffTypes.Days:
						iRetunVal = tsTimeSpan.Days;
						break;
					case DateDiffTypes.Hours:
						iRetunVal = tsTimeSpan.Hours;
						break;
					case DateDiffTypes.Minutes:
						iRetunVal = tsTimeSpan.Minutes;
						break;
					case DateDiffTypes.Seconds:
						iRetunVal = tsTimeSpan.Seconds;
						break;
				}
				return iRetunVal;
			} catch (Exception ex) {
				throw ex;
			}

		}



	}
}

