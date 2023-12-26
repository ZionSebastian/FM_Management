using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;
using SBOHelper.Utils;

namespace FMGeneral
{
    using Class_Files;
    public class Form__140 : B1Form
    {
        public Form__140()
            {
                FormType = "140";//Business Partner Master Data
            }

            [B1Listener(BoEventTypes.et_FORM_LOAD, true)]
            public virtual bool OnBeforeFormLoad(ItemEvent pVal)
            {
                Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
                // ADD YOUR ACTION CODE HERE ...

                try
                {
                SAPbouiCOM.Item oNewItem = default(SAPbouiCOM.Item);
                SAPbouiCOM.ComboBox combobox = default(SAPbouiCOM.ComboBox);
                SAPbouiCOM.Item oItem = default(SAPbouiCOM.Item);
                SAPbouiCOM.StaticText oStaticText = default(SAPbouiCOM.StaticText);
                SAPbouiCOM.EditText oEditText = default(SAPbouiCOM.EditText);
                SAPbouiCOM.Button oButton = default(SAPbouiCOM.Button);
                SAPbouiCOM.ChooseFromList oCFL = default(SAPbouiCOM.ChooseFromList);
                SAPbouiCOM.CheckBox chkReserve = default(SAPbouiCOM.CheckBox);
                var _with_ODLN = form.DataSources.DBDataSources.Item("ODLN");


                    //string strProdCode = clsHIC_ABF.GetLotNumber(_With_PCH.GetValue("DocDate", 0).ToString().Trim());
                    string docnum = _with_ODLN.GetValue("DocNum", 0).ToString().Trim();
                    globalvariables.TransFormType = "140";
                    globalvariables.DeliveryDocNum = docnum;




                TItem.ControlCreator(BoFormItemTypes.it_STATIC, form, "lblEPE", "70", "B");
                oItem = form.Items.Item("lblEPE");
                oItem.Width = form.Items.Item("14").Width - 80;
                oItem.LinkTo = "txtEPE";
                oStaticText = oItem.Specific;
                oStaticText.Caption = "Export Packing";

                TItem.ControlCreator(BoFormItemTypes.it_EDIT, form, "txtEPE", "lblEPE", "R");
                oItem = form.Items.Item("txtEPE");
                oItem.Enabled = true;
                oItem.Top = form.Items.Item("lblEPE").Top;
                oItem.Height = 14;
                oItem.Width = 80;
                }
                catch (Exception ex)
                {
                    TNotification.StatusBarError(ex.Message);
                }

                return true;

            }
            [B1Listener(BoEventTypes.et_FORM_DATA_ADD, true)]
            public virtual bool OnBeforeFormDataAdd(BusinessObjectInfo pVal)
            {
                Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
                // ADD YOUR ACTION CODE HERE ...

                //try
                //{
                //    string s = "Testing";
                //    //    var _with = form.DataSources.DBDataSources.Item("@CCS_OAUT");
                //    //    if (_with.GetValue("U_Type", 0).ToString() == "QA")
                //    //    {
                //    //        SAPbobsCOM.Recordset oRs = (SAPbobsCOM.Recordset)B1Connections.diCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                //    //        string sQryStr = "Update \"@CCS_OQAT\" SET \"U_Picked\"='Y' Where \"DocEntry\"='" + _with.GetValue("U_QATEntry",0).ToString() + "'";
                //    //        oRs.DoQuery(sQryStr);

                //    //    }
                //    //    return true;
                //}
                //catch (Exception ex)
                //{

                //    return false;
                //}
                return true;
            }

        
    }


}
