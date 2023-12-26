using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;
using System.Xml;
//using SAPbouiCOM.Framework;

namespace SBOHelper.Utils
{

	internal class TXML
	{

		/// <summary>
		/// Saves XML representation of a form to the path
		/// <para>Path should end with a slash. Filename is 'UniqueID'.xml </para>
		/// </summary>
		/// <param name="_form">The form to save. </param>
		/// <param name="_path">String with path information e.g. 'c:\temp\' </param>
		/// <returns>Returns the XML string or nothing. </returns>
		public static System.Xml.XmlDocument saveForm(ref SAPbouiCOM.Form _form, string _path)
		{

			try {
				System.Xml.XmlDocument oXMLDoc = new System.Xml.XmlDocument();
				oXMLDoc.LoadXml(_form.GetAsXML());
				oXMLDoc.Save(_path + _form.UniqueID + ".xml");
				return oXMLDoc;

			} catch (Exception ex) {
				throw ex;
			}

		}




        //public static void ReplaceUIDandLoadToB1(string FileName, string a_uid)
        //{
        //    const string XML_FormUIDPath = "Application/forms/action/form/@uid";
        //    MSXML2.DOMDocument oXmlDoc = default(MSXML2.DOMDocument);
        //    MSXML2.IXMLDOMNode oNode = default(MSXML2.IXMLDOMNode);
        //    try
        //    {
        //        oXmlDoc = new MSXML2.DOMDocument();
        //        //// load the content of the XML File
        //        string sPath = null;
        //        //sPath = IO.Directory.GetParent(Application.StartupPath).ToString
        //        sPath = System.Windows.Forms.Application.StartupPath;
        //        oXmlDoc.load(FileName);
        //        //// Find and Change the UID dymamically
        //        oNode = oXmlDoc.documentElement.selectSingleNode(XML_FormUIDPath);
        //        oXmlDoc.selectSingleNode(XML_FormUIDPath).nodeValue = a_uid.Trim();
        //        //Save the changed xml data to verify the new Form UID
        //        oXmlDoc.save(sPath + "\\" + "xmlChanged.xml");
        //        //Load the xml into de Form
        //        B1Connections.theAppl.LoadBatchActions(oXmlDoc.xml);
        //    }
        //    catch (Exception ex)
        //    {
        //        TNotification.StatusBarError(ex.Message);
        //    }
        //}

        /// <summary>
        /// Loads the xml string defining a form.
        /// </summary>
        /// <param name="_xmlFilePath">Xml file containing the form.</param>
        public static System.Xml.XmlDocument loadXmlForm(string _xmlFilePath)
        {
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();

            if (!System.IO.File.Exists(_xmlFilePath))
            {
                _xmlFilePath = _xmlFilePath.Insert(0, "..\\");
            }

            if (System.IO.File.Exists(_xmlFilePath))
            {
                xmlDoc.Load(_xmlFilePath);
            }
            else
            {
                TNotification.StatusBarError("ERROR: File " + _xmlFilePath + " not found");
                return null;
            }
            B1Connections.theAppl.LoadBatchActions(xmlDoc.InnerXml);
            return xmlDoc;
        }
        /// <summary>
        /// Loads a form into B1 using a xml file and sets the uniqueID
        /// to the fathers UID + the one, you defined in the constructor.
        /// </summary>
        /// <example>The code could look like this:
        /// <code escaped="true">
        ///Dim t_UI As New DevEnhancer.UI.Operations(B1Connections.diCompany, B1Connections.theAppl)
        ///Dim oTestForm As SAPbouiCOM.Form = t_UI.loadForm("TestForm.xml", form)
        ///oTestForm.Visible = True
        /// </code>
        /// </example>
        /// <param name="_xmlFilePath">The path to the xmlfile, containing the form.</param>
        /// <param name="_callingForm">The calling form, required for UID generation.</param>	
        /// <returns>Returns the form.</returns>			
        public static SAPbouiCOM.Form loadForm(string _xmlFilePath, SAPbouiCOM.Form _callingForm)
        {
            string callingFormUID = _callingForm.UniqueID;
            System.Xml.XmlDocument xmlDoc = loadXmlForm(_xmlFilePath);
            string UIDPath = "Application/forms/action/form/@uid";
            string formUID = xmlDoc.SelectSingleNode(UIDPath).Value;
            SAPbouiCOM.Form oForm = default(SAPbouiCOM.Form);

            if (xmlDoc.HasChildNodes)
            {
                // xmlDoc.SelectSingleNode(UIDPath).Value = callingFormUID & _suffixUID
                try
                {
              xmlDoc.SelectSingleNode(UIDPath).Value = GenerateUniqueFormUID(_callingForm.TypeEx);
                string xmlStr = xmlDoc.DocumentElement.OuterXml;
                B1Connections.theAppl.LoadBatchActions(ref xmlStr);
                oForm = B1Connections.theAppl.Forms.ActiveForm;
            
                    UserDataSource oUDS = oForm.DataSources.UserDataSources.Item("FolderDS");
                    if (oUDS != null)
                    {
                        oUDS.Value = "1";
                    }
                }
                catch
                {
                    
                }
                return oForm;
            }
            else
            {
                TNotification.StatusBarError("ERROR: XML File containing the form not found");
                return null;
            }
        }

