using System;
using System.Collections.Generic;

namespace ANT_MANNE_Projet_Fourmi
{
    public class Grille
    {
        //Attributs
        public static int Nb_lignes { get; set; }
        public static int Nb_colonnes { get; set; }
        public static int Nb_tours_simulation { get; set; }
        public static int Nb_fourmis { get; set; }
        public static int Nb_ilots_sucre { get; set; }
        public static int Nb_cailloux { get; set; }
        public static int Nb_init_pheromones_sucre { get; set; }
        public static int Taux_diminution_pheromones_sucre { get; set; }
        public static Fourmi[] Tab_Fourmis { get; set; }
        public static Case[,] Tab_Cases { get; set; }
        public static List<Case> List_p2 { get; set; }

        public static void Placement_Nid(Case[,] Tab_Cases)
        {

            Random rn = new Random();

            int l_nid = rn.Next(Nb_lignes / 4, Nb_lignes - Nb_lignes / 4 - 1);
            int c_nid = rn.Next(Nb_colonnes / 4, Nb_colonnes - Nb_colonnes / 4 - 1);

            Case nid_sud_ouest = Tab_Cases[l_nid, c_nid];
            nid_sud_ouest.Contenu = 'N';
            Case nid_sud_est = Tab_Cases[l_nid, c_nid + 1];
            nid_sud_est.Contenu = 'N';
            Case nid_nord_ouest = Tab_Cases[l_nid - 1, c_nid];
            nid_nord_ouest.Contenu = 'N';
            Case nid_nord_est = Tab_Cases[l_nid - 1, c_nid + 1];
            nid_nord_est.Contenu = 'N';

            Placement_Pheromones_Nid(Tab_Cases, nid_sud_ouest, nid_sud_est, nid_nord_ouest, nid_nord_est);
            Placement_Fourmis(Tab_Cases, nid_nord_ouest);
        }

