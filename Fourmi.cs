using System;
using System.Collections.Generic;

namespace ANT_MANNE_Projet_Fourmi
{
    public class Fourmi
    {
        public int Pos_x { get; set; }
        public int Pos_y { get; set; }
        public bool Porte_sucre { get; set; }

        public Fourmi()
        {

        }

        public Fourmi(bool ps)
        {
            Porte_sucre = ps;
        }

        public bool ChercheSucre()
        {
            if (this.Porte_sucre)
                return false;
            else
                return true;
        }

        public bool RentreNid()
        {
            return !this.ChercheSucre();
        }

        public void Action()
        {
            Case p1 = Grille.Tab_Cases[this.Pos_x, this.Pos_y];
            Case p2;
            Scan_Places(this);

            for (int p = 0; p < Grille.List_p2.Count; p++)
            {
                p2 = Grille.List_p2[p];

                if (this.ChercheSucre() && p2.ContientSucre())
                {
                    Prendre_Sucre(this, p1, p2);
                    return;
                }

                else if (this.RentreNid() && p2.ContientNid())
                {
                    Deposer_Sucre(this);
                    return;
                }

                else if (this.RentreNid() && p2.Vide() && p2.PlusProcheNid(p1))
                {
                    //Deplacement(this, p1, p2);
                    //p1.Pheromone_sucre = Grille.Nb_init_pheromones_sucre;
                }

                else if (this.ChercheSucre() && p1.SurUnePiste() && p2.Vide() && p2.PlusLoinNid(p1) && p2.SurUnePiste())
                {
                    //Deplacement(this, p1, p2);
                }

                else if (this.ChercheSucre() && p2.SurUnePiste() && p2.Vide())
                {
                    //Deplacement(this, p1, p2);
                }

                else if (this.ChercheSucre() && p2.Vide())
                {
                    Deplacement(this, p1, p2);
                    return;
                }
                else
                {
                    continue;
                }
            }
        }

        private static void Scan_Places(Fourmi f)
        {
            Grille.List_p2 = new List<Case>();

            for (int x = Math.Max(0, f.Pos_x - 1); x < Math.Min(f.Pos_x + 2, Grille.Nb_lignes - 1); x++)
            {
                for (int y = Math.Max(0, f.Pos_y - 1); y < Math.Min(f.Pos_y + 2, Grille.Nb_colonnes - 1); y++)
                {
                    if (Grille.Tab_Cases[x, y] != Grille.Tab_Cases[f.Pos_x, f.Pos_y])
                    {
                        Grille.List_p2.Add(Grille.Tab_Cases[x, y]);
                    }
                }
            }

            Grille.Random_List(Grille.List_p2);
        } 

        private static void Deplacement(Fourmi f, Case p1, Case p2)
        {
            f.Pos_x = p2.Pos_x;
            f.Pos_y = p2.Pos_y;
            p2.Contenu = 'a';
            p2.Num_fourmi = p1.Num_fourmi;
            p1.Contenu = 'F';
            p1.Num_fourmi = -1;
        }

        private static void Prendre_Sucre(Fourmi f, Case p1, Case p2)
        {
            f.Porte_sucre = true;
            p1.Contenu = 'A';
            p1.Pheromone_sucre = Grille.Nb_init_pheromones_sucre;
            p2.Nb_sucre--;
        }

        private static void Deposer_Sucre(Fourmi f)
        {
            f.Porte_sucre = false;
            Grille.Tab_Cases[f.Pos_x, f.Pos_y].Contenu = 'a';
        }
    }
}