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

	internal class TForm
	{

		public static bool Disable(SAPbouiCOM.Form _form)
		{

			ArrayList itemCollection = null;
			SAPbouiCOM.Item oItem = default(SAPbouiCOM.Item);
			try {
				itemCollection = TItem.GetItems(_form);
				//_form.Freeze(True)
				foreach (SAPbouiCOM.Item tempLoopVar_oItem in itemCollection) {
					if (tempLoopVar_oItem.Type == BoFormItemTypes.it_FOLDER | tempLoopVar_oItem.Type == BoFormItemTypes.it_STATIC | tempLoopVar_oItem.Type == BoFormItemTypes.it_LINKED_BUTTON) {
						oItem = tempLoopVar_oItem;
						oItem.Enabled = true;
					} else {
						oItem = tempLoopVar_oItem;
						//for excluding button 1, 2 and txtTemp
                        if (oItem.UniqueID == "0_U_G" | oItem.UniqueID == "1" | oItem.UniqueID == "2" | oItem.UniqueID == "-1" | oItem.UniqueID=="Item_Info")
                        {
							if (oItem.UniqueID != "-1") {
								oItem.Enabled = true;
							}
						} else {
							oItem.Enabled = false;
						}
					}
				}
				//Also to disable form we can use the following
				//'_form.Mode = BoFormMode.fm_VIEW_MODE
				//'_form.Refresh()
				//'_form.Update()
				// _form.Freeze(False)
				return true;
			} catch (Exception ex) {
				//_form.Freeze(False)
				throw ex;
			}

		}

		public static bool ResizeItems(SAPbouiCOM.Form _form)
		{

			ArrayList itemCollection = null;
			SAPbouiCOM.Item oItem = default(SAPbouiCOM.Item);
			try
			{
				itemCollection = TItem.GetItems(_form);
				//_form.Freeze(True)
				foreach (SAPbouiCOM.Item tempLoopVar_oItem in itemCollection)
				{
					if (tempLoopVar_oItem.Type == BoFormItemTypes.it_FOLDER | tempLoopVar_oItem.Type == BoFormItemTypes.it_LINKED_BUTTON)
					{
						oItem = tempLoopVar_oItem;
						oItem.Enabled = true;
					}
					else
					{
						oItem = tempLoopVar_oItem;
						//for excluding button 1, 2 and txtTemp
						if (oItem.UniqueID == "1" | oItem.UniqueID == "2" | oItem.UniqueID == "-1" | oItem.UniqueID == "Item_Info")
						{
							if (oItem.UniqueID != "-1")
							{
								oItem.AffectsFormMode = false;
								oItem.FontSize = 12;
								oItem.Height = 18;
								oItem.TextStyle = 1;
							}
						}
						else
						{
							oItem.AffectsFormMode = false;
							oItem.FontSize = 18;
							oItem.Height = 20;
							oItem.TextStyle = 1;
						}
					}
				}
				//Also to disable form we can use the following
				//'_form.Mode = BoFormMode.fm_VIEW_MODE
				//'_form.Refresh()
				//'_form.Update()
				// _form.Freeze(False)
				return true;
			}
			catch (Exception ex)
			{
				//_form.Freeze(False)
				throw ex;
			}

		}

		public static bool Enable(SAPbouiCOM.Form _form)
		{
			ArrayList itemCollection = null;
			SAPbouiCOM.Item oItem = default(SAPbouiCOM.Item);
			try {
				itemCollection = TItem.GetItems(_form);
				_form.Freeze(true);
				foreach (SAPbouiCOM.Item tempLoopVar_oItem in itemCollection) {
					oItem = tempLoopVar_oItem;
					oItem.Enabled = true;
				}
				_form.Freeze(false);
				return true;
			} catch (Exception ex) {
				_form.Freeze(false);
				throw ex;
			}
		}

		public static bool Open(string sUDOObjectType, string sObjectKey, SAPbouiCOM.BoFormObjectEnum formType)
		{

			SAPbouiCOM.Form oForm = default(SAPbouiCOM.Form);
			try {
				oForm = B1Connections.theAppl.OpenForm(formType, sUDOObjectType, sObjectKey);
			} catch (Exception ex) {
				throw ex;
			}
            return true;
		}

        public static void EnableMenu(SAPbouiCOM.Form form, string menuID,bool enableFlag)
        {
            try
            {
                form.EnableMenu(menuID, enableFlag);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool Resize_Control(SAPbouiCOM.Form _form)
        {

            ArrayList itemCollection = null;
            SAPbouiCOM.Item oItem = default(SAPbouiCOM.Item);
            try
            {
                itemCollection = TItem.GetItems(_form);
                //_form.Freeze(True)
                foreach (SAPbouiCOM.Item tempLoopVar_oItem in itemCollection)
                {
                    if (tempLoopVar_oItem.Type != BoFormItemTypes.it_MATRIX && tempLoopVar_oItem.Type != BoFormItemTypes.it_EXTEDIT)
                    {
                        oItem = tempLoopVar_oItem;
                        oItem.Height = 18;
                    }                    
                }

                return true;
            }
            catch (Exception ex)
            {
                //_form.Freeze(False)
                throw ex;
            }
        }

    }

}
