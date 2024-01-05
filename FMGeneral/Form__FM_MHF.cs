using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;
using System;
using SBOHelper.Utils;

namespace FMGeneral
{
    public class Form__FM_MHF : B1Form
    {
        public Form__FM_MHF()
        {
            FormType = "FM_MHF";
        }


        [B1Listener(BoEventTypes.et_FORM_DATA_LOAD, false)]
        public virtual void OnAfterFormDataLoad(BusinessObjectInfo pVal)
        {
            bool ActionSuccess = pVal.ActionSuccess;
            Form form = B1Connections.theAppl.Forms.Item(pVal.FormUID);
            Matrix oMatrix;
            ComboBox oComboBox;
            // ADD YOUR ACTION CODE HERE ...
            try
            {
                var _with = form.DataSources.DBDataSources.Item("@FM_OMHF");
                
                    if (form.Mode == BoFormMode.fm_OK_MODE)
                    {
                        //string test = _with.GetValue("U_APInvEntry", 0).ToString().Trim();
                        //if ( _with.GetValue("U_APInvEntry", 0).ToString().Trim() != "")
                        //{
                        //    form.Items.Item("btnInvGen").Enabled = false;
                        //}
                        //else
                        //{
                        //    form.Items.Item("btnInvGen").Enabled = true;
                        //}

                        string signeduser = TUser.UserCode;
                        string InvAccess = TSQL.GetSingleRecord("select U_MHFAccess from OUSR WHERE USER_CODE='" + signeduser + "'").ToString().Trim();
                        if (InvAccess == "Y")
                        {
                            form.Items.Item("11_U_Cb").Enabled = true;
                            form.Items.Item("txtWTCode").Enabled = true;

                            string test = _with.GetValue("U_APInvEntry", 0).ToString().Trim();
                            if (_with.GetValue("U_APInvEntry", 0).ToString().Trim() != "")
                            {
                                form.Items.Item("btnInvGen").Enabled = false;
                            }
                            else
                            {
                                form.Items.Item("btnInvGen").Enabled = true;
                            }
                        if (_with.GetValue("Status", 0).ToString().Trim() == "C")
                        {
                            form.Items.Item("0_U_G").Enabled = false;
                            form.Items.Item("txtSpCode").Enabled = false;
                            form.Items.Item("22_U_E").Enabled = false;
                            form.Items.Item("23_U_E").Enabled = false;
                            form.Items.Item("24_U_E").Enabled = false;
                            form.Items.Item("25_U_E").Enabled = false;
                            form.Items.Item("txtMtrcla").Enabled = false;
                            form.Items.Item("txtVchle").Enabled = false;
                            form.Items.Item("txtWTCode").Enabled = false;
                            form.Items.Item("cmbMonth").Enabled = false;
                            form.Items.Item("Item_7").Enabled = false;
                            form.Items.Item("Item_0").Enabled = false;
                            form.Items.Item("Item_21").Enabled = false;
                        }
                        else
                        {
                            form.Items.Item("0_U_G").Enabled = true;
                            form.Items.Item("txtSpCode").Enabled = true;
                            form.Items.Item("22_U_E").Enabled = true;
                            form.Items.Item("23_U_E").Enabled = true;
                            form.Items.Item("24_U_E").Enabled = true;
                            form.Items.Item("25_U_E").Enabled = true;
                            form.Items.Item("txtMtrcla").Enabled = true;
                            form.Items.Item("txtVchle").Enabled = true;
                            form.Items.Item("txtWTCode").Enabled = true;
                            form.Items.Item("cmbMonth").Enabled = true;
                            form.Items.Item("Item_7").Enabled = true;
                            form.Items.Item("Item_0").Enabled = true;
                            form.Items.Item("Item_21").Enabled = true;
                        }

                    }
                        else
                        {
                            if (_with.GetValue("Status", 0).ToString().Trim() == "C")
                            {
                                form.Items.Item("0_U_G").Enabled = false;
                                form.Items.Item("txtSpCode").Enabled = false;
                                form.Items.Item("22_U_E").Enabled = false;
                                form.Items.Item("23_U_E").Enabled = false;
                                form.Items.Item("24_U_E").Enabled = false;
                                form.Items.Item("25_U_E").Enabled = false;
                                form.Items.Item("txtMtrcla").Enabled = false;
                                form.Items.Item("txtVchle").Enabled = false;
                                form.Items.Item("11_U_Cb").Enabled = false;
                                form.Items.Item("cmbMonth").Enabled = false;
                                form.Items.Item("Item_7").Enabled = false;
                                form.Items.Item("Item_0").Enabled = false;
                                form.Items.Item("Item_21").Enabled = false;
                            }
                            
                            form.Items.Item("btnInvGen").Enabled = false;
                            form.Items.Item("11_U_Cb").Enabled = false;
                            form.Items.Item("txtWTCode").Enabled = false;
                        }
                    }
                    else
                    {
                        form.Items.Item("btnInvGen").Enabled = false;

                    }

                


            }
            catch (Exception ex)
            {
                TNotification.StatusBarError(ex.Message);
            }
        }
    }
}
