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
 *                     ClasseFichier.cs
 * ================================================================ */
using System;
using System.IO;
using System.Windows.Forms;
using GestionServicesGEII.Librairie_Générique;

namespace GestionServicesGEII.Librairie_Fichier
{
    class ClasseFichier : ClasseGénérique, IDisposable
    {
        #region Champ de la classe
        //------------------------
        /// <summary>
        /// Déclaration des champs de la classe Fichier
        /// </summary>
		private string cheminFichier = string.Empty;
        private string répertoireFichier = string.Empty;
        private string nomFichier = string.Empty;
        private string extensionFichier = string.Empty;
        private bool fichierOuvert = false;
        private OpenFileDialog explorateurDeFichier = new OpenFileDialog();
        private SaveFileDialog sauvegardeFichier = new SaveFileDialog();
        private ClasseGénérique ClasseGénérique;
        //---------------------------
        #endregion Champ de la classe

        #region propriétés de la classe
        //-----------------------------
        public string CheminFichier
        {
            get
            {
                if (cheminFichier == string.Empty)
                    OuvrirFichier(cheminFichier);
                return cheminFichier;
            }
            protected set
            {
                if (cheminFichier != value)
                {
                    cheminFichier = value;
                }
            }
        }
        public string RépertoireFichier
        {
            get
            {
                if (répertoireFichier == string.Empty)
                    OuvrirFichier();
                return répertoireFichier;
            }
            protected set
            {
                if (répertoireFichier != value)
                {
                    répertoireFichier = value;
                }
            }
        }

        public string NomFichier
        {
            get
            {
                if (nomFichier == string.Empty)
                    OuvrirFichier();
                return nomFichier;
            }
            set
            {
                if (nomFichier != value)
                {
                    nomFichier = value;
                }

            }
        }

        public string ExtensionFichier
        {
            get
            {
                if (extensionFichier == string.Empty)
                    OuvrirFichier();
                return extensionFichier;
            }
            protected set
            {
                if (extensionFichier != value)
                {
                    extensionFichier = value;
                }
            }
        }
        public bool FichierOuvert
        {
            get
            {
                return fichierOuvert;
            }
            protected set
            {
                if (fichierOuvert != value)
                    fichierOuvert = value;
            }
        }
        public OpenFileDialog ExplorateurDeFichier
        {
            get
            {
                return explorateurDeFichier;
            }
            protected set
            {
                if (explorateurDeFichier != value)
                    explorateurDeFichier = value;
            }
        }
        public SaveFileDialog SauvegardeFichier
        {
            get
            {
                return sauvegardeFichier;
            }
            protected set
            {
                if (sauvegardeFichier != value)
                    sauvegardeFichier = value;
            }
        }
        //--------------------------------
        #endregion propriétés de la classe

        #region méthodes de la classe
        //---------------------------
        public ClasseFichier()
        {
        }
        ~ClasseFichier()
        {
            nettoyageDesVariablesFichier();
            explorateurDeFichier = null;
            sauvegardeFichier = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                explorateurDeFichier.Dispose();
                sauvegardeFichier.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #region Ouverture de fichier
        //--------------------------
        /// <summary>
        /// Appel du dialogue ouverture de fichier
        /// </summary>
        protected bool OuvrirFichier(string titreFenetre)
        {
            ClasseGénérique = new ClasseGénérique();
            FichierOuvert = false;
            try
            {
                ExplorateurDeFichier = ClasseGénérique.dialogueExplorateur(ExplorateurDeFichier, titreFenetre);
                if (ExplorateurDeFichier.ShowDialog() != DialogResult.Cancel)
                {
                    CheminFichier = ExplorateurDeFichier.FileName;
                    RépertoireFichier = Path.GetDirectoryName(CheminFichier);
                    NomFichier = Path.GetFileNameWithoutExtension(CheminFichier);
                    ExtensionFichier = Path.GetExtension(CheminFichier);
                    fichierOuvert = true;
                }
            }
            catch (Exception erreur)
            {
                MessageBox.Show("Erreur:Impossible de lire le fichier sur le disque.\nErreur d'origine : " + erreur.Message);
                fichierOuvert = false;
            }
            return FichierOuvert;
        }

        protected bool OuvrirFichier()
        {
            FichierOuvert = OuvrirFichier("Explorateur de Fichier");
            return FichierOuvert;
        }
        //-----------------------------
        #endregion Ouverture de fichier
        protected void nettoyageDesVariablesFichier()
        {
            CheminFichier = string.Empty;
            RépertoireFichier = string.Empty;
            NomFichier = string.Empty;
            ExtensionFichier = string.Empty;
            FichierOuvert = false;
            ExplorateurDeFichier = null;
            SauvegardeFichier = null;
        }
        protected static void SuprimeToutesLesTachesExcel()
        {
            System.Diagnostics.Process[ ] processExcel = System.Diagnostics.Process.GetProcessesByName("Excel");
            foreach (System.Diagnostics.Process p in processExcel)
            {
                if (!string.IsNullOrEmpty(p.ProcessName))
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }
                }
            }
            System.Diagnostics.Process[ ] processConhost = System.Diagnostics.Process.GetProcessesByName("conhost");
            foreach (System.Diagnostics.Process p in processConhost)
            {
                if (!string.IsNullOrEmpty(p.ProcessName))
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }
                }
            }
        }
        //--------------------------------
        #endregion propriétés de la classe
    }
}
