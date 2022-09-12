namespace ANT_MANNE_Projet_Fourmi
{
    public class Case
    {
        public int Pos_x { get; set; }
        public int Pos_y { get; set; }
        public char Contenu { get; set; }
        public int Pheromone_nid { get; set; }
        public int Pheromone_sucre { get; set; }
        public int Nb_sucre { get; set; }
        public int Num_fourmi { get; set; }

        public Case() { }

        public Case(int x, int y, char cont, int numf, int nbs, int pheroN, int PheroS)
        {
            Pos_x = x;
            Pos_y = y;
            Contenu = cont;
            Num_fourmi = numf;
            Nb_sucre = nbs;
            Pheromone_nid = pheroN;
            Pheromone_sucre = PheroS;
        }

        public Case(int x, int y, char cont, int nf)
        {
            Pos_x = x;
            Pos_y = y;
            Contenu = cont;
            Num_fourmi = nf;
        }

        //Prédicats à implémenter mais qui ne sont pas utilisés dans le programme
        //private bool ContientFourmi()
        //{
        //    if (this.Contenu == 'a' || this.Contenu == 'A')
        //        return true;
        //    else
        //        return false;
        //}

        //private bool ContientCaillou()
        //{
        //    if (this.Contenu == 's')
        //        return true;
        //    else
        //        return false;
        //}

        public bool ContientSucre()
        {
            if (this.Nb_sucre > 0)
                return true;
            else
                return false;
        }

        public bool ContientNid()
        {
            if (this.Contenu == 'N')
                return true;
            else
                return false;
        }

        public bool Vide()
        {
            if (this.Contenu == 'F')
                return true;
            else
                return false;
        }

        public bool SurUnePiste()
        {
            if (this.Pheromone_sucre > 0)
                return true;
            else
                return false;
        }

        public bool PlusProcheNid(Case adj)
        {
            if (this.Pheromone_nid > adj.Pheromone_nid)
                return true;
            else
                return false;
        }

        public bool PlusLoinNid(Case p2)
        {
            if (!this.PlusProcheNid(p2))
                return true;
            else
                return false;
        }
    }
}