        private static void Placement_Pheromones_Nid(Case[,] Tab_Cases, Case nid_sud_ouest, Case nid_sud_est, Case nid_nord_ouest, Case nid_nord_est)
        {
            /*etat de la grille
            Pos_x\Pos_y| 0  1  2  3  4  5  6  7  8  9
                    ---|----------------------------
                     0 | 0  0  0  0  0  0  0  0  0  0
                     1 | 0  0  0  0  0  0  0  0  0  0
                     2 | 0  0  0  0  0  0  0  0  0  0
                     3 | 0  0  0  0  0  0  0  0  0  0
                     4 | 0  0  0  0  0  0  0  0  0  0
                     5 | 0  0  0  0  0  0  0  0  0  0
                     6 | 0  0  0  0  0  N  N  0  0  0
                     7 | 0  0  0  0  0  N  N  0  0  0
                     8 | 0  0  0  0  0  0  0  0  0  0
                     9 | 0  0  0  0  0  0  0  0  0  0
             */

            int maxPhero = Math.Max(Math.Max(nid_sud_ouest.Pos_y, Nb_colonnes - nid_sud_est.Pos_y - 1), Math.Max(nid_nord_ouest.Pos_x, Nb_lignes - nid_sud_ouest.Pos_x - 1));

            //Gestion des 4 rectangles :

            /*Boucle du rectangle Nord_Ouest
		    Pos_x\Pos_y| 0  1  2  3  4  5 
			        ---|------------------
			         0 | 0  0  0  0  0  0 
			         1 | 0  0  0  0  0  0 
			         2 | 0  0  0  0  0  0 
			         3 | 0  0  0  0  0  0 
			         4 | 0  0  0  0  0  0 
			         5 | 0  0  0  0  0  0 
			         6 | 0  0  0  0  0  N 
            */
            //On part de N donc il faut décrémenter l'indice des Pos_x et l'indice des Pos_y depuis N
            //Il faut faire attention que notre (N.Pos_y - ?) et que notre (N.Pos_x - ?) ne soit jamais < 0

            int nb_iteration = Math.Max(nid_nord_ouest.Pos_x, nid_nord_ouest.Pos_y);
            int pn = maxPhero;
            for (int i = 0; i <= nb_iteration; i++)
            {
                for (int c = 0; c <= i && nid_nord_ouest.Pos_y - c >= 0 && nid_nord_ouest.Pos_x - i >= 0; c++)
                {
                    Tab_Cases[nid_nord_ouest.Pos_x - i, nid_nord_ouest.Pos_y - c].Pheromone_nid = pn;
                }
                for (int r = 0; r < i && nid_nord_ouest.Pos_y - i >= 0 && nid_nord_ouest.Pos_x - r >= 0; r++)
                {
                    Tab_Cases[nid_nord_ouest.Pos_x - r, nid_nord_ouest.Pos_y - i].Pheromone_nid = pn;
                }
                pn--;
            }

            /*etat de la grille
            Pos_x\Pos_y| 0  1  2  3  4  5  6  7  8  9
                    ---|----------------------------
                     0 | 1  1  1  1  1  1  0  0  0  0
                     1 | 2  2  2  2  2  2  0  0  0  0
                     2 | 2  3  3  3  3  3  0  0  0  0
                     3 | 2  3  4  4  4  4  0  0  0  0
                     4 | 2  3  4  5  5  5  0  0  0  0
                     5 | 2  3  4  5  6  6  0  0  0  0
                     6 | 2  3  4  5  6  N  N  0  0  0
                     7 | 0  0  0  0  0  N  N  0  0  0
                     8 | 0  0  0  0  0  0  0  0  0  0
                     9 | 0  0  0  0  0  0  0  0  0  0
            */

            /*Boucle du rectangle Nord_Est
            Pos_x\Pos_y| 6  7  8  9
                ---|-----------
                 0 | 0  0  0  0
                 1 | 0  0  0  0
                 2 | 0  0  0  0
                 3 | 0  0  0  0
                 4 | 0  0  0  0
                 5 | 0  0  0  0
                 6 | N  0  0  0
            */
            //On part de N donc il faut décrémenter l'indice des Pos_x et incrémenter l'indice des Pos_y depuis N
            //Il faut faire attention que notre (N.Pos_y + ?) ne soit jamais > Nb_colonnes et que notre (N.Pos_x - ?) ne soit jamais < 0

            nb_iteration = Math.Max(nid_nord_est.Pos_x, Nb_colonnes - nid_nord_est.Pos_y);
            pn = maxPhero;
            for (int i = 0; i <= nb_iteration; i++)
            {
                for (int c = 0; c <= i && nid_nord_est.Pos_y + c < Nb_colonnes && nid_nord_est.Pos_x - i >= 0; c++)
                {
                    Tab_Cases[nid_nord_est.Pos_x - i, nid_nord_est.Pos_y + c].Pheromone_nid = pn;
                }
                for (int r = 0; r < i && nid_nord_est.Pos_y + i < Nb_colonnes && nid_nord_est.Pos_x - r >= 0; r++)
                {
                    Tab_Cases[nid_nord_est.Pos_x - r, nid_nord_est.Pos_y + i].Pheromone_nid = pn;
                }
                pn--;
            }

            /*etat de la grille
		    Pos_x\Pos_y| 0  1  2  3  4  5  6  7  8  9
			    ---|----------------------------
			     0 | 1  1  1  1  1  1  1  1  1  1
			     1 | 2  2  2  2  2  2  2  2  2  2
			     2 | 2  3  3  3  3  3  3  3  3  3
			     3 | 2  3  4  4  4  4  4  4  4  4
			     4 | 2  3  4  5  5  5  5  5  5  4
			     5 | 2  3  4  5  6  6  6  6  5  4
			     6 | 2  3  4  5  6  N  N  6  5  4
			     7 | 0  0  0  0  0  N  N  0  0  0
			     8 | 0  0  0  0  0  0  0  0  0  0
			     9 | 0  0  0  0  0  0  0  0  0  0
		    */

            /*Boucle du rectangle Sud_Ouest
		    Pos_x\Pos_y| 0  1  2  3  4  5
			        ---|-----------------
			         7 | 0  0  0  0  0  N 
			         8 | 0  0  0  0  0  0
			         9 | 0  0  0  0  0  0
		    */
            //On part de N donc il faut décrémenter l'indice des Pos_y et incrémenter l'indice des Pos_x depuis N
            //Il faut faire attention que notre (N.Pos_x + ?) ne soit jamais > Nb_lignes et que notre (N.Pos_y - ?) ne soit jamais < 0

            nb_iteration = Math.Max(Nb_lignes - nid_sud_ouest.Pos_x, nid_sud_ouest.Pos_y);// 6
            pn = maxPhero;
            for (int i = 0; i <= nb_iteration; i++)
            {
                for (int c = 0; c <= i && nid_sud_ouest.Pos_x + i < Nb_lignes && nid_sud_ouest.Pos_y - c >= 0; c++)
                {
                    Tab_Cases[nid_sud_ouest.Pos_x + i, nid_sud_ouest.Pos_y - c].Pheromone_nid = pn;
                }
                for (int r = 0; r < i && nid_sud_ouest.Pos_x + r < Nb_lignes && nid_sud_ouest.Pos_y - i >= 0; r++)
                {
                    Tab_Cases[nid_sud_ouest.Pos_x + r, nid_sud_ouest.Pos_y - i].Pheromone_nid = pn;
                }
                pn--;
            }

            /*etat de la grille
		    Pos_x\Pos_y| 0  1  2  3  4  5  6  7  8  9
			        ---|----------------------------
			         0 | 1  1  1  1  1  1  1  1  1  1
			         1 | 2  2  2  2  2  2  2  2  2  2
			         2 | 2  3  3  3  3  3  3  3  3  3
			         3 | 2  3  4  4  4  4  4  4  4  4
			         4 | 2  3  4  5  5  5  5  5  5  4
			         5 | 2  3  4  5  6  6  6  6  5  4
			         6 | 2  3  4  5  6  N  N  6  5  4
			         7 | 2  3  4  5  6  N  N  0  0  0
			         8 | 2  3  4  5  6  6  0  0  0  0
			         9 | 2  3  4  5  5  5  0  0  0  0
		    */

            /*Boucle du rectangle Sud_Est
		    Pos_x\Pos_y|6  7  8  9
			        ---|---------
			         7 | N  0  0  0
			         8 | 0  0  0  0
			         9 | 0  0  0  0
	        */
            //On part de N donc il faut incrémenter l'indice des Pos_y et l'indice des Pos_x depuis N
            //Il faut faire attention que notre (N.Pos_x + ?) ne soit jamais > Nb_lignes et que notre (N.Pos_y + ?) ne soit jamais > Nb_colonnes

            nb_iteration = Math.Max(Nb_lignes - nid_sud_est.Pos_x, Nb_colonnes - nid_sud_est.Pos_y);
            pn = maxPhero;
            for (int i = 0; i <= nb_iteration; i++)
            {
                for (int c = 0; c <= i && nid_sud_est.Pos_x + i < Nb_lignes && nid_sud_est.Pos_y + c < Nb_colonnes; c++)
                {
                    Tab_Cases[nid_sud_est.Pos_x + i, nid_sud_est.Pos_y + c].Pheromone_nid = pn;
                }
                for (int r = 0; r < i && nid_sud_est.Pos_x + r < Nb_lignes && nid_sud_est.Pos_y + i < Nb_colonnes; r++)
                {
                    Tab_Cases[nid_sud_est.Pos_x + r, nid_sud_est.Pos_y + i].Pheromone_nid = pn;
                }
                pn--;
            }

            /*etat de la grille
		    Pos_x\Pos_y| 0  1  2  3  4  5  6  7  8  9
			        ---|----------------------------
			         0 | 1  1  1  1  1  1  1  1  1  1
			         1 | 2  2  2  2  2  2  2  2  2  2
			         2 | 2  3  3  3  3  3  3  3  3  3
			         3 | 2  3  4  4  4  4  4  4  4  4
			         4 | 2  3  4  5  5  5  5  5  5  4
			         5 | 2  3  4  5  6  6  6  6  5  4
			         6 | 2  3  4  5  6  N  N  6  5  4
			         7 | 2  3  4  5  6  N  N  6  5  4
			         8 | 2  3  4  5  6  6  6  6  5  4
			         9 | 2  3  4  5  5  5  5  5  5  4
		    */

            //Sachant que les boucles font en sorte de placer le nombre de phéromones max sur les cases Nid
        }

