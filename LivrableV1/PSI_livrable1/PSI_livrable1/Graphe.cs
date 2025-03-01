using PSI_livrable1;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Représente un graphe non orienté composé de noeuds et de liens entre eux.
/// </summary>
public class Graphe
{
    public Dictionary<int, Noeud> Noeuds { get; set; }
    public List<Lien> Liens { get; set; }

    /// <summary>
    /// Initialise une nouvelle instance de la classe Graphe.
    /// </summary>
    public Graphe()
    {
        Noeuds = new Dictionary<int, Noeud>();
        Liens = new List<Lien>();
    }

    /// <summary>
    /// Ajoute un lien bidirectionnel entre deux noeuds identifiés par leurs IDs.
    /// Si les noeuds n'existent pas, ils sont créés automatiquement.
    /// </summary>
    /// <param name="id1">L'identifiant du premier noeud</param>
    /// <param name="id2">L'identifiant du deuxième noeud</param>
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

    /// <summary>
    /// Retourne le premier noeud du graphe.
    /// </summary>
    /// <returns>Le premier noeud du graphe ou null si le graphe est vide</returns>
    public Noeud ObtenirPremierNoeud()
    {
        foreach (var noeud in Noeuds.Values)
        {
            return noeud;
        }
        return null;
    }

    /// <summary>
    /// Effectue un parcours en largeur du graphe à partir d'un noeud de départ.
    /// Affiche les IDs des noeuds visités dans l'ordre du parcours.
    /// </summary>
    /// <param name="depart">Le noeud de départ pour le parcours</param>
    public void largeur(Noeud depart)
    {
        List<int> visites = new List<int>();
        Queue<Noeud> file = new Queue<Noeud>();

        file.Enqueue(depart);
        visites.Add(depart.Id);
        Console.Write(depart.Id + " ");

        while (file.Count > 0)
        {
            Noeud actuel = file.Dequeue();

            for (int i = 0; i < actuel.Voisins.Count; i++)
            {
                Noeud voisin = actuel.Voisins[i];
                bool estDejaVisite = false;

                for (int j = 0; j < visites.Count; j++)
                {
                    if (visites[j] == voisin.Id)
                    {
                        estDejaVisite = true;
                        break;
                    }
                }

                if (!estDejaVisite)
                {
                    visites.Add(voisin.Id);
                    file.Enqueue(voisin);
                    Console.Write(voisin.Id + " ");
                }
            }
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Effectue un parcours en profondeur du graphe à partir d'un noeud donné.
    /// Affiche les IDs des noeuds visités dans l'ordre du parcours.
    /// </summary>
    /// <param name="actuel">Le noeud actuel dans le parcours</param>
    /// <param name="visites">L'ensemble des noeuds déjà visités</param>
    public void Profondeur(Noeud actuel, List<int> visites)
    {
        if (actuel == null)
            return;

        bool estDejaVisite = false;
        for (int i = 0; i < visites.Count; i++)
        {
            if (visites[i] == actuel.Id)
            {
                estDejaVisite = true;
                break;
            }
        }

        if (estDejaVisite)
            return;

        Console.Write(actuel.Id + " ");
        visites.Add(actuel.Id);

        for (int i = 0; i < actuel.Voisins.Count; i++)
        {
            Profondeur(actuel.Voisins[i], visites);
        }
    }

    /// <summary>
    /// Vérifie si le graphe est connexe, c'est-à-dire s'il existe un chemin
    /// entre n'importe quelle paire de noeuds du graphe.
    /// </summary>
    /// <returns>true si le graphe est connexe, false sinon</returns>
    public bool EstConnexe()
    {
        if (Noeuds.Count == 0)
            return false;

        List<int> visites = new List<int>();
        Profondeur(ObtenirPremierNoeud(), visites);

        int nombreNoeudsVisites = visites.Count;
        int nombreTotalNoeuds = Noeuds.Count;

        return nombreNoeudsVisites == nombreTotalNoeuds;
    }

    /// <summary>
    /// Vérifie si le graphe contient au moins un cycle.
    /// </summary>
    /// <returns>true si le graphe contient un cycle, false sinon</returns>
    public bool ContientCycle()
    {
        List<int> visites = new List<int>();
        return ContientCycleDFS(ObtenirPremierNoeud(), null, visites);
    }

    /// <summary>
    /// Méthode auxiliaire pour la détection de cycle utilisant un parcours en profondeur.
    /// </summary>
    /// <param name="actuel">Le noeud actuel dans le parcours</param>
    /// <param name="parent">Le noeud parent du noeud actuel</param>
    /// <param name="visites">L'ensemble des noeuds déjà visités</param>
    /// <returns>true si un cycle est détecté, false sinon</returns>
    private bool ContientCycleDFS(Noeud actuel, Noeud parent, List<int> visites)
    {
        if (actuel == null)
            return false;

        visites.Add(actuel.Id);

        for (int i = 0; i < actuel.Voisins.Count; i++)
        {
            Noeud voisin = actuel.Voisins[i];
            bool estDejaVisite = false;

            for (int j = 0; j < visites.Count; j++)
            {
                if (visites[j] == voisin.Id)
                {
                    estDejaVisite = true;
                    break;
                }
            }

            if (!estDejaVisite)
            {
                bool cycleDetecte = ContientCycleDFS(voisin, actuel, visites);
                if (cycleDetecte)
                {
                    return true;
                }
            }
            else if (voisin != parent)
            {
                return true;
            }
        }
        return false;
    }
}
