#  Liv'Paris - Projet PSI

## À propos du projet

**Liv'Paris** est une plateforme de gestion de livraisons gastronomiques développée dans le cadre d'un projet PSI (Projet Sciences de l'Informatique) à ESILV.

### Auteurs

- **Esteban Gaubert**
- **Louis Le Forestier**
- **Maxime Laurent**

---

##  Table des matières

1. [Architecture générale](#architecture-générale)
2. [Algorithmes de recherche](#algorithmes-de-recherche-de-plus-court-chemin)
3. [Modules principaux](#modules-principaux)
4. [Gestion de la base de données](#gestion-de-la-base-de-données)
5. [Configuration et installation](#configuration-et-installation)

---

## Architecture générale

Nous avons séparé notre code dans le plus de classes possibles pour assurer une bonne séparation des responsabilités.

### Flux principal

- La classe \Application\ initialise la connexion à la base de données MySQL et crée le graphe du métro
- L'affichage initial permet à l'utilisateur de se connecter ou créer un compte
- Via la classe \Authentification\, l'utilisateur est orienté vers :
  - **Mode Client** : accès à l'interface client
  - **Mode Cuisinier** : accès à l'interface cuisinier
  - **Mode Admin** : accès au module d'administration avec requêtes SQL

Le graphe et la connexion BDD sont transmis à travers toutes les classes pour garantir un accès cohérent aux ressources.

### Diagramme des classes

![Architecture des classes](/images/classes.png)

### Configuration

Pour tester notre code, modifiez le fichier \ConnexionBDD\ avec votre propre mot de passe MySQL.
Le fichier \SQL.sql\ permet de créer la BDD, les tableaux et les données de test.

---

## Algorithmes de recherche de plus court chemin

Dans le cadre du projet Liv'Paris, nous avons mis en œuvre trois algorithmes de recherche de plus court chemin pour optimiser les itinéraires de livraison entre cuisiniers et clients. 

### Classe \PlusCourtChemin\

Cette classe implémente trois algorithmes de recherche de chemin, sachant que le graphe est connexe. Un prétraitement dans le sous-programme \TrouverStationsCorrespondance\ permet de gérer les correspondances entre lignes de métro.

####  Performance

Nous avons ajouté un timer pour chronométrer les trois algorithmes. **Dijkstra est de loin le plus rapide** (~20x plus rapide que les autres).

### 1 Algorithme de Dijkstra

**Objectif :** Calculer le plus court chemin en termes de temps entre deux stations de métro.

**Fonctionnement :**

- Initialisation des structures de données pour suivre le temps minimum et les prédécesseurs
- Exploration itérative des nœuds pour mettre à jour les temps et prédécesseurs
- Gestion des correspondances pour permettre les changements de ligne
- Reconstruction du chemin à partir du nœud d'arrivée

**Retour :** Liste de nœuds (stations) formant le chemin optimal

### 2 Algorithme de Bellman-Ford

**Objectif :** Trouver le plus court chemin en prenant en compte les cycles négatifs potentiels.

> **Note :** Dans notre cas, cet algorithme n'est pas nécessaire car le graphe ne contient que des arêtes positives et n'est pas orienté.

**Fonctionnement :**

- Initialisation similaire à Dijkstra
- Relaxation successive des arêtes pour mettre à jour les temps
- Gestion des correspondances pour les changements de ligne
- Reconstruction du chemin à partir du nœud d'arrivée

### 3 Algorithme de Floyd-Warshall

**Objectif :** Calculer les plus courts chemins entre **toutes les paires de nœuds** via une matrice de distances.

**Fonctionnement :**

- Initialisation d'une matrice de distances
- Remplissage de la matrice avec les poids des liens existants
- Application itérative de l'algorithme pour mettre à jour les distances
- Conversion de la matrice en dictionnaire pour un accès optimisé

---

## Modules principaux

###  Accès Admin - Classe \MenuModules\

- **Rôle :** Gérer toutes les tables de la BDD en accès "root"
- **Fonctionnalités :**
  - Centralise toutes les commandes sur les modules
  - Affiche un menu interactif pour l'utilisateur

###  Module Client

**Méthodes principales :**

| Méthode | Description |
|---------|-------------|
| \AjouterClient()\ | Création d'un nouveau client |
| \SupprimerClient()\ | Suppression d'un client |
| \ModifierClient()\ | Modification des informations client |
| \AfficherClientsAlphabetique()\ | Listing alphabétique |
| \AfficherClientsParRue()\ | Listing par localisation |
| \AfficherClientsParAchats()\ | Listing par volume d'achat |

**Note :** Gestion spéciale pour les clients avec/sans compte utilisateur.

###  Module Cuisinier

**Méthodes principales :**

| Méthode | Description |
|---------|-------------|
| \AjouterCuisinier()\ | Création d'un nouveau cuisinier |
| \SupprimerCuisinier()\ | Suppression d'un cuisinier |
| \ModifierCuisinier()\ | Modification des informations |
| \AfficherClientsServis()\ | Affichage des clients réguliers |
| \AfficherPlatsRealises()\ | Historique des plats préparés |
| \AfficherPlatDuJour()\ | Spécialité du jour |

**Note :** Gestion spéciale pour les cuisiniers avec/sans compte utilisateur.

###  Module Commande

**Méthodes principales :**

| Méthode | Description |
|---------|-------------|
| \CreerCommande()\ | Création d'une nouvelle commande |
| \ModifierCommande()\ | Modification d'une commande |
| \CalculerPrixCommande()\ | Calcul du prix total |
| \DeterminerCheminLivraison()\ | Optimisation de la route |
| \SimulerEtapesCommande()\ | Simulation du processus |

###  Module Statistiques

**Méthodes principales :**

| Méthode | Description |
|---------|-------------|
| \AfficherLivraisonsParCuisinier()\ | Statistiques par cuisinier |
| \AfficherCommandesParPeriode()\ | Commandes par période |
| \AfficherMoyennePrixCommandes()\ | Prix moyen des commandes |
| \AfficherMoyenneComptesClients()\ | Moyenne des achats clients |
| \AfficherCommandesParTypePlat()\ | Commandes par type de plat |
| \AfficherStatistiquesCreatives()\ | Analyses personnalisées |

### Gestion des identifiants

Les IDs sont générés de manière itérative selon le format :
- \USR001\, \USR002\, ... pour les utilisateurs
- \CLI001\, \CLI002\, ... pour les clients
- \CUI001\, \CUI002\, ... pour les cuisiniers

---

## Gestion de la base de données

###  Modifications de la structure

La base de données a subi un réarrangement du schéma pour assurer une meilleure séparation des données.

#### Changements majeurs

 **Passage en VARCHAR pour les IDs :**
- Tous les IDs sont maintenant en \VARCHAR(50)\
- Suppression des \AUTO_INCREMENT\

 **Suppression de la table \LigneDeCommande\**

 **Optimisation de la séparation des informations :**
- Table \Utilisateur\ : informations de base (nom, prénom, adresse)
- Table \Client\ : informations spécifiques + station de métro
- Table \Cuisinier\ : informations spécifiques + station de métro

### Architecture résultante

\\\
Utilisateur (base)
 Client (extension)
    Station de métro (pour calcul de trajet)
 Cuisinier (extension)
     Station de métro (pour calcul de trajet)
\\\

![Schéma SQL](/images/SQL.jpg)

### Avantages de cette structure

 Un utilisateur peut être **client ET/OU cuisinier** simultanément

 **Séparation claire** des responsabilités entre les entités

 **Compatibilité** avec le code C# existant

 **Gestion simplifiée** des IDs en VARCHAR

### Fichiers SQL

Le fichier \SQL.sql\ contient :
-  Création de la base de données
-  Création des tables
-  Données de test pour la validation

**Fichiers disponibles dans \/SQL/\ :**
- \Creation_PSI_LoMaEs.sql\ - Création de la structure
- \DonneesTest_PSI_LoMaEs.sql\ - Données de test
- \DonnéeTest2.sql\ - Données additionnelles
- \SQL.sql\ - Fichier principal

---

## Configuration et installation

###  Prérequis

- **.NET Framework** (C#)
- **MySQL** pour la base de données
- **Visual Studio** ou équivalent

###  Configuration initiale

1. **Base de données MySQL :**
   \\\sql
   -- Exécutez le fichier SQL.sql pour créer la structure
   \\\

2. **Fichier de configuration \ConnexionBDD\ :**
   - Modifiez le mot de passe pour correspondre à votre installation MySQL
   - Assurez-vous que l'adresse du serveur est correcte

###  Démarrage du projet

\\\csharp
// Le point d'entrée principal
// Lancez la classe Application pour initialiser le système
\\\

###  Points importants

-  Code C# pour les classes Visualisation basé sur la doc officielle C#
-  Interface graphique (Forms) prévue pour le Livrable 3
-  Tests effectués avec les données du fichier \DonneesTest_PSI_LoMaEs.sql\

---

##  Documentation supplémentaire

- **Arbre des classes** : /images/classes.png
- **Schéma de la base de données** : /images/SQL.jpg
- **Fichiers SQL** : /SQL/ (création, données de test)

---

## Notes de développement

- Les commentaires XML (///) ont été complétés avec ChatGPT, puis relus et validés
- La partie graphique/visuelle sera développée dans le Livrable 3
- Performance : Dijkstra recommandé pour les calculs de trajets en production

---

**Dernière mise à jour :** Février 2025  
**Version :** Livrable 2
