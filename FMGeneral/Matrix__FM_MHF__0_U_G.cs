

namespace FMGeneral
{
    using SAPbobsCOM;
    using SAPbouiCOM;
    using B1WizardBase;
    using System;
    using SBOHelper.Utils;
    using System.Globalization;


    class Matrix__FM_MHF__0_U_G : B1Item
    {
        public Matrix__FM_MHF__0_U_G()
        {
            FormType = "FM_MHF";
            ItemUID = "0_U_G";
        }



        [B1Listener(BoEventTypes.et_RIGHT_CLICK, true)]
        public virtual bool OnBeforeRightClick(ContextMenuInfo pVal)
        {
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("0_U_G");
            Matrix matrix = ((Matrix)(item.Specific));
            // ADD YOUR ACTION CODE HERE ...
            SAPbouiCOM.Form oForm = default(SAPbouiCOM.Form);

            try
            {
                oForm = B1Connections.theAppl.Forms.ActiveForm;
                switch (pVal.ColUID)
                {
                    case "#":
                        oForm.EnableMenu("1292", true);
                        oForm.EnableMenu("1293", true);
                        
                        break;
                    default:
                        oForm.EnableMenu("1292", true);
                        oForm.EnableMenu("1293", true);
                        break;
                }

            }
            catch (Exception ex)
            {
                TNotification.StatusBarError(ex.Message);
            }
            finally
            {
                form.Freeze(false);
            }
            return true;
        }

        [B1Listener(BoEventTypes.et_COMBO_SELECT, true)]
        public virtual bool OnBeforeComboSelect(ItemEvent pVal)
        {
            bool ActionSuccess = pVal.ActionSuccess;
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("0_U_G");
            Matrix matrix = ((Matrix)(item.Specific));
            
            var _with = form.DataSources.DBDataSources.Item("@FM_OMHF");
            if (_with.GetValue("Status", 0) == "O")
            {
                try
                {
                    form.Freeze(true);
                    matrix.FlushToDataSource();
                    switch (pVal.ColUID)
                    {
                        case "C_0_18":

                            break;

                    }
                    matrix.LoadFromDataSourceEx(false);
                    matrix.AutoResizeColumns();
                }
                catch (Exception ex)
                {
                    form.Freeze(false);
                    TNotification.StatusBarError(ex.Message);
                }
                finally
                {
                    form.Freeze(false);
                }
            }

            return true;
        }