        private static void Placement_Fourmis(Case[,] Tab_Cases, Case nid_nord_ouest)
        {
            int index_fourmi = 0;

            for (int x = nid_nord_ouest.Pos_x - 1; x < nid_nord_ouest.Pos_x + 3; x++)
            {
                for (int y = nid_nord_ouest.Pos_y - 1; y < nid_nord_ouest.Pos_y + 3; y++)
                {
                    if (Tab_Cases[x, y].Contenu == 'F')
                    {
                        Tab_Fourmis[index_fourmi].Pos_x = x;
                        Tab_Fourmis[index_fourmi].Pos_y = y;
                        Tab_Cases[x, y].Contenu = 'a';
                        Tab_Cases[x, y].Num_fourmi = index_fourmi;
                        index_fourmi++;
                    }
                }
            }
        }

        public static void Placement_Cailloux(Case[,] Tab_Cases)
        {
            int caillou_x;
            int caillou_y;

            Random rng_caillou = new Random();

            for (int i = 0; i < Nb_cailloux; i++)
            {
                //Tirage valeur
                caillou_x = rng_caillou.Next(0, Nb_lignes);
                caillou_y = rng_caillou.Next(0, Nb_colonnes);

                if (Tab_Cases[caillou_x, caillou_y].Contenu == 'F')
                {
                    Tab_Cases[caillou_x, caillou_y].Contenu = 's';
                }
                else
                {
                    i--;
                    continue;
                }
            }
        }

