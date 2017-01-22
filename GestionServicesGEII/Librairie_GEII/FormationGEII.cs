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
 *                     FormationGEII.cs
 * ================================================================ */
namespace GestionServicesGEII.Librairie_GEII
{
	public class Promotion
    {
		private int nbEtudiants;
        private int nbGroupeTD;
        private int nbGroupeTP;

        public int NbEtudiants
        {
            get
            {
                return nbEtudiants;
            }

            set
            {
                if (nbEtudiants != value)
                    nbEtudiants = value;
            }
        }
        public int NbGroupeTD
        {
            get
            {
                return nbGroupeTD;
            }

            set
            {
                if (nbGroupeTD != value)
                    nbGroupeTD = value;
            }
        }
        public int NbGroupeTP
        {
            get
            {
                return nbGroupeTP;
            }

            set
            {
                if (nbGroupeTP != value)
                    nbGroupeTP = value;
            }
        }
    }

    public class Formation
    {
		private Promotion fi = new Promotion();
		private Promotion apprentis = new Promotion();
        private Promotion aternance = new Promotion();
        private Promotion sagema = new Promotion();

        public Promotion FI
        {
            get
            {
                return fi;
            }

            set
            {
                fi = value;
            }
        }
        public Promotion Apprentis
        {
            get
            {
                return apprentis;
            }

            set
            {
                apprentis = value;
            }
        }
        public Promotion Aternance
        {
            get
            {
                return aternance;
            }

            set
            {
                aternance = value;
            }
        }
        public Promotion Sagema
        {
            get
            {
                return sagema;
            }

            set
            {
                sagema = value;
            }
        }
    }

    public class FormationGEII
    {
        private static Formation formation = new Formation();

        public FormationGEII()
		{

		}
      
        public Formation Formation
        {
            get
            {
                return formation;
            }

            set
            {
                formation = value;
            }
        }
    }

}

