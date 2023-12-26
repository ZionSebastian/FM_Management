using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using SAPbobsCOM;
using SAPbouiCOM;

namespace SBOHelper.Utils
{

	internal class TItem
	{

		/// <summary>
		/// To enable or disable an item on the form.
		/// </summary>
		/// <param name="_form"></param>
		/// <param name="_itemUID"></param>
		/// <param name="_Enable"></param>
		/// <returns>if success returns true</returns>
		/// <remarks></remarks>
		public static bool Enable(SAPbouiCOM.Form _form, string _itemUID, bool _Enable)
		{

			SAPbouiCOM.Item oItem = null;
			try {
				oItem = _form.Items.Item(_itemUID);
				oItem.Enabled = _Enable;
				return true;
			} catch (Exception ex) {
				throw ex;
			} finally {
				TGeneric.ReleaseComObject((object)oItem);
				TGeneric.CollectGabage();
			}

		}

        public static void SetColor(SAPbouiCOM.Form _form, string _itemUID,int fColor,int style,int fontSize)
        {
            SAPbouiCOM.Item oItem = null;
            try
            {
                oItem = _form.Items.Item(_itemUID);
                oItem.ForeColor = fColor;
                oItem.FontSize = fontSize;
                oItem.TextStyle = style;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                TGeneric.ReleaseComObject((object)oItem);
                TGeneric.CollectGabage();
            }
        }

		/// <summary>
		/// To enable or disable  visibility of an item.
		/// </summary>
		/// <param name="_form"></param>
		/// <param name="_itemUID"></param>
		/// <param name="_Disable"></param>
		/// <returns>if success returns true </returns>
		/// <remarks></remarks>
		public static bool Visible(SAPbouiCOM.Form _form, string _itemUID, bool _Disable)
		{

			SAPbouiCOM.Item oItem = null;
			try {
				oItem = _form.Items.Item(_itemUID);
				oItem.Visible = _Disable;
				return true;
			} catch (Exception ex) {
				throw ex;
			} finally {
				TGeneric.ReleaseComObject((object)oItem);
				TGeneric.CollectGabage();
			}

		}

		public static void MoveItem(Item Ctrl)
		{
			TItem.MoveItem(Ctrl, -1, -1, -1, -1);
		}

		public static void MoveItem(Item Ctrl, int Top)
		{
			TItem.MoveItem(Ctrl, Top, -1, -1, -1);
		}

		public static void MoveItem(Item Ctrl, int Top, int Left)
		{
			TItem.MoveItem(Ctrl, Top, Left, -1, -1);
		}

		public static void MoveItem(Item Ctrl, int Top, int Left, int Width)
		{
			TItem.MoveItem(Ctrl, Top, Left, Width, -1);
		}

		public static void MoveItem(Item Ctrl, int Top, int Left, int Width, int Height)
		{
			try {
				if ((Top != -1)) {
					Ctrl.Top = Top;
				}
				if ((Left != -1)) {
					Ctrl.Left = Left;
				}
				if ((Width != -1)) {
					Ctrl.Width = Width;
				}
				if ((Height != -1)) {
					Ctrl.Height = Height;
				}
			} catch (Exception exception) {
				throw exception;
			}
		}

		public static void MoveItemToRightOfOtherItem(Item NewItem, Item BaseItem)
		{
			TItem.MoveItemToRightOfOtherItem(NewItem, BaseItem, 0, -1, -1, -1, -1);
		}

		public static void MoveItemToRightOfOtherItem(Item NewItem, Item BaseItem, int Gap)
		{
			TItem.MoveItemToRightOfOtherItem(NewItem, BaseItem, Gap, -1, -1, -1, -1);
		}

		public static void MoveItemToRightOfOtherItem(Item NewItem, Item BaseItem, int Gap, int Width)
		{
			TItem.MoveItemToRightOfOtherItem(NewItem, BaseItem, Gap, Width, -1, -1, -1);
		}
		public static void MoveItemToRightOfOtherItem(Item NewItem, Item BaseItem, int Gap, int Width, int Height)
		{
			TItem.MoveItemToRightOfOtherItem(NewItem, BaseItem, Gap, Width, Height, -1, -1);
		}
		public static void MoveItemToRightOfOtherItem(Item NewItem, Item BaseItem, int Gap, int Width, int Height, int Top)
		{
			TItem.MoveItemToRightOfOtherItem(NewItem, BaseItem, Gap, Width, Height, Top, -1);
		}

