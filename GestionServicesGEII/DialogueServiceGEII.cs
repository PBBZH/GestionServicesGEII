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
 *                     DialogueServiceGEII.cs
 * ================================================================ */
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using GestionServicesGEII.Librairie_BaseDeDonnées;
using GestionServicesGEII.Librairie_GEII;
using GestionServicesGEII.Librairie_Registre;
using GestionServicesGEII.Librairie_Fichier;
using GestionServicesGEII.Librairie_Générique;
using programme = System.Windows.Forms.Application;
using DataTable = System.Data.DataTable;

namespace GestionServicesGEII
{
    partial class DialogueServiceGEII : Form
    {
        private static ClasseBaseDeDonnées baseDeDonnées = new ClasseBaseDeDonnées();
        //private static FichierDeService Service = null;
        private static FichierDeSelection Selection = new FichierDeSelection();
        private static PasserelleBdD_vers_Excel transfertBdbVersExcel = new PasserelleBdD_vers_Excel();

        private ServiceGeii serviceGeii = new ServiceGeii();
        private DataSet dataSetExcel = new DataSet("Fichier de Service");
        private DataTable dataTableExcel = new DataTable("Table Excel");
        private DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

        private string ancienneDonnée = string.Empty;
        internal static ClasseBaseDeDonnées BaseDeDonnées
        {
            get
            {
                return baseDeDonnées;
            }
            set
            {
                baseDeDonnées = value;
            }
        }
        public ServiceGeii ServiceGeii
        {
            get
            {
                return serviceGeii;
            }
            set
            {
                serviceGeii = value;
            }
        }
        public DataSet DataSetExcel
        {
            get
            {
                return dataSetExcel;
            }
            protected set
            {
                dataSetExcel = value;
            }
        }
        public DataTable DataTableExcel
        {
            get
            {
                return dataTableExcel;
            }
            protected set
            {
                dataTableExcel = value;
            }
        }
        internal static PasserelleBdD_vers_Excel TransfertBdbVersExcel
        {
            get
            {
                return transfertBdbVersExcel;
            }
            set
            {
                transfertBdbVersExcel = value;
            }
        }

        public DialogueServiceGEII()
        {
            InitializeComponent();
            InititialiseFormatEntetes();
        }
        ~DialogueServiceGEII()
        {
            nettoyageDuFormulaire();
        }
        private void menuOuvrirFichier_Click(object sender, EventArgs e)
        {
            FichierDeService.Service = new ClasseExcel();
            FichierDeService.Service.OuvrirFichierExcel("Fichier de Service");
            if (FichierDeService.Service.FichierOuvert == true)
                BaseDeDonnées.AfficherListesDonnées(FeuilleClasseur);

        }
        private void menuFichierFermer_Click(object sender, EventArgs e)
        {
            if (FichierDeService.Service != null)
            {
                FichierDeService.Service.fermerClasseurExcel();
            }
            this.nettoyageFenetreEtDonnées();
        }
        private void menuFichierQuitter_Click(object sender, EventArgs e)
        {
            if (FichierDeService.Service != null)
            {
                FichierDeService.Service.fermerClasseurExcel();
            }
            this.nettoyageFenetreEtDonnées();
            programme.Exit();
        }
        private void OutilsBoutonFermer_Click(object sender, EventArgs e)
        {
            if (FichierDeService.Service != null)
            {
                FichierDeService.Service.fermerClasseurExcel();
            }
            this.nettoyageFenetreEtDonnées();
        }

