-- données test 

USE PSI_LoMaEs;

-- Insertion des utilisateurs
INSERT INTO utilisateur (id_utilisateur, nom, prénom, email, adresse, telephone, mot_de_passe) VALUES
('U001', 'Martin', 'Sophie', 'sophie.martin@email.com', '15 rue de la Paix, Paris', '0612345678', 'Mdp123'),
('U002', 'Dubois', 'Pierre', 'pierre.dubois@email.com', '23 avenue Victor Hugo, Paris', '0623456789', 'Mdp456'),
('U003', 'Bernard', 'Marie', 'marie.bernard@email.com', '8 boulevard Saint-Michel, Paris', '0634567890', 'Mdp789'),
('U004', 'Petit', 'Lucas', 'lucas.petit@email.com', '45 rue du Louvre, Paris', '0645678901', 'Mdp101'),
('U005', 'Robert', 'Emma', 'emma.robert@email.com', '12 rue de Rivoli, Paris', '0656789012', 'Mdp102');

-- Insertion des cuisiniers
INSERT INTO cuisinier (id_cuisinier, id_utilisateur, StationMetro, zones_livraison, note_moyenne, nombre_livraisons) VALUES
('C001', 'U001', 'Tuileries', '1er, 2e, 3e arrondissement', 4.5, 25),
('C002', 'U003', 'Saint-Michel', '4e, 5e, 6e arrondissement', 4.8, 30),
('C003', 'U005', 'Louvre-Rivoli', '7e, 8e, 9e arrondissement', 4.2, 15);

-- Insertion des clients
INSERT INTO client (id_client, id_utilisateur, StationMetro, entreprise_nom, referent) VALUES
('CL001', 'U002', 'Victor Hugo', NULL, NULL),
('CL002', 'U004', 'Louvre-Palais Royal', 'Tech Solutions', 'Jean Dupont');

-- Insertion des recettes
INSERT INTO Recette_ (id_recette, nom, description, origine) VALUES
('R001', 'Coq au Vin Traditionnel', 'Recette classique française', 'France'),
('R002', 'Pad Thai Authentique', 'Recette traditionnelle thaïlandaise', 'Thaïlande'),
('R003', 'Tiramisu Italien', 'Dessert italien classique', 'Italie'),
('R004', 'Sushi California', 'Recette de sushi populaire', 'Japon'),
('R005', 'Ratatouille Provençale', 'Plat traditionnel provençal', 'France');

-- Insertion des plats
INSERT INTO Plat_ (id_plat, id_cuisinier, nom, type, portions, date_fabrication, date_peremption, prix_par_personne, nationalite, regime, photo) VALUES
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
INSERT INTO Ingrédients (id_ingredient, id_plat, nom) VALUES
('I001', 'P001', 'Poulet'),
('I002', 'P001', 'Vin rouge'),
('I003', 'P002', 'Nouilles de riz'),
('I004', 'P002', 'Crevettes'),
('I005', 'P003', 'Café');

-- Insertion des commandes
INSERT INTO Commande_ (id_commande, id_client, date_commande, prix_total, statut) VALUES
('CMD001', 'CL001', '2024-03-20 19:30:00', 43.00, 'Livrée'),
('CMD002', 'CL002', '2024-03-21 20:00:00', 60.00, 'En cours'),
('CMD003', 'CL001', '2024-03-22 19:45:00', 35.00, 'Confirmée'),
('CMD004', 'CL002', '2024-03-23 19:35:00', 45.00, 'En attente'),
('CMD005', 'CL001', '2024-03-24 19:35:00', 55.00, 'Livrée');

-- Insertion des livraisons
INSERT INTO Livraison (id_livraison, id_commande, date_livraison, trajet) VALUES
('L001', 'CMD001', '2024-03-20 19:30:00', 'Station A -> Station B -> Station C'),
('L002', 'CMD002', '2024-03-21 20:00:00', 'Station D -> Station E -> Station F'),
('L003', 'CMD003', '2024-03-22 19:45:00', 'Station G -> Station H -> Station I'),
('L004', 'CMD004', NULL, NULL),
('L005', 'CMD005', '2024-03-24 19:35:00', 'Station J -> Station K -> Station L');

-- Insertion des transactions
INSERT INTO Transaction_ (id_transaction, id_commande, montant, date_paiement, statut) VALUES
('T001', 'CMD001', 43.00, '2024-03-20 19:25:00', 'Payé'),
('T002', 'CMD002', 60.00, '2024-03-21 19:55:00', 'Payé'),
('T003', 'CMD003', 35.00, '2024-03-22 19:40:00', 'En attente'),
('T004', 'CMD004', 45.00, NULL, 'En attente'),
('T005', 'CMD005', 55.00, '2024-03-24 19:35:00', 'Payé');

-- Insertion des avis
INSERT INTO Avis_ (id_avis, id_client, id_cuisinier, id_commande, note, commentaire, date_publication) VALUES
('A001', 'CL001', 'C001', 'CMD001', 5.0, 'Excellent plat, livraison rapide', '2024-03-21'),
('A002', 'CL002', 'C002', 'CMD002', 4.5, 'Très bon service', '2024-03-22'),
('A003', 'CL001', 'C003', 'CMD003', 4.0, 'Bon rapport qualité-prix', '2024-03-23'),
('A004', 'CL002', 'C001', 'CMD004', 4.8, 'Délicieux, à recommander', '2024-03-24'),
('A005', 'CL001', 'C002', 'CMD005', 5.0, 'Parfait, je recommande', '2024-03-25'); 