		public static void MoveItemToRightOfOtherItem(Item NewItem, Item BaseItem, int Gap, int Width, int Height, int Top, int Left)
		{
			if ((Gap < 0)) {
				Gap = 0;
			}
			if ((Top != -1)) {
				NewItem.Top = Top;
			} else {
				NewItem.Top = BaseItem.Top;
			}

			if ((Left != -1)) {
				NewItem.Left = Left;

			} else {
				NewItem.Left = ((BaseItem.Width + BaseItem.Left) + Math.Abs(Gap));
			}

			if ((Width != -1)) {
				NewItem.Width = Width;
			}
			if ((Height != -1)) {
				NewItem.Height = Height;
			}
		}

		public static void MoveItemUnderOtherItem(Item Ctrl, Item AnchorControl)
		{
			TItem.MoveItemUnderOtherItem(Ctrl, AnchorControl, 0, -1, -1);
		}

		public static void MoveItemUnderOtherItem(Item Ctrl, Item AnchorControl, int Gap)
		{
			TItem.MoveItemUnderOtherItem(Ctrl, AnchorControl, Gap, -1, -1);
		}

		public static void MoveItemUnderOtherItem(Item Ctrl, Item AnchorControl, int Gap, int Width)
		{
			TItem.MoveItemUnderOtherItem(Ctrl, AnchorControl, Gap, Width, -1);
		}

		public static void MoveItemUnderOtherItem(Item Ctrl, Item AnchorControl, int Gap, int Width, int Height)
		{
			if ((Gap < 0)) {
				Gap = 0;
			}
			Ctrl.Top = ((AnchorControl.Top + AnchorControl.Height) + Gap);
			Ctrl.Left = AnchorControl.Left;
			if ((Width != -1)) {
				Ctrl.Width = Width;
			}
			if ((Height != -1)) {
				Ctrl.Height = Height;
			}
		}

		public static ArrayList GetItems(SAPbouiCOM.Form _form)
		{
			try {
				ArrayList oItemCollection = new ArrayList();
				SAPbouiCOM.Item oItem = default(SAPbouiCOM.Item);
				foreach (SAPbouiCOM.Item tempLoopVar_oItem in _form.Items) {
					oItem = tempLoopVar_oItem;
					oItemCollection.Add(oItem);
				}
				return oItemCollection;
			} catch (Exception ex) {
				throw ex;
			}
		}

