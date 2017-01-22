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
 *                     ClasseBaseDeDonnées.cs
 * ================================================================ */
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using GestionServicesGEII.Librairie_Fichier;
using GestionServicesGEII.Librairie_Générique;
using DataTable = System.Data.DataTable;

namespace GestionServicesGEII.Librairie_BaseDeDonnées
{
    class ClasseBaseDeDonnées : ClasseGénérique, IDisposable
    {
        #region Champ de la classe
        //------------------------
        private static DataSet dataSetExcel = new DataSet();
        private static DataTable dataTableExcel = new DataTable();
        private DialogueServiceGEII dialogueServiceGEII = new DialogueServiceGEII();
        //---------------------------
        #endregion Champ de la classe

        #region Propriétés de la classe
        //-----------------------------
        public DataSet DataSetExcel
        {
            get
            {
                if (dataSetExcel == null)
                    dataSetExcel = new DataSet();
                return dataSetExcel;
            }
            set
            {
                if (dataSetExcel != value)
                    dataSetExcel = value;
            }
        }
        public DataTable DataTableExcel
        {
            get
            {
                return dataTableExcel;
            }
            set
            {
                if (dataTableExcel != value)
                    dataTableExcel = value;
            }
        }
        internal DialogueServiceGEII DialogueServiceGEII
        {
            get
            {
                return dialogueServiceGEII;
            }
            set
            {
                if (dialogueServiceGEII != value)
                    dialogueServiceGEII = value;
            }
        }
        //--------------------------------
        #endregion Propriétés de la classe

