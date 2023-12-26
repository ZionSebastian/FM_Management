using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMGeneral.Class_Files
{
    class globalvariables
    {
        static string strBranch = "";
        static string strFormType;
        static string rfFormType;
        static string DlvryDocNum;
        static string eplentry;
        static string dlvrycustcode;
        static string eplprintDocEntry;
        static bool blnCRSPrint;
        static string strPrintType;


        public static string DftBranch
        {
            get { return strBranch; }

            set { strBranch = value; }
        }

        public static string TransFormType
        {
            get { return strFormType; }

            set { strFormType = value; }
        }

        public static string RefFormType
        {
            get { return rfFormType; }

            set { rfFormType = value; }
        }

        public static string DeliveryDocNum
        {
            get { return DlvryDocNum; }

            set { DlvryDocNum = value; }
        }

        public static string EPLEntry
        {
            get { return eplentry; }

            set { eplentry = value; }
        }
        
        public static string DlvryCustCode
        {
            get { return dlvrycustcode; }

            set { dlvrycustcode = value; }
        }

        public static string EPLPrintDocEntry
        {
            get { return eplprintDocEntry; }

            set { eplprintDocEntry = value; }
        }

        public static bool CRSPrint
        {
            get { return blnCRSPrint; }

            set { blnCRSPrint = value; }
        }


        public static string PrintType
        {
            get { return strPrintType; }

            set { strPrintType = value; }
        }

    }
}
