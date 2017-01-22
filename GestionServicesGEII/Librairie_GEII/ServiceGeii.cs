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
 *                     ServiceGeii.cs
 * ================================================================ */
using System.Data;
using DataTable = System.Data.DataTable;

namespace GestionServicesGEII.Librairie_GEII
{
    public class ServiceGeii
    {
        private static DataTable semestre, uE, module, formation, groupe, noms, cours, titulaires, vacataires = null;
        private static DataSet nomSemestre, nomUE, nomModule, nomFormation, nomGroupe, nomTitulaires, nomVacataires = null;
        private FormationGEII geii_1 = new FormationGEII();
        private FormationGEII geii_2 = new FormationGEII();
        private FormationGEII lP_Sari = new FormationGEII();
        private FormationGEII lP_MEE = new FormationGEII();


        #region Instanciation des variables
        //---------------------------------
        public DataSet NomSemestre
        {
            get
            {
                return nomSemestre;
            }
            protected set
            {
                if (nomSemestre != value)
                    nomSemestre = value;
            }
        }
        public DataTable Semestre
        {
            get
            {
                return semestre;
            }
            protected set
            {
                if (semestre != value)
                    semestre = value;
            }
        }
        public DataSet NomFormation
        {
            get
            {
                return nomFormation;
            }
            protected set
            {
                if (nomFormation != value)
                    nomFormation = value;
            }
        }
        public DataTable Formation
        {
            get
            {
                return formation;
            }
            protected set
            {
                if (formation != value)
                    formation = value;
            }
        }
        public DataSet NomUE
        {
            get
            {
                return nomUE;
            }
            protected set
            {
                if (nomUE != value)
                    nomUE = value;
            }
        }
        public DataTable UE
        {
            get
            {
                return uE;
            }
            protected set
            {
                if (uE != value)
                    uE = value;
            }
        }
        public DataSet NomModule
        {
            get
            {
                return nomModule;
            }
            protected set
            {
                if (nomModule != value)
                    nomModule = value;
            }
        }
        public DataTable Module
        {
            get
            {
                return module;
            }
            protected set
            {
                if (module != value)
                    module = value;
            }
        }
        public DataTable Cours
        {
            get
            {
                return cours;
            }
            protected set
            {
                if (cours != value)
                    cours = value;
            }
        }
        public DataSet NomNoms
        {
            get
            {
                return nomGroupe;
            }
            protected set
            {
                if (nomGroupe != value)
                    nomGroupe = value;
            }
        }
        public DataTable Noms
        {
            get
            {
                return noms;
            }
            protected set
            {
                if (noms != value)
                    noms = value;
            }
        }
        public DataSet NomGroupe
        {
            get
            {
                return nomGroupe;
            }
            protected set
            {
                if (nomGroupe != value)
                    nomGroupe = value;
            }
        }
        public DataTable Groupe
        {
            get
            {
                return groupe;
            }
            protected set
            {
                if (groupe != value)
                    groupe = value;
            }
        }
        public DataSet NomTitulaires
        {
            get
            {
                return nomTitulaires;
            }
            protected set
            {
                if (nomTitulaires != value)
                    nomTitulaires = value;
            }
        }
        public DataTable Titulaires
        {
            get
            {
                return titulaires;
            }
            protected set
            {
                if (titulaires != value)
                    titulaires = value;
            }
        }
        public DataSet NomVacataires
        {
            get
            {
                return nomVacataires;
            }
            protected set
            {
                if (nomVacataires != value)
                    nomVacataires = value;
            }
        }
        public DataTable Vacataires
        {
            get
            {
                return vacataires;
            }
            protected set
            {
                if (vacataires != value)
                    vacataires = value;
            }
        }
        public FormationGEII Geii_1
        {
            get
            {
                return geii_1;
            }
            private set
            {
                if (geii_1 != value)
                    geii_1 = value;
            }
        }
        private FormationGEII Geii_2
        {
            get
            {
                return geii_2;
            }
            set
            {
                if (geii_2 != value)
                    geii_2 = value;
            }
        }
        private FormationGEII LP_Sari
        {
            get
            {
                return lP_Sari;
            }
            set
            {
                if (lP_Sari != value)
                    lP_Sari = value;
            }
        }
        private FormationGEII LP_MEE
        {
            get
            {
                return lP_MEE;
            }
            set
            {
                if (lP_MEE != value)
                    lP_MEE = value;
            }
        }

        //------------------------------------
        #endregion Instanciation des variables

        public void nettoyageDesDonnées()
        {
            NomSemestre = NomUE = NomFormation = NomModule = NomTitulaires = NomVacataires = null;
            Semestre = UE = Formation = Module = Titulaires = Vacataires = Cours = null;
        }
        public ServiceGeii()
        {
        }
    }
}
