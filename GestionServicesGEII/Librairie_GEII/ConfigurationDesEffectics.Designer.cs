namespace GestionServicesGEII
{
	partial class ConfigurationEffectifs
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationEffectifs));
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.titreFenetre = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.boutonFermerConfig = new System.Windows.Forms.Button();
            this.boutonValiderConfig = new System.Windows.Forms.Button();
            this.boutonGroupeConfig = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chkEffacerFenêtre = new System.Windows.Forms.CheckBox();
            this.fenetreRésumé = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.choixMax12 = new System.Windows.Forms.CheckBox();
            this.choixMax14 = new System.Windows.Forms.CheckBox();
            this.saisieEffectif = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // flowLayoutPanel2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel2, 3);
            this.flowLayoutPanel2.Controls.Add(this.titreFenetre);
            resources.ApplyResources(this.flowLayoutPanel2, "flowLayoutPanel2");
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // titreFenetre
            // 
            this.titreFenetre.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.titreFenetre.Cursor = System.Windows.Forms.Cursors.Arrow;
            resources.ApplyResources(this.titreFenetre, "titreFenetre");
            this.titreFenetre.ForeColor = System.Drawing.Color.Gold;
            this.titreFenetre.Name = "titreFenetre";
            this.titreFenetre.ReadOnly = true;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel1.Controls.Add(this.boutonFermerConfig);
            this.flowLayoutPanel1.Controls.Add(this.boutonValiderConfig);
            this.flowLayoutPanel1.Controls.Add(this.boutonGroupeConfig);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // boutonFermerConfig
            // 
            resources.ApplyResources(this.boutonFermerConfig, "boutonFermerConfig");
            this.boutonFermerConfig.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.boutonFermerConfig.Name = "boutonFermerConfig";
            this.boutonFermerConfig.UseVisualStyleBackColor = false;
            this.boutonFermerConfig.Click += new System.EventHandler(this.boutonFermer_Click);
            // 
            // boutonValiderConfig
            // 
            this.boutonValiderConfig.BackColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.boutonValiderConfig, "boutonValiderConfig");
            this.boutonValiderConfig.Name = "boutonValiderConfig";
            this.boutonValiderConfig.UseVisualStyleBackColor = false;
            this.boutonValiderConfig.Click += new System.EventHandler(this.boutonValiderConfig_Click);
            // 
            // boutonGroupeConfig
            // 
            this.boutonGroupeConfig.BackColor = System.Drawing.Color.DarkSeaGreen;
            resources.ApplyResources(this.boutonGroupeConfig, "boutonGroupeConfig");
            this.boutonGroupeConfig.Name = "boutonGroupeConfig";
            this.boutonGroupeConfig.UseVisualStyleBackColor = false;
            this.boutonGroupeConfig.Click += new System.EventHandler(this.boutonGroupeConfig_Click);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkEffacerFenêtre, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.fenetreRésumé, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.saisieEffectif, 1, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // chkEffacerFenêtre
            // 
            resources.ApplyResources(this.chkEffacerFenêtre, "chkEffacerFenêtre");
            this.chkEffacerFenêtre.Name = "chkEffacerFenêtre";
            this.chkEffacerFenêtre.UseVisualStyleBackColor = true;
            this.chkEffacerFenêtre.CheckedChanged += new System.EventHandler(this.chkEffacerFenêtre_CheckedChanged);
            // 
            // fenetreRésumé
            // 
            this.fenetreRésumé.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.fenetreRésumé.DetectUrls = false;
            resources.ApplyResources(this.fenetreRésumé, "fenetreRésumé");
            this.fenetreRésumé.ForeColor = System.Drawing.Color.Yellow;
            this.fenetreRésumé.Name = "fenetreRésumé";
            this.fenetreRésumé.ReadOnly = true;
            this.tableLayoutPanel1.SetRowSpan(this.fenetreRésumé, 3);
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.choixMax12, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.choixMax14, 1, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // flowLayoutPanel3
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.flowLayoutPanel3, 2);
            this.flowLayoutPanel3.Controls.Add(this.label2);
            resources.ApplyResources(this.flowLayoutPanel3, "flowLayoutPanel3");
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // choixMax12
            // 
            resources.ApplyResources(this.choixMax12, "choixMax12");
            this.choixMax12.Checked = true;
            this.choixMax12.CheckState = System.Windows.Forms.CheckState.Checked;
            this.choixMax12.Name = "choixMax12";
            this.choixMax12.UseVisualStyleBackColor = true;
            this.choixMax12.CheckedChanged += new System.EventHandler(this.choixMax12_CheckedChanged);
            // 
            // choixMax14
            // 
            resources.ApplyResources(this.choixMax14, "choixMax14");
            this.choixMax14.Name = "choixMax14";
            this.choixMax14.UseVisualStyleBackColor = true;
            this.choixMax14.CheckedChanged += new System.EventHandler(this.choixMax14_CheckedChanged);
            // 
            // saisieEffectif
            // 
            resources.ApplyResources(this.saisieEffectif, "saisieEffectif");
            this.saisieEffectif.Name = "saisieEffectif";
            this.saisieEffectif.UseWaitCursor = true;
            // 
            // ConfigurationEffectifs
            // 
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigurationEffectifs";
            this.ShowInTaskbar = false;
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button boutonFermerConfig;
		private System.Windows.Forms.Button boutonValiderConfig;
		private System.Windows.Forms.Button boutonGroupeConfig;
		private System.Windows.Forms.TextBox saisieEffectif;
		private System.Windows.Forms.RichTextBox fenetreRésumé;
		public System.Windows.Forms.TextBox titreFenetre;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox choixMax12;
		private System.Windows.Forms.CheckBox choixMax14;
        private System.Windows.Forms.CheckBox chkEffacerFenêtre;
    }
}