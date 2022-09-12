using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ANT_MANNE_Projet_Fourmi
{
    public partial class Map : Form
    {
        public Map()
        {
            InitializeComponent();

            Grille.Nb_colonnes = 20;
            Grille.Nb_lignes = 20;
            Grille.Nb_tours_simulation = 10;
            Grille.Nb_fourmis = 12;
            Grille.Nb_ilots_sucre = 15;
            Grille.Nb_cailloux = 10;
            Grille.Nb_init_pheromones_sucre = 10;
            Grille.Taux_diminution_pheromones_sucre = 10;

            Plateau.ColumnCount = Grille.Nb_colonnes;
            for (int i = 0; i < Grille.Nb_lignes; i++)
            {
                Plateau.Rows.Add();
                Plateau.Rows[i].Height = 33;
            }

            Initialisation();
            Code_Couleur();
            Simulation();
        }

        public void Initialisation()
        {
            Grille.Tab_Cases = new Case[Grille.Nb_lignes, Grille.Nb_colonnes];
            for (int x = 0; x < Grille.Nb_lignes; x++)
            {
                for (int y = 0; y < Grille.Nb_colonnes; y++)
                {
                    Grille.Tab_Cases[x, y] = new Case(x, y, 'F', -1);
                }
            }

            Grille.Tab_Fourmis = new Fourmi[Grille.Nb_fourmis];
            for (int i = 0; i < Grille.Nb_fourmis; i++)
            {
                Grille.Tab_Fourmis[i] = new Fourmi(false);
            }

            Grille.Placement_Nid(Grille.Tab_Cases);
            Grille.Placement_Cailloux(Grille.Tab_Cases);
            Grille.Placement_Sucre(Grille.Tab_Cases);
            //Affichage_Plateau(Grille.Tab_Cases);

            string path = @"..\..\ecritureFichier.txt";
            StreamWriter sw = new StreamWriter(path, true);

            sw.WriteLine(Grille.Nb_lignes + " " + Grille.Nb_colonnes + " " + Grille.Nb_tours_simulation);

            for (int x = 0; x < Grille.Nb_lignes; x++)
            {
                sw.WriteLine("");
                for (int y = 0; y < Grille.Nb_colonnes; y++)
                {
                    sw.WriteLine("[" + x + "," + y + "] " + Grille.Tab_Cases[x, y].Contenu + Grille.Tab_Cases[x, y].Num_fourmi + " " + Grille.Tab_Cases[x, y].Nb_sucre + " " + Grille.Tab_Cases[x, y].Pheromone_nid + " " + Grille.Tab_Cases[x, y].Pheromone_sucre);
                }
            }

            sw.WriteLine("\n");
            sw.Close();
        }

        private void Code_Couleur()
        {
            for (int x = 0; x < Grille.Nb_lignes; x++)
            {
                for (int y = 0; y < Grille.Nb_colonnes; y++)
                {
                    if (Grille.Tab_Cases[x, y].Contenu == 'N')
                    {
                        Plateau.Rows[x].Cells[y].Style.BackColor = Color.Green;
                    }
                    if (Grille.Tab_Cases[x, y].Contenu == 'S')
                    {
                        Plateau.Rows[x].Cells[y].Style.BackColor = Color.Yellow;
                    }
                    if (Grille.Tab_Cases[x, y].Contenu == 's')
                    {
                        Plateau.Rows[x].Cells[y].Style.BackColor = Color.Brown;
                    }
                    if (Grille.Tab_Cases[x, y].Contenu == 'F')
                    {
                        Plateau.Rows[x].Cells[y].Style.BackColor = Color.LightGray;
                    }
                    if (Grille.Tab_Cases[x, y].Contenu == 'a')
                    {
                        Plateau.Rows[x].Cells[y].Style.BackColor = Color.Aquamarine;
                    }
                    if (Grille.Tab_Cases[x, y].Contenu == 'A')
                    {
                        Plateau.Rows[x].Cells[y].Style.BackColor = Color.Blue;
                    }
                }
            }
        }

        private void Simulation()
        {
            string path = @"..\..\ecritureFichier.txt";

            for (int i = 0; i < Grille.Nb_tours_simulation; i++)
            {
                Grille.Evaporation_Pheromones_Sucre();

                for (int f = 0; f < Grille.Tab_Fourmis.Length; f++)
                {
                    Grille.Tab_Fourmis[f].Action();
                }
                Code_Couleur();


                StreamWriter sw = new StreamWriter(path, true);

                for (int x = 0; x < Grille.Nb_lignes; x++)
                {
                    sw.WriteLine("");
                    for (int y = 0; y < Grille.Nb_colonnes; y++)
                    {
                        sw.WriteLine("[" + x + "," + y + "] " + Grille.Tab_Cases[x, y].Contenu + Grille.Tab_Cases[x, y].Num_fourmi + " " + Grille.Tab_Cases[x, y].Nb_sucre + " " + Grille.Tab_Cases[x, y].Pheromone_nid + " " + Grille.Tab_Cases[x, y].Pheromone_sucre);
                    }
                }
                sw.WriteLine("\n");

                sw.Close();
                Affichage_Plateau(Grille.Tab_Cases);
            }
        }

        private void Affichage_Plateau(Case[,] Tab_Cases)
        {
            for (int x = 0; x < Grille.Nb_lignes; x++)
            {
                for (int y = 0; y < Grille.Nb_colonnes; y++)
                {
                    if (Tab_Cases[x, y].Contenu is 'F')
                        Plateau.Rows[x].Cells[y].Value = Tab_Cases[x, y].Pheromone_nid;
                    else if (Tab_Cases[x, y].Contenu != 'F')
                        Plateau.Rows[x].Cells[y].Value = Tab_Cases[x, y].Contenu;
                }
            }
        }

        private void Tours_Simulation_Tick(object sender, EventArgs e)
        {
            //string path = @"..\..\ecritureFichier.txt";

            //for (int i = 0; i < Grille.Nb_tours_simulation; i++)
            //{
            //    Grille.Evaporation_Pheromones_Sucre();

            //    for (int f = 0; f < Grille.Tab_Fourmis.Length; f++)
            //    {
            //        Grille.Tab_Fourmis[f].Action();
            //        Code_Couleur();
            //        for (int x = 0; x < Grille.Nb_lignes; x++)
            //        {
            //            for (int y = 0; y < Grille.Nb_colonnes; y++)
            //            {
            //                if (Grille.Tab_Cases[x, y].Contenu == 'F')
            //                    Plateau.Rows[x].Cells[y].Value = Grille.Tab_Cases[x, y].Pheromone_nid;
            //                else if (Grille.Tab_Cases[x, y].Contenu != 'F')
            //                    Plateau.Rows[x].Cells[y].Value = Grille.Tab_Cases[x, y].Contenu;
            //            }
            //        }
            //    }


            //    StreamWriter sw = new StreamWriter(path, true);

            //    for (int x = 0; x < Grille.Nb_lignes; x++)
            //    {
            //        for (int y = 0; y < Grille.Nb_colonnes; y++)
            //        {
            //            sw.WriteLine(Grille.Tab_Cases[x, y].Contenu + " " + Grille.Tab_Cases[x, y].Nb_sucre + " " + Grille.Tab_Cases[x, y].Pheromone_nid + " " + Grille.Tab_Cases[x, y].Pheromone_sucre);
            //        }
            //    }

            //    sw.Close();
            //}
        }
    }
}