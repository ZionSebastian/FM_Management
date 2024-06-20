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
                #region FG Production Cost
                new B1DbTable("@FM_OFPC", "FG Production Cost", BoUTBTableType.bott_Document),
                new B1DbTable("@FM_FPC1", "FG Production Cost Row", BoUTBTableType.bott_DocumentLines),
                    #endregion
                

                #region Production Budget
                    new B1DbTable("@FM_OPBD", "Production Budget", BoUTBTableType.bott_MasterData),
                    new B1DbTable("@FM_PBD1", "Production Budget Row", BoUTBTableType.bott_MasterDataLines),
                    new B1DbTable("@FM_OBCT", "Budget Category", BoUTBTableType.bott_MasterData),
                #endregion

                #region AP Debit Memo
                    new B1DbTable("@FM_ORPD", "AP Debit Memo", BoUTBTableType.bott_Document),
                    new B1DbTable("@FM_RPD1", "AP Debit Memo Row", BoUTBTableType.bott_DocumentLines),
                #endregion

                #region Standard BOM List
                new B1DbTable("@FM_OSBL", "Standard BOM List", BoUTBTableType.bott_Document),
                new B1DbTable("@FM_SBL1", "Standard BOM List Row", BoUTBTableType.bott_DocumentLines),
                new B1DbTable("@FM_SBL2", "Standard BOM List Row2", BoUTBTableType.bott_DocumentLines),
                new B1DbTable("@FM_SBL3", "Standard BOM List Row3", BoUTBTableType.bott_DocumentLines),
                new B1DbTable("@FM_SBL4", "Standard BOM List Row4", BoUTBTableType.bott_DocumentLines),
                new B1DbTable("@FM_SBL5", "Standard BOM List Row5", BoUTBTableType.bott_DocumentLines),
                    #endregion
                #region No-Object Tables
                    new B1DbTable("@CCS_EPLSETT", "EPL Settings", BoUTBTableType.bott_NoObject),
                    new B1DbTable("@CCS_DSBSETT", "Daily SMS Bank Settings", BoUTBTableType.bott_NoObject),
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

                #region AP Debit Memo
                   new B1DbColumn("@FM_ORPD", "DocDate", "Document Date", BoFieldTypes.db_Date, BoFldSubTypes.st_None, 0, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_ORPD", "DocDueDate", "Due Date", BoFieldTypes.db_Date, BoFldSubTypes.st_None, 0, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_ORPD", "CardCode", "Supplier Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_ORPD", "CardName", "Supplier Name", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_ORPD", "NumAtCard", "Supplier Ref. No.", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 60, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_ORPD", "DocType", "Document Type", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 2, true, new B1WizardBase.B1DbValidValue[]{
                   new B1WizardBase.B1DbValidValue("I", "Item"),
                   new B1WizardBase.B1DbValidValue("S", "Service")}, -1),
                   new B1DbColumn("@FM_ORPD", "DiscPrcnt", "Discount Percentage", BoFieldTypes.db_Float, BoFldSubTypes.st_Percentage, 0, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_ORPD", "Remark", "Remarks", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 150, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_ORPD", "DocTotal", "Document Total", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_ORPD", "JrnlEntry", "Journal Entry", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_ORPD", "TotlBfrDsc", "Total Before Discount", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_ORPD", "TaxAmount", "Tax Amount", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_ORPD", "ITG_SignDigest", "ITG_SignDigest", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 200, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_ORPD", "DocCur", "Document Currency", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 3, true, new B1WizardBase.B1DbValidValue[]{
                   new B1WizardBase.B1DbValidValue("AOA", "AOA"),
                   new B1WizardBase.B1DbValidValue("USD", "USD"),
                   new B1WizardBase.B1DbValidValue("EUR", "EUR")}, -1),
                   new B1DbColumn("@FM_ORPD", "Address", "Addrress", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 200, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_ORPD", "Address2", "Addrress2", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 200, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_ORPD", "DocRate", "Document Rate", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_ORPD", "GroupNum", "GroupNum", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_ORPD", "PymntGrp", "Payment Group", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),


                   new B1DbColumn("@FM_RPD1", "ItemCode", "Item Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_RPD1", "Dscription", "Description", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_RPD1", "Quantity", "Quantity", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_RPD1", "UnitPrice", "Unit Price", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_RPD1", "DiscPrcnt", "Discount Percentage", BoFieldTypes.db_Float, BoFldSubTypes.st_Percentage, 0, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_RPD1", "VatGroup", "VATCode", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 10, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_RPD1", "LineTotal", "LineTotal", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_RPD1", "UomCode", "UoM Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 10, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_RPD1", "AcctCode", "GL Account", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_RPD1", "AcctName", "GL Account Name", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 60, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_RPD1", "WtLiable", "WTaxLiable", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 2, true, new B1WizardBase.B1DbValidValue[]{
                   new B1WizardBase.B1DbValidValue("Y", "Yes"),
                   new B1WizardBase.B1DbValidValue("N", "No")}, -1),
                   new B1DbColumn("@FM_RPD1", "Dprtmnts", "Departments", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_RPD1", "Employee", "Employee", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_RPD1", "Vehicle", "Vehicle", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_RPD1", "Asset", "Asset", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_RPD1", "Rate", "Rate", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_RPD1", "ItemAcct", "Item Account", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_RPD1", "VATAcct", "VAT Account", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_RPD1", "VATAmount", "VAT Amount", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   new B1DbColumn("@FM_RPD1", "Price", "Price", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                   
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


                new B1DbColumn("@FM_BER2", "Bank", "Bank", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                new B1DbColumn("@FM_BER2", "RsrAmount", "Reserved Amount Kz", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                new B1DbColumn("@FM_BER2", "RsrAmtUSD", "Reserved Amount USD", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                new B1DbColumn("@FM_BER2", "RsrAmtEUR", "Reserved Amount EUR", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),

                new B1DbColumn("OUSR", "MHFAccess", "MHF Invoice Access", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 3, true, new B1WizardBase.B1DbValidValue[]{
                   new B1WizardBase.B1DbValidValue("Y", "Yes"),
                   new B1WizardBase.B1DbValidValue("N", "No")}, -1),
                
                #endregion


                #region FG Production Cost

                new B1DbColumn("@FM_OFPC", "DocDate", "Document Date", BoFieldTypes.db_Date, BoFldSubTypes.st_None, 0, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                new B1DbColumn("@FM_OFPC", "Month", "Month", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),

                new B1DbColumn("@FM_FPC1", "FnshGoods", "Finished Goods", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                new B1DbColumn("@FM_FPC1", "Cost", "Cost", BoFieldTypes.db_Float, BoFldSubTypes.st_Price, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
               
                
                #endregion

                #region Standard BOM List
                 new B1DbColumn("@FM_OSBL", "DocDate", "Document Date", BoFieldTypes.db_Date, BoFldSubTypes.st_None, 0, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),

                 new B1DbColumn("@FM_SBL1", "Category", "Category", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[] {
                   new B1WizardBase.B1DbValidValue("127", "Billet General"),
                   new B1WizardBase.B1DbValidValue("135", "Cornier General"),
                   new B1WizardBase.B1DbValidValue("140", "Fer Plat General"),
                   new B1WizardBase.B1DbValidValue("143", "Perfil General"),
                   new B1WizardBase.B1DbValidValue("128", "Rebars General")}, -1),
                 new B1DbColumn("@FM_SBL1", "RwMtrlCode", "Raw Material Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL1", "RwMtrlName", "Raw Material Name", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL1", "UOM", "UOM", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL1", "StdRange", "Standard Range", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL1", "EndStdRng", "End Standard Range", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL1", "FshGdCode", "Finished Good Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL1", "FshGdName", "Finished Good Name", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),

                 new B1DbColumn("@FM_SBL2", "Category", "Category", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[] {
                   new B1WizardBase.B1DbValidValue("127", "Billet General"),
                   new B1WizardBase.B1DbValidValue("135", "Cornier General"),
                   new B1WizardBase.B1DbValidValue("140", "Fer Plat General"),
                   new B1WizardBase.B1DbValidValue("143", "Perfil General"),
                   new B1WizardBase.B1DbValidValue("128", "Rebars General")}, -1),
                 new B1DbColumn("@FM_SBL2", "RwMtrlCode", "Raw Material Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL2", "RwMtrlName", "Raw Material Name", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL2", "UOM", "UOM", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL2", "StdRange", "Standard Range", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL2", "EndStdRng", "End Standard Range", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL2", "FshGdCode", "Finished Good Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL2", "FshGdName", "Finished Good Name", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),

                 //new B1DbColumn("@FM_SBL2", "A100X10", "ANGLE 100X100X10", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL2", "A20X2.5", "ANGLE 20X00X2.5", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL2", "A20X2", "ANGLE 20X20X2", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL2", "A20X3", "ANGLE 20X20X3", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL2", "A25X2", "ANGLE 25X25X2", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL2", "A25X2.5", "ANGLE 25X25X2.5", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL2", "A25X3", "ANGLE 25X25X3", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL2", "A30X03", "ANGLE 30X30X03", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL2", "A30X2", "ANGLE 30X30X2", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL2", "A30X2.5", "ANGLE 30X30X2.5", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL2", "A35X03", "ANGLE 35X35X03", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL2", "A40X02", "ANGLE 40X40X02", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL2", "A40X03", "ANGLE 40X40X03", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL2", "A40X2.5", "ANGLE 40X40X2.5", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL2", "A40X4", "ANGLE 40X40X4", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL2", "A40X5", "ANGLE 40X40X5", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL2", "A45X03", "ANGLE 45X45X03", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL2", "A50X3", "ANGLE 50X50X3", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL2", "A50X4", "ANGLE 50X50X4", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL2", "A50X5", "ANGLE 50X50X5", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL2", "A60X5", "ANGLE 60X60X5", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL2", "A60X6", "ANGLE 60X60X6", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL2", "A70X5", "ANGLE 70X70X5", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL2", "A70X6", "ANGLE 70X70X6", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL2", "A70X7", "ANGLE 70X70X7", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL2", "A80X08", "ANGLE 80X80X08", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 

                 new B1DbColumn("@FM_SBL3", "Category", "Category", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[] {
                   new B1WizardBase.B1DbValidValue("127", "Billet General"),
                   new B1WizardBase.B1DbValidValue("135", "Cornier General"),
                   new B1WizardBase.B1DbValidValue("140", "Fer Plat General"),
                   new B1WizardBase.B1DbValidValue("143", "Perfil General"),
                   new B1WizardBase.B1DbValidValue("128", "Rebars General")}, -1),
                 new B1DbColumn("@FM_SBL3", "RwMtrlCode", "Raw Material Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL3", "RwMtrlName", "Raw Material Name", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL3", "UOM", "UOM", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL3", "StdRange", "Standard Range", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL3", "EndStdRng", "End Standard Range", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL3", "FshGdCode", "Finished Good Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL3", "FshGdName", "Finished Good Name", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),

                 //new B1DbColumn("@FM_SBL3", "B20X5", "BARRA 20X5", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL3", "B25X3", "BARRA 25X3", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL3", "B25X5", "BARRA 25X5", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL3", "B40X3", "BARRA 40X3", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL3", "B40X5", "BARRA 40X5", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL3", "B60X5", "BARRA 60X5", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL3", "B60X6", "BARRA 60X6", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL3", "F20X03", "FER PLAT 20X03", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL3", "F30X03", "FER PLAT 30X03", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL3", "F30X05", "FER PLAT 30X05", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL3", "F40X04", "FER PLAT 40X04", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL3", "F50X05", "FER PLAT 50X05", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL3", "F50X4", "FER PLAT 50X4", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL3", "F50X4.5", "FER PLAT 50X4.5", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL3", "F70X6.5", "FER PLAT 70X6.5", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),



                 new B1DbColumn("@FM_SBL4", "Category", "Category", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[] {
                   new B1WizardBase.B1DbValidValue("127", "Billet General"),
                   new B1WizardBase.B1DbValidValue("135", "Cornier General"),
                   new B1WizardBase.B1DbValidValue("140", "Fer Plat General"),
                   new B1WizardBase.B1DbValidValue("143", "Perfil General"),
                   new B1WizardBase.B1DbValidValue("128", "Rebars General")}, -1),
                 new B1DbColumn("@FM_SBL4", "RwMtrlCode", "Raw Material Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL4", "RwMtrlName", "Raw Material Name", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL4", "UOM", "UOM", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL4", "StdRange", "Standard Range", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL4", "EndStdRng", "End Standard Range", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL4", "FshGdCode", "Finished Good Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL4", "FshGdName", "Finished Good Name", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),

                 //new B1DbColumn("@FM_SBL4", "P100X50", "PERFIL_I 100X50", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL4", "P100X50", "PERFIL_U 100X50", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL4", "P120X55", "PERFIL_U 120X55", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL4", "P80X45", "PERFIL_U 80X45", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),


                 new B1DbColumn("@FM_SBL5", "Category", "Category", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[] {
                   new B1WizardBase.B1DbValidValue("127", "Billet General"),
                   new B1WizardBase.B1DbValidValue("135", "Cornier General"),
                   new B1WizardBase.B1DbValidValue("140", "Fer Plat General"),
                   new B1WizardBase.B1DbValidValue("143", "Perfil General"),
                   new B1WizardBase.B1DbValidValue("128", "Rebars General")}, -1),
                 new B1DbColumn("@FM_SBL5", "RwMtrlCode", "Raw Material Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL5", "RwMtrlName", "Raw Material Name", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL5", "UOM", "UOM", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL5", "StdRange", "Standard Range", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL5", "EndStdRng", "End Standard Range", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL5", "FshGdCode", "Finished Good Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 new B1DbColumn("@FM_SBL5", "FshGdName", "Finished Good Name", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 80, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),

                 //new B1DbColumn("@FM_SBL5", "8MM", "8MM", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL5", "10MM", "10MM", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL5", "12MM", "12MM", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL5", "14MM", "14MM", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL5", "16MM", "16MM", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL5", "18MM", "18MM", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL5", "20MM", "20MM", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL5", "22MM", "22MM", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL5", "25MM", "25MM", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
                 //new B1DbColumn("@FM_SBL5", "32MM", "32MM", BoFieldTypes.db_Float , BoFldSubTypes.st_Quantity, 30, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),

                 #endregion


                #region No-Object Tables
                   new B1DbColumn("@CCS_EPLSETT", "Values", "Settings Value", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),

                   new B1DbColumn("@CCS_DSBSETT", "AcctName", "Account Name", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1WizardBase.B1DbValidValue[-1 + 1], -1),
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

                    #region FG Production Cost
               new B1Udo("FM_FPC","FG Production Cost", "FM_OFPC", new string[] {
                                     "FM_FPC1"}, BoUDOObjType.boud_Document, BoYesNoEnum.tYES, BoYesNoEnum.tNO, BoYesNoEnum.tNO, BoYesNoEnum.tNO,
                                BoYesNoEnum.tNO, BoYesNoEnum.tYES, BoYesNoEnum.tYES, null, new string[]{ "DocEntry","DocNum"}, new string[]{ "DocEntry","DocNum"}),
                    #endregion


                    #region AP Debit Memo
                     new B1Udo("FM_RPD","AP Debit Memo", "FM_ORPD", new string[] {
                                     "FM_RPD1"}, BoUDOObjType.boud_Document, BoYesNoEnum.tYES, BoYesNoEnum.tNO, BoYesNoEnum.tNO, BoYesNoEnum.tNO,
                                BoYesNoEnum.tNO, BoYesNoEnum.tYES, BoYesNoEnum.tYES, null, new string[]{ "DocEntry","DocNum"}, new string[]{ "DocEntry","DocNum"}),
                    #endregion
                    #region Standard BOM List
                new B1Udo("FM_SBL","Standard BOM List", "FM_OSBL", new string[] {
                                     "FM_SBL1","FM_SBL2","FM_SBL3","FM_SBL4","FM_SBL5"}, BoUDOObjType.boud_Document, BoYesNoEnum.tYES, BoYesNoEnum.tNO, BoYesNoEnum.tNO, BoYesNoEnum.tNO,
                                BoYesNoEnum.tNO, BoYesNoEnum.tYES, BoYesNoEnum.tYES, null, new string[]{ "DocEntry","DocNum"}, new string[]{ "DocEntry","DocNum"}),
                    #endregion
                    #endregion
                };
        }
    }
}