        public static void ControlCreator(SAPbouiCOM.BoFormItemTypes _ControlType, SAPbouiCOM.Form _form, string _Uid, string _ReffItem, string _RefArea)
        {
            SAPbouiCOM.Item oItem = default(SAPbouiCOM.Item);
            oItem = _form.Items.Add(_Uid.Trim(), _ControlType);
            if (_RefArea == "B")
            {
                oItem.Left = _form.Items.Item(_ReffItem.Trim()).Left;
                oItem.Width = _form.Items.Item(_ReffItem.Trim()).Width;
                oItem.Top = _form.Items.Item(_ReffItem.Trim()).Top + _form.Items.Item(_ReffItem.Trim()).Height + 1;
                oItem.Height = _form.Items.Item(_ReffItem.Trim()).Height;
                oItem.FromPane = _form.Items.Item(_ReffItem.Trim()).FromPane;
                oItem.ToPane = _form.Items.Item(_ReffItem.Trim()).ToPane;
            }
            else if (_RefArea == "BB")
            {
                oItem.Left = _form.Items.Item(_ReffItem.Trim()).Left;
                oItem.Width = _form.Items.Item(_ReffItem.Trim()).Width;
                oItem.Top = _form.Items.Item(_ReffItem.Trim()).Top + _form.Items.Item(_ReffItem.Trim()).Height + 2;
                oItem.Height = _form.Items.Item(_ReffItem.Trim()).Height;
                oItem.FromPane = _form.Items.Item(_ReffItem.Trim()).FromPane;
                oItem.ToPane = _form.Items.Item(_ReffItem.Trim()).ToPane;
            }
            else if (_RefArea == "T")
            {
                oItem.Left = _form.Items.Item(_ReffItem.Trim()).Left;
                oItem.Width = _form.Items.Item(_ReffItem.Trim()).Width;
                oItem.Top = _form.Items.Item(_ReffItem.Trim()).Top - _form.Items.Item(_ReffItem.Trim()).Height - 1;
                oItem.Height = _form.Items.Item(_ReffItem.Trim()).Height;
                oItem.FromPane = _form.Items.Item(_ReffItem.Trim()).FromPane;
                oItem.ToPane = _form.Items.Item(_ReffItem.Trim()).ToPane;
            }
            else if (_RefArea == "L")
            {
                oItem.Left = _form.Items.Item(_ReffItem.Trim()).Left - _form.Items.Item(_ReffItem.Trim()).Width - 1;
                oItem.Width = _form.Items.Item(_ReffItem.Trim()).Width;
                oItem.Top = _form.Items.Item(_ReffItem.Trim()).Top;
                oItem.Height = _form.Items.Item(_ReffItem.Trim()).Height;
                oItem.FromPane = _form.Items.Item(_ReffItem.Trim()).FromPane;
                oItem.ToPane = _form.Items.Item(_ReffItem.Trim()).ToPane;
            }
            else if (_RefArea == "R")
            {
                oItem.Left = _form.Items.Item(_ReffItem.Trim()).Left + _form.Items.Item(_ReffItem.Trim()).Width + 1;
                oItem.Width = _form.Items.Item(_ReffItem.Trim()).Width;
                oItem.Top = _form.Items.Item(_ReffItem.Trim()).Top;
                oItem.Height = _form.Items.Item(_ReffItem.Trim()).Height;
                oItem.FromPane = _form.Items.Item(_ReffItem.Trim()).FromPane;
                oItem.ToPane = _form.Items.Item(_ReffItem.Trim()).ToPane;

            }
            else if (_RefArea == "N")
            {
                oItem.Left = _form.Items.Item(_ReffItem.Trim()).Left-40 ;
                oItem.Width = _form.Items.Item(_ReffItem.Trim()).Width;
                oItem.Top = _form.Items.Item(_ReffItem.Trim()).Top;
                oItem.Height = _form.Items.Item(_ReffItem.Trim()).Height;
                oItem.FromPane = _form.Items.Item(_ReffItem.Trim()).FromPane;
                oItem.ToPane = _form.Items.Item(_ReffItem.Trim()).ToPane;

            }
            else
            {
            }



        }


