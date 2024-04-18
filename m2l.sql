-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Hôte : localhost:3306
-- Généré le : jeu. 18 avr. 2024 à 07:12
-- Version du serveur :  5.7.24
-- Version de PHP : 7.2.19

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `m2l`
--
CREATE DATABASE IF NOT EXISTS `m2l` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `m2l`;

-- --------------------------------------------------------

--
-- Structure de la table `enfant`
--

CREATE TABLE `enfant` (
  `idenfant` int(11) NOT NULL,
  `nom` varchar(64) NOT NULL,
  `prenom` varchar(64) NOT NULL,
  `sexe` varchar(16) NOT NULL,
  `date` date NOT NULL,
  `numsecu` varchar(32) NOT NULL,
  `documents` varchar(64) NOT NULL,
  `cantine` varchar(64) NOT NULL,
  `contrainte` varchar(64) NOT NULL,
  `observation` varchar(128) DEFAULT NULL,
  `vaccin` varchar(64) DEFAULT NULL,
  `nomresponsable` varchar(64) DEFAULT NULL,
  `nommedecin` varchar(64) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `enfant`
--

INSERT INTO `enfant` (`idenfant`, `nom`, `prenom`, `sexe`, `date`, `numsecu`, `documents`, `cantine`, `contrainte`, `observation`, `vaccin`, `nomresponsable`, `nommedecin`) VALUES
(1, 'LITOTO', 'Toto', 'Garçon', '2010-11-01', '185057800608436', 'Oui', 'Oui', 'Non', 'Aucune observation particulère', 'En règle', 'LITOTO', 'DANIEL');

-- --------------------------------------------------------

--
-- Structure de la table `famille`
--

CREATE TABLE `famille` (
  `idparent` int(11) NOT NULL,
  `nom1` varchar(64) NOT NULL,
  `prenom1` varchar(64) NOT NULL,
  `telperso1` varchar(10) NOT NULL,
  `telpro1` varchar(10) NOT NULL,
  `nom2` varchar(64) DEFAULT NULL,
  `prenom2` varchar(64) DEFAULT NULL,
  `telperso2` varchar(10) DEFAULT NULL,
  `telpro2` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `famille`
--

INSERT INTO `famille` (`idparent`, `nom1`, `prenom1`, `telperso1`, `telpro1`, `nom2`, `prenom2`, `telperso2`, `telpro2`) VALUES
(1, 'LITOTO', 'Toto', '0102030405', '0102030405', 'DUBOIS', 'Antoine', '0102030405', '0102030405');

-- --------------------------------------------------------

--
-- Structure de la table `medecin`
--

CREATE TABLE `medecin` (
  `idmedecin` int(11) NOT NULL,
  `nom` varchar(64) NOT NULL,
  `tel` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `medecin`
--

INSERT INTO `medecin` (`idmedecin`, `nom`, `tel`) VALUES
(1, 'DANIEL', '0102030405');

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `enfant`
--
ALTER TABLE `enfant`
  ADD PRIMARY KEY (`idenfant`),
  ADD KEY `FOREIGN KEY` (`nomresponsable`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
