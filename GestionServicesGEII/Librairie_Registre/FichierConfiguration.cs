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
 *                     FichierConfiguration.cs
 * ================================================================ */
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionServicesGEII.Librairie_Générique;
using System.Windows.Forms;

namespace GestionServicesGEII.Librairie_Registre
{
    public partial class FichierRegistre : Form
    {
        public FichierRegistre()
        {
            InitializeComponent();
        }
        public string RépertoireDeTravail
        {
            get
            {
                if (répertoireDeTravail.Text == "non défini")
                    répertoireDeTravail.Text = "Test";
                return répertoireDeTravail.Text;
            }
            set
            {
                if (répertoireDeTravail.Text != value)
                    répertoireDeTravail.Text = value;
            }
        }
        //public string NomRépertoireConfiguration
        //{
        //    get
        //    {
        //        if (nomRépertoireConfiguration == null)
        //            nomRépertoireConfiguration = FichierConfiguration.RépertoireDeTravail;
        //        return nomRépertoireConfiguration;
        //    }
        //    private set
        //    {
        //        if (nomRépertoireConfiguration != value)
        //            nomRépertoireConfiguration = value;
        //    }
        //}
    }
}
