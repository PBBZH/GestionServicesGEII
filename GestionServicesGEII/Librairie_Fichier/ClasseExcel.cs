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
 *                     ClasseExcel.cs
 * ================================================================ */
using System;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using GestionServicesGEII.Librairie_Générique;

namespace GestionServicesGEII.Librairie_Fichier
{
    class ClasseExcel : ClasseFichier
    {
        #region Champs de la classe
        //-------------------------
        private Workbook classeurExcel = null;
        private Worksheet[ ] feuille = null;
        private string[ ] nomFeuille = null;
        private uint nbFeuille;
        private string conStr = string.Empty;
        private ClasseGénérique ClasseGénérique = new ClasseGénérique();
        private Excel.Application appExcel;
        //----------------------------
        #endregion champs de la classe

        #region Propriétés de la classe
        //-----------------------------
        public Workbook ClasseurExcel
        {
            get
            {
                return classeurExcel;
            }
            protected set
            {
                if (classeurExcel != value)
                {
                    classeurExcel = value;
                }
            }
        }
        public Worksheet[ ] Feuille
        {
            get
            {
                return feuille;
            }
            protected set
            {
                if (feuille != value)
                {
                    feuille = value;
                }
            }
        }
        public string[ ] NomFeuille
        {
            get
            {
                return nomFeuille;
            }
            protected set
            {
                if (nomFeuille != value)
                {
                    nomFeuille = value;
                }
            }
        }
        public uint NbFeuille
        {
            get
            {
                return nbFeuille;
            }
            protected set
            {
                if (nbFeuille != value)
                {
                    nbFeuille = value;
                }
            }
        }
        public string ConStr
        {
            get
            {
                if (conStr == string.Empty)
                {
                    OuvrirFichierExcel("Recherche du fichiers de travail");
                    return conStr;
                }
                return conStr;
            }
            protected set
            {
                if (conStr != value)
                {
                    conStr = value;
                }
            }
        }

        internal Excel.Application AppExcel
        {
            get
            {
                return appExcel;
            }

            set 
            {
                appExcel = value;
            }
        }

        //--------------------------------
        #endregion Propriétés de la classe

        #region Méthodes de la classe
        //---------------------------
        public ClasseExcel()
        {
                        
        }
        ~ClasseExcel()
        {
            nettoyageDesVariablesExcel();
        }

        #region Test de l'existence d'un fichier ouvert
        //---------------------------------------------
        /// <summary>
        /// Test de l'existence d'un fichier ouvert
        /// </summary>
        private bool TestFichierOuvert()
        {
            try
            {
                bool test = base.FichierOuvert;
                return test;
            }
            catch
            {
                ClasseurExcel = null;
                return false;
            }
        }
        //-----------------------------------------
        #endregion Test de l'existance d'un fichier

