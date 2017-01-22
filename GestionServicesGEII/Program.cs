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
 *                     Program.cs
 * ================================================================ */
using System;
using System.Windows.Forms;
using GestionServicesGEII.Librairie_BaseDeDonnées;
using GestionServicesGEII.Librairie_Fichier;
using GestionServicesGEII.Librairie_GEII;
using GestionServicesGEII.Librairie_Générique;
using GestionServicesGEII.Librairie_Registre;

namespace GestionServicesGEII
{
	static class Program
	{
		/// <summary>
		/// Point d'entrée principal de l'application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new DialogueServiceGEII());
		}
	}
}
