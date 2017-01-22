/* ===================================================================
 * copyright : PB_BZH 2017
 * contact : mailto:admin@pbbzh.fr
 * ===================================================================
 *  IUT de BREST : D�partement GEII
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
 *                     ModificationDuRegistre.cs
 * ================================================================ */
using System;
using Microsoft.Win32;      
using System.Windows.Forms; 

namespace Utility.ModifyRegistry
{
    /// <summary>
    /// Une classe utile pour lire / �crire / supprimer / compter les cl�s de registre
    /// </summary>
    public class ModificationDuRegistre
	{
		private bool signaleErreur = false;
		/// <summary>
		/// une propri�t� pour montrer ou cacher les erreurs 
		/// (defaut = false)
		/// </summary>
		public bool SignaleErreur
		{
			get
            {
                return signaleErreur;
            }
			set
            {
                signaleErreur = value;
            }
		}

		private string sousCl� = "SOFTWARE\\" + Application.ProductName.ToUpper();
        /// <summary>
        /// une propri�t�e pour donner une valeur � SousCl�.
        /// (defaut = "SOFTWARE\\" + Application.NomProduit.ToUpper())
        /// </summary>
        public string SousCl�
		{
			get
            {
                return sousCl�;
            }
			set
            {
                sousCl� = value;
            }
		}

		private RegistryKey cl�BaseDeRegistre = Registry.LocalMachine;
        /// <summary>
        /// une propri�t�e pour donner une valeur � la cl� dans le registre.
        /// (defaut = Registry.LocalMachine)
        /// </summary>
        public RegistryKey Cl�BaseDeRegistre
		{
			get
            {
                return cl�BaseDeRegistre;
            }
			set
            {
                cl�BaseDeRegistre = value;
            }
		}

		/// <summary>
		/// Lire une cl� du registre.
		/// entr�e: NomCl� (string)
		/// sortie: valeur (string) 
		/// </summary>
		public string Lire(string NomCl�)
		{
			RegistryKey _nomCl� = cl�BaseDeRegistre ;
			RegistryKey _nomSousCl� = _nomCl�.OpenSubKey(sousCl�);
			if ( _nomSousCl� == null )
			{
				return null;
			}
			else
			{
				try 
				{
					return (string)_nomSousCl�.GetValue(NomCl�.ToUpper());
				}
				catch (Exception e)
				{
					AfficheMessageErreur(e, "Lire dans le registre : " + NomCl�.ToUpper());
					return null;
				}
			}
		}	

		/// <summary>
		/// Ecrire une valeur dans une cl� du registre.
		/// entr�e: NomCl� (string) , Valeur (object)
		/// sortie: true or false 
		/// </summary>
		public bool Ecrire(string NomCl�, object Valeur)
		{
			try
			{
				RegistryKey _nomCl� = cl�BaseDeRegistre ;
				RegistryKey _nomSousCl� = _nomCl�.CreateSubKey(sousCl�);
				_nomSousCl�.SetValue(NomCl�.ToUpper(), Valeur);
				return true;
			}
			catch (Exception e)
			{
				AfficheMessageErreur(e, "Ecrire dans le registre : " + NomCl�.ToUpper());
				return false;
			}
		}

		/// <summary>
		/// Effacer une cl� du registre.
		/// entr�e: NomCl� (string)
		/// sortie: true or false 
		/// </summary>
		public bool EffaceCl�(string NomCl�)
		{
			try
			{
				RegistryKey _nomCl� = cl�BaseDeRegistre ;
				RegistryKey _nomSousCl� = _nomCl�.CreateSubKey(sousCl�);
				if ( _nomSousCl� == null )
					return true;
				else
					_nomSousCl�.DeleteValue(NomCl�);

				return true;
			}
			catch (Exception e)
			{
				AfficheMessageErreur(e, "Cl� �ffac�e : " + sousCl�);
				return false;
			}
		}

		/// <summary>
		/// Effacer une cl� et tous ces enfants dans le registre.
		/// entr�e: void
		/// sortie: true or false 
		/// </summary>
		public bool EffaceSousCl�()
		{
			try
			{
				RegistryKey _nomCl� = cl�BaseDeRegistre ;
				RegistryKey _nomSousCl� = _nomCl�.OpenSubKey(sousCl�);
				if ( _nomSousCl� != null )
					_nomCl�.DeleteSubKeyTree(sousCl�);

				return true;
			}
			catch (Exception e)
			{
				AfficheMessageErreur(e, "Efface sous-cl� : " + sousCl�);
				return false;
			}
		}

		/// <summary>
		/// compte le nombre de sous cl�s dans la cl� courante.
		/// entr�e: void
		/// sortie: number of subkeys
		/// </summary>
		public int CompteLesSousCl�()
		{
			try
			{
				RegistryKey _nomCl� = cl�BaseDeRegistre ;
				RegistryKey _nomSousCl� = _nomCl�.OpenSubKey(sousCl�);
				if ( _nomSousCl� != null )
					return _nomSousCl�.SubKeyCount;
				else
					return 0; 
			}
			catch (Exception e)
			{
				AfficheMessageErreur(e, "Recherche du nombre de sous-cl�s : " + sousCl�);
				return 0;
			}
		}

		/// <summary>
		/// Compte le nombre de valeur dans une cl� du registre.
		/// entr�e: void
		/// sortie: number of keys
		/// </summary>
		public int CompteValeur()
		{
			try
			{
				RegistryKey _nomCl� = cl�BaseDeRegistre ;
				RegistryKey _nomSousCl� = _nomCl�.OpenSubKey(sousCl�);
				if ( _nomSousCl� != null )
					return _nomSousCl�.ValueCount;
				else
					return 0; 
			}
			catch (Exception e)
			{
				AfficheMessageErreur(e, "Recherche du nombre de cl�s : " + sousCl�);
				return 0;
			}
		}
		
		private void AfficheMessageErreur(Exception e, string Title)
		{
			if (signaleErreur == true)
				MessageBox.Show(e.Message,
								Title
								,MessageBoxButtons.OK
								,MessageBoxIcon.Error);
		}
	}
}
