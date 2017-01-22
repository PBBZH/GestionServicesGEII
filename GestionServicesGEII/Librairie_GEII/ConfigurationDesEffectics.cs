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
 *                     ConfigurationDesEffectics.cs
 * ================================================================ */
using System;
using System.Windows.Forms;
using GestionServicesGEII.Librairie_GEII;

namespace GestionServicesGEII
{
    partial class ConfigurationEffectifs : Form
	{
		private static FormationGEII Geii_1 = new FormationGEII();
		private static FormationGEII Geii_2 = new FormationGEII();
		private static FormationGEII LP_Sari = new FormationGEII();
		private static FormationGEII LP_MEE = new FormationGEII();

        private int nbEtudiantMaxParGroupeTP;
        private Int16 nbEtudiant = 0;
        private int nbGroupeTd;
        private int nbGroupeTp;
		//private int[] GroupeTP;
		//Bprivate int[] GroupeTd;

		private bool calculGroupeFait;

        public ConfigurationEffectifs()
		{
			InitializeComponent();
		}

		private void boutonGroupeConfig_Click(object sender, EventArgs e)
		{
			try
			{
				this.fenetreRésumé.Clear();
				nbEtudiant = Int16.Parse(this.saisieEffectif.Text);
				nbGroupeTd = 0;
				nbGroupeTp = 0;
				int[] groupeTp = { 0, 0, 0, 0, 0, 0 };
				int[] groupeTd = { 0, 0, 0, 0 };

				if(choixMax12.Checked == true)
				{
					nbEtudiantMaxParGroupeTP = 12;
				}
				else
				{
					nbEtudiantMaxParGroupeTP = 14;
				}
				if(nbEtudiant != 0)
				{
					// calcul des nombres de groupes de TP
					if((nbEtudiant % nbEtudiantMaxParGroupeTP) < 7)
						nbGroupeTp = nbEtudiant / nbEtudiantMaxParGroupeTP;
					else
						nbGroupeTp = (nbEtudiant / nbEtudiantMaxParGroupeTP) + 1;
					// calcul des nombres de groupes de TD
					if((nbGroupeTp % 2) != 0)
						nbGroupeTd = (nbGroupeTp / 2) + (nbGroupeTp % 2);
					else
						nbGroupeTd = nbGroupeTp / 2;
					// calcul des nombres d'étudiants par groupes de TP
					int baseCalculNbEtudiants = ((nbEtudiant /nbGroupeTp)/2)*2;
					int nbEtudiantsRestants = nbEtudiant - (baseCalculNbEtudiants * nbGroupeTp);
					int nbRestant = nbEtudiantsRestants;
					for(int i = 0; i < nbGroupeTp; i++)
					{
						if((nbRestant % 2) == 0)
						{
							if(nbRestant != 0)
							{
								groupeTp[i] = baseCalculNbEtudiants + 2;
								nbRestant -= 2;
							}
							else
								groupeTp[i] = baseCalculNbEtudiants;
						}
						else
						{
							groupeTp[i] = baseCalculNbEtudiants + 1;
							nbRestant--;
						}
					}
					for(int i = 0; i < nbGroupeTp; i++)
					{
						for (int j = i+1; j < nbGroupeTp; j++)
						{
							maxValeur(ref groupeTp[i], ref groupeTp[j]);
						}
					}
					int numeroGroupeTP = 11;
					int numeroGroupeTD = 11;

					this.fenetreRésumé.AppendText(nbEtudiant.ToString() + " étudiants prévus en première année\n");
					this.fenetreRésumé.AppendText("soit :\n");
					//constitution des groupe de TD
					//-----------------------------
					this.fenetreRésumé.AppendText("\t- " + nbGroupeTd + " groupe de TD\n");
					this.fenetreRésumé.AppendText("\t- " + nbGroupeTp + " groupe de TP\n\n");
					for(int i = 0; i < nbGroupeTd; i++)
					{
						groupeTd[i] = groupeTp[i] + groupeTp[nbGroupeTp - 1 - i];
						this.fenetreRésumé.AppendText("\t\t-> G" + (numeroGroupeTD + i).ToString() + " : " + groupeTd[i] + " étudiants\n");
						char j = 'A';
						{
							if (groupeTp[i] != 0)
								this.fenetreRésumé.AppendText("\t\t\t-> G" + (numeroGroupeTP + i).ToString() + j++ + " : " + groupeTp[i] + " étudiants\n");
							if (groupeTp[i] != 0)
								this.fenetreRésumé.AppendText("\t\t\t-> G" + (numeroGroupeTP + i).ToString() + j + " : " + (groupeTd[i] - groupeTp[i]) + " étudiants\n");
						}
					}
					calculGroupeFait = true;
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show
					(
						ex.Message +
						"\tEntrer le nombre d'étudiants prévus pour la promotion",
						"Alerte",
						MessageBoxButtons.RetryCancel,
						MessageBoxIcon.Hand,
						MessageBoxDefaultButton.Button1
					);
			}
		}

		private void maxValeur(ref int value1, ref int value2)
		{
			int max;
			if (value1 < value2)
			{
				max = value1;
				value1 = value2;
				value2 = max;
			}
		}
		

		private void boutonValiderConfig_Click(object sender, EventArgs e)
		{
			//if(nbEtudiant != 0)
			//{

				if(calculGroupeFait)
				{
					Geii_1.Formation.FI.NbEtudiants = nbEtudiant;
					Geii_1.Formation.FI.NbGroupeTD = nbGroupeTd;
					Geii_1.Formation.FI.NbGroupeTP = nbGroupeTp;
					base.DialogResult = DialogResult.OK;
				}
				else
					MessageBox.Show
						(
							"Vous devez effectuer le calcul des groupes",
							"Alerte",
							MessageBoxButtons.RetryCancel,
							MessageBoxIcon.Hand,
							MessageBoxDefaultButton.Button1
						);
			//}
			//else
			//	MessageBox.Show
			//		(
			//			"Entrer le nombre d'étudiants prévus pour la promotion",
			//			"Alerte",
			//			MessageBoxButtons.RetryCancel,
			//			MessageBoxIcon.Hand,
			//			MessageBoxDefaultButton.Button1
			//		);
		}

		
		private void boutonFermer_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
		}

        private void choixMax12_CheckedChanged(object sender, EventArgs e)
		{
			if(choixMax14.Checked == true)
				choixMax14.Checked = false;
			else
				choixMax12.Checked = true;
		}

		private void choixMax14_CheckedChanged(object sender, EventArgs e)
		{
			if(choixMax12.Checked == true)
				choixMax12.Checked = false;
			else
				choixMax14.Checked = true;
		}

        private void chkEffacerFenêtre_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEffacerFenêtre.Checked == true)
            {
                chkEffacerFenêtre.Checked = false;
            }
        }
    }
}
