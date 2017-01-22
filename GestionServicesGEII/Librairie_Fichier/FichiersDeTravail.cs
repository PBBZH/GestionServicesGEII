using Excel = Microsoft.Office.Interop.Excel;

namespace GestionServicesGEII.Librairie_Fichier
{
    public class FichierDeService
    {
        private static ClasseExcel fichierDeService;
        internal static ClasseExcel Service
        {
            get
            {
                return fichierDeService;
            }

            set
            {
                fichierDeService = value;
            }
        }
        public FichierDeService()
        {
        }

    }

    public class FichierDeSelection
    {
        private static ClasseExcel fichierDeSelection;
        public FichierDeSelection()
        {
        }

        internal static ClasseExcel Selection
        {
            get
            {
                return fichierDeSelection;
            }

            set
            {
                fichierDeSelection = value;
            }
        }
    }

    public class FichierExcel
    {
        private static Excel.Application fichierExcel;
        public FichierExcel()
        {
        }

        public static Excel.Application F_Excel
        {
            get
            {
                return fichierExcel;
            }

            set
            {
                fichierExcel = value;
            }
        }
    }
}