        [B1Listener(BoEventTypes.et_CHOOSE_FROM_LIST, true)]
        public virtual bool OnBeforeChooseFromList(ItemEvent pVal)
        {
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("0_U_G");
            Matrix matrix = ((Matrix)(item.Specific));
            // ADD YOUR ACTION CODE HERE ...
            try
            {
                //SAPbouiCOM.Conditions Conds = default(SAPbouiCOM.Conditions);
                //var _With_OCOM = form.DataSources.DBDataSources.Item("@CCS_OCOM");
                //var _With_COM1 = form.DataSources.DBDataSources.Item("@CCS_COM1");
                var _With_OMHF = form.DataSources.DBDataSources.Item("@FM_OMHF");
                var _With_MHF1 = form.DataSources.DBDataSources.Item("@FM_MHF1");
                SAPbouiCOM.Conditions oConditions = null;

                string Dimension1 = _With_MHF1.GetValue("U_Dimnsn", pVal.Row - 1).ToString().Trim();
                switch (pVal.ColUID)
                {
                    case "Col_5":
                        oConditions = TConditions.Create("Inactive", "N", BoConditionOperation.co_EQUAL);
                        TChooseFromList.SetCondition(pVal, form, oConditions);
                        break;
                    case "C_0_16":
                        string Dimension = _With_MHF1.GetValue("U_Dimnsn", pVal.Row-1 ).ToString().Trim();

                        
                        if (Dimension == "1")
                        {

                            oConditions = TConditions.Create("DimCode", "1", BoConditionOperation.co_EQUAL);
                            TChooseFromList.SetCondition(pVal, form, oConditions);

                        }
                        else if (Dimension == "3")
                        {

                            oConditions = TConditions.Create("DimCode", "3", BoConditionOperation.co_EQUAL);
                            TChooseFromList.SetCondition(pVal, form, oConditions);


                        }
                        else if (Dimension == "4")
                        {

                            oConditions = TConditions.Create("DimCode", "4", BoConditionOperation.co_EQUAL);
                            TChooseFromList.SetCondition(pVal, form, oConditions);
                        }
                        break;

                    case "Col_3":
                        

                          
                        
                        break;



                }



            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
            finally
            { form.Freeze(false); }
            return true;
        }


        [B1Listener(BoEventTypes.et_COMBO_SELECT, false)]
        public virtual void OnAfterComboSelect(ItemEvent pVal)
        {
            bool ActionSuccess = pVal.ActionSuccess;
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("0_U_G");
            Matrix matrix = ((Matrix)(item.Specific));

            //ADD YOUR ACTION CODE HERE ...

            try
            {
                form.Freeze(true);

                var _With_OMHF = form.DataSources.DBDataSources.Item("@FM_OMHF");
                var _With_MHF1 = form.DataSources.DBDataSources.Item("@FM_MHF1");
                Column oColumn;
                matrix.FlushToDataSource();
                switch (pVal.ColUID)
                {
                    case "C_0_18":
                        string Dimension = _With_MHF1.GetValue("U_Dimnsn", pVal.Row - 1).ToString().Trim();

                        break;
                }
                matrix.LoadFromDataSourceEx(false);
                matrix.AutoResizeColumns();
                // }
                form.Freeze(false);

            }
            catch (Exception ex)
            {
                form.Freeze(false);
                B1Connections.theAppl.MessageBox(ex.Message);
            }

        }


        /// <summary>
        /// For selecting Item,supervisor,warehouse from CFL
        /// </summary>
        /// <param name="pVal"></param>
        [B1Listener(BoEventTypes.et_CHOOSE_FROM_LIST, false)]
        public virtual void OnAfterChooseFromList(ItemEvent pVal)
        {
            bool ActionSuccess = pVal.ActionSuccess;
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("0_U_G");
            Matrix matrix = ((Matrix)(item.Specific));
            // ADD YOUR ACTION CODE HERE ...
            try
            {
                if (pVal.ActionSuccess)
                {
                    SAPbouiCOM.DataTable dataTableCFL = null;
                    dataTableCFL = TChooseFromList.GetValue(pVal, form);
                    if (dataTableCFL != null)
                    {
                        form.Freeze(true);
                        var _with_FM_MHF1 = form.DataSources.DBDataSources.Item("@FM_MHF1");
                        _with_FM_MHF1.Clear();
                        matrix.FlushToDataSource();

                        switch (pVal.ColUID)
                        {
                            case "Col_5":
                                _with_FM_MHF1.SetValue("U_VATCode", pVal.Row - 1, dataTableCFL.GetValue("Code", 0).ToString().Trim());
                                matrix.LoadFromDataSourceEx();
                                break;
                            case "C_0_16":

                                _with_FM_MHF1.SetValue("U_CostCntr", pVal.Row - 1, dataTableCFL.GetValue("OcrName", 0).ToString().Trim());
                                _with_FM_MHF1.SetValue("U_CstCrCode", pVal.Row - 1, dataTableCFL.GetValue("OcrCode", 0).ToString().Trim());
                                matrix.LoadFromDataSourceEx();
                                break;

                            case "C_0_15":

                                _with_FM_MHF1.SetValue("U_GLAcct", pVal.Row - 1, dataTableCFL.GetValue("AcctCode", 0).ToString().Trim());
                               // _with_FM_MHF1.SetValue("U_CstCrCode", pVal.Row - 1, dataTableCFL.GetValue("AcctName", 0).ToString().Trim());
                                matrix.LoadFromDataSourceEx();
                                break;
                            case "Col_3":

                                _with_FM_MHF1.SetValue("U_Departmnt", pVal.Row - 1, dataTableCFL.GetValue("OcrName", 0).ToString().Trim());
                                _with_FM_MHF1.SetValue("U_DprtCode", pVal.Row - 1, dataTableCFL.GetValue("OcrCode", 0).ToString().Trim());
                                matrix.LoadFromDataSourceEx();
                                break;
                        }

                        matrix.AutoResizeColumns();

                        if (form.Mode == BoFormMode.fm_OK_MODE)
                        {
                            form.Mode = BoFormMode.fm_UPDATE_MODE;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                form.Freeze(false);
                TNotification.StatusBarError(ex.Message);
            }
            finally
            {
                form.Freeze(false);

            }

        }



        [B1Listener(BoEventTypes.et_VALIDATE, false)]
        public virtual void OnAfterValidate(ItemEvent pVal)
        {
            bool ActionSuccess = pVal.ActionSuccess;
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Item item = form.Items.Item("0_U_G");
            Matrix matrix = ((Matrix)(item.Specific));
            // ADD YOUR ACTION CODE HERE ...
            String query = string.Empty;
            double value = 0;
            

            string timeString1 = "";
            string timeString2 = "";

            TimeSpan timeDifference1 = new TimeSpan();
            TimeSpan timeDifference2 = new TimeSpan();
            TimeSpan TimeVal = new TimeSpan();

            CheckBox checkbox;
            ComboBox cmbType;
            try
            {
                if (pVal.ActionSuccess && pVal.InnerEvent == false)
                //if ( pVal.InnerEvent == false)
                {
                    
                    var _With_OMHF = form.DataSources.DBDataSources.Item("@FM_OMHF");
                    var _With_MHF1 = form.DataSources.DBDataSources.Item("@FM_MHF1");
                    form.Freeze(true);
                    switch (pVal.ColUID)
                    {
                       
                        case "C_0_17":
                            
                            if(Convert.ToDouble(matrix.Columns.Item("C_0_14").Cells.Item(pVal.Row).Specific.Value) > 0)
                            {
                                matrix.FlushToDataSource();
                                int lastrow = matrix.VisualRowCount;
                                string LastItem = _With_MHF1.GetValue("U_Remark", lastrow - 1).ToString().Trim();
                                if (LastItem != "")
                                {
                                    TMatrix.addRow(form, "0_U_G", "#", "@FM_MHF1");
                                    TMatrix.RefreshRowNo(form, "0_U_G", "#");
                                }
                            }
                            break;
                        
                        case "C_0_6":  case "C_0_7":

                            TMatrix.RefreshRowNo(form, "0_U_G", "#");
                            //matrix.Columns.Item("#").Cells.Item(pVal.Row).Click();
                            //TNotification.MessageBox("FGFGHGJJKLL");
                            //TNotification.StatusBarNoTyped("");
                            //matrix.SelectRow(pVal.Row,true,true);
                            //if (pVal.ColUID == "C_0_6")
                            //{
                            //    matrix.Columns.Item("C_0_6").Cells.Item(pVal.Row).Click();
                            //}
                            //else
                            //{
                            //    matrix.Columns.Item("C_0_7").Cells.Item(pVal.Row).Click();
                            //}
                            // Calculate the time difference
                            if (matrix.Columns.Item("C_0_6").Cells.Item(pVal.Row).Specific.Value != ""&& matrix.Columns.Item("C_0_7").Cells.Item(pVal.Row).Specific.Value != "")
                            {
                                timeString1 = matrix.Columns.Item("C_0_6").Cells.Item(pVal.Row).Specific.Value;
                                timeString2 = matrix.Columns.Item("C_0_7").Cells.Item(pVal.Row).Specific.Value;

                                
                                DateTime time1 = DateTime.ParseExact(timeString1, "HHmm", null);
                                DateTime time2 = DateTime.ParseExact(timeString2, "HHmm", null);

                                timeDifference1 = time2 - time1;

                                if (timeDifference1.TotalHours < 0)
                                {
                                    timeDifference1 = timeDifference1 + TimeSpan.FromDays(1);
                                }

                                if (matrix.Columns.Item("C_0_8").Cells.Item(pVal.Row).Specific.Value != "" && matrix.Columns.Item("C_0_9").Cells.Item(pVal.Row).Specific.Value != "")
                                {
                                    timeString1 = matrix.Columns.Item("C_0_8").Cells.Item(pVal.Row).Specific.Value;
                                    timeString2 = matrix.Columns.Item("C_0_9").Cells.Item(pVal.Row).Specific.Value;
                                     time1 = DateTime.ParseExact(timeString1, "HHmm", null);
                                     time2 = DateTime.ParseExact(timeString2, "HHmm", null);

                                     timeDifference2 = time2 - time1;
                                }
                                TimeVal = timeDifference1 + timeDifference2;
                                string test = TimeVal.ToString();
                                double decimalTime = TimeVal.Hours + (TimeVal.Minutes / 60.0);

                                // Display the decimal representation

                                matrix.Columns.Item("C_0_10").Cells.Item(pVal.Row).Specific.Value = decimalTime.ToString("0.00");
                                matrix.Columns.Item("C_0_12").Cells.Item(pVal.Row).Specific.Value = decimalTime.ToString("0.00");

                                //cost calculation


                                double PricePerHr = Convert.ToDouble(matrix.Columns.Item("Col_1").Cells.Item(pVal.Row).Specific.Value);

                                if (PricePerHr>0)
                                {

                                
                                double Totalworktime = Convert.ToDouble(matrix.Columns.Item("C_0_12").Cells.Item(pVal.Row).Specific.Value);
                                double Totalextratime = Convert.ToDouble(matrix.Columns.Item("C_0_13").Cells.Item(pVal.Row).Specific.Value);
                                double PricePerExtraHr = Convert.ToDouble(matrix.Columns.Item("Col_2").Cells.Item(pVal.Row).Specific.Value);


                                    double TotalCost = (Totalworktime * PricePerHr) + (Totalextratime * PricePerExtraHr);


                                matrix.Columns.Item("C_0_14").Cells.Item(pVal.Row).Specific.Value = TotalCost.ToString("0.00");
                                }
                                else
                                {
                                    double Totalextratime = Convert.ToDouble(matrix.Columns.Item("C_0_13").Cells.Item(pVal.Row).Specific.Value);
                                    double PricePerExtraHr = Convert.ToDouble(matrix.Columns.Item("Col_2").Cells.Item(pVal.Row).Specific.Value);
                                    double PricePershift = Convert.ToDouble(matrix.Columns.Item("Col_0").Cells.Item(pVal.Row).Specific.Value);
                                    double TotalCost = PricePershift + (Totalextratime * PricePerExtraHr);
                                    matrix.Columns.Item("C_0_14").Cells.Item(pVal.Row).Specific.Value = TotalCost.ToString("0.00");
                                }
                            }
                            
                            break;
                        
                        case "C_0_8": case "C_0_9":
                            //matrix.Columns.Item("C_0_8").Cells.Item(pVal.Row).Click();
                            TMatrix.RefreshRowNo(form, "0_U_G", "#");
                            //if (pVal.ColUID == "C_0_8")
                            //{
                            //    matrix.Columns.Item("C_0_8").Cells.Item(pVal.Row).Click();
                            //}
                            //else
                            //{
                            //    matrix.Columns.Item("C_0_9").Cells.Item(pVal.Row).Click();
                            //}
                            if (matrix.Columns.Item("C_0_9").Cells.Item(pVal.Row).Specific.Value != "" && matrix.Columns.Item("C_0_8").Cells.Item(pVal.Row).Specific.Value != "")
                            {
                                timeString1 = matrix.Columns.Item("C_0_8").Cells.Item(pVal.Row).Specific.Value;
                                timeString2 = matrix.Columns.Item("C_0_9").Cells.Item(pVal.Row).Specific.Value;
                                DateTime time1 = DateTime.ParseExact(timeString1, "HHmm", null);
                                DateTime time2 = DateTime.ParseExact(timeString2, "HHmm", null);

                                timeDifference1 = time2 - time1;

                                if (timeDifference1.TotalHours < 0)
                                {
                                    timeDifference1 = timeDifference1 + TimeSpan.FromDays(1);
                                }
                                if (matrix.Columns.Item("C_0_6").Cells.Item(pVal.Row).Specific.Value != "" && matrix.Columns.Item("C_0_7").Cells.Item(pVal.Row).Specific.Value != "")
                                {
                                    timeString1 = matrix.Columns.Item("C_0_6").Cells.Item(pVal.Row).Specific.Value;
                                    timeString2 = matrix.Columns.Item("C_0_7").Cells.Item(pVal.Row).Specific.Value;
                                    time1 = DateTime.ParseExact(timeString1, "HHmm", null);
                                    time2 = DateTime.ParseExact(timeString2, "HHmm", null);

                                    timeDifference2 = time2 - time1;
                                }
                                TimeVal = timeDifference1 + timeDifference2;
                                string test = TimeVal.ToString();

                                double decimalTime = TimeVal.Hours + (TimeVal.Minutes / 60.0);

                                matrix.Columns.Item("C_0_10").Cells.Item(pVal.Row).Specific.Value = decimalTime.ToString("0.00");
                                matrix.Columns.Item("C_0_12").Cells.Item(pVal.Row).Specific.Value = decimalTime.ToString("0.00");

                                
                                double PricePerHr = Convert.ToDouble(matrix.Columns.Item("Col_1").Cells.Item(pVal.Row).Specific.Value);
                                if (PricePerHr > 0)
                                {


                                    double Totalworktime = Convert.ToDouble(matrix.Columns.Item("C_0_12").Cells.Item(pVal.Row).Specific.Value);
                                    double Totalextratime = Convert.ToDouble(matrix.Columns.Item("C_0_13").Cells.Item(pVal.Row).Specific.Value);
                                    //double PricePerExtraHr = Convert.ToDouble(_With_OMHF.GetValue("U_ExTimePrc", 0).ToString().Trim());
                                    double PricePerExtraHr = Convert.ToDouble(matrix.Columns.Item("Col_2").Cells.Item(pVal.Row).Specific.Value);


                                    double TotalCost = (Totalworktime * PricePerHr) + (Totalextratime * PricePerExtraHr);


                                    matrix.Columns.Item("C_0_14").Cells.Item(pVal.Row).Specific.Value = TotalCost.ToString("0.00");
                                }
                                else
                                {
                                    double Totalextratime = Convert.ToDouble(matrix.Columns.Item("C_0_13").Cells.Item(pVal.Row).Specific.Value);
                                    //double PricePerExtraHr = Convert.ToDouble(_With_OMHF.GetValue("U_ExTimePrc", 0).ToString().Trim());
                                    double PricePerExtraHr = Convert.ToDouble(matrix.Columns.Item("Col_2").Cells.Item(pVal.Row).Specific.Value);
                                    //double PricePershift = Convert.ToDouble(_With_OMHF.GetValue("U_PrcPrShft", 0).ToString().Trim());
                                    double PricePershift = Convert.ToDouble(matrix.Columns.Item("Col_0").Cells.Item(pVal.Row).Specific.Value);
                                    double TotalCost = PricePershift + (Totalextratime * PricePerExtraHr);
                                    matrix.Columns.Item("C_0_14").Cells.Item(pVal.Row).Specific.Value = TotalCost.ToString("0.00");
                                }
                            }
                            break;
                        
                        case "C_0_11":
                            TMatrix.RefreshRowNo(form, "0_U_G", "#");
                            if (matrix.Columns.Item("C_0_10").Cells.Item(pVal.Row).Specific.Value != "")
                            {
                                double worktime = Convert.ToDouble(matrix.Columns.Item("C_0_10").Cells.Item(pVal.Row).Specific.Value);
                                double breaktime = Convert.ToDouble(matrix.Columns.Item("C_0_11").Cells.Item(pVal.Row).Specific.Value);
                                

                                double timeDiff = worktime - breaktime;
                                

                                matrix.Columns.Item("C_0_12").Cells.Item(pVal.Row).Specific.Value = timeDiff.ToString("0.00");

                              
                                double PricePerHr = Convert.ToDouble(matrix.Columns.Item("Col_1").Cells.Item(pVal.Row).Specific.Value);
                                if (PricePerHr > 0)
                                {


                                    double Totalworktime = Convert.ToDouble(matrix.Columns.Item("C_0_12").Cells.Item(pVal.Row).Specific.Value);
                                    double Totalextratime = Convert.ToDouble(matrix.Columns.Item("C_0_13").Cells.Item(pVal.Row).Specific.Value);
                                    //double PricePerExtraHr = Convert.ToDouble(_With_OMHF.GetValue("U_ExTimePrc", 0).ToString().Trim());
                                    double PricePerExtraHr = Convert.ToDouble(matrix.Columns.Item("Col_2").Cells.Item(pVal.Row).Specific.Value);


                                    double TotalCost = (Totalworktime * PricePerHr) + (Totalextratime * PricePerExtraHr);


                                    matrix.Columns.Item("C_0_14").Cells.Item(pVal.Row).Specific.Value = TotalCost.ToString("0.00");
                                }
                                else
                                {
                                    double Totalextratime = Convert.ToDouble(matrix.Columns.Item("C_0_13").Cells.Item(pVal.Row).Specific.Value);
                                    //double PricePerExtraHr = Convert.ToDouble(_With_OMHF.GetValue("U_ExTimePrc", 0).ToString().Trim());
                                    double PricePerExtraHr = Convert.ToDouble(matrix.Columns.Item("Col_2").Cells.Item(pVal.Row).Specific.Value);
                                    //double PricePershift = Convert.ToDouble(_With_OMHF.GetValue("U_PrcPrShft", 0).ToString().Trim());
                                    double PricePershift = Convert.ToDouble(matrix.Columns.Item("Col_0").Cells.Item(pVal.Row).Specific.Value);
                                    double TotalCost = PricePershift + (Totalextratime * PricePerExtraHr);
                                    matrix.Columns.Item("C_0_14").Cells.Item(pVal.Row).Specific.Value = TotalCost.ToString("0.00");
                                }



                                //
                            }
                            if (matrix.Columns.Item("C_0_10").Cells.Item(pVal.Row).Specific.Value == "" || Convert.ToDouble(matrix.Columns.Item("C_0_10").Cells.Item(pVal.Row).Specific.Value) == '0')
                            {
                               matrix.Columns.Item("C_0_12").Cells.Item(pVal.Row).Specific.Value = Convert.ToString(-1*Convert.ToDouble(matrix.Columns.Item("C_0_11").Cells.Item(pVal.Row).Specific.Value));
                            }
                            break;
                        case "C_0_10":
                            if (matrix.Columns.Item("C_0_11").Cells.Item(pVal.Row).Specific.Value != "")
                            {
                                double worktime = Convert.ToDouble(matrix.Columns.Item("C_0_10").Cells.Item(pVal.Row).Specific.Value);
                                double breaktime = Convert.ToDouble(matrix.Columns.Item("C_0_11").Cells.Item(pVal.Row).Specific.Value);


                                double timeDiff = worktime - breaktime;


                                matrix.Columns.Item("C_0_12").Cells.Item(pVal.Row).Specific.Value = timeDiff.ToString("0.00");
                            }
                            if (matrix.Columns.Item("C_0_11").Cells.Item(pVal.Row).Specific.Value == "" || Convert.ToDouble(matrix.Columns.Item("C_0_11").Cells.Item(pVal.Row).Specific.Value) == '0')
                            {
                                matrix.Columns.Item("C_0_12").Cells.Item(pVal.Row).Specific.Value = matrix.Columns.Item("C_0_10").Cells.Item(pVal.Row).Specific.Value;
                            }
                            break;
                        case "C_0_12": case "C_0_13":
                            TMatrix.RefreshRowNo(form, "0_U_G", "#");
                            if (matrix.Columns.Item("C_0_12").Cells.Item(pVal.Row).Specific.Value != "")
                            {


                                //cost calculation

                                //double PricePerHr = Convert.ToDouble(_With_OMHF.GetValue("U_PrcPrHr", 0).ToString().Trim());
                                double PricePerHr = Convert.ToDouble(matrix.Columns.Item("Col_1").Cells.Item(pVal.Row).Specific.Value);
                                if (PricePerHr > 0)
                                {


                                    double Totalworktime = Convert.ToDouble(matrix.Columns.Item("C_0_12").Cells.Item(pVal.Row).Specific.Value);
                                    double Totalextratime = Convert.ToDouble(matrix.Columns.Item("C_0_13").Cells.Item(pVal.Row).Specific.Value);
                                    //double PricePerExtraHr = Convert.ToDouble(_With_OMHF.GetValue("U_ExTimePrc", 0).ToString().Trim());
                                    double PricePerExtraHr = Convert.ToDouble(matrix.Columns.Item("Col_2").Cells.Item(pVal.Row).Specific.Value);

                                    double TotalCost = (Totalworktime * PricePerHr) + (Totalextratime * PricePerExtraHr);


                                    matrix.Columns.Item("C_0_14").Cells.Item(pVal.Row).Specific.Value = TotalCost.ToString("0.00");
                                }
                                else
                                {
                                    double Totalextratime = Convert.ToDouble(matrix.Columns.Item("C_0_13").Cells.Item(pVal.Row).Specific.Value);
                                    //double PricePerExtraHr = Convert.ToDouble(_With_OMHF.GetValue("U_ExTimePrc", 0).ToString().Trim());
                                    double PricePerExtraHr = Convert.ToDouble(matrix.Columns.Item("Col_2").Cells.Item(pVal.Row).Specific.Value);
                                    //double PricePershift = Convert.ToDouble(_With_OMHF.GetValue("U_PrcPrShft", 0).ToString().Trim());
                                    double PricePershift = Convert.ToDouble(matrix.Columns.Item("Col_0").Cells.Item(pVal.Row).Specific.Value);
                                    double TotalCost = PricePershift + (Totalextratime * PricePerExtraHr);
                                    matrix.Columns.Item("C_0_14").Cells.Item(pVal.Row).Specific.Value = TotalCost.ToString("0.00");
                                }
                            }
                            
                            break;
                        
                        case "Col_0": case "Col_1": case "Col_2":
                            double PricePerHr1 = Convert.ToDouble(matrix.Columns.Item("Col_1").Cells.Item(pVal.Row).Specific.Value);
                            if (PricePerHr1 > 0)
                            {


                                double Totalworktime = Convert.ToDouble(matrix.Columns.Item("C_0_12").Cells.Item(pVal.Row).Specific.Value);
                                double Totalextratime = Convert.ToDouble(matrix.Columns.Item("C_0_13").Cells.Item(pVal.Row).Specific.Value);
                                //double PricePerExtraHr = Convert.ToDouble(_With_OMHF.GetValue("U_ExTimePrc", 0).ToString().Trim());
                                double PricePerExtraHr = Convert.ToDouble(matrix.Columns.Item("Col_2").Cells.Item(pVal.Row).Specific.Value);

                                double TotalCost = (Totalworktime * PricePerHr1) + (Totalextratime * PricePerExtraHr);


                                matrix.Columns.Item("C_0_14").Cells.Item(pVal.Row).Specific.Value = TotalCost.ToString("0.00");
                            }
                            else
                            {
                                double Totalextratime = Convert.ToDouble(matrix.Columns.Item("C_0_13").Cells.Item(pVal.Row).Specific.Value);
                                //double PricePerExtraHr = Convert.ToDouble(_With_OMHF.GetValue("U_ExTimePrc", 0).ToString().Trim());
                                double PricePerExtraHr = Convert.ToDouble(matrix.Columns.Item("Col_2").Cells.Item(pVal.Row).Specific.Value);
                                //double PricePershift = Convert.ToDouble(_With_OMHF.GetValue("U_PrcPrShft", 0).ToString().Trim());
                                double PricePershift = Convert.ToDouble(matrix.Columns.Item("Col_0").Cells.Item(pVal.Row).Specific.Value);
                                double TotalCost = PricePershift + (Totalextratime * PricePerExtraHr);
                                matrix.Columns.Item("C_0_14").Cells.Item(pVal.Row).Specific.Value = TotalCost.ToString("0.00");
                            }
                            break;
                    }

                    form.Freeze(false);
                }

            }
            catch (Exception ex)
            {
                form.Freeze(false);
                TNotification.StatusBarError(ex.Message);
            }
            finally
            {
                form.Freeze(false);
            }
        }
    }

}

