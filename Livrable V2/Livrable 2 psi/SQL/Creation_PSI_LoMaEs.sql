DROP DATABASE IF EXISTS PSI;
DROP DATABASE IF EXISTS PSI_LoMaEs;
CREATE DATABASE PSI_LoMaEs;
USE PSI_LoMaEs;

CREATE TABLE utilisateur(
    id_utilisateur VARCHAR(50),
    nom VARCHAR(100),
    prénom VARCHAR(100),
    email VARCHAR(150),
    adresse VARCHAR(255),
    telephone VARCHAR(50),
    mot_de_passe VARCHAR(100),
    PRIMARY KEY(id_utilisateur)
);

CREATE TABLE client(
    id_client VARCHAR(50),
    id_utilisateur VARCHAR(50),
    StationMetro VARCHAR(100),
    entreprise_nom VARCHAR(100),
    referent VARCHAR(100),
    PRIMARY KEY(id_client),
    FOREIGN KEY(id_utilisateur) REFERENCES utilisateur(id_utilisateur)
);

CREATE TABLE cuisinier(
    id_cuisinier VARCHAR(50),
    id_utilisateur VARCHAR(50),
    StationMetro VARCHAR(100),
    zones_livraison VARCHAR(255),
    note_moyenne DECIMAL(2,1),
    nombre_livraisons INT,
    PRIMARY KEY(id_cuisinier),
    FOREIGN KEY(id_utilisateur) REFERENCES utilisateur(id_utilisateur)
);

CREATE TABLE Plat_(
    id_plat VARCHAR(50),
    id_cuisinier VARCHAR(50),
    nom VARCHAR(100),
    type VARCHAR(50),
    portions VARCHAR(50),
    date_fabrication DATE,
    date_peremption DATE,
    prix_par_personne DECIMAL(10,2),
    nationalite VARCHAR(100),
    regime VARCHAR(100),
    photo VARCHAR(255),
    PRIMARY KEY(id_plat),
    FOREIGN KEY(id_cuisinier) REFERENCES cuisinier(id_cuisinier)
);

CREATE TABLE Commande_(
    id_commande VARCHAR(50),
    id_client VARCHAR(50),
    id_cuisinier VARCHAR(50),
    id_plat VARCHAR(50),
    date_commande DATETIME,
    prix_total DECIMAL(10,2),
    statut VARCHAR(50),
    PRIMARY KEY(id_commande),
    FOREIGN KEY(id_client) REFERENCES client(id_client),
    FOREIGN KEY(id_cuisinier) REFERENCES cuisinier(id_cuisinier),
    FOREIGN KEY(id_plat) REFERENCES Plat_(id_plat)
);

CREATE TABLE Livraison(
    id_livraison VARCHAR(50),
    id_commande VARCHAR(50),
    date_livraison DATETIME,
    trajet VARCHAR(255),
    PRIMARY KEY(id_livraison),
    FOREIGN KEY(id_commande) REFERENCES Commande_(id_commande)
);

CREATE TABLE Transaction_(
    id_transaction VARCHAR(50),
    id_commande VARCHAR(50),
    montant DECIMAL(10,2),
    date_paiement DATETIME,
    statut VARCHAR(50),
    PRIMARY KEY(id_transaction),
    FOREIGN KEY(id_commande) REFERENCES Commande_(id_commande)
);

CREATE TABLE Avis_(
    id_avis VARCHAR(50),
    id_client VARCHAR(50),
    id_cuisinier VARCHAR(50),
    id_commande VARCHAR(50),
    note DECIMAL(2,1),
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

CREATE TABLE s_inspire_de(
    id_plat VARCHAR(50),
    id_recette VARCHAR(50),
    PRIMARY KEY(id_plat, id_recette),
    FOREIGN KEY(id_plat) REFERENCES Plat_(id_plat),
    FOREIGN KEY(id_recette) REFERENCES Recette_(id_recette)
);

CREATE TABLE Ingrédients(
    id_ingredient VARCHAR(50),
    id_plat VARCHAR(50),
    nom VARCHAR(100),
    PRIMARY KEY(id_ingredient),
    FOREIGN KEY(id_plat) REFERENCES Plat_(id_plat)
);
