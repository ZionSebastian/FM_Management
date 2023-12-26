using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using SAPbobsCOM;
using SAPbouiCOM;
////using SAPbouiCOM.Framework;
using System.IO;
using B1WizardBase;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;

namespace SBOHelper.Utils
{

    internal class TReportLayoutsService
    {
        public static void addReportType(string typeName, string addonName, string addonFormType, string menuID)
        {
            //SAPbobsCOM.CompanyService oCompanyService = default(SAPbobsCOM.CompanyService);
            try
            {
                SAPbobsCOM.Company company = new SAPbobsCOM.Company();
              //  company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
                company =B1Connections.diCompany;
                SAPbobsCOM.ReportTypesService rptTypeService = (SAPbobsCOM.ReportTypesService)
                  company.GetCompanyService().GetBusinessService(SAPbobsCOM.ServiceTypes.ReportTypesService);
                SAPbobsCOM.ReportType newType = (SAPbobsCOM.ReportType)
                rptTypeService.GetDataInterface(SAPbobsCOM.ReportTypesServiceDataInterfaces.rtsReportType);
                newType.TypeName = typeName.Trim();
                newType.AddonName = addonName.Trim();
                newType.AddonFormType = addonFormType.Trim();
                newType.MenuID = menuID.Trim();
                SAPbobsCOM.ReportTypeParams newTypeParam = rptTypeService.AddReportType(newType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void addReportLayout(string name, string type)
        {
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
            //company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            company =B1Connections.diCompany;
            SAPbobsCOM.ReportLayoutsService rptService = (SAPbobsCOM.ReportLayoutsService)
              company.GetCompanyService().GetBusinessService(SAPbobsCOM.ServiceTypes.ReportLayoutsService);
            SAPbobsCOM.ReportLayout newReport = (SAPbobsCOM.ReportLayout)
                //rptService.GetDataInterface(SAPbobsCOM.ReportLayoutsServiceDataInterfaces.rlsdiReportLayout);
                  rptService.GetDataInterface(ReportLayoutsServiceDataInterfaces.rlsdiReportLayout);
            newReport.Author = B1Connections.diCompany.UserName;
            newReport.Category = SAPbobsCOM.ReportLayoutCategoryEnum.rlcCrystal;
            newReport.Name = name;
            SAPbobsCOM.ReportTypeParams newTypeParam = null;
            newTypeParam = getReportType(type);
            newReport.TypeCode = newTypeParam.TypeCode; //newTypeParam.TypeCode;
            SAPbobsCOM.ReportLayoutParams newReportParam = rptService.AddReportLayout(newReport);

        }

        public static void setReportType(string typeCode, SAPbobsCOM.ReportLayoutParams newReportParam)
        {
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
            //company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            company =B1Connections.diCompany;
            SAPbobsCOM.ReportTypesService rptTypeService = (SAPbobsCOM.ReportTypesService)
              company.GetCompanyService().GetBusinessService(SAPbobsCOM.ServiceTypes.ReportTypesService);
            SAPbobsCOM.ReportType newType = (SAPbobsCOM.ReportType)
                rptTypeService.GetDataInterface(SAPbobsCOM.ReportTypesServiceDataInterfaces.rtsReportType);

            SAPbobsCOM.ReportTypeParams newTypeParam = null;
            newTypeParam = getReportType(typeCode);

            newType = rptTypeService.GetReportType(newTypeParam);
            newType.DefaultReportLayout = newReportParam.LayoutCode;
            rptTypeService.UpdateReportType(newType);
        }

        public static void setReportType(string typeCode, string type)
        {
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
            //company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
            company =B1Connections.diCompany;
            SAPbobsCOM.ReportTypesService rptTypeService = (SAPbobsCOM.ReportTypesService)
               company.GetCompanyService().GetBusinessService(SAPbobsCOM.ServiceTypes.ReportTypesService);
            SAPbobsCOM.ReportType newType = (SAPbobsCOM.ReportType)
                rptTypeService.GetDataInterface(SAPbobsCOM.ReportTypesServiceDataInterfaces.rtsReportType);

            SAPbobsCOM.ReportTypeParams newTypeParam = null;
            newTypeParam = getReportType(type);

            newType = rptTypeService.GetReportType(newTypeParam);
            newType.DefaultReportLayout = typeCode;// "A0010001";
            rptTypeService.UpdateReportType(newType);
        }

        public static SAPbobsCOM.ReportTypeParams getReportType(string typeName)
        {
            SAPbobsCOM.ReportTypeParams typeParam = null;
            try
            {
                SAPbobsCOM.Company company = new SAPbobsCOM.Company();
                //company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
                company =B1Connections.diCompany;
                SAPbobsCOM.ReportTypesService rptTypeService = (SAPbobsCOM.ReportTypesService)
                 company.GetCompanyService().GetBusinessService(SAPbobsCOM.ServiceTypes.ReportTypesService);

                //SAPbobsCOM.ReportTypeParams newTypeParam = (ReportTypeParams)rptTypeService.GetReportTypeList();

                foreach (SAPbobsCOM.ReportTypeParams newTypeParam in rptTypeService.GetReportTypeList())
                {
                    // TNotification.MessageBox(newTypeParam.TypeCode);
                    if (newTypeParam.AddonFormType.Trim().Equals(typeName.Trim()))
                    {

                        typeParam = newTypeParam;
                        break;
                    }
                }
            }
            catch
            {
            }

            return typeParam;
        }

        //public static SAPbobsCOM.ReportLayoutParams getReportParam(string typeName)
        //{
        //    SAPbobsCOM.ReportLayoutParams typeParam = null;
        //    try
        //    {
        //        SAPbobsCOM.ReportLayoutsService rptService = (SAPbobsCOM.ReportLayoutsService)
        //      B1Connections.diCompany. .Company.GetCompanyService().GetBusinessService(SAPbobsCOM.ServiceTypes.ReportLayoutsService);

        //        //SAPbobsCOM.ReportTypeParams newTypeParam = (ReportTypeParams)rptTypeService.GetReportTypeList();

        //        foreach (SAPbobsCOM.ReportLayoutParams newTypeParam in rptService.GetReportLayoutList())
        //        {
        //            // TNotification.MessageBox(newTypeParam.TypeCode);
        //            if (newTypeParam.AddonFormType.Trim().Equals(typeName.Trim()))
        //            {

        //                typeParam = newTypeParam;
        //                break;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //    }

        //    return typeParam;
        //}

        //public static bool SetDbLogonInfo(ReportDocument _rptDoc, string pwd)
        //{
        //    ConnectionInfo crConnectionInfo = new ConnectionInfo();
        //    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        //    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        //    Tables CrTables = default(Tables);
        //    try
        //    {
        //        var _with1 = crConnectionInfo;
        //        _with1.ServerName = B1Connections.theAppl.Company.ServerName;
        //        _with1.DatabaseName = B1Connections.diCompany.CompanyDB;
        //        _with1.UserID = B1Connections.diCompany.DbUserName;
        //        _with1.Password = pwd;

        //        CrTables = _rptDoc.Database.Tables;
        //        foreach (Table CrTable in CrTables)
        //        {
        //            crtableLogoninfo = CrTable.LogOnInfo;
        //            crtableLogoninfo.ConnectionInfo = crConnectionInfo;
        //            CrTable.ApplyLogOnInfo(crtableLogoninfo);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {



        //    }
        //    return true;
        //}
        //public static bool SetDbLogonInfo(ReportDocument _rptDoc, string pwd, string user)
        //{
        //    ConnectionInfo crConnectionInfo = new ConnectionInfo();
        //    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        //    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        //    Tables CrTables = default(Tables);
        //    try
        //    {
        //        var _with1 = crConnectionInfo;
        //        _with1.ServerName = B1Connections.diCompany.Server;
        //        _with1.DatabaseName = B1Connections.diCompany.CompanyDB;
        //        _with1.UserID = user;
        //        _with1.Password = pwd;

        //        CrTables = _rptDoc.Database.Tables;
        //        foreach (Table CrTable in CrTables)
        //        {
        //            crtableLogoninfo = CrTable.LogOnInfo;
        //            crtableLogoninfo.ConnectionInfo = crConnectionInfo;
        //            CrTable.ApplyLogOnInfo(crtableLogoninfo);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    //finally
        //    //{
        //    //    crConnectionInfo = null;
        //    //    crtableLogoninfos = null;
        //    //    crtableLogoninfo = null;
        //    //}
        //    return true;
        //}
        public static void uploadReport()
        {
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
           // company = (SAPbobsCOM.Company)SAPbouiCOM.Framework.Application.SBO_Application.Company.GetDICompany();
        company =B1Connections.diCompany;
            SAPbobsCOM.BlobParams oBlobParams = (SAPbobsCOM.BlobParams)
        company.GetCompanyService().GetDataInterface(SAPbobsCOM.CompanyServiceDataInterfaces.csdiBlobParams);
            oBlobParams.Table = "RDOC";
            oBlobParams.Field = "Template";
            SAPbobsCOM.BlobTableKeySegment oKeySegment = oBlobParams.BlobTableKeySegments.Add();
            oKeySegment.Name = "DocCode";
            oKeySegment.Value = ""; //newReportParam.LayoutCode;
            FileStream oFile = new FileStream("C:\\CR\\demo.rpt", System.IO.FileMode.Open);
            int fileSize = (int)oFile.Length;
            byte[] buf = new byte[fileSize];
            oFile.Read(buf, 0, fileSize);
            oFile.Dispose();
            SAPbobsCOM.Blob oBlob = (SAPbobsCOM.Blob)
                     company.GetCompanyService().GetDataInterface(SAPbobsCOM.CompanyServiceDataInterfaces.csdiBlob);
            oBlob.Content = Convert.ToBase64String(buf, 0, fileSize);
            company.GetCompanyService().SetBlob(oBlobParams, oBlob);

        }
    }
}

