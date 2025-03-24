drop database if exists PSI;
create database PSI_LoMaEs;
use PSI_LoMaEs;

CREATE TABLE utilisateur( 
id VARCHAR(50),
nom VARCHAR(100), 
prénom VARCHAR(100), 
email VARCHAR(150), 
téléphone VARCHAR(50), 
adresse VARCHAR(255), 
type__Cuisinier_Client_ VARCHAR(50), 
mot_de_passe VARCHAR(100), 
PRIMARY KEY(id) 
); 
CREATE TABLE cuisinier( 
id VARCHAR(50), 
type__Cuisinier_Client_ VARCHAR(50), 
zones_livraison VARCHAR(255), 
note_moyenne DECIMAL(2,1), 
nombre_livraisons INT, 
id_1 VARCHAR(50) NOT NULL, 
PRIMARY KEY(id), 
UNIQUE(id_1), 
FOREIGN KEY(id_1) REFERENCES utilisateur(id) 
); 
CREATE TABLE client( 
id VARCHAR(50), 
id_utilisateur VARCHAR(50), 
type_client__Particulier_Entreprise_ VARCHAR(50), 
entreprise_nom VARCHAR(100), 
référent VARCHAR(100), 
id_1 VARCHAR(50) NOT NULL, 
PRIMARY KEY(id), 
UNIQUE(id_1), 
FOREIGN KEY(id_1) REFERENCES utilisateur(id) 
); 
CREATE TABLE Commande_( 
id VARCHAR(50), 
id_client VARCHAR(50), 
total_prix DECIMAL(10,2), 
statut_En_attente__Confirmée__En_cours__Livrée__Annulée_ VARCHAR(50),
date_commande DATE, 
id_1 VARCHAR(50) NOT NULL, 
PRIMARY KEY(id), 
FOREIGN KEY(id_1) REFERENCES client(id) 
); 
CREATE TABLE Livraison( 
id VARCHAR(50), 
id_cuisinier VARCHAR(50), 
id_commande VARCHAR(50), 
id_ligne_commande VARCHAR(50), 
date_livraison DATETIME, 
trajet VARCHAR(255), 
id_1 VARCHAR(50) NOT NULL, 
PRIMARY KEY(id), 
FOREIGN KEY(id_1) REFERENCES cuisinier(id)
 ); 
CREATE TABLE Transaction_( 
id VARCHAR(50), 
id_commande VARCHAR(50), 
montant DECIMAL(10,2), 
date_paiement DATETIME, 
statut_Payé__En_attente__Échoué_ VARCHAR(50), 
id_1 VARCHAR(50) NOT NULL, 
PRIMARY KEY(id), 
UNIQUE(id_1), 
FOREIGN KEY(id_1) REFERENCES Commande_(id) 
); 
CREATE TABLE Avis_( 
id VARCHAR(50), 
id_client VARCHAR(50), 
id_cuisinier VARCHAR(50), 
note DECIMAL(2,1), 
id_commande VARCHAR(50), 
commentaire TEXT, 
date_publication DATE, 
id_1 VARCHAR(50) NOT NULL, 
id_2 VARCHAR(50) NOT NULL, 
PRIMARY KEY(id), 
FOREIGN KEY(id_1) REFERENCES cuisinier(id), 
FOREIGN KEY(id_2) REFERENCES client(id) 
); 
CREATE TABLE Recette_( 
id VARCHAR(50), 
nom VARCHAR(100), 
description TEXT, 
ingrédients TEXT, 
origine VARCHAR(100), 
PRIMARY KEY(id) 
); 
CREATE TABLE LigneCommande_( 
id VARCHAR(50), 
id_commande VARCHAR(50), 
id_plat VARCHAR(50), 
quantite INT, 
prix_total DECIMAL(10,2), 
date_livraison DATETIME, 
adresse_livraison VARCHAR(255), 
id_1 VARCHAR(50) NOT NULL, 
id_2 VARCHAR(50) NOT NULL, 
PRIMARY KEY(id), 
UNIQUE(id_1), 
FOREIGN KEY(id_1) REFERENCES Livraison(id), 
FOREIGN KEY(id_2) REFERENCES Commande_(id) 
); 
CREATE TABLE Plat_( 
id VARCHAR(50), 
id_cuisinier VARCHAR(50), 
nom VARCHAR(100), 
type__entrée__plat__dessert_ VARCHAR(50), 
portions VARCHAR(50), 
date_fabrication DATE, 
date_péremption DATE, 
prix_par_personne DECIMAL(10,2), 
nationalité_chinoise__mexicaine___ VARCHAR(100), 
régime_Végétarien__Sans_gluten__Viande_Halal____ VARCHAR(100), 
ingrédients TEXT, 
photo VARCHAR(255), 
id_1 VARCHAR(50) NOT NULL, 
id_2 VARCHAR(50) NOT NULL, 
PRIMARY KEY(id), 
FOREIGN KEY(id_1) REFERENCES LigneCommande_(id), 
FOREIGN KEY(id_2) REFERENCES cuisinier(id) 
); 
CREATE TABLE s_inspire_de( 
id VARCHAR(50), 
id_1 VARCHAR(50), 
PRIMARY KEY(id, id_1), 
FOREIGN KEY(id) REFERENCES Plat_(id), 
FOREIGN KEY(id_1) REFERENCES Recette_(id) 
);