        private void ServiceGEII_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (FichierDeService.Service != null)
            {
                FichierDeService.Service.fermerClasseurExcel();
            }
            this.nettoyageFenetreEtDonnées();
            programme.Exit();
        }
        private void menuFichierEnregistrer_Click(object sender, EventArgs e)
        {
            //FichierDeService.enregistrerFichier();
        }
        private void enregistrerSous()
        {
            //FichierDeService.enregistrerFichier(false);
        }
        private void menuFichierEnregisterSous_Click(object sender, EventArgs e)
        {
            //FichierDeService.enregistrerFichier(false);
        }
        private void nettoyageDuFormulaire()
        {
            this.nettoyageFenetreEtDonnées();
        }
        private void nettoyageFenetreEtDonnées()
        {
            try
            {
                this.FeuilleClasseur.DataSource = null;
                this.VisualisationDonnée1.DataSource = null;
                this.Semestre.DataSource = null;
                this.UE.DataSource = null;
                this.Module.DataSource = null;
                this.Formation.DataSource = null;
                this.Cours.DataSource = null;
                this.Noms.DataSource = null;
                this.Titulaires.DataSource = null;
                this.Vacataires.DataSource = null;
                this.Commentaires.DataSource = null;

                DataSetExcel = null;
                DataTableExcel = null;
                //FichierDeService = new ClasseExcel();
                //FichierDeSelection = new ClasseExcel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void afficherToutesLesTables_Click(object sender, EventArgs e)
        {
            //BaseDeDonnées.EnsembleDesTables(VisualisationDonnée1);
        }
        private void AfficherConnexionNomsTables_Click(object sender, EventArgs e)
        {
            VisualisationDonnée1.DataSource = BaseDeDonnées.EnsembleDesTables();
        }
        private void élémentMenuGEii_1_Click(object sender, EventArgs e)
        {
            ConfigurationEffectifs ConfigurationEffectifs = new ConfigurationEffectifs();
            ConfigurationEffectifs.titreFenetre.Text = "PRÉVISIONNEL DES EFFECTIS GEII 1";
            ConfigurationEffectifs.ShowDialog();
            étatEffectifGeii1.Text = ServiceGeii.Geii_1.Formation.FI.NbEtudiants.ToString() + " étudiants : ";
            étatNbGroupeTd.Text = ServiceGeii.Geii_1.Formation.FI.NbGroupeTD + " groupes de TD, ";
            étatNbGroupeTp.Text = ServiceGeii.Geii_1.Formation.FI.NbGroupeTP + " groupes de TP\t";
            étatTitreEffectifGeii1.Visible
                = étatEffectifGeii1.Visible
                = étatNbGroupeTd.Visible
                = étatNbGroupeTp.Visible
                = true;
        }
        private void menuFichierImprimer_Click(object sender, EventArgs e)
        {
            //Open the print dialog
            //PrintDialog printDialog = new PrintDialog();
            //printDialog.Document = printDocument1;
            //printDialog.UseEXDialog = true;
            ////Get the document
            //if (DialogResult.OK == printDialog.ShowDialog())
            //{
            //    printDocument1.DocumentName = "Test Page Print";
            //    printDocument1.Print();
            //}
        }
        private void menuFichierAperçuAvantImpression_Click(object sender, EventArgs e)
        {

        }
        private void boutonModifier_Click(object sender, EventArgs e)
        {
            string nomColonne = string.Empty;
            if (VisualisationDonnée1.CurrentCell.Value != null)
            {
                for (ushort p = 0; p < VisualisationDonnée1.Columns.Count; p++)
                {
                    if (VisualisationDonnée1.Columns[p].HeaderText == "Noms")
                    {
                        if (VisualisationDonnée1.CurrentCell.ColumnIndex == p)
                        {
                            ancienneDonnée = VisualisationDonnée1.CurrentCell.Value.ToString();
                            VisualisationDonnée1.CurrentCell.Value = Noms.Text;
                        }
                        else
                        {
                            MessageBox.Show("Veuillez saisir la case dans la colonne 'Noms'");
                        }
                    }
                }
            }
        }
        private void validationFeuilleClasseur_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FichierDeService.Service.changeFeuilleVisible(FeuilleClasseur);
        }
        private void InititialiseFormatEntetes()
        {
            // Défini le format des entêtes de colonnes.
            // -----------------------------------------
            columnHeaderStyle.ForeColor = Color.LightBlue;
            columnHeaderStyle.Font = new System.Drawing.Font("Tahoma", 12, FontStyle.Bold);
            columnHeaderStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            VisualisationDonnée1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            VisualisationDonnée1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        private void InititialiseFormatTexteParDefaut()
        {
            // Défini le format des cellules dans la vue
            // -----------------------------------------
            VisualisationDonnée1.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 10, FontStyle.Regular);
            VisualisationDonnée1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            VisualisationDonnée1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        internal void formatageCouleurLignes()
        {
            string typeDeCours = string.Empty;
            DataGridViewCell cellule = null;
            int nbLignes = 0;
            int nbColonnes = 0;
            int indexColonne = 0;

            nbColonnes = VisualisationDonnée1.Columns.Count;
            foreach (DataGridViewColumn p in VisualisationDonnée1.Columns)
                if ((p.HeaderText) == "Cours")
                {
                    indexColonne = p.Index;
                    break;
                }
            nbLignes = VisualisationDonnée1.Rows.Count;

            foreach (DataGridViewRow ligne in VisualisationDonnée1.Rows)
            {
                cellule = VisualisationDonnée1.Rows[ligne.Index].Cells[indexColonne];
                if (cellule.Value != null)
                {
                    typeDeCours = cellule.Value.ToString();
                    switch (typeDeCours)
                    {
                        case "CM":
                            ligne.DefaultCellStyle.BackColor = Color.LightBlue;
                            break;
                        case "TD":
                            ligne.DefaultCellStyle.BackColor = Color.LightGreen;
                            break;
                        case "TP":
                            ligne.DefaultCellStyle.BackColor = Color.LightGray;
                            break;
                        default:
                            break;
                    }
                }
            }
            InititialiseFormatTexteParDefaut();
        }
        private void validationSemestre_SelectedIndexChanged(object sender, EventArgs e)
        {
            BaseDeDonnées.AfficherListesDonnées(UE, Semestre, Semestre.Name);
        }
        private void validationUE_SelectedIndexChanged(object sender, EventArgs e)
        {
            BaseDeDonnées.AfficherListesDonnées(Module, UE, UE.Name);
        }
        private void validationModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            BaseDeDonnées.AfficherListesDonnées(Cours);
        }
        private void validationFormation_SelectedIndexChanged(object sender, EventArgs e)
        {
            BaseDeDonnées.afficheMatière(UE, Module, Formation, Cours, "");
        }
        private void buttonEnregistrer_Click(object sender, EventArgs e)
        {
            TransfertBdbVersExcel.SauvegardeBaseDeDonnées_Vers_Excel(BaseDeDonnées.DataSetExcel);
        }
        private void boutonAnnuler_Click(object sender, EventArgs e)
        {
            string nomColonne = string.Empty;
            if (VisualisationDonnée1.CurrentCell.Value != null)
            {
                for (ushort p = 0; p < VisualisationDonnée1.Columns.Count; p++)
                {
                    if (VisualisationDonnée1.Columns[p].HeaderText == "Noms")
                    {
                        if (VisualisationDonnée1.CurrentCell.ColumnIndex == p)
                        {
                            VisualisationDonnée1.CurrentCell.Value = ancienneDonnée;
                        }
                        else
                        {
                            MessageBox.Show("Veuillez saisir la case dans la colonne 'Noms'");
                        }
                    }
                }
            }
        }
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaseDeDonnées.initialisationDesTables();
        }
        private void COURS_SelectionChangeCommitted(object sender, EventArgs e)
        {
            BaseDeDonnées.afficheMatière(UE, Module, Formation, Cours, "");
        }
        private void COURS_SelectedValueChanged(object sender, EventArgs e)
        {
            BaseDeDonnées.afficheMatière(UE, Module, Formation, Cours, "");
        }
        private void AfficherLesCritèresDeSélection_Click(object sender, EventArgs e)
        {
            BaseDeDonnées.initialisationDesTables();
        }
        private void ElementMenuConfigurationChemins_Click(object sender, EventArgs e)
        {
            FichierRegistre FichierConfiguration = new FichierRegistre();
            FichierConfiguration.titreFenetre.Text = "Gestionnaire de l'emplacement des fichiers";
            FichierConfiguration.Show();
        }

        private void AfficheModuleComplet_Click(object sender, EventArgs e)
        {
            BaseDeDonnées.afficheMatière(UE, Module, Cours, "");
        }

        private void DialogueServiceGEII_Load(object sender, EventArgs e)
        {

        }
    }
}
