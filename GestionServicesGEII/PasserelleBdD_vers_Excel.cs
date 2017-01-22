using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using GestionServicesGEII.Librairie_Fichier;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using Excel = Microsoft.Office.Interop.Excel.Application;

namespace GestionServicesGEII
{
    internal class  PasserelleBdD_vers_Excel
    {
        private static ClasseExcel ClasseFichierExcel = new ClasseExcel();
        private static DataSet DataSetExcel = new DataSet();
        private void CréerFichierTemporaire(string nomFichierTemporaire)
        {
            Excel FichierExcel = null;
            try
            {
                FichierExcel = new Excel();
                Workbooks nouveauClasseurExcel = FichierExcel.Workbooks;
                _Workbook nouveauFichierExcel = nouveauClasseurExcel.Add(XlWBATemplate.xlWBATWorksheet);
                _Worksheet nouvelleFeuille = (_Worksheet)nouveauFichierExcel.Worksheets[1];
                Range rang = nouvelleFeuille.get_Range("A1", Missing.Value);
                object[ ] données = new object[ ] { "" };
                rang.GetType().InvokeMember(
                        "Value",
                        BindingFlags.SetProperty,
                        null,
                        rang,
                        données
                    );
                nouveauFichierExcel.Close(true, nomFichierTemporaire, null);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                if (FichierExcel != null)
                    FichierExcel.Quit();
            }
        }

        public void SauvegardeBaseDeDonnées_Vers_Excel(DataSet baseDeDonnéesExcel)
        {
            Excel FichierExcel = null;
            Workbook nouveauFichierExcel = null;
            Sheets EnsembleDesFeuilles = null;
            Worksheet nouvelleFeuille = null;

            ClasseFichierExcel.NomFichier = ClasseFichierExcel.enregistrerFichier(false);
            string nouveauChemin = ClasseFichierExcel.RépertoireFichier + "\\" + ClasseFichierExcel.NomFichier;
            string nomTable = DataSetExcel.Tables.IndexOf("Table_complete").ToString();

            DataSet dataSet = DataSetExcel;

            try
            {
                if (File.Exists(nouveauChemin) == true)
                {
                    File.Delete(nouveauChemin);
                }
                if (File.Exists(nouveauChemin) == false)
                {
                    CréerFichierTemporaire(nouveauChemin);
                }
                if (File.Exists(nouveauChemin) == true)
                {
                    FichierExcel = new Excel();
                    nouveauFichierExcel = FichierExcel.Workbooks.Open
                        (
                            @nouveauChemin,
                            0,
                            false,
                            5,
                            "",
                            "",
                            false,
                            XlPlatform.xlWindows,
                            "",
                            true,
                            false,
                            0,
                            true,
                            false,
                            false
                        );

                }
                EnsembleDesFeuilles = nouveauFichierExcel.Sheets;

                foreach (Worksheet feuille in EnsembleDesFeuilles)
                {
                    feuille.Select(Type.Missing);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(feuille);
                }
                for (int indexTable = 0; indexTable < dataSet.Tables.Count; indexTable++)
                {
                    //string nomTable = BaseDeDonnées.DataSetExcel.Tables[indexTable].ToString();
                    DataTable table = dataSet.Tables[indexTable];
                    if (table.TableName == "Table_complete")
                    {
                        nouvelleFeuille = (Worksheet)EnsembleDesFeuilles.Add//
                            (
                                EnsembleDesFeuilles[1],
                                Type.Missing,
                                Type.Missing,
                                Type.Missing
                            );

                        nouvelleFeuille.Name = table.TableName;
                        for (int compteurColonne = 0; compteurColonne < table.Columns.Count; compteurColonne++)
                            nouvelleFeuille.Cells[1, compteurColonne + 1] = "'" + table.Columns[compteurColonne].ColumnName;
                        for (int compteurColonne = 0; compteurColonne < table.Columns.Count; compteurColonne++)
                        {
                            for (int compteurLigne = 0; compteurLigne < table.Rows.Count; compteurLigne++)
                            {
                                if (table.Rows[compteurLigne][compteurColonne] == null)
                                    nouvelleFeuille.Cells[compteurColonne, compteurLigne] = string.Empty;
                                if (table.Columns[compteurColonne].DataType == Type.GetType("System.String"))
                                    nouvelleFeuille.Cells[compteurLigne + 2, compteurColonne + 1] = "'" +
                                        table.Rows[compteurLigne][compteurColonne];
                                else
                                    nouvelleFeuille.Cells[compteurLigne + 2, compteurColonne + 1] =
                                        table.Rows[compteurLigne][compteurColonne];
                            }
                        }
                        Range Selection = nouvelleFeuille.Range[nouvelleFeuille.Cells[1, 1],
                            nouvelleFeuille.Cells[table.Rows.Count + 1, table.Columns.Count]];
                        Selection.Name = "Table_complete";
                    }
                }
                nouveauFichierExcel.Save();
                nouveauFichierExcel.Close(null, null, null);
                FichierExcel.Quit();
            }
            finally
            {
                nouvelleFeuille = null;
                EnsembleDesFeuilles = null;
                nouveauFichierExcel = null;
                FichierExcel = null;
            }
        }
    }
}