        #region méthodes de la classe
        //-------------------------
        public ClasseBaseDeDonnées()
        {
            //FichierDeService FichierDeService = new FichierDeService();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                dialogueServiceGEII.Close();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public DataSet LectureFichier(ClasseExcel FichierDialogue)
        {
            string nomTable = string.Empty;
            string[ ] tablesExcel = RechercheNomFeuilleExcel(FichierDialogue);
            string conStr = FichierDialogue.ConStr;

            using (OleDbConnection connectionExcel = new OleDbConnection(conStr))
            {
                try
                {
                    connectionExcel.Open();
                    foreach (string nomFeuilleExcel in tablesExcel)
                    {
                        try
                        {
                            DataTable dataTable = new DataTable(nomFeuilleExcel);
                            string requete = string.Format("SELECT * FROM [{0}]", nomFeuilleExcel);
                            OleDbDataAdapter adaptateurExcel = new OleDbDataAdapter(requete, conStr);
                            OleDbCommand commande = new OleDbCommand(requete, connectionExcel);
                            adaptateurExcel.Fill(dataTable);
                            if (DataSetExcel.Tables.Contains(dataTable.TableName) == false)
                                DataSetExcel.Tables.Add(dataTable);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    connectionExcel.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return DataSetExcel;
        }
        public DataSet LectureFichier()
        {
            LectureFichier(FichierDeService.Service);
            LectureFichier(FichierDeSelection.Selection);
            return DataSetExcel;
        }
        protected static string[ ] RechercheNomFeuilleExcel(ClasseExcel FichierDialogue)
        {
            string[ ] nomFeuilleExcel = { };
            string conStr = FichierDialogue.ConStr;
            using (OleDbConnection connectionExcel = new OleDbConnection(conStr))
            {
                try
                {
                    connectionExcel.Open();
                    using (DataTable nomDataTable = connectionExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null))
                    {
                        if (nomDataTable == null)
                            return null;
                        nomFeuilleExcel = new string[nomDataTable.Rows.Count];
                        uint i = 0;
                        foreach (DataRow ligne in nomDataTable.Rows)
                        {
                            nomFeuilleExcel[i++] = ligne["TABLE_NAME"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return nomFeuilleExcel;
        }

        #region Lecture de la base de données en fonction des paramètres sélectionnés dans les combobox
        //-----------------------------------
        /// <summary>
        /// Lecture de la base de données en fonction 
        /// des paramètres sélectionnés dans les combobox
        /// </summary>
        /// <param name="table"></param>
        /// <param name="liste_1"></param>
        /// <param name="tableDeDonnée"></param>
        public void LireDonnées(
            DataGridView table, ComboBox liste_1, string tableDeDonnée)
        {
            if (tableDeDonnée == "")
                tableDeDonnée = "Table_complete";
            string selection_1 = liste_1.Text;
            string champs_1 = liste_1.Name;
            string filtreliste_1 = string.Format(("{0}  = '{1}'"), champs_1, selection_1);

            string filtreListe = filtreliste_1;

            if (liste_1.DisplayMember != string.Empty)
            {
                BindingSource rechercheChamps = new BindingSource();
                DataTable Table_complete = DataSetExcel.Tables[tableDeDonnée];
                string nomTrouvé = String.Empty;

                rechercheChamps.Filter = filtreListe;
                rechercheChamps.DataSource = Table_complete;
                rechercheChamps.Sort = "Cours";

                table.DataSource = Table_complete;
            }
        }
        public void LireDonnées(
            DataGridView table, ComboBox liste_1, ComboBox liste_2, ComboBox liste_3, string tableDeDonnée)
        {
            if (tableDeDonnée == "")
                tableDeDonnée = "Table_complete";
            string selection_1 = liste_1.Text;
            string champs_1 = liste_1.Name;
            string filtreliste_1 = string.Format(("{0}  = '{1}'"), champs_1, selection_1);

            string selection_2 = liste_2.Text;
            string champs_2 = liste_2.Name;
            string filtreliste_2 = string.Format(("{0}  = '{1}'"), champs_2, selection_2);

            string selection_3 = liste_3.Text;
            string champs_3 = liste_3.Name;
            string filtreliste_3 = string.Format(("{0}  = '{1}'"), champs_3, selection_3);

            string filtreListe = filtreliste_1 + " and " + filtreliste_2 + " and " + filtreliste_3;

            if ((liste_1.DisplayMember != string.Empty) && (liste_2.DisplayMember != string.Empty) && (liste_3.DisplayMember != string.Empty))
            {
                BindingSource rechercheChamps = new BindingSource();
                DataTable Table_complete = DataSetExcel.Tables[tableDeDonnée];
                string nomTrouvé = String.Empty;

                rechercheChamps.Filter = filtreListe;
                rechercheChamps.DataSource = Table_complete;
                rechercheChamps.Sort = "Cours";

                table.DataSource = Table_complete;
            }
        }
        public void LireDonnées(
            DataGridView table, ComboBox liste_1, ComboBox liste_2, ComboBox liste_3, ComboBox liste_4, string tableDeDonnée)
        {
            if (tableDeDonnée == "")
                tableDeDonnée = "Table_complete";
            string selection_1 = liste_1.Text;
            string champs_1 = liste_1.Name;
            string filtreliste_1 = string.Format(("{0}  = '{1}'"), champs_1, selection_1);

            string selection_2 = liste_2.Text;
            string champs_2 = liste_2.Name;
            string filtreliste_2 = string.Format(("{0}  = '{1}'"), champs_2, selection_2);

            string selection_3 = liste_3.Text;
            string champs_3 = liste_3.Name;
            string filtreliste_3 = string.Format(("{0}  = '{1}'"), champs_3, selection_3);

            string selection_4 = liste_4.Text;
            string champs_4 = liste_4.Name;
            string filtreliste_4 = string.Format(("{0}  = '{1}'"), champs_4, selection_4);

            string filtreListe = filtreliste_1 + " and " + filtreliste_2 + " and " + filtreliste_3 + " and " + filtreliste_4;

            if ((liste_1.DisplayMember != string.Empty) && (liste_2.DisplayMember != string.Empty) 
                && (liste_3.DisplayMember != string.Empty) && (liste_4.DisplayMember != string.Empty))
            {
                BindingSource rechercheChamps = new BindingSource();

                DataSet dataSet = new DataSet();
                DataTable tableComplete = DataSetExcel.Tables[tableDeDonnée];

                rechercheChamps.Filter = filtreListe;
                rechercheChamps.DataSource = tableComplete;
                table.DataSource = rechercheChamps;
            }
        }
        //-----------------------------------
        #endregion Lecture de la base de données
        public DataTable EnsembleDesTables()
        {
            string conStr = FichierDeSelection.Selection.ConStr;
            DataTable schémaTable = null;
            using (OleDbConnection connexionExcel = new OleDbConnection(conStr))
            {
                try
                {
                    connexionExcel.Open();
                    using (OleDbCommand commandeExcel = new OleDbCommand())
                    {
                        commandeExcel.Connection = connexionExcel;
                        try
                        {
                            schémaTable = connexionExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
                                new object[ ] { null, null, null, null });

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            return schémaTable;
        }
        #region fonctions affichage des listes déroulantes de données
        //-----------------------------------------------------------
        internal void initialisationDesTables()
        {
            if (DataSetExcel.Tables.Contains("Formation") == false)
                DataSetExcel = LectureFichier(FichierDeSelection.Selection);
            AfficherListesDonnées(DialogueServiceGEII.Semestre);
            AfficherListesDonnées(DialogueServiceGEII.Formation);
            AfficherListesDonnées(DialogueServiceGEII.Noms);
            AfficherListesDonnées(DialogueServiceGEII.Titulaires);
            AfficherListesDonnées(DialogueServiceGEII.Vacataires);
            AfficherListesDonnées(DialogueServiceGEII.Commentaires);
            if (DataSetExcel.Tables.Contains("Table_complete") == false)
            {
                DataSetExcel = LectureFichier(FichierDeService.Service);
            }
            afficheMatière(DialogueServiceGEII.UE, DialogueServiceGEII.Module, DialogueServiceGEII.Formation, "");
            DialogueServiceGEII.formatageCouleurLignes();
        }
        internal void afficheDonneesTriees(ComboBox CB1, ComboBox tableSelecton)
        {
            string selection = CB1.Text;
            string champ = CB1.DisplayMember;
            AfficherListesDonnées(CB1, tableSelecton);
            LireDonnées(DialogueServiceGEII.VisualisationDonnée1, CB1, "");
            DialogueServiceGEII.formatageCouleurLignes();
        }
        internal void afficheDonneesTriees(ComboBox CB1, ComboBox CB2, string tableSelection)
        {
            string selection = CB1.Text;
            string champ = CB1.DisplayMember;
            AfficherListesDonnées(CB1, CB2, "UE");
            LireDonnées(DialogueServiceGEII.VisualisationDonnée1, CB1, "");
            DialogueServiceGEII.formatageCouleurLignes();
        }
        internal void afficheMatière(ComboBox CB1, ComboBox CB2, ComboBox CB3, String tableSelecton)
        {
            LireDonnées(DialogueServiceGEII.VisualisationDonnée1, CB1, CB2, CB3, tableSelecton);
            DialogueServiceGEII.formatageCouleurLignes();
        }
        internal void afficheMatière(ComboBox CB1, ComboBox CB2, ComboBox CB3, ComboBox CB4, string table)
        {
            LireDonnées(DialogueServiceGEII.VisualisationDonnée1, CB1, CB2, CB3, CB4, "");
            DialogueServiceGEII.formatageCouleurLignes();
        }
        public void AfficherListesDonnées(ComboBox liste, ComboBox critèreDeSelection = null, string table = null)
        {
            switch (liste.Name)
            {
                case "FeuilleClasseur":
                    if (FichierDeService.Service.NomFichier != string.Empty)
                        FichierDeService.Service.RempliComboBoxFeuilleExcel
                            (
                                liste,
                                FichierDeService.Service.NbFeuille,
                                FichierDeService.Service.Feuille,
                                FichierDeService.Service.NomFeuille
                            );
                    break;
                case "Semestre":
                    DialogueServiceGEII.Semestre = rempliComboBox(liste, DataSetExcel, liste.Name);
                    break;
                case "Formation":
                    DialogueServiceGEII.Formation = rempliComboBox(liste, DataSetExcel, liste.Name);
                    break;
                case "Noms":
                    DialogueServiceGEII.Noms = rempliComboBox(liste, DataSetExcel, liste.Name);
                    break;
                case "Titulaires":
                    DialogueServiceGEII.Titulaires = rempliComboBox(liste, DataSetExcel, liste.Name);
                    break;
                case "Vacataires":
                    DialogueServiceGEII.Vacataires = rempliComboBox(liste, DataSetExcel, liste.Name);
                    break;
                case "Cours":
                    DialogueServiceGEII.Cours = rempliComboBox(liste, DataSetExcel, liste.Name);
                    break;
                case "Commentaires":
                    DialogueServiceGEII.Commentaires = rempliComboBox(liste, DataSetExcel, liste.Name);
                    break;
                case "UE":
                    DialogueServiceGEII.UE = rempliComboBox(liste, DataSetExcel, liste.Name, table, critèreDeSelection);
                    break;
                case "Module":
                    DialogueServiceGEII.Module = rempliComboBox(liste, DataSetExcel, liste.Name, table, critèreDeSelection);
                    break;
                default:
                    MessageBox.Show("la variable " + liste.Name + " est introuvable");
                    break;
            }
        }
        //--------------------------------------------------------------
        #endregion fonctions affichage des listes déroulantes de données
        //------------------------------
        #endregion méthodes de la classe

    }
}
