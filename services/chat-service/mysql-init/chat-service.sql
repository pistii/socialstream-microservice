-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2025. Ápr 19. 16:07
-- Kiszolgáló verziója: 10.4.20-MariaDB-log
-- PHP verzió: 8.0.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `chat-service`
--
CREATE DATABASE IF NOT EXISTS `chat-service`;
USE `chat-service`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `chatcontent`
--

CREATE TABLE `chatcontent` (
  `MessageId` int(11) NOT NULL AUTO_INCREMENT,
  `PublicId` varchar(32) NOT NULL,
  `AuthorId` int(11) NOT NULL,
  `chatContentId` int(11) NOT NULL,
  `message` varchar(800) DEFAULT NULL,
  `sentDate` datetime DEFAULT NULL,
  `Status` enum('Read','Sent','Delivered') NOT NULL,
  PRIMARY KEY (`MessageId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `chatfile`
--

CREATE TABLE `chatfile` (
  `FileId` int(11) NOT NULL AUTO_INCREMENT,
  `ChatContentId` int(11) DEFAULT NULL,
  `FileType` varchar(30) DEFAULT NULL,
  `FileToken` varchar(100) DEFAULT NULL,
  `FileSize` int(11) DEFAULT 0,
  PRIMARY KEY (`FileId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `chatroom`
--

CREATE TABLE `chatroom` (
  `chatRoomId` int(11) NOT NULL AUTO_INCREMENT,
  `PublicId` varchar(32) NOT NULL,
  `receiverPublicId` varchar(32) NOT NULL,
  `senderId` int(11) DEFAULT NULL,
  `receiverId` int(11) DEFAULT NULL,
  `startedDateTime` datetime DEFAULT NULL,
  `endedDateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`chatRoomId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `personalchatroom`
--

CREATE TABLE `personalchatroom` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `FK_PersonalId` int(20) NOT NULL,
  `FK_ChatRoomId` int(16) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `chatcontent`
--
ALTER TABLE `chatcontent`
  ADD KEY `chatContentId` (`chatContentId`),
  ADD KEY `AuthorId` (`AuthorId`);
  
--
-- A tábla indexei `chatfile`
--
ALTER TABLE `chatfile`
  ADD KEY `ChatContentId` (`ChatContentId`);

--
-- A tábla indexei `personalchatroom`
--
ALTER TABLE `personalchatroom`
  ADD KEY `FK_ChatRoomId` (`FK_ChatRoomId`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