        private Worksheet changeFeuille(Worksheet feuille)
        {
            feuille.Select();
            Worksheet feuilleActive = feuille;
            return feuille;
        }
        private void ecritListe(Worksheet feuille, string plage)
        {
            _Worksheet cetteFeuille = feuille;
            Excel.Range zoneID = cetteFeuille.get_Range(plage).Select();
        }
        protected internal void changeFeuilleVisible(ComboBox FeuilleClasseur)
        {
            try
            {
                Excel.Worksheet feuilleActive = null;
                for (int i = 0; i < NbFeuille; i++)
                {
                    if (NomFeuille[i] == FeuilleClasseur.SelectedItem.ToString())
                    {
                        feuilleActive = changeFeuille(Feuille[i]);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #region Ouvrir le fichier Excel
        //-----------------------------
        /// <summary>
        /// Ouverture du fichier Excel
        /// </summary>
        protected string chaineDeConnexion(string ExtensionFichier)
        {
            string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
            string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR={1}'";
            switch (ExtensionFichier)
            {
                case ".xls": //Excel1 97-03
                    ConStr = string.Format(Excel03ConString, CheminFichier, "YES");
                    break;

                case ".xlsx": //Excel1 07
                    ConStr = string.Format(Excel07ConString, CheminFichier, "YES");
                    break;
            }
            return ConStr;
        }
        public void OuvrirFichierExcel(string nom, bool VisibilitéFichier = false)
        {
            FichierOuvert = OuvrirFichier("Ouverture du fichier de Sevice pour le GEII");
            this.ConStr = chaineDeConnexion(ExtensionFichier);
            if (FichierOuvert)
            {
                try
                {
                    FichierExcel.F_Excel = new Excel.Application();
                    //-Déclaration du classeur de travail
                    //-----------------------------------
                    if (ClasseurExcel == null)
                        ClasseurExcel = FichierExcel.F_Excel.Workbooks.Open
                            (
                                CheminFichier,
                                2,
                                false,
                                1,
                                "",
                                "",
                                true,
                                XlPlatform.xlWindows,
                                "\t", // que veut dire ce champs ?
                                false,
                                false,
                                1,
                                0
                            );
                    Librairie_Fichier.FichierExcel.F_Excel.Visible = VisibilitéFichier;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



                //-Déclaration et méthodes pour changer et visualiser les différentes Feuilles du classeur
                //----------------------------------------------------------------------------------------
                try
                {
                    // UInt32.Parse : conversion String en UInt32
                    NbFeuille = UInt32.Parse(ClasseurExcel.Sheets.Count.ToString());

                    Feuille = new Worksheet[NbFeuille];
                    NomFeuille = new string[NbFeuille];
                    for (uint i = 0; i < NbFeuille; i++)
                    {
                        Feuille[i] = ClasseurExcel.Worksheets[i + 1];
                        NomFeuille[i] = Feuille[i].Name.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //-----------------------------------
        #endregion Ouverture du Fichier Excel

        #region Fermer et enregistrer Fichier et classeur
        //------------------------------------------------
        /// <summary>
        /// Ouverture du Fichier Excel
        /// </summary>

        protected void fermerFichierExcel()
        {
            Librairie_Fichier.FichierExcel.F_Excel.Quit();
            Librairie_Fichier.FichierExcel.F_Excel = null;
        }

        public void fermerClasseurExcel()
        {
            if (TestFichierOuvert())
            {
                {
                    try
                    {
                        enregistrerFichier();
                        ClasseurExcel.Close(0);
                        ClasseurExcel = null;
                        fermerFichierExcel();
                        SuprimeToutesLesTachesExcel();
                    }
                    catch
                    {
                        //MessageBox.Show("Il n'y a plus de fichier ouvert.");
                        ClasseurExcel = null;
                    }
                }
            }
        }

        #region Obtenir la permission de fermeture du fichier
        //------------------------------------------------------
        /// <summary>
        /// Obtenir la permission de fermeture du fichier
        /// il est en effet nécessaire de s'assurer que le fichier
        /// ne sera pas fermé si les modifications n'ont pas été
        /// sauvegardées sous le même nom ou sous un autre
        /// </summary>
        /// <paramref name="demande">
        /// Spécifie si l'utilisateur doit 
        /// confirmer la sauvegarde
        /// </paramref>
        /// <paramref name="afficherDialogue">
        /// Spécifie si l'utilisateur 
        /// a la possibilité de choisir le fichier
        /// </paramref>
        public string permissionFermeture;

        public string enregistrerFichier(bool demande, bool afficherDialogue)
        {
            if (TestFichierOuvert())
            {
                try
                {
                    if (!demande)
                    {
                        if (!afficherDialogue
                            && !string.IsNullOrEmpty(NomFichier)
                            && !string.IsNullOrEmpty(RépertoireFichier))
                        {
                            DialogResult réponse = MessageBox.Show
                            (
                                "Voulez-vous sauvegarder les modifications ?",
                                "Sauver", MessageBoxButtons.YesNoCancel,
                                MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1 // le premier bouton est le bouton par défaut
                            );
                            switch (réponse)
                            {
                                case DialogResult.Yes:
                                    // sauvegarde des données
                                    Librairie_Fichier.FichierExcel.F_Excel.ActiveWorkbook.Save();
                                    permissionFermeture = "Oui";
                                    break;
                                case DialogResult.No:
                                    permissionFermeture = "Non";
                                    break;
                                case DialogResult.Cancel:
                                    permissionFermeture = "Annuler";
                                    break;
                                default:
                                    break;
                            }
                        }

                        else
                        {
                            if (afficherDialogue)
                            {
                                SauvegardeFichier = dialogueExplorateur(SauvegardeFichier, "Sauvegarde du fichier");
                                if (SauvegardeFichier.ShowDialog() == DialogResult.OK)
                                {
                                    this.NomFichier = Path.GetFileName(SauvegardeFichier.FileName);
                                    this.RépertoireFichier = Path.GetDirectoryName(SauvegardeFichier.FileName);
                                    Librairie_Fichier.FichierExcel.F_Excel.ActiveWorkbook.SaveAs(NomFichier);
                                }
                            }
                        }
                    }
                    else
                    {
                        Librairie_Fichier.FichierExcel.F_Excel.ActiveWorkbook.Save();
                        permissionFermeture = "Oui";
                    }
                }
                catch
                {
                    this.ClasseurExcel = null;
                }
            }
            return NomFichier;
        }

        //------------------------------------------------------
        #endregion Obtenir la permission de fermeture du fichier

        public string enregistrerFichier()
        {
            NomFichier = enregistrerFichier(false, false);
            return NomFichier;
        }

        public string enregistrerFichier(bool demande)
        {
            NomFichier = enregistrerFichier(demande, true);
            return NomFichier;
        }

        //------------------------------------------------------
        #endregion Fermer et enregistrer Fichier et classeur
        private void nettoyageDesVariablesExcel()
        {
            nettoyageDesVariablesFichier();
        }
        #endregion Méthodes de la classe
    }
}
