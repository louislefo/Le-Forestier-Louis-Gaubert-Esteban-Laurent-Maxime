using System;
using System.Collections.Generic;

namespace LivrableV3
{
    /// classe pour les donnees importees
    public class DonneesImportees
    {
        public List<Utilisateur> utilisateurs;
        public List<Client> clients;
        public List<Cuisinier> cuisiniers;
        public List<Commande> commandes;
        public List<Livraison> livraisons;
    }

    /// classe pour les utilisateurs
    public class Utilisateur
    {
        public string id_utilisateur;
        public string nom;
        public string prenom;
        public string email;
        public string adresse;
        public string telephone;
        public string mot_de_passe;
    }

    /// classe pour les clients
    public class Client
    {
        public string id_client;
        public string id_utilisateur;
        public string StationMetro;
        public string entreprise_nom;
        public string referent;
    }

    /// classe pour les cuisiniers
    public class Cuisinier
    {
        public string id_cuisinier;
        public string id_utilisateur;
        public string StationMetro;
        public string zones_livraison;
        public double note_moyenne;
        public int nombre_livraisons;
    }

    /// classe pour les commandes
    public class Commande
    {
        public string id_commande;
        public string id_client;
        public string id_cuisinier;
        public string id_plat;
        public string date_commande;
        public double prix_total;
        public string statut;
    }

    /// classe pour les livraisons
    public class Livraison
    {
        public string id_livraison;
        public string id_commande;
        public string date_livraison;
        public string trajet;
    }
} 