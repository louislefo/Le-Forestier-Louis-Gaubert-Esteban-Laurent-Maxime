DROP DATABASE IF EXISTS PSI;
DROP DATABASE IF EXISTS PSI_LoMaEs;
CREATE DATABASE PSI_LoMaEs;
USE PSI_LoMaEs;

CREATE TABLE utilisateur(
    id_utilisateur VARCHAR(50),
    nom VARCHAR(100),
    prénom VARCHAR(100),
    email VARCHAR(150),
    téléphone VARCHAR(50),
    adresse VARCHAR(255),
    type__Cuisinier_Client_ VARCHAR(50),
    mot_de_passe VARCHAR(100),
    PRIMARY KEY(id_utilisateur)
);

CREATE TABLE cuisinier(
    id_cuisinier VARCHAR(50),
    type__Cuisinier_Client_ VARCHAR(50),
    zones_livraison VARCHAR(255),
    note_moyenne DECIMAL(2,1),
    nombre_livraisons INT,
    id_utilisateur VARCHAR(50) NOT NULL,
    PRIMARY KEY(id_cuisinier),
    UNIQUE(id_utilisateur),
    FOREIGN KEY(id_utilisateur) REFERENCES utilisateur(id_utilisateur)
);

CREATE TABLE client(
    id_client VARCHAR(50),
    id_utilisateur VARCHAR(50),
    type_client__Particulier_Entreprise_ VARCHAR(50),
    entreprise_nom VARCHAR(100),
    référent VARCHAR(100),
    PRIMARY KEY(id_client),
    UNIQUE(id_utilisateur),
    FOREIGN KEY(id_utilisateur) REFERENCES utilisateur(id_utilisateur)
);

CREATE TABLE Commande_(
    id_commande VARCHAR(50),
    id_client VARCHAR(50),
    total_prix DECIMAL(10,2),
    statut_En_attente__Confirmée__En_cours__Livrée__Annulée_ VARCHAR(50),
    date_commande DATE,
    PRIMARY KEY(id_commande),
    FOREIGN KEY(id_client) REFERENCES client(id_client)
);

CREATE TABLE Livraison(
    id_livraison VARCHAR(50),
    id_cuisinier VARCHAR(50),
    id_commande VARCHAR(50),
    id_ligne_commande VARCHAR(50),
    date_livraison DATETIME,
    trajet VARCHAR(255),
    PRIMARY KEY(id_livraison),
    FOREIGN KEY(id_cuisinier) REFERENCES cuisinier(id_cuisinier),
    FOREIGN KEY(id_commande) REFERENCES Commande_(id_commande)
);

CREATE TABLE Transaction_(
    id_transaction VARCHAR(50),
    id_commande VARCHAR(50),
    montant DECIMAL(10,2),
    date_paiement DATETIME,
    statut_Payé__En_attente__Échoué_ VARCHAR(50),
    PRIMARY KEY(id_transaction),
    UNIQUE(id_commande),
    FOREIGN KEY(id_commande) REFERENCES Commande_(id_commande)
);

CREATE TABLE Avis_(
    id_avis VARCHAR(50),
    id_client VARCHAR(50),
    id_cuisinier VARCHAR(50),
    note DECIMAL(2,1),
    id_commande VARCHAR(50),
    commentaire TEXT,
    date_publication DATE,
    PRIMARY KEY(id_avis),
    FOREIGN KEY(id_client) REFERENCES client(id_client),
    FOREIGN KEY(id_cuisinier) REFERENCES cuisinier(id_cuisinier),
    FOREIGN KEY(id_commande) REFERENCES Commande_(id_commande)
);

CREATE TABLE Recette_(
    id_recette VARCHAR(50),
    nom VARCHAR(100),
    description TEXT,
    origine VARCHAR(100),
    PRIMARY KEY(id_recette)
);

CREATE TABLE Plat_(
    id_plat VARCHAR(50),
    id_cuisinier VARCHAR(50),
    nom VARCHAR(100),
    type__entrée__plat__dessert_ VARCHAR(50),
    portions VARCHAR(50),
    date_fabrication DATE,
    date_péremption DATE,
    prix_par_personne DECIMAL(10,2),
    nationalité_chinoise__mexicaine___ VARCHAR(100),
    régime VARCHAR(100),
    photo VARCHAR(255),
    PRIMARY KEY(id_plat),
    FOREIGN KEY(id_cuisinier) REFERENCES cuisinier(id_cuisinier)
);

CREATE TABLE LigneCommande_(
    id_ligne_commande VARCHAR(50),
    id_commande VARCHAR(50),
    id_plat VARCHAR(50),
    quantite INT,
    prix_total DECIMAL(10,2),
    date_livraison DATETIME,
    adresse_livraison VARCHAR(255),
    PRIMARY KEY(id_ligne_commande),
    FOREIGN KEY(id_commande) REFERENCES Commande_(id_commande),
    FOREIGN KEY(id_plat) REFERENCES Plat_(id_plat)
);



CREATE TABLE s_inspire_de(
    id_plat VARCHAR(50),
    id_recette VARCHAR(50),
    PRIMARY KEY(id_plat, id_recette),
    FOREIGN KEY(id_plat) REFERENCES Plat_(id_plat),
    FOREIGN KEY(id_recette) REFERENCES Recette_(id_recette)
);

CREATE TABLE Ingrédients(
    id_ingrédient VARCHAR(50),
    id_plat VARCHAR(50),
    nom VARCHAR(100),
    PRIMARY KEY(id_ingrédient),
    FOREIGN KEY(id_plat) REFERENCES Plat_(id_plat)
);