        public static void Placement_Sucre(Case[,] Tab_Cases)
        {
            Random rng_sucre = new Random();

            int sucre_x;
            int sucre_y;

            for (int i = 0; i < Nb_ilots_sucre; i++)
            {
                sucre_x = rng_sucre.Next(0, Nb_lignes);
                sucre_y = rng_sucre.Next(0, Nb_colonnes);

                if (Tab_Cases[sucre_x, sucre_y].Contenu == 'F')
                {
                    Tab_Cases[sucre_x, sucre_y].Contenu = 'S';
                    Tab_Cases[sucre_x, sucre_y].Nb_sucre = rng_sucre.Next(5, 31);
                }
                else
                {
                    i--;
                    continue;
                }
            }
        }

        public static void Evaporation_Pheromones_Sucre()
        {
            for (int x = 0; x < Grille.Nb_lignes; x++)
            {
                for (int y = 0; y < Grille.Nb_colonnes; y++)
                {
                    if (Grille.Tab_Cases[x, y].Pheromone_sucre > 0)
                    {
                        Grille.Tab_Cases[x, y].Pheromone_sucre = Grille.Tab_Cases[x, y].Pheromone_sucre - (Grille.Taux_diminution_pheromones_sucre / 100 * Grille.Nb_init_pheromones_sucre);
                    }

                }
            }
        }

        public static void Random_List(List<Case> list)
        {
            Random rng = new Random();
            int c = 0;
            Case temp;
            while (c < list.Count-1)
            {
                c++;
                int n = rng.Next(c - 1);
                temp = list[n];
                list[n] = list[c];
                list[c] = temp;
            }
        }
    }
}