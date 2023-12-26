

namespace FMGeneral
{
    using SAPbobsCOM;
    using SAPbouiCOM;
    using B1WizardBase;
    using System;
    using SBOHelper.Utils;
    using Class_Files;

    public class Form__42 : B1Form
    {

        string strQuery = String.Empty;

        public Form__42()
        {
            FormType = "42"; //Batch Creation
        }

        /// <summary>
        /// To load user defined fields to system form 
        /// </summary>
        /// <param name="pVal"></param>
        [B1Listener(BoEventTypes.et_FORM_LOAD, false)]
        public virtual void OnAfterFormLoad(ItemEvent pVal)
        {
            bool ActionSuccess = pVal.ActionSuccess;
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            // ADD YOUR ACTION CODE HERE ...


            SAPbouiCOM.Item oNewItem = default(SAPbouiCOM.Item);
            SAPbouiCOM.ComboBox combobox = default(SAPbouiCOM.ComboBox);
            SAPbouiCOM.Item oItem = default(SAPbouiCOM.Item);
            SAPbouiCOM.StaticText oStaticText = default(SAPbouiCOM.StaticText);
            SAPbouiCOM.EditText oEditText = default(SAPbouiCOM.EditText);
            SAPbouiCOM.Button oButton = default(SAPbouiCOM.Button);
            SAPbouiCOM.ChooseFromList oCFL = default(SAPbouiCOM.ChooseFromList);
            SAPbouiCOM.CheckBox chkReserve = default(SAPbouiCOM.CheckBox);


            //SAPbouiCOM.Item oItem = default(SAPbouiCOM.Item);
            //SAPbouiCOM.EditText oEditText = default(SAPbouiCOM.EditText);
            Matrix oMatrix = ((Matrix)(form.Items.Item("4").Specific));
            Matrix oMatrix1 = ((Matrix)(form.Items.Item("3").Specific));
            Matrix oMatrix2 = ((Matrix)(form.Items.Item("5").Specific));
            //SAPbouiCOM.Button oButton = default(SAPbouiCOM.Button);

            try
            {

                SAPbouiCOM.Column oColumn;
                oColumn = oMatrix.Columns.Item("0");
                oEditText = (SAPbouiCOM.EditText)oColumn.Cells.Item(1).Specific;
                if (pVal.ActionSuccess == true)
                {
                    //ZN test
                    string EPEntry= globalvariables.EPLEntry;
                    string EPCardCode = globalvariables.TransFormType;
                    if (EPEntry != "" && EPCardCode=="140")
                    {
                        string strFormTyp = globalvariables.TransFormType;
                        string sampleFormTyp = globalvariables.RefFormType;
                        //if ((strFormTyp != "" && strFormTyp != null) || (sampleFormTyp != "" && sampleFormTyp != null))
                        //{

                        //
                        //string strDocNo = oEditText.Value;
                        //if (strDocNo.Contains("PD") || strDocNo.Contains("SI"))
                        //{
                        oItem = form.Items.Add("btnBatch", SAPbouiCOM.BoFormItemTypes.it_BUTTON);
                        oItem.Left = form.Items.Item("2").Left + form.Items.Item("2").Width + 5;
                        oItem.Top = form.Items.Item("2").Top;
                        oItem.Width = form.Items.Item("2").Width;
                        oItem.Height = form.Items.Item("2").Height;
                        oButton = (SAPbouiCOM.Button)oItem.Specific;
                        oButton.Caption = "Batch";

                        //TItem.ControlCreator(BoFormItemTypes.it_STATIC, form, "lblEPE", "2", "R");
                        //        oItem = form.Items.Item("lblEPE");
                        //        oItem.Width = form.Items.Item("10000052").Width-70;
                        //        oItem.LinkTo = "txtEPE";
                        //        oStaticText = oItem.Specific;
                        //        oStaticText.Caption = "Export Packing";

                        //        TItem.ControlCreator(BoFormItemTypes.it_EDIT, form, "txtEPE", "lblEPE", "R");
                        //        oItem = form.Items.Item("txtEPE");
                        //        oItem.Enabled = true;
                        //        oItem.Top = form.Items.Item("lblEPE").Top+3;
                        //        oItem.Height = 14;
                        //        oItem.Width = 80;
                        //        oItem.LinkTo = "txtEPE";

                        //oMatrix1.Columns.Item("5").Cells.Item(1).Click();
                        //TItem.Enable(form, "36", false);

                        form.Items.Item("btnBatch").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                        //}
                        //else
                        //{
                        //    TItem.Enable(form, "36", true);
                        //    TMatrix.EnableColumns(oMatrix1, "2", true);
                        //}



                        //if (form.Mode == BoFormMode.fm_UPDATE_MODE)
                        //{
                        //    form.Items.Item("1").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                        //    form.Items.Item("1").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                        //}
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                //TNotification.StatusBarError(ex.Message);
            }
        }

    }
}
