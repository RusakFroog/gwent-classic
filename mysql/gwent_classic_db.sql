-- --------------------------------------------------------
-- Хост:                         127.0.0.1
-- Версия сервера:               9.0.1 - MySQL Community Server - GPL
-- Операционная система:         Linux
-- HeidiSQL Версия:              12.6.0.6765
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Дамп структуры базы данных gwent_classic
CREATE DATABASE IF NOT EXISTS `gwent_classic` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `gwent_classic`;

-- Дамп структуры для таблица gwent_classic.accounts
CREATE TABLE IF NOT EXISTS `accounts` (
  `id` int NOT NULL AUTO_INCREMENT,
  `login` varchar(40) COLLATE utf8mb4_general_ci NOT NULL,
  `name` varchar(20) COLLATE utf8mb4_general_ci NOT NULL,
  `email` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `hashed_password` longtext COLLATE utf8mb4_general_ci NOT NULL,
  `decks` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  CONSTRAINT `accounts_chk_1` CHECK (json_valid(`decks`))
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Дамп данных таблицы gwent_classic.accounts: ~0 rows (приблизительно)
REPLACE INTO `accounts` (`id`, `login`, `name`, `email`, `hashed_password`, `decks`) VALUES
	(2, 'rusak', 'rusak', '123@gma.cm', '$2a$11$xtXwUj6R9NRtPENeYN9eb.2UQf1Kly6q2nBp/rQvvEaqUQ45vFQYK', '{"None":[],"Monsters":[],"Nilfgaardian":[],"NorthenRealms":[],"Scoiatael":[],"Skellige":[]}');

-- Дамп структуры для таблица gwent_classic.cards
CREATE TABLE IF NOT EXISTS `cards` (
  `id` int NOT NULL AUTO_INCREMENT,
  `type_id` tinyint NOT NULL,
  `strength` int NOT NULL,
  `fraction_id` tinyint NOT NULL DEFAULT (0),
  `field_lines` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `is_hero` tinyint(1) NOT NULL DEFAULT (0),
  `muster_cards` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  KEY `FK_fractions_cards` (`fraction_id`) USING BTREE,
  KEY `FK_card_types_cards` (`type_id`),
  CONSTRAINT `FK_card_types_cards` FOREIGN KEY (`type_id`) REFERENCES `card_types` (`id`),
  CONSTRAINT `FK_fractions_cards` FOREIGN KEY (`fraction_id`) REFERENCES `fractions` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=187 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Дамп данных таблицы gwent_classic.cards: ~175 rows (приблизительно)
REPLACE INTO `cards` (`id`, `type_id`, `strength`, `fraction_id`, `field_lines`, `is_hero`, `muster_cards`) VALUES
	(1, 3, 2, 0, '["Siege"]', 0, '[1]'),
	(2, 3, 4, 0, '["Ranger"]', 0, '[2]'),
	(3, 0, 5, 0, '["Closer"]', 0, '[]'),
	(4, 0, 5, 0, '["Closer"]', 0, '[]'),
	(5, 0, 6, 0, '["Closer"]', 0, '[]'),
	(6, 5, 6, 0, '["Closer", "Ranger"]', 0, '[]'),
	(7, 0, 3, 0, '["Closer"]', 0, '[]'),
	(8, 6, 8, 0, '["Closer"]', 0, '[]'),
	(9, 7, 2, 0, '["Closer"]', 0, '[]'),
	(12, 0, 6, 1, '["Siege"]', 0, '[]'),
	(13, 0, 6, 1, '["Closer"]', 0, '[]'),
	(14, 0, 6, 1, '["Siege"]', 0, '[]'),
	(15, 3, 6, 1, '["Siege"]', 0, '[15]'),
	(16, 3, 6, 1, '["Closer"]', 0, '[16]'),
	(17, 3, 6, 1, '["Closer"]', 0, '[17]'),
	(18, 3, 6, 1, '["Closer"]', 0, '[18]'),
	(19, 0, 5, 1, '["Closer"]', 0, '[]'),
	(20, 0, 5, 1, '["Closer"]', 0, '[]'),
	(21, 0, 5, 1, '["Closer"]', 0, '[]'),
	(22, 0, 5, 1, '["Closer"]', 0, '[]'),
	(23, 0, 5, 1, '["Closer"]', 0, '[]'),
	(24, 0, 5, 1, '["Siege"]', 0, '[]'),
	(25, 0, 5, 1, '["Ranger"]', 0, '[]'),
	(26, 3, 5, 1, '["Closer"]', 0, '[26]'),
	(27, 0, 4, 1, '["Closer"]', 0, '[]'),
	(28, 3, 4, 1, '["Closer"]', 0, '[28, 29, 30]'),
	(29, 3, 4, 1, '["Closer"]', 0, '[28, 29, 30]'),
	(30, 3, 4, 1, '["Closer"]', 0, '[28, 29, 30]'),
	(31, 3, 4, 1, '["Closer"]', 0, '[31]'),
	(32, 3, 4, 1, '["Closer"]', 0, '[32]'),
	(33, 3, 4, 1, '["Closer"]', 0, '[33]'),
	(34, 3, 4, 1, '["Closer"]', 0, '[34]'),
	(35, 0, 2, 1, '["Ranger"]', 0, '[]'),
	(36, 0, 2, 1, '["Closer", "Ranger"]', 0, '[]'),
	(37, 0, 2, 1, '["Ranger"]', 0, '[]'),
	(38, 0, 2, 1, '["Ranger"]', 0, '[]'),
	(39, 0, 2, 1, '["Closer", "Ranger"]', 0, '[]'),
	(40, 0, 2, 1, '["Closer"]', 0, '[]'),
	(41, 0, 2, 1, '["Ranger"]', 0, '[]'),
	(42, 3, 2, 1, '["Closer"]', 0, '[42, 43, 44]'),
	(43, 3, 2, 1, '["Closer"]', 0, '[42, 43, 44]'),
	(44, 3, 2, 1, '["Closer"]', 0, '[42, 43, 44]'),
	(45, 3, 1, 1, '["Closer"]', 0, '[45, 46, 47]'),
	(46, 3, 1, 1, '["Closer"]', 0, '[45, 46, 47]'),
	(47, 3, 1, 1, '["Closer"]', 0, '[45, 46, 47]'),
	(48, 5, 8, 1, '["Closer", "Ranger"]', 1, '[]'),
	(49, 0, 10, 1, '["Closer"]', 1, '[]'),
	(50, 0, 10, 1, '["Closer"]', 1, '[]'),
	(51, 0, 10, 1, '["Ranger"]', 1, '[]'),
	(52, 1, 1, 2, '["Ranger"]', 0, '[]'),
	(53, 1, 1, 2, '["Ranger"]', 0, '[]'),
	(54, 4, 2, 2, '["Closer"]', 0, '[]'),
	(55, 0, 2, 2, '["Closer"]', 0, '[]'),
	(56, 0, 2, 2, '["Ranger"]', 0, '[]'),
	(57, 0, 2, 2, '["Closer"]', 0, '[]'),
	(58, 4, 3, 2, '["Closer"]', 0, '[]'),
	(59, 0, 3, 2, '["Ranger"]', 0, '[]'),
	(60, 0, 3, 2, '["Siege"]', 0, '[]'),
	(61, 0, 3, 2, '["Closer"]', 0, '[]'),
	(62, 2, 4, 2, '["Closer"]', 0, '[]'),
	(63, 0, 4, 2, '["Ranger"]', 0, '[]'),
	(64, 0, 4, 2, '["Closer"]', 0, '[]'),
	(65, 0, 4, 2, '["Ranger"]', 0, '[]'),
	(66, 4, 5, 2, '["Closer"]', 0, '[]'),
	(67, 4, 5, 2, '["Closer"]', 0, '[]'),
	(68, 0, 5, 2, '["Siege"]', 0, '[]'),
	(69, 0, 5, 2, '["Ranger"]', 0, '[]'),
	(70, 0, 6, 2, '["Siege"]', 0, '[]'),
	(71, 0, 6, 2, '["Closer"]', 0, '[]'),
	(72, 0, 6, 2, '["Ranger"]', 0, '[]'),
	(73, 0, 6, 2, '["Ranger"]', 0, '[]'),
	(74, 2, 7, 2, '["Closer"]', 0, '[]'),
	(75, 2, 9, 2, '["Closer"]', 0, '[]'),
	(76, 0, 10, 2, '["Ranger"]', 0, '[]'),
	(77, 0, 10, 2, '["Siege"]', 0, '[]'),
	(78, 0, 10, 2, '["Ranger"]', 0, '[]'),
	(79, 1, 0, 2, '["Siege"]', 0, '[]'),
	(80, 0, 10, 2, '["Closer"]', 1, '[]'),
	(81, 1, 10, 2, '["Closer"]', 1, '[]'),
	(82, 0, 10, 2, '["Siege"]', 1, '[]'),
	(83, 0, 10, 2, '["Ranger"]', 1, '[]'),
	(84, 1, 1, 3, '["Siege"]', 0, '[]'),
	(85, 1, 1, 3, '["Siege"]', 0, '[]'),
	(86, 1, 1, 3, '["Siege"]', 0, '[]'),
	(87, 0, 1, 3, '["Closer"]', 0, '[]'),
	(88, 0, 1, 3, '["Closer"]', 0, '[]'),
	(89, 4, 1, 3, '["Closer"]', 0, '[]'),
	(90, 2, 1, 3, '["Siege"]', 0, '[]'),
	(91, 0, 2, 3, '["Closer"]', 0, '[]'),
	(92, 4, 4, 3, '["Closer"]', 0, '[]'),
	(93, 0, 4, 3, '["Ranger"]', 0, '[]'),
	(94, 0, 4, 3, '["Ranger"]', 0, '[]'),
	(95, 2, 4, 3, '["Closer"]', 0, '[]'),
	(96, 1, 5, 3, '["Siege"]', 0, '[]'),
	(97, 4, 5, 3, '["Ranger"]', 0, '[]'),
	(98, 2, 5, 3, '["Closer"]', 0, '[]'),
	(99, 0, 5, 3, '["Ranger"]', 0, '[]'),
	(100, 0, 5, 3, '["Ranger"]', 0, '[]'),
	(101, 0, 5, 3, '["Closer"]', 0, '[]'),
	(102, 0, 5, 3, '["Closer"]', 0, '[]'),
	(103, 0, 6, 3, '["Siege"]', 0, '[]'),
	(104, 0, 6, 3, '["Siege"]', 0, '[]'),
	(105, 0, 6, 3, '["Siege"]', 0, '[]'),
	(106, 0, 6, 3, '["Siege"]', 0, '[]'),
	(107, 0, 6, 3, '["Siege"]', 0, '[]'),
	(108, 0, 6, 3, '["Ranger"]', 0, '[]'),
	(109, 4, 8, 3, '["Siege"]', 0, '[]'),
	(110, 0, 10, 3, '["Ranger"]', 1, '[]'),
	(111, 0, 10, 3, '["Closer"]', 1, '[]'),
	(112, 0, 10, 3, '["Closer"]', 1, '[]'),
	(113, 0, 10, 3, '["Closer"]', 1, '[]'),
	(114, 1, 0, 4, '["Ranger"]', 0, '[]'),
	(115, 1, 0, 4, '["Ranger"]', 0, '[]'),
	(116, 1, 0, 4, '["Ranger"]', 0, '[]'),
	(117, 0, 1, 4, '["Ranger"]', 0, '[]'),
	(118, 3, 2, 4, '["Ranger"]', 0, '[118, 119, 120]'),
	(119, 3, 2, 4, '["Ranger"]', 0, '[118, 119, 120]'),
	(120, 3, 2, 4, '["Ranger"]', 0, '[118, 119, 120]'),
	(121, 0, 2, 4, '["Ranger"]', 0, '[]'),
	(123, 3, 3, 4, '["Ranger"]', 0, '[123, 124, 125]'),
	(124, 3, 3, 4, '["Ranger"]', 0, '[123, 124, 125]'),
	(125, 3, 3, 4, '["Ranger"]', 0, '[123, 124, 125]'),
	(126, 0, 3, 4, '["Closer", "Ranger"]', 0, '[]'),
	(127, 0, 4, 4, '["Ranger"]', 0, '[]'),
	(128, 0, 4, 4, '["Ranger"]', 0, '[]'),
	(129, 0, 5, 4, '["Closer"]', 0, '[]'),
	(130, 0, 5, 4, '["Closer"]', 0, '[]'),
	(131, 0, 5, 4, '["Closer"]', 0, '[]'),
	(132, 0, 5, 4, '["Closer"]', 0, '[]'),
	(133, 0, 5, 4, '["Closer"]', 0, '[]'),
	(134, 0, 5, 4, '["Closer", "Ranger"]', 0, '[]'),
	(135, 0, 5, 4, '["Closer", "Ranger"]', 0, '[]'),
	(136, 3, 5, 4, '["Closer"]', 0, '[136, 137, 138]'),
	(137, 3, 5, 4, '["Closer"]', 0, '[136, 137, 138]'),
	(138, 3, 5, 4, '["Closer"]', 0, '[136, 137, 138]'),
	(139, 0, 6, 4, '["Closer", "Ranger"]', 0, '[]'),
	(140, 0, 6, 4, '["Closer", "Ranger"]', 0, '[]'),
	(141, 0, 6, 4, '["Closer", "Ranger"]', 0, '[]'),
	(142, 0, 6, 4, '["Closer", "Ranger"]', 0, '[]'),
	(143, 0, 6, 4, '["Closer", "Ranger"]', 0, '[]'),
	(144, 0, 6, 4, '["Closer", "Ranger"]', 0, '[]'),
	(145, 0, 6, 4, '["Ranger"]', 0, '[]'),
	(146, 0, 6, 4, '["Closer"]', 0, '[]'),
	(147, 6, 8, 4, '["Siege"]', 0, '[]'),
	(148, 5, 10, 4, '["Ranger"]', 0, '[]'),
	(149, 0, 10, 4, '["Ranger"]', 1, '[]'),
	(150, 0, 10, 4, '["Ranger"]', 1, '[]'),
	(151, 5, 10, 4, '["Closer"]', 1, '[]'),
	(152, 0, 10, 4, '["Ranger"]', 1, '[]'),
	(154, 1, 2, 5, '["Closer"]', 0, '[]'),
	(155, 7, 2, 5, '["Siege"]', 0, '[]'),
	(158, 4, 4, 5, '["Closer"]', 0, '[]'),
	(159, 4, 4, 5, '["Closer"]', 0, '[]'),
	(160, 4, 4, 5, '["Closer"]', 0, '[]'),
	(161, 0, 4, 5, '["Closer"]', 0, '[]'),
	(162, 0, 4, 5, '["Closer"]', 0, '[]'),
	(163, 0, 4, 5, '["Closer"]', 0, '[]'),
	(164, 0, 4, 5, '["Siege"]', 0, '[]'),
	(165, 3, 4, 5, '["Ranger"]', 0, '[165]'),
	(166, 0, 4, 5, '["Closer"]', 0, '[]'),
	(167, 0, 4, 5, '["Closer"]', 0, '[]'),
	(168, 0, 6, 5, '["Closer"]', 0, '[]'),
	(169, 0, 6, 5, '["Ranger"]', 0, '[]'),
	(170, 6, 6, 5, '["Ranger"]', 0, '[]'),
	(171, 4, 6, 5, '["Closer"]', 0, '[]'),
	(172, 0, 6, 5, '["Closer"]', 0, '[]'),
	(173, 4, 6, 5, '["Siege"]', 0, '[]'),
	(176, 5, 12, 5, '["Closer", "Ranger"]', 0, '[]'),
	(178, 0, 10, 5, '["Ranger"]', 1, '[]'),
	(179, 3, 10, 5, '["Closer"]', 1, '[158, 159, 160]'),
	(182, 2, 0, 0, '["Closer"]', 1, '[]'),
	(183, 0, 7, 0, '["Closer"]', 1, '[]'),
	(184, 1, 7, 0, '["Ranger"]', 1, '[]'),
	(185, 3, 15, 0, '["Closer"]', 1, '[7]'),
	(186, 3, 15, 0, '["Closer"]', 1, '[7]');

-- Дамп структуры для таблица gwent_classic.card_types
CREATE TABLE IF NOT EXISTS `card_types` (
  `id` tinyint NOT NULL AUTO_INCREMENT,
  `name` varchar(50) COLLATE utf8mb4_general_ci NOT NULL DEFAULT 'None',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Дамп данных таблицы gwent_classic.card_types: ~9 rows (приблизительно)
REPLACE INTO `card_types` (`id`, `name`) VALUES
	(0, 'None'),
	(1, 'Medic'),
	(2, 'Spy'),
	(3, 'Muster'),
	(4, 'TightBond'),
	(5, 'MoraleBoost'),
	(6, 'Scorch'),
	(7, 'CommanderHorn');

-- Дамп структуры для таблица gwent_classic.fractions
CREATE TABLE IF NOT EXISTS `fractions` (
  `id` tinyint NOT NULL,
  `name` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Дамп данных таблицы gwent_classic.fractions: ~6 rows (приблизительно)
REPLACE INTO `fractions` (`id`, `name`) VALUES
	(0, 'None'),
	(1, 'Monsters'),
	(2, 'Nilfgaardian'),
	(3, 'NorthenRealms'),
	(4, 'Scoiatael'),
	(5, 'Skellige');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
