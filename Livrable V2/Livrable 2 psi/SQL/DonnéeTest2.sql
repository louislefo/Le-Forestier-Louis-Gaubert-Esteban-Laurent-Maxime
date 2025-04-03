USE PSI_LoMaEs;

-- Insertion des utilisateurs
INSERT INTO utilisateur (id_utilisateur, nom, prénom, email, adresse, telephone, mot_de_passe) VALUES
('USR001', 'Dupont', 'Jean', 'jean.dupont@gmail.com', '12 rue du Faubourg, Paris', '0678451236', 'MotDePasse123'),
('USR002', 'Moreau', 'Alice', 'alice.moreau@yahoo.fr', '35 avenue des Champs-Élysées, Paris', '0712345698', 'Alice2024'),
('USR003', 'Lefebvre', 'Thomas', 'thomas.lefebvre@hotmail.com', '8 boulevard Haussmann, Paris', '0623457896', 'Thomas123!'),
('USR004', 'Roux', 'Camille', 'camille.roux@gmail.com', '16 rue de la République, Paris', '0745698712', 'CamRoux2024'),
('USR005', 'Lambert', 'Julie', 'julie.lambert@outlook.fr', '27 rue Saint-Honoré, Paris', '0698745632', 'JulieL456');

-- Insertion des clients
INSERT INTO client (id_client, id_utilisateur, StationMetro, entreprise_nom, referent) VALUES
('CLI001', 'USR001', 'Châtelet', NULL, NULL),
('CLI002', 'USR002', 'Franklin D. Roosevelt', 'Marketing Plus', 'Sophie Durand'),
('CLI003', 'USR003', 'Opéra', 'Tech Innovations', 'Paul Martin'),
('CLI004', 'USR004', 'Bastille', NULL, NULL),
('CLI005', 'USR005', 'Concorde', 'Design Studio', 'Marc Petit');

-- Insertion des cuisiniers
INSERT INTO cuisinier (id_cuisinier, id_utilisateur, StationMetro, zones_livraison, note_moyenne, nombre_livraisons) VALUES
('CUI001', 'USR001', 'Châtelet', '1er, 2e, 3e, 4e arrondissement', 4.7, 42),
('CUI002', 'USR002', 'Franklin D. Roosevelt', '8e, 16e arrondissement', 4.3, 28),
('CUI003', 'USR003', 'Opéra', '2e, 9e, 10e arrondissement', 4.9, 57),
('CUI004', 'USR004', 'Bastille', '4e, 11e, 12e arrondissement', 4.1, 31),
('CUI005', 'USR005', 'Concorde', '1er, 8e arrondissement', 4.6, 39);

-- Insertion des recettes
INSERT INTO Recette_ (id_recette, nom, description, origine) VALUES
('REC001', 'Boeuf Bourguignon', 'plat mijoté à base de boeuf et de vin rouge', 'France'),
('REC002', 'Risotto aux Champignons', 'riz crémeux aux champignons et parmesan', 'Italie'),
('REC003', 'Paella Valenciana', 'riz safrané avec fruits de mer et poulet', 'Espagne'),
('REC004', 'Tarte Tatin', 'tarte aux pommes caramélisées', 'France'),
('REC005', 'Curry Vert Thaï', 'curry épicé à base de lait de coco', 'Thaïlande');

-- Insertion des plats
INSERT INTO Plat_ (id_plat, id_cuisinier, nom, type, portions, date_fabrication, date_peremption, prix_par_personne, nationalite, regime, photo) VALUES
('PLA001', 'CUI001', 'Boeuf Bourguignon Maison', 'plat', '4 personnes', '2024-04-01', '2024-04-03', 22.50, 'française', 'standard', 'boeuf_bourguignon.jpg'),
('PLA002', 'CUI002', 'Risotto Forestier', 'plat', '2 personnes', '2024-04-01', '2024-04-02', 18.75, 'italienne', 'végétarien', 'risotto.jpg'),
('PLA003', 'CUI003', 'Paella Royale', 'plat', '6 personnes', '2024-04-01', '2024-04-03', 26.00, 'espagnole', 'standard', 'paella.jpg'),
('PLA004', 'CUI004', 'Tarte Tatin Traditionnelle', 'dessert', '8 parts', '2024-04-01', '2024-04-04', 15.25, 'française', 'végétarien', 'tarte_tatin.jpg'),
('PLA005', 'CUI005', 'Curry Vert au Poulet', 'plat', '3 personnes', '2024-04-01', '2024-04-02', 20.50, 'thaïlandaise', 'standard', 'curry_vert.jpg');