		public static string GenerateUniqueFormUID(string wantedFormUid)
		{
			bool uidFound = false;
			int identifier = 0;
			while (!uidFound) {
				if (!IsFormOpen(wantedFormUid + identifier, false)) {
					wantedFormUid += Convert.ToString(identifier);
					uidFound = true;
				} else {
					identifier += 1;
				}
			}
			return wantedFormUid;
		}

		public static bool IsFormOpen(string formUid, bool selectIfOpen)
		{
			int i = 0;
			bool found = false;
			Form f = default(Form);
			while (!found && i < B1Connections.theAppl.Forms.Count) {
				f = B1Connections.theAppl.Forms.Item(i);
				if (f.UniqueID == formUid) {
					found = true;
					if (selectIfOpen) {
						if (!f.Selected) {
							B1Connections.theAppl.Forms.Item(formUid).Select();
						}
					}
				} else {
					i += 1;
				}
			}
			if (found) {
				return true;
			} else {
				return false;
			}
		}
        public static SAPbouiCOM.Form LoadForm(string FileName)
        {
            SAPbouiCOM.Form functionReturnValue = default(SAPbouiCOM.Form);
            string sPath = string.Empty;
            sPath = FileName;

            functionReturnValue = null;
            try
            {
                string FormUID = LoadUniqueFormXML(sPath);

                //// Apanhar o formulário carregado, através do seu ID exclusivo
                functionReturnValue = B1Connections.theAppl.Forms.Item(FormUID);
            }
            catch (Exception ex)
            {
                //oApplication.MessageBox("LoadForm(" + FileName + "): " + oCompany.GetLastErrorCode + ", " + ex.Message);
            }
            return functionReturnValue;
        }
        public static string LoadUniqueFormXML(string FileName)
        {
            string functionReturnValue = null;

            System.Xml.XmlDocument xDoc = new System.Xml.XmlDocument();
            functionReturnValue = "";

            try
            {
                xDoc.Load(FileName);
                functionReturnValue = xDoc.SelectSingleNode("Application/forms/action/form/@FormType").Value + "_" + MaximoTipoForm(xDoc.SelectSingleNode("Application/forms/action/form/@FormType").Value).ToString();
                xDoc.SelectSingleNode("Application/forms/action/form/@uid").Value = functionReturnValue;
                B1Connections.theAppl.LoadBatchActions(xDoc.InnerXml);
            }
            catch (Exception ex)
            {
                B1Connections.theAppl.MessageBox(ex.Message);
            }
            return functionReturnValue;
        }
        public static long MaximoTipoForm(string Tipo)
        {
            long functionReturnValue = 0;
            functionReturnValue = 0;

            try
            {
                foreach (SAPbouiCOM.Form iform in B1Connections.theAppl.Forms)
                {
                    if (iform.TypeEx == Tipo)
                    {
                        if (iform.TypeCount > functionReturnValue)
                        {
                            functionReturnValue = iform.TypeCount;
                        }
                    }
                }
                functionReturnValue = functionReturnValue + 1;
            }
            catch (Exception ex)
            {
                B1Connections.theAppl.MessageBox(ex.Message);
            }
            return functionReturnValue;
        }

	}

}
