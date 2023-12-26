using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;
////using SAPbouiCOM.Framework;

namespace SBOHelper.Utils
{

	internal class TMatrix
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_form"></param>
		/// <param name="_matrixUID"></param>
		/// <param name="_columnName"></param>
		/// <param name="_dbDataSource"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static bool addRow(SAPbouiCOM.Form _form, string _matrixUID, string _columnName, string _dbDataSource)
		{

			try {

				if (_form.Mode != BoFormMode.fm_FIND_MODE) {
					SAPbouiCOM.Matrix oMatrix = (Matrix)_form.Items.Item(_matrixUID).Specific;
					SAPbouiCOM.Column oColumn = oMatrix.Columns.Item(_columnName);
					_form.DataSources.DBDataSources.Item(_dbDataSource).Clear();
					int rowCount = oMatrix.VisualRowCount;
					oMatrix.AddRow(1, rowCount);
					//oMatrix.SelectRow(1 + rowCount, true, false);
                    oColumn.Cells.Item(oMatrix.VisualRowCount).Click(BoCellClickType.ct_Regular,0);
                    if (_form.Mode != BoFormMode.fm_ADD_MODE)
						_form.Mode = BoFormMode.fm_UPDATE_MODE;
					oMatrix.FlushToDataSource();
					
				}

			} catch (Exception ex) {
				throw ex;
			}
            return true;
		}
        public static bool addRow(SAPbouiCOM.Form _form, string _matrixUID, string _columnName, string _dbDataSource,bool clickRequired)
        {

            try
            {

                if (_form.Mode != BoFormMode.fm_FIND_MODE)
                {
                    SAPbouiCOM.Matrix oMatrix = (Matrix)_form.Items.Item(_matrixUID).Specific;
                    SAPbouiCOM.Column oColumn = oMatrix.Columns.Item(_columnName);
                    _form.DataSources.DBDataSources.Item(_dbDataSource).Clear();
                    int rowCount = oMatrix.VisualRowCount;
                    oMatrix.AddRow(1, rowCount);
                    //oMatrix.SelectRow(1 + rowCount, true, false);
                    if (clickRequired)
                    {
                        oColumn.Cells.Item(oMatrix.VisualRowCount).Click(BoCellClickType.ct_Regular, 0);
                    }
                    if (_form.Mode != BoFormMode.fm_ADD_MODE)
                        _form.Mode = BoFormMode.fm_UPDATE_MODE;
                    oMatrix.FlushToDataSource();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public static bool addRow(SAPbouiCOM.Form _form, string _matrixUID, string _columnName, string _dbDataSource, int _CurrentRow)
        {

            try
            {

                if (_form.Mode != BoFormMode.fm_FIND_MODE)
                {
                    SAPbouiCOM.Matrix oMatrix = (Matrix)_form.Items.Item(_matrixUID).Specific;
                    SAPbouiCOM.Column oColumn = oMatrix.Columns.Item(_columnName);
                    _form.DataSources.DBDataSources.Item(_dbDataSource).Clear();
                    int selRow = oMatrix.GetNextSelectedRow(0, BoOrderType.ot_SelectionOrder);
                    int rowCount = oMatrix.VisualRowCount;
                    oMatrix.AddRow(1, selRow);
                    //oMatrix.SelectRow(1 + rowCount, true, false);
                    oColumn.Cells.Item(oMatrix.VisualRowCount).Click(BoCellClickType.ct_Regular, 0);
                    if (_form.Mode != BoFormMode.fm_ADD_MODE)
                        _form.Mode = BoFormMode.fm_UPDATE_MODE;
                    oMatrix.FlushToDataSource();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_form"></param>
		/// <param name="_matrixUID"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static bool deleteRow(SAPbouiCOM.Form _form, string _matrixUID)
		{

			try {

				if (_form.Mode != BoFormMode.fm_FIND_MODE) {
					SAPbouiCOM.Matrix oMatrix =(Matrix) _form.Items.Item(_matrixUID).Specific;
					int selRow = oMatrix.GetNextSelectedRow(0, BoOrderType.ot_SelectionOrder);
					if (selRow == -1) {
						B1Connections.theAppl.SetStatusBarMessage("Select a row to delete. ", BoMessageTime.bmt_Short, true);
						return false;
					}
					oMatrix.DeleteRow(selRow);
					if (_form.Mode != BoFormMode.fm_ADD_MODE)
						_form.Mode = BoFormMode.fm_UPDATE_MODE;
				}

				return true;
			} catch (Exception ex) {
				throw ex;
			}
		}

        public static bool deleteRow(SAPbouiCOM.Form _form, string _matrixUID, int selectedRow)
        {

            try
            {

                if (_form.Mode != BoFormMode.fm_FIND_MODE)
                {
                    SAPbouiCOM.Matrix oMatrix = (Matrix)_form.Items.Item(_matrixUID).Specific;
                    int selRow = selectedRow;
                    if (selRow == -1)
                    {
                        B1Connections.theAppl.SetStatusBarMessage("Select a row to delete. ", BoMessageTime.bmt_Short, true);
                        return false;
                    }
                    oMatrix.DeleteRow(selRow);
                    if (_form.Mode != BoFormMode.fm_ADD_MODE)
                        _form.Mode = BoFormMode.fm_UPDATE_MODE;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_form"></param>
		/// <param name="_matrixUID"></param>
		/// <param name="_column"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static bool RefreshRowNo(SAPbouiCOM.Form _form, string _matrixUID, string _column)
		{

			SAPbouiCOM.Matrix oMatrix = default(SAPbouiCOM.Matrix);
			SAPbouiCOM.Column oColumn = default(SAPbouiCOM.Column);
			try {
				oMatrix = (Matrix)_form.Items.Item(_matrixUID).Specific;
				oColumn = oMatrix.Columns.Item(_column.Trim());

				var _with1 = _form.DataSources.DBDataSources.Item(oColumn.DataBind.TableName);

				if (oMatrix.RowCount > 0) {
					_with1.Clear();
					oMatrix.FlushToDataSource();
					for (int iRowNo = 0; iRowNo <= _with1.Size - 1; iRowNo++) {
						_with1.SetValue(oColumn.DataBind.Alias, iRowNo, Convert.ToString((1 + iRowNo)));
					}
					oMatrix.LoadFromDataSourceEx(false );
				}
				return true;
			} catch (Exception ex) {
				throw ex;
			}
		}

        public static bool RefreshRowNo_UDF(SAPbouiCOM.Form _form, string _matrixUID, string _TableName, string _FieldName)
        {

            try
            {
                _form.Freeze(true);
                SAPbouiCOM.Matrix oMatrix = (SAPbouiCOM.Matrix)_form.Items.Item(_matrixUID).Specific;
                var _withFFT1 = _form.DataSources.DBDataSources.Item(_TableName);
                oMatrix.FlushToDataSource();
                for (int i = 0; i <= oMatrix.RowCount; i++)
                {
                    _withFFT1.SetValue(_FieldName, i - 1, Convert.ToString(i));
                    oMatrix.LoadFromDataSourceEx();
                }
                return true;
            }
            catch (Exception ex)
            {
                _form.Freeze(false);
                throw ex;
            }
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_form"></param>
		/// <param name="_matrixUID"></param>
		/// <param name="_colList"></param>
		/// <param name="_flag"></param>
		/// <remarks></remarks>

		public static void EnableColumns(SAPbouiCOM.Form _form, string _matrixUID, string _colList, bool _flag)
		{
			SAPbouiCOM.Matrix oMatrix = default(SAPbouiCOM.Matrix);
			Object colID = null;
			List<string> lstColumns = null;
			try {
				//Creating a new List<T> of type <string>
				lstColumns = new List<string>(_colList.Split(',').Length );
				//AddRange is a method of List<T> objects that enables the conversion.
				//We just need to pass the reference to the array.
				lstColumns.AddRange(_colList.Split(','));
				oMatrix = (Matrix)_form.Items.Item(_matrixUID).Specific;
				foreach (object colID_loopVariable in lstColumns) {
					colID = colID_loopVariable;
					oMatrix.Columns.Item(colID).Editable = _flag;
				}
			} catch (Exception ex) {
				TNotification.StatusBarError(ex.Message);
			}
		}


		public static void EnableColumns(SAPbouiCOM.Matrix _matrix, string _colList, bool _flag)
		{
			Object colID = null;
			List<string> lstColumns = null;
			try {
				//Creating a new List<T> of type <string>
				lstColumns = new List<string>(_colList.Split(',').Length);
				//AddRange is a method of List<T> objects that enables the conversion.
				//We just need to pass the reference to the array.
				lstColumns.AddRange(_colList.Split(','));
				foreach (object colID_loopVariable in lstColumns) {
					colID = colID_loopVariable;
					_matrix.Columns.Item(colID).Editable = _flag;
				}
			} catch (Exception ex) {
				TNotification.StatusBarError(ex.Message);
			}
		}

		public static void VisibleColumns(SAPbouiCOM.Matrix _matrix, string _colList, bool _flag)
		{
			Object colID = null;
			List<string> lstColumns = null;
			try
			{
				//Creating a new List<T> of type <string>
				lstColumns = new List<string>(_colList.Split(',').Length);
				//AddRange is a method of List<T> objects that enables the conversion.
				//We just need to pass the reference to the array.
				lstColumns.AddRange(_colList.Split(','));
				foreach (object colID_loopVariable in lstColumns)
				{
					colID = colID_loopVariable;
					_matrix.Columns.Item(colID).Visible  = _flag;
				}
			}
			catch (Exception ex)
			{
				TNotification.StatusBarError(ex.Message);
			}
		}

		public static void CtrlTab(ref ItemEvent pVal)
		{
			string sSlctdVal = null;
			SAPbouiCOM.Form oForm = default(SAPbouiCOM.Form);
			SAPbouiCOM.Matrix oMatrix = default(SAPbouiCOM.Matrix);
			SAPbouiCOM.EditText oEditText = default(SAPbouiCOM.EditText);
			SAPbouiCOM.Column oColumn = default(SAPbouiCOM.Column);
			try {
				oForm = B1Connections.theAppl.Forms.ActiveForm;
				oMatrix = (Matrix )oForm.Items.Item(pVal.ItemUID).Specific;
				oColumn = oMatrix.Columns.Item(pVal.ColUID);
				oEditText =(EditText) oMatrix.GetCellSpecific(pVal.ColUID, pVal.Row);
				sSlctdVal = oEditText.Value;
				// oEditText.Value = TEditText.SetValue(oEditText, String.Empty)
				var _with2 = oForm.DataSources.DBDataSources.Item(oMatrix.Columns.Item(pVal.ColUID).DataBind.TableName);
				_with2.Clear();
				oMatrix.FlushToDataSource();
				_with2.SetValue(oMatrix.Columns.Item(pVal.ColUID).DataBind.Alias, pVal.Row - 1, string.Empty);
				oMatrix.LoadFromDataSourceEx(false);
				oMatrix.Columns.Item(pVal.ColUID).Cells.Item(pVal.Row).Click(BoCellClickType.ct_Regular,0);
				_with2.Clear();
				oMatrix.FlushToDataSource();
				_with2.SetValue(oMatrix.Columns.Item(pVal.ColUID).DataBind.Alias, pVal.Row - 1, sSlctdVal);
				oMatrix.LoadFromDataSourceEx(false);
			} catch (Exception ex) {
				TNotification.StatusBarError(ex.Message);
			}

			//_matrix.Columns.Item(_uid2).Cells.Item(_row.Row).Click(BoCellClickType.ct_Regular)
			//_form.ActiveItem = _uid2

			//_matrix.Columns.Item(_uid2).Cells.Item(_row.Row).Click(BoCellClickType.ct_Regular)

		}

        public static void SetBackColourColumns(SAPbouiCOM.Matrix _matrix, string _colList, int _colour)
        {
            Object colID = null;
            List<string> lstColumns = null;
            try
            {
                //Creating a new List<T> of type <string>
                lstColumns = new List<string>(_colList.Split(',').Length);
                //AddRange is a method of List<T> objects that enables the conversion.
                //We just need to pass the reference to the array.
                lstColumns.AddRange(_colList.Split(','));
                foreach (object colID_loopVariable in lstColumns)
                {
                    colID = colID_loopVariable;
                    _matrix.Columns.Item(colID).BackColor = _colour;
                }
            }
            catch (Exception ex)
            {
                TNotification.StatusBarError(ex.Message);
            }
        }

        public static int GetColumnPosition(SAPbouiCOM.Matrix oMatrix, string colUID)
        {
            int iRetVal = -1;
            try
            {
                for (int iRows = 0; iRows <= oMatrix.Columns.Count - 1; iRows++)
                {
                    if (oMatrix.Columns.Item(iRows).UniqueID.Equals(colUID.Trim()))
                    {
                        iRetVal = iRows;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
                return iRetVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

	}

}
