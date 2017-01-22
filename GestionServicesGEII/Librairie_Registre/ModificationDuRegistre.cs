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
 *                     ModificationDuRegistre.cs
 * ================================================================ */
using System;
using Microsoft.Win32;      
using System.Windows.Forms; 

namespace Utility.ModifyRegistry
{
    /// <summary>
    /// Une classe utile pour lire / écrire / supprimer / compter les clés de registre
    /// </summary>
    public class ModificationDuRegistre
	{
		private bool signaleErreur = false;
		/// <summary>
		/// une propriété pour montrer ou cacher les erreurs 
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

		private string sousClé = "SOFTWARE\\" + Application.ProductName.ToUpper();
        /// <summary>
        /// une propriétée pour donner une valeur à SousClé.
        /// (defaut = "SOFTWARE\\" + Application.NomProduit.ToUpper())
        /// </summary>
        public string SousClé
		{
			get
            {
                return sousClé;
            }
			set
            {
                sousClé = value;
            }
		}

		private RegistryKey cléBaseDeRegistre = Registry.LocalMachine;
        /// <summary>
        /// une propriétée pour donner une valeur à la clé dans le registre.
        /// (defaut = Registry.LocalMachine)
        /// </summary>
        public RegistryKey CléBaseDeRegistre
		{
			get
            {
                return cléBaseDeRegistre;
            }
			set
            {
                cléBaseDeRegistre = value;
            }
		}

		/// <summary>
		/// Lire une clé du registre.
		/// entrée: NomClé (string)
		/// sortie: valeur (string) 
		/// </summary>
		public string Lire(string NomClé)
		{
			RegistryKey _nomClé = cléBaseDeRegistre ;
			RegistryKey _nomSousClé = _nomClé.OpenSubKey(sousClé);
			if ( _nomSousClé == null )
			{
				return null;
			}
			else
			{
				try 
				{
					return (string)_nomSousClé.GetValue(NomClé.ToUpper());
				}
				catch (Exception e)
				{
					AfficheMessageErreur(e, "Lire dans le registre : " + NomClé.ToUpper());
					return null;
				}
			}
		}	

		/// <summary>
		/// Ecrire une valeur dans une clé du registre.
		/// entrée: NomClé (string) , Valeur (object)
		/// sortie: true or false 
		/// </summary>
		public bool Ecrire(string NomClé, object Valeur)
		{
			try
			{
				RegistryKey _nomClé = cléBaseDeRegistre ;
				RegistryKey _nomSousClé = _nomClé.CreateSubKey(sousClé);
				_nomSousClé.SetValue(NomClé.ToUpper(), Valeur);
				return true;
			}
			catch (Exception e)
			{
				AfficheMessageErreur(e, "Ecrire dans le registre : " + NomClé.ToUpper());
				return false;
			}
		}

		/// <summary>
		/// Effacer une clé du registre.
		/// entrée: NomClé (string)
		/// sortie: true or false 
		/// </summary>
		public bool EffaceClé(string NomClé)
		{
			try
			{
				RegistryKey _nomClé = cléBaseDeRegistre ;
				RegistryKey _nomSousClé = _nomClé.CreateSubKey(sousClé);
				if ( _nomSousClé == null )
					return true;
				else
					_nomSousClé.DeleteValue(NomClé);

				return true;
			}
			catch (Exception e)
			{
				AfficheMessageErreur(e, "Clé éffacée : " + sousClé);
				return false;
			}
		}

		/// <summary>
		/// Effacer une clé et tous ces enfants dans le registre.
		/// entrée: void
		/// sortie: true or false 
		/// </summary>
		public bool EffaceSousClé()
		{
			try
			{
				RegistryKey _nomClé = cléBaseDeRegistre ;
				RegistryKey _nomSousClé = _nomClé.OpenSubKey(sousClé);
				if ( _nomSousClé != null )
					_nomClé.DeleteSubKeyTree(sousClé);

				return true;
			}
			catch (Exception e)
			{
				AfficheMessageErreur(e, "Efface sous-clé : " + sousClé);
				return false;
			}
		}

		/// <summary>
		/// compte le nombre de sous clés dans la clé courante.
		/// entrée: void
		/// sortie: number of subkeys
		/// </summary>
		public int CompteLesSousClé()
		{
			try
			{
				RegistryKey _nomClé = cléBaseDeRegistre ;
				RegistryKey _nomSousClé = _nomClé.OpenSubKey(sousClé);
				if ( _nomSousClé != null )
					return _nomSousClé.SubKeyCount;
				else
					return 0; 
			}
			catch (Exception e)
			{
				AfficheMessageErreur(e, "Recherche du nombre de sous-clés : " + sousClé);
				return 0;
			}
		}

		/// <summary>
		/// Compte le nombre de valeur dans une clé du registre.
		/// entrée: void
		/// sortie: number of keys
		/// </summary>
		public int CompteValeur()
		{
			try
			{
				RegistryKey _nomClé = cléBaseDeRegistre ;
				RegistryKey _nomSousClé = _nomClé.OpenSubKey(sousClé);
				if ( _nomSousClé != null )
					return _nomSousClé.ValueCount;
				else
					return 0; 
			}
			catch (Exception e)
			{
				AfficheMessageErreur(e, "Recherche du nombre de clés : " + sousClé);
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
