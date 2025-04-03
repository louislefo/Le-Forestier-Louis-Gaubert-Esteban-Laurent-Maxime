-- données test 

USE PSI_LoMaEs;

-- Insertion des utilisateurs
INSERT INTO utilisateur (id_utilisateur, nom, prénom, email, téléphone, adresse, type__Cuisinier_Client_, mot_de_passe) VALUES
('U001', 'Martin', 'Sophie', 'sophie.martin@email.com', '0612345678', '15 rue de la Paix, Paris', 'Cuisinier', 'Mdp123'),
('U002', 'Dubois', 'Pierre', 'pierre.dubois@email.com', '0623456789', '23 avenue Victor Hugo, Paris', 'Client', 'Mdp456'),
('U003', 'Bernard', 'Marie', 'marie.bernard@email.com', '0634567890', '8 boulevard Saint-Michel, Paris', 'Cuisinier', 'Mdp789'),
('U004', 'Petit', 'Lucas', 'lucas.petit@email.com', '0645678901', '45 rue du Louvre, Paris', 'Client', 'Mdp101'),
('U005', 'Robert', 'Emma', 'emma.robert@email.com', '0656789012', '12 rue de Rivoli, Paris', 'Cuisinier', 'Mdp102');

-- Insertion des cuisiniers
INSERT INTO cuisinier (id_cuisinier, type__Cuisinier_Client_, zones_livraison, note_moyenne, nombre_livraisons, id_utilisateur, StationMetro) VALUES
('C001', 'Cuisinier', '1er, 2e, 3e arrondissement', 4.5, 25, 'U001', 'Tuileries'),
('C002', 'Cuisinier', '4e, 5e, 6e arrondissement', 4.8, 30, 'U003', 'Saint-Michel'),
('C003', 'Cuisinier', '7e, 8e, 9e arrondissement', 4.2, 15, 'U005', 'Louvre-Rivoli');

-- Insertion des clients
INSERT INTO client (id_client, id_utilisateur, type_client__Particulier_Entreprise_, entreprise_nom, référent, StationMetro) VALUES
('CL001', 'U002', 'Particulier', NULL, NULL, 'Victor Hugo'),
('CL002', 'U004', 'Entreprise', 'Tech Solutions', 'Jean Dupont', 'Louvre-Palais Royal');

-- Insertion des recettes
INSERT INTO Recette_ (id_recette, nom, description, origine) VALUES
('R001', 'Coq au Vin Traditionnel', 'Recette classique française', 'France'),
('R002', 'Pad Thai Authentique', 'Recette traditionnelle thaïlandaise', 'Thaïlande'),
('R003', 'Tiramisu Italien', 'Dessert italien classique', 'Italie'),
('R004', 'Sushi California', 'Recette de sushi populaire', 'Japon'),
('R005', 'Ratatouille Provençale', 'Plat traditionnel provençal', 'France');

-- Insertion des plats
INSERT INTO Plat_ (id_plat, id_cuisinier, nom, type__entrée__plat__dessert_, portions, date_fabrication, date_péremption, prix_par_personne, nationalité_chinoise__mexicaine___, régime, photo) VALUES
('P001', 'C001', 'Coq au Vin', 'plat', '4 personnes', '2024-03-25', '2024-03-26', 25.00, 'française', 'standard', 'coq_vin.jpg'),
('P002', 'C002', 'Pad Thai', 'plat', '2 personnes', '2024-03-25', '2024-03-26', 18.00, 'thaïlandaise', 'végétarien', 'pad_thai.jpg'),
('P003', 'C003', 'Tiramisu', 'dessert', '6 personnes', '2024-03-25', '2024-03-26', 12.00, 'italienne', 'végétarien', 'tiramisu.jpg'),
('P004', 'C001', 'Sushi California', 'entrée', '8 pièces', '2024-03-25', '2024-03-26', 15.00, 'japonaise', 'standard', 'sushi.jpg'),
('P005', 'C002', 'Ratatouille', 'plat', '4 personnes', '2024-03-25', '2024-03-26', 20.00, 'française', 'végétarien', 'ratatouille.jpg');

