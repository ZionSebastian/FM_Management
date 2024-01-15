using SAPbobsCOM;
using SAPbouiCOM;
using B1WizardBase;

namespace FMGeneral
{
    public class FMGeneral_Db : B1Db
    {
        public FMGeneral_Db()
        {
            Tables = new B1DbTable[] {

                #region Zion
                
                    #region Machine Hiring
                new B1DbTable("@FM_OMHF", "Machine Hiring", BoUTBTableType.bott_Document),
                new B1DbTable("@FM_MHF1", "Machine Hiring Row", BoUTBTableType.bott_DocumentLines),
                    #endregion
                    #region Export Packing
                new B1DbTable("@FM_OEPL", "Export Packing List", BoUTBTableType.bott_Document),
                new B1DbTable("@FM_EPL1", "Export Packing List Row", BoUTBTableType.bott_DocumentLines),
                    #endregion
                    #region Production Data
                new B1DbTable("@FM_OPRD", "Production Data", BoUTBTableType.bott_Document),
                new B1DbTable("@FM_PRD1", "Production Data Row1", BoUTBTableType.bott_DocumentLines),
                new B1DbTable("@FM_PRD2", "Production Data Row2", BoUTBTableType.bott_DocumentLines),
                new B1DbTable("@FM_PRD3", "Production Data Row3", BoUTBTableType.bott_DocumentLines),
                #endregion 
                #region Bank Exchange Rate
                new B1DbTable("@FM_OBER", "Bank Exchange Rate", BoUTBTableType.bott_Document),
                new B1DbTable("@FM_BER1", "Bank Exchange Rate Row", BoUTBTableType.bott_DocumentLines),
                new B1DbTable("@FM_BER2", "Bank Exchange Rate Row2", BoUTBTableType.bott_DocumentLines),
                    #endregion
                

                #region Production Budget
                    new B1DbTable("@FM_OPBD", "Production Budget", BoUTBTableType.bott_MasterData),
                    new B1DbTable("@FM_PBD1", "Production Budget Row", BoUTBTableType.bott_MasterDataLines),
                    new B1DbTable("@FM_OBCT", "Budget Category", BoUTBTableType.bott_MasterData),
                #endregion

                    #region No-Object Tables
                    new B1DbTable("@CCS_EPLSETT", "EPL Settings", BoUTBTableType.bott_NoObject),
                    #endregion
                #endregion
            };

            Columns = new B1DbColumn[]
            {
                #region Zion
                
                    #region Machine Hiring
                   new B1DbColumn("@FM_OMHF", "DocDate", "Document Date", BoFieldTypes.db_Date, BoFldSubTypes.st_None, 0, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OMHF", "SplrCode", "Supplier Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OMHF", "SplrName", "Supplier Name", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OMHF", "Brand", "Brand", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OMHF", "Model", "Model", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OMHF", "Capacity", "Capacity", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OMHF", "Matricula", "Matricula", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OMHF", "PrcPrShft", "Price Per Shift", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OMHF", "PrcPrHr", "Price Per Hour", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OMHF", "ExTimePrc", "Extra Time", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OMHF", "APInvEntry", "AP Invoice Entry", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OMHF", "APInvNum", "AP Invoice Num", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OMHF", "WTCode", "WithHoldingTax Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OMHF", "APInvDate", "AP Invoice Date", BoFieldTypes.db_Date, BoFldSubTypes.st_None, 0, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OMHF", "Month", "Month", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   //new B1DbColumn("@FM_OMHF", "GLAcct", "GL Account", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   //new B1DbColumn("@FM_OMHF", "Asset", "Asset", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   //new B1DbColumn("@FM_OMHF", "AssetCode", "Asset Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   //new B1DbColumn("@FM_OMHF", "Departmnt", "Department", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   //new B1DbColumn("@FM_OMHF", "DprtCode", "Department Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   //new B1DbColumn("@FM_OMHF", "VATCode", "VAT Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OMHF", "Remark", "Remarks", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 250, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OMHF", "Vehicle", "Vehicle", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OMHF", "VhcleCode", "Vehicle Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),


                   new B1DbColumn("@FM_MHF1", "Date", "Date", BoFieldTypes.db_Date, BoFldSubTypes.st_None, 0, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_MHF1", "Driver", "Driver", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_MHF1", "Situation", "Situation", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 2, true, new B1WizardBase.B1DbValidValue[] {
                   new B1WizardBase.B1DbValidValue("O", "Own"),
                   new B1WizardBase.B1DbValidValue("R", "Rent")}, -1),
                   new B1DbColumn("@FM_MHF1", "Shift", "Shift", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 2, true, new B1WizardBase.B1DbValidValue[] {
                   new B1WizardBase.B1DbValidValue("D", "Day"),
                   new B1WizardBase.B1DbValidValue("N", "Night")}, -1),
                   new B1DbColumn("@FM_MHF1", "StrtTime", "Start Time", BoFieldTypes.db_Date, BoFldSubTypes.st_Time, 0, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_MHF1", "EndTime", "End Time", BoFieldTypes.db_Date, BoFldSubTypes.st_Time, 0, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_MHF1", "StrtTime2", "Start Time2", BoFieldTypes.db_Date, BoFldSubTypes.st_Time, 0, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_MHF1", "EndTime2", "End Time 2", BoFieldTypes.db_Date, BoFldSubTypes.st_Time, 0, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_MHF1", "ToltTime", "Total Time", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_MHF1", "BrkTime", "Bread Down Time", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_MHF1", "WrkTime", "Worked Time", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_MHF1", "ExtTime", "Extra Time", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_MHF1", "TotlCost", "Total Cost", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 60, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_MHF1", "GLAcct", "GL Account", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_MHF1", "CostCntr", "Cost Centre", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 60, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_MHF1", "CstCrCode", "Cost Centre Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_MHF1", "Departmnt", "Department", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 60, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_MHF1", "DprtCode", "Department Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_MHF1", "Remark", "Remarks", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 250, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_MHF1", "Dimnsn", "Dimension", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 3, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_MHF1", "PrcPrShft", "Price Per Shift", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_MHF1", "PrcPrHr", "Price Per Hour", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_MHF1", "ExTimePrc", "Extra Time", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_MHF1", "VATCode", "VAT Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                    #endregion

                    #region Export Packing
                   new B1DbColumn("@FM_OEPL", "DocDate", "Document Date", BoFieldTypes.db_Date, BoFldSubTypes.st_None, 0, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OEPL", "CustCode", "Customer Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OEPL", "CustName", "Customer Name", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OEPL", "TotalTon", "Total Tonnes", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OEPL", "VehcleNum", "Vehicle Number", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),

                   new B1DbColumn("@FM_EPL1", "BatchNum", "Batch Number", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_EPL1", "ItemCode", "Item Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_EPL1", "ItemName", "Billet Name", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_EPL1", "BtchQty", "Qty in Batch", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_EPL1", "QtyUse", "Qty Used", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_EPL1", "TotalPerSize", "Total by size", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   
                    #endregion 

                   #region Production Data
                   new B1DbColumn("@FM_OPRD", "DocDate", "Document Date", BoFieldTypes.db_Date, BoFldSubTypes.st_None, 0, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OPRD", "TtlFPTon", "Total Tons", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OPRD", "TtlFPHeat", "Total Heat", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OPRD", "RMGrsBlt", "Total RM Gross Billet", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OPRD", "RMNetPrd", "Total RM Net Production", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OPRD", "SMGrsBlt", "Total SM Gross Billet", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_OPRD", "SMNetPrd", "Total SM Net Production", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),

                   new B1DbColumn("@FM_PRD1", "FPMonth", "FP Month", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_PRD1", "Heat", "Heat", BoFieldTypes.db_Numeric, BoFldSubTypes.st_None, 10, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_PRD1", "Tons", "Tons", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),

                   new B1DbColumn("@FM_PRD2", "RMMonth", "RM Month", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_PRD2", "GrossBilt", "Gross Billet", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_PRD2", "NetPrdtn", "Net Production", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),

                   new B1DbColumn("@FM_PRD3", "SMMonth", "SM Month", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_PRD3", "GrossBilt", "Gross Billet", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_PRD3", "NetPrdtn", "Net Production", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),


                #endregion
    
                #region Bank Exchange Rate

                new B1DbColumn("@FM_OBER", "DocDate", "Document Date", BoFieldTypes.db_Date, BoFldSubTypes.st_None, 0, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),

                new B1DbColumn("@FM_BER1", "ExCurr", "Exchange Currency", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 4, true, new B1WizardBase.B1DbValidValue[] {
                   new B1WizardBase.B1DbValidValue("EUR", "EURO"),
                   new B1WizardBase.B1DbValidValue("USD", "USD")}, -1),
                new B1DbColumn("@FM_BER1", "BFA", "BFA", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                new B1DbColumn("@FM_BER1", "SBA", "SBA", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                new B1DbColumn("@FM_BER1", "BCGA", "BCGA", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                new B1DbColumn("@FM_BER1", "BNA", "BNA", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                new B1DbColumn("@FM_BER1", "BMR", "BMR", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),


                new B1DbColumn("@FM_BER2", "Bank", "Bank", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                new B1DbColumn("@FM_BER2", "RsrAmount", "Reserved Amount Kz", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                new B1DbColumn("@FM_BER2", "RsrAmtUSD", "Reserved Amount USD", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                new B1DbColumn("@FM_BER2", "RsrAmtEUR", "Reserved Amount EUR", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),

                new B1DbColumn("OUSR", "MHFAccess", "MHF Invoice Access", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 3, true, new B1WizardBase.B1DbValidValue[]{
                   new B1WizardBase.B1DbValidValue("Y", "Yes"),
                   new B1WizardBase.B1DbValidValue("N", "No")}, -1),
                
                #endregion

                #region No-Object Tables
                   new B1DbColumn("@CCS_EPLSETT", "Values", "Settings Value", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                #endregion
                   
                   
                   #region Production Budget
                    new B1DbColumn("@FM_OPBD", "DocDate", "Document Date", BoFieldTypes.db_Date, BoFldSubTypes.st_None, 0, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                    new B1DbColumn("@FM_OPBD", "Year", "Year",BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 10, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),

                    new B1DbColumn("@FM_PBD1", "CtgryCode", "Category Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 10, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                    new B1DbColumn("@FM_PBD1", "Category", "Category", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 150, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                    new B1DbColumn("@FM_PBD1", "January", "January", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                    new B1DbColumn("@FM_PBD1", "February", "February", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                    new B1DbColumn("@FM_PBD1", "March", "March", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                    new B1DbColumn("@FM_PBD1", "April", "April", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                    new B1DbColumn("@FM_PBD1", "May", "May", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                    new B1DbColumn("@FM_PBD1", "June", "June", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                    new B1DbColumn("@FM_PBD1", "July", "July", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                    new B1DbColumn("@FM_PBD1", "August", "August", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                    new B1DbColumn("@FM_PBD1", "September", "September", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                    new B1DbColumn("@FM_PBD1", "October", "October", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                    new B1DbColumn("@FM_PBD1", "November", "November", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                    new B1DbColumn("@FM_PBD1", "December", "December", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                    new B1DbColumn("@FM_PBD1", "TotalBdgt", "Total Budget", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),

                    new B1DbColumn("@FM_OBCT", "Category", "Category", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 150, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                #endregion
               

                #endregion
            };

            Udos = new B1Udo[] {
                #region Zion
                
                    #region Machine Hiring
                     new B1Udo("FM_MHF","Machine Hiring", "FM_OMHF", new string[] {
                                     "FM_MHF1"}, BoUDOObjType.boud_Document, BoYesNoEnum.tYES, BoYesNoEnum.tNO, BoYesNoEnum.tNO, BoYesNoEnum.tNO,
                                BoYesNoEnum.tNO, BoYesNoEnum.tYES, BoYesNoEnum.tYES, null, new string[]{ "DocEntry","DocNum"}, new string[]{ "DocEntry","DocNum"}),
                    #endregion
                    #region Export Packing
                     new B1Udo("FM_EPL","Export Packing", "FM_OEPL", new string[] {
                                     "FM_EPL1"}, BoUDOObjType.boud_Document, BoYesNoEnum.tYES, BoYesNoEnum.tNO, BoYesNoEnum.tNO, BoYesNoEnum.tNO,
                                BoYesNoEnum.tNO, BoYesNoEnum.tYES, BoYesNoEnum.tYES, null, new string[]{ "DocEntry","DocNum"}, new string[]{ "DocEntry","DocNum"}),
                   
                     #endregion 
                    #region  QA Test Report
                    new B1Udo("FM_PRD","Production Data", "FM_OPRD", new string[] {
                                     "FM_PRD1","FM_PRD2","FM_PRD3"}, BoUDOObjType.boud_Document, BoYesNoEnum.tYES, BoYesNoEnum.tNO, BoYesNoEnum.tNO, BoYesNoEnum.tNO,
                                BoYesNoEnum.tNO, BoYesNoEnum.tYES, BoYesNoEnum.tYES, null, new string[]{ "DocEntry","DocNum"}, new string[]{ "DocEntry","DocNum"}),
                    #endregion 
                   
                    #region Bank Exchange Rate
               new B1Udo("FM_BER","Bank Exchange Rate", "FM_OBER", new string[] {
                                     "FM_BER1"}, BoUDOObjType.boud_Document, BoYesNoEnum.tYES, BoYesNoEnum.tNO, BoYesNoEnum.tNO, BoYesNoEnum.tNO,
                                BoYesNoEnum.tNO, BoYesNoEnum.tYES, BoYesNoEnum.tYES, null, new string[]{ "DocEntry","DocNum"}, new string[]{ "DocEntry","DocNum"}),
                    #endregion
               #region Production Budget
                    new B1Udo("FM_PBD", "Production Budget", "FM_OPBD", new string[] {"FM_PBD1"},BoUDOObjType.boud_MasterData, BoYesNoEnum.tYES, BoYesNoEnum.tNO, BoYesNoEnum.tNO, BoYesNoEnum.tNO, BoYesNoEnum.tNO,
                                    BoYesNoEnum.tNO, BoYesNoEnum.tNO, "AFM_OPBD", new string[] {
                                    "Code","Name"
                                    }, new string[] {
                                    "Code","Name"
                                              }),
                    new B1Udo("FM_BCT", "Budget Category", "FM_OBCT", new string[] {},BoUDOObjType.boud_MasterData, BoYesNoEnum.tYES, BoYesNoEnum.tNO, BoYesNoEnum.tNO, BoYesNoEnum.tNO, BoYesNoEnum.tNO,
                                    BoYesNoEnum.tNO, BoYesNoEnum.tNO, "", new string[] {
                                    "Code","Name"
                                    }, new string[] {
                                    "Code","Name"
                                              }),
                    #endregion
                    #endregion
                };
        }
    }
}