		public static void Click(SAPbouiCOM.Form _form, string _uid)
		{
			SAPbouiCOM.Item oItem = default(SAPbouiCOM.Item);
			try {
				oItem = _form.Items.Item(_uid);
				if (oItem.Type == BoFormItemTypes.it_EDIT || oItem.Type == BoFormItemTypes.it_EXTEDIT) {
					if (oItem.Enabled) {
						oItem.Click(BoCellClickType.ct_Regular);
					}
				}
			} catch (Exception ex) {
				throw ex;
			}
		}
        public static void ControlCreator2(SAPbouiCOM.BoFormItemTypes _ControlType, SAPbouiCOM.Form _form, string _Uid, string _ReffItem, string _RefArea, String _Caption, String _LinkTo)
        {
            SAPbouiCOM.Item oItem = default(SAPbouiCOM.Item);
            oItem = _form.Items.Add(_Uid.Trim(), _ControlType);
            if (_RefArea == "B")
            {
                oItem.Left = _form.Items.Item(_ReffItem.Trim()).Left;
                oItem.Width = _form.Items.Item(_ReffItem.Trim()).Width;
                oItem.Top = _form.Items.Item(_ReffItem.Trim()).Top + _form.Items.Item(_ReffItem.Trim()).Height + 1;
                oItem.Height = _form.Items.Item(_ReffItem.Trim()).Height;
                oItem.FromPane = _form.Items.Item(_ReffItem.Trim()).FromPane;
                oItem.ToPane = _form.Items.Item(_ReffItem.Trim()).ToPane;
            }
            else if (_RefArea == "T")
            {
                oItem.Left = _form.Items.Item(_ReffItem.Trim()).Left;
                oItem.Width = _form.Items.Item(_ReffItem.Trim()).Width;
                oItem.Top = _form.Items.Item(_ReffItem.Trim()).Top - _form.Items.Item(_ReffItem.Trim()).Height - 1;
                oItem.Height = _form.Items.Item(_ReffItem.Trim()).Height;
                oItem.FromPane = _form.Items.Item(_ReffItem.Trim()).FromPane;
                oItem.ToPane = _form.Items.Item(_ReffItem.Trim()).ToPane;
            }
            else if (_RefArea == "L")
            {
                oItem.Left = _form.Items.Item(_ReffItem.Trim()).Left - _form.Items.Item(_ReffItem.Trim()).Width - 1;
                oItem.Width = _form.Items.Item(_ReffItem.Trim()).Width;
                oItem.Top = _form.Items.Item(_ReffItem.Trim()).Top;
                oItem.Height = _form.Items.Item(_ReffItem.Trim()).Height;
                oItem.FromPane = _form.Items.Item(_ReffItem.Trim()).FromPane;
                oItem.ToPane = _form.Items.Item(_ReffItem.Trim()).ToPane;
            }
            else if (_RefArea == "R")
            {
                oItem.Left = _form.Items.Item(_ReffItem.Trim()).Left + _form.Items.Item(_ReffItem.Trim()).Width + 1;
                oItem.Width = _form.Items.Item(_ReffItem.Trim()).Width;
                oItem.Top = _form.Items.Item(_ReffItem.Trim()).Top;
                oItem.Height = _form.Items.Item(_ReffItem.Trim()).Height;
                oItem.FromPane = _form.Items.Item(_ReffItem.Trim()).FromPane;
                oItem.ToPane = _form.Items.Item(_ReffItem.Trim()).ToPane;

            }
            else if (_RefArea == "S")
            {
                oItem.Left = _form.Items.Item(_ReffItem.Trim()).Left;
                oItem.Width = _form.Items.Item(_ReffItem.Trim()).Width;
                oItem.Top = _form.Items.Item(_ReffItem.Trim()).Top;
                oItem.Height = _form.Items.Item(_ReffItem.Trim()).Height;
                oItem.FromPane = _form.Items.Item(_ReffItem.Trim()).FromPane;
                oItem.ToPane = _form.Items.Item(_ReffItem.Trim()).ToPane;

            }
            if (_ControlType == BoFormItemTypes.it_FOLDER)
            {
                SAPbouiCOM.Folder oCtrl;
                oCtrl =(SAPbouiCOM.Folder) oItem.Specific;
                oCtrl.Caption = _Caption.Trim();
                oCtrl.GroupWith(_ReffItem.Trim());
            }
            if (_ControlType == BoFormItemTypes.it_STATIC)
            {
                SAPbouiCOM.StaticText oCtrl;
                oCtrl = ( SAPbouiCOM.StaticText)oItem.Specific;
                oCtrl.Caption = _Caption.Trim();
                oItem.LinkTo = _LinkTo;
            }
          
            if (_ControlType == BoFormItemTypes.it_BUTTON)
            {
                SAPbouiCOM.Button oCtrl;
                oCtrl = (SAPbouiCOM.Button)oItem.Specific;
                oCtrl.Caption = _Caption.Trim();

            }



        }

        public static void MoveItemToRightOfOtherItem1(Item NewItem, Item BaseItem, int Gap, int Width, int Height, int Top, int Left)
        {
            if ((Gap < 0))
            {
                Gap = 2;
            }
            if ((Top != -1))
            {
                NewItem.Top = Top;
            }
            else
            {
                NewItem.Top = BaseItem.Top;
            }

            if ((Left != -1))
            {
                NewItem.Left = Left;

            }
            else
            {
                NewItem.Left = ((BaseItem.Width + BaseItem.Left) + Math.Abs(Gap));
            }
            
            if ((Height != -1))
            {
                NewItem.Height = Height;
            }
            if (Width == -1)
            {
                NewItem.Width = 20;
            }
        }

	}

}
