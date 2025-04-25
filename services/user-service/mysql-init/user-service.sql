CREATE DATABASE IF NOT EXISTS `user-service`;
USE `user-service`;


CREATE TABLE `user` (
  `userID` int(11) NOT NULL AUTO_INCREMENT,
  `email` varchar(100) NOT NULL,
  `SecondaryEmailAddress` varchar(100) DEFAULT NULL,
  `password` varchar(140) NOT NULL,
  `registrationDate` datetime DEFAULT NULL,
  `isActivated` tinyint(1) NOT NULL,
  `PublicId` varchar(32) NOT NULL,
  `LastOnline` datetime NOT NULL,
  `isOnlineEnabled` int(11) NOT NULL DEFAULT 1,
    PRIMARY KEY (`userId`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `personal` (
  `id` int(11) NOT NULL,
  `firstName` varchar(30) NOT NULL,
  `middleName` varchar(30) DEFAULT NULL,
  `lastName` varchar(30) NOT NULL,
  `isMale` tinyint(1) NOT NULL,
  `PlaceOfResidence` varchar(70) DEFAULT NULL,
  `avatar` varchar(100) DEFAULT NULL,
  `phoneNumber` varchar(15) DEFAULT NULL,
  `DateOfBirth` date NOT NULL,
  `PlaceOfBirth` varchar(100) NOT NULL,
  `Profession` varchar(60) NOT NULL,
  `Workplace` varchar(120) NOT NULL,
  `publicStudyId` int(11) DEFAULT NULL,
  KEY `publicStudyId` (`publicStudyId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='personal infos about the user';


CREATE TABLE `settings` (
  `PK_Id` int(11) NOT NULL AUTO_INCREMENT,
  `FK_UserId` int(11) DEFAULT NULL,
  `NextReminder` datetime DEFAULT NULL,
  `postCreateEnabledToId` int(11) NOT NULL DEFAULT 1,
   PRIMARY KEY (`PK_Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


CREATE TABLE `setting_post_enabled` (
  `Id` int(11) NOT NULL,
  `Name` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


CREATE TABLE `study` (
  `PK_Id` int(11) NOT NULL AUTO_INCREMENT,
  `FK_UserId` int(11) NOT NULL,
  `SchoolName` varchar(120) DEFAULT NULL,
  `Class` varchar(120) DEFAULT NULL,
  `StartYear` year(4) DEFAULT NULL,
  `EndYear` year(4) DEFAULT NULL,
  `initId` bigint(20) NOT NULL,
    PRIMARY KEY (`PK_Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


CREATE TABLE `userrestriction` (
  `userId` int(11) NOT NULL,
  `restrictionId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


CREATE TABLE `userstatus` (
  `PK_StatusId` int(11) NOT NULL AUTO_INCREMENT,
  `statusName` varchar(30) NOT NULL,
    PRIMARY KEY (`PK_StatusId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


INSERT INTO `userstatus` (`PK_StatusId`, `statusName`) VALUES
(1, 'activated'),
(2, 'unactivated'),
(3, 'banned'),
(4, 'suspended');
