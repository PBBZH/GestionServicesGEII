/* ===================================================================
 * copyright : PB_BZH 2017
 * contact : mailto:admin@pbbzh.fr
 * ===================================================================
 *  IUT de BREST : Département GEII
 * -------------------------------------------------------------------
 * Auteur : Patrick Bourges
 * Le 16/1/2017 - 21:28
 * -------------------------------------------------------------------
 *         PROJET GESTION DES SERVICES DU PERSONNEL
 * -------------------------------------------------------------------
 * Version : 2.1.1
 * -------------------------------------------------------------------
 *  Technologie : C# .NET V5.0
 *  Visual Studio Professionnal 2015
 * -------------------------------------------------------------------
 *                     ClasseGénérique.cs
 * ================================================================ */
using System;
using System.Data;
using System.Windows.Forms;
using GestionServicesGEII.Librairie_Fichier;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace GestionServicesGEII.Librairie_Générique
{
     
    class ClasseGénérique
    {
        public ClasseGénérique()
        {
        }

        public T dialogueExplorateur<T>(T file, string titre) where T : FileDialog, new()
        {
            file = new T();
            file.Filter =
                "Fichier Excel : *.xlsx|*.xlsx|" +
                "Fichier Excel 98 : *.xls|*.xls|" +
                "Fichier Excel avec macro : *.xlsm|*.Xlsm|" +
                "Tous les fichiers : *.*|*.*";
            file.FilterIndex = 1;
            //file.InitialDirectory = @NomRépertoireConfiguration;
            file.SupportMultiDottedExtensions = true;
            file.Title = titre;
            return file;
        }
        public T dialogueExplorateur<T>(string titre) where T : FileDialog, new()
        {
            T répertoire = new T();
            répertoire.SupportMultiDottedExtensions = false;
            répertoire.Title = titre;
            return répertoire;
        }

        public T rempliComboBox<T>(T liste, DataSet dataSet, string nomListe) where T : ComboBox, new()
        {
            DataTable nomDataTable = new DataTable();
            string nomTable = String.Empty;
            string nomTouvé = string.Empty;

            foreach (DataTable dataTable in dataSet.Tables)
            {
                if (dataTable.TableName.ToString() == nomListe)
                {
                    nomTable = dataTable.TableName;
                    nomDataTable = dataTable;
                    break;
                }
            }

            liste.DataSource = nomDataTable;
            liste.DisplayMember = liste.Name;

            return liste;
        }

        public T rempliComboBox<T>(T liste, DataSet dataSet, string nomListe, string variable, T critèreDeSelection) where T : ComboBox, new()
        {
            DataTable maDataTable = new DataTable();
            string nomTable = String.Empty;
            string nomTouvé = string.Empty;

            string selection = critèreDeSelection.Text;
            string champs = critèreDeSelection.Name;

            if (critèreDeSelection.DisplayMember != string.Empty)
            {
                foreach (DataTable dataTable in dataSet.Tables)
                {
                    if (dataTable.TableName.ToString() == nomListe)
                    {
                        nomTable = dataTable.TableName;
                        maDataTable = dataTable;
                        break;
                    }
                }
                BindingSource rechercheChamps = new BindingSource();
                rechercheChamps.DataSource = maDataTable;
                rechercheChamps.Filter = champs + "= '" + selection + "'";

                liste.DataSource = rechercheChamps;
                liste.DisplayMember = liste.Name;


                return liste;
            }
            else
                return null;
        }

        private void rempliComboBoxFeuillesExcel<T>(T liste, uint NbFeuille, Worksheet[ ] Feuille, string[ ] NomFeuille)
                   where T : ComboBox, new()
        {
            RempliComboBoxFeuilleExcel(liste, NbFeuille, Feuille, NomFeuille);
        }
        public void RempliComboBoxFeuilleExcel<T>(T liste, uint NbFeuille, Worksheet[ ] Feuille, string[ ] NomFeuille) where T : ComboBox, new()
        {
            if (liste.Items.Count == 0)
            {
                try
                {
                    NbFeuille = UInt16.Parse(FichierExcel.F_Excel.Sheets.Count.ToString());
                    Feuille = new Worksheet[NbFeuille];
                    NomFeuille = new string[NbFeuille];
                    for (UInt16 i = 0; i < NbFeuille; i++)
                    {
                        Feuille[i] = FichierExcel.F_Excel.Worksheets[i + 1];
                        NomFeuille[i] = Feuille[i].Name.ToString();
                    }
                    liste.DataSource = NomFeuille;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}