-- Insertion des relations plat-recette
INSERT INTO s_inspire_de (id_plat, id_recette) VALUES
('PLA001', 'REC001'),
('PLA002', 'REC002'),
('PLA003', 'REC003'),
('PLA004', 'REC004'),
('PLA005', 'REC005');

-- Insertion des ingrédients
INSERT INTO Ingrédients (id_ingredient, id_plat, nom) VALUES
('ING001', 'PLA001', 'Boeuf'),
('ING002', 'PLA001', 'Vin rouge de Bourgogne'),
('ING003', 'PLA002', 'Riz arborio'),
('ING004', 'PLA002', 'Champignons de Paris'),
('ING005', 'PLA003', 'Riz bomba'),
('ING006', 'PLA003', 'Fruits de mer'),
('ING007', 'PLA004', 'Pommes Golden'),
('ING008', 'PLA004', 'Pâte brisée'),
('ING009', 'PLA005', 'Poulet'),
('ING010', 'PLA005', 'Lait de coco');

-- Insertion des commandes
INSERT INTO Commande_ (id_commande, id_client, id_cuisinier, id_plat, date_commande, prix_total, statut) VALUES
('COM001', 'CLI001', 'CUI003', 'PLA003', '2024-04-02 18:15:00', 52.00, 'En préparation'),
('COM002', 'CLI002', 'CUI005', 'PLA005', '2024-04-02 19:30:00', 41.00, 'Confirmée'),
('COM003', 'CLI003', 'CUI001', 'PLA001', '2024-04-03 12:45:00', 45.00, 'Livrée'),
('COM004', 'CLI004', 'CUI002', 'PLA002', '2024-04-03 20:00:00', 37.50, 'En attente'),
('COM005', 'CLI005', 'CUI004', 'PLA004', '2024-04-04 13:15:00', 30.50, 'Annulée');

-- Insertion des livraisons
INSERT INTO Livraison (id_livraison, id_commande, date_livraison, trajet) VALUES
('LIV001', 'COM001', '2024-04-02 19:45:00', 'Opéra -> Châtelet'),
('LIV002', 'COM002', '2024-04-02 20:20:00', 'Concorde -> Franklin D. Roosevelt'),
('LIV003', 'COM003', '2024-04-03 13:30:00', 'Châtelet -> Opéra'),
('LIV004', 'COM004', NULL, 'Franklin D. Roosevelt -> Bastille'),
('LIV005', 'COM005', NULL, 'Bastille -> Concorde');

-- Insertion des transactions
INSERT INTO Transaction_ (id_transaction, id_commande, montant, date_paiement, statut) VALUES
('TRA001', 'COM001', 52.00, '2024-04-02 18:20:00', 'Payé'),
('TRA002', 'COM002', 41.00, '2024-04-02 19:35:00', 'En attente'),
('TRA003', 'COM003', 45.00, '2024-04-03 12:50:00', 'Payé'),
('TRA004', 'COM004', 37.50, NULL, 'En attente'),
('TRA005', 'COM005', 30.50, '2024-04-04 13:20:00', 'Remboursé');

-- Insertion des avis
INSERT INTO Avis_ (id_avis, id_client, id_cuisinier, id_commande, note, commentaire, date_publication) VALUES
('AVI001', 'CLI001', 'CUI003', 'COM001', 4.5, 'Excellente paella, très parfumée et généreuse en fruits de mer', '2024-04-03'),
('AVI002', 'CLI002', 'CUI005', 'COM002', 4.2, 'Curry délicieux mais un peu trop épicé à mon goût', '2024-04-03'),
('AVI003', 'CLI003', 'CUI001', 'COM003', 4.8, 'Boeuf tendre et sauce savoureuse, parfait !', '2024-04-04'),
('AVI004', 'CLI004', 'CUI002', 'COM004', 3.9, 'Bon risotto mais il manquait un peu de crémeux', '2024-04-05'),
('AVI005', 'CLI005', 'CUI004', 'COM005', NULL, NULL, NULL);
