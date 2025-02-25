using PSI_livrable1;
using System;
using System.Collections.Generic;
using System.Linq;

public class Graphe
{
    public Dictionary<int, Noeud> Noeuds { get; set; }
    public List<Lien> Liens { get; set; }

    public Graphe()
    {
        Noeuds = new Dictionary<int, Noeud>();
        Liens = new List<Lien>();
    }

    public void AjouterLien(int id1, int id2)
    {
        if (!Noeuds.ContainsKey(id1))
            Noeuds[id1] = new Noeud(id1);

        if (!Noeuds.ContainsKey(id2))
            Noeuds[id2] = new Noeud(id2);

        Noeud n1 = Noeuds[id1];
        Noeud n2 = Noeuds[id2];

        if (!n1.Voisins.Contains(n2))
        {
            n1.AjouterVoisin(n2);
            Liens.Add(new Lien(n1, n2));
        }
    }

    public Noeud ObtenirPremierNoeud()
    {
        return Noeuds.Values.FirstOrDefault();
    }

    public void largeur(Noeud depart)
    {
        HashSet<int> visites = new HashSet<int>();
        Queue<Noeud> file = new Queue<Noeud>();

        file.Enqueue(depart);
        visites.Add(depart.Id);

        while (file.Count > 0)
        {
            Noeud actuel = file.Dequeue();
            Console.Write(actuel.Id + " ");

            foreach (var voisin in actuel.Voisins)
            {
                if (!visites.Contains(voisin.Id))
                {
                    visites.Add(voisin.Id);
                    file.Enqueue(voisin);
                }
            }
        }
        Console.WriteLine();
    }

    public void Profondeur(Noeud actuel, HashSet<int> visites)
    {
        if (actuel == null || visites.Contains(actuel.Id))
            return;

        Console.Write(actuel.Id + " ");
        visites.Add(actuel.Id);

        foreach (var voisin in actuel.Voisins)
        {
            Profondeur(voisin, visites);
        }
    }

    public bool EstConnexe()
    {
        if (Noeuds.Count == 0)
            return false;

        HashSet<int> visites = new HashSet<int>();
        Profondeur(ObtenirPremierNoeud(), visites);

        return visites.Count == Noeuds.Count;
    }

    public bool ContientCycle()
    {
        HashSet<int> visites = new HashSet<int>();
        return ContientCycleDFS(ObtenirPremierNoeud(), null, visites);
    }

    private bool ContientCycleDFS(Noeud actuel, Noeud parent, HashSet<int> visites)
    {
        if (actuel == null)
            return false;

        visites.Add(actuel.Id);

        foreach (var voisin in actuel.Voisins)
        {
            if (!visites.Contains(voisin.Id))
            {
                if (ContientCycleDFS(voisin, actuel, visites))
                    return true;
            }
            else if (voisin != parent)
            {
                return true; // Cycle détecté
            }
        }
        return false;
    }
}