-- Insertion des relations plat-recette
INSERT INTO s_inspire_de (id_plat, id_recette) VALUES
('P001', 'R001'),
('P002', 'R002'),
('P003', 'R003'),
('P004', 'R004'),
('P005', 'R005');

-- Insertion des ingrédients
INSERT INTO Ingrédients (id_ingrédient, id_plat, nom) VALUES
('I001', 'P001', 'Poulet'),
('I002', 'P001', 'Vin rouge'),
('I003', 'P002', 'Nouilles de riz'),
('I004', 'P002', 'Crevettes'),
('I005', 'P003', 'Café');

-- Insertion des commandes
INSERT INTO Commande_ (id_commande, id_client, total_prix, statut_En_attente__Confirmée__En_cours__Livrée__Annulée_, date_commande) VALUES
('CMD001', 'CL001', 43.00, 'Livrée', '2024-03-20'),
('CMD002', 'CL002', 60.00, 'En cours', '2024-03-21'),
('CMD003', 'CL001', 35.00, 'Confirmée', '2024-03-22'),
('CMD004', 'CL002', 45.00, 'En attente', '2024-03-23'),
('CMD005', 'CL001', 55.00, 'Livrée', '2024-03-24');

-- Insertion des lignes de commande
INSERT INTO LigneCommande_ (id_ligne_commande, id_commande, id_plat, quantite, prix_total, date_livraison, adresse_livraison) VALUES
('LC001', 'CMD001', 'P001', 1, 25.00, '2024-03-20 19:30:00', '15 rue de la Paix, Paris'),
('LC002', 'CMD001', 'P003', 1, 12.00, '2024-03-20 19:30:00', '15 rue de la Paix, Paris'),
('LC003', 'CMD002', 'P002', 2, 36.00, '2024-03-21 20:00:00', '23 avenue Victor Hugo, Paris'),
('LC004', 'CMD002', 'P004', 1, 15.00, '2024-03-21 20:00:00', '23 avenue Victor Hugo, Paris'),
('LC005', 'CMD003', 'P005', 1, 20.00, '2024-03-22 19:45:00', '8 boulevard Saint-Michel, Paris');

-- Insertion des transactions
INSERT INTO Transaction_ (id_transaction, id_commande, montant, date_paiement, statut_Payé__En_attente__Échoué_) VALUES
('T001', 'CMD001', 43.00, '2024-03-20 19:25:00', 'Payé'),
('T002', 'CMD002', 60.00, '2024-03-21 19:55:00', 'Payé'),
('T003', 'CMD003', 35.00, '2024-03-22 19:40:00', 'En attente'),
('T004', 'CMD004', 45.00, NULL, 'En attente'),
('T005', 'CMD005', 55.00, '2024-03-24 19:35:00', 'Payé');

-- Insertion des livraisons
INSERT INTO Livraison (id_livraison, id_cuisinier, id_commande, id_ligne_commande, date_livraison, trajet) VALUES
('L001', 'C001', 'CMD001', 'LC001', '2024-03-20 19:30:00', 'Station A -> Station B -> Station C'),
('L002', 'C002', 'CMD002', 'LC003', '2024-03-21 20:00:00', 'Station D -> Station E -> Station F'),
('L003', 'C003', 'CMD003', 'LC005', '2024-03-22 19:45:00', 'Station G -> Station H -> Station I'),
('L004', 'C001', 'CMD004', 'LC004', NULL, NULL),
('L005', 'C002', 'CMD005', 'LC002', '2024-03-24 19:35:00', 'Station J -> Station K -> Station L');

-- Insertion des avis
INSERT INTO Avis_ (id_avis, id_client, id_cuisinier, note, id_commande, commentaire, date_publication) VALUES
('A001', 'CL001', 'C001', 5.0, 'CMD001', 'Excellent plat, livraison rapide', '2024-03-21'),
('A002', 'CL002', 'C002', 4.5, 'CMD002', 'Très bon service', '2024-03-22'),
('A003', 'CL001', 'C003', 4.0, 'CMD003', 'Bon rapport qualité-prix', '2024-03-23'),
('A004', 'CL002', 'C001', 4.8, 'CMD004', 'Délicieux, à recommander', '2024-03-24'),
('A005', 'CL001', 'C002', 5.0, 'CMD005', 'Parfait, je recommande', '2024-03-25'); 