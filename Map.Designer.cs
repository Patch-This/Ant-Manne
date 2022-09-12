
namespace ANT_MANNE_Projet_Fourmi
{
    partial class Map
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Plateau = new System.Windows.Forms.DataGridView();
            this.Tours_Simulation = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Plateau)).BeginInit();
            this.SuspendLayout();
            // 
            // Plateau
            // 
            this.Plateau.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.Plateau.AllowUserToAddRows = false;
            this.Plateau.AllowUserToDeleteRows = false;
            this.Plateau.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Plateau.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Plateau.ColumnHeadersVisible = false;
            this.Plateau.Location = new System.Drawing.Point(0, 0);
            this.Plateau.Name = "Plateau";
            this.Plateau.RowHeadersVisible = false;
            this.Plateau.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.Plateau.RowTemplate.Height = 24;
            this.Plateau.Size = new System.Drawing.Size(1000, 1000);
            this.Plateau.TabIndex = 1;
            // 
            // Tours_Simulation
            // 
            this.Tours_Simulation.Enabled = true;
            this.Tours_Simulation.Interval = 2000;
            this.Tours_Simulation.Tick += new System.EventHandler(this.Tours_Simulation_Tick);
            // 
            // Map
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 815);
            this.Controls.Add(this.Plateau);
            this.Name = "Map";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Plateau)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer Tours_Simulation;
        public System.Windows.Forms.DataGridView Plateau;
    }
}

