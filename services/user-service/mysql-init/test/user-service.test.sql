CREATE DATABASE IF NOT EXISTS `user-service`;
USE `user-service`;


CREATE TABLE `user` (
  `userID` int(11) AUTO_INCREMENT NOT NULL,
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
  KEY `publicStudyId` (`publicStudyId`),
  CONSTRAINT `fk_personal` FOREIGN KEY (`id`) REFERENCES `user`(`userID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='personal infos about the user';


CREATE TABLE `settings` (
  `PK_Id` int(11) AUTO_INCREMENT NOT NULL,
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
  `PK_Id` int(11) AUTO_INCREMENT NOT NULL,
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
  `PK_StatusId` int(11) AUTO_INCREMENT NOT NULL,
  `statusName` varchar(30) NOT NULL,
    PRIMARY KEY (`PK_StatusId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


INSERT INTO `userstatus` (`PK_StatusId`, `statusName`) VALUES
(1, 'activated'),
(2, 'unactivated'),
(3, 'banned'),
(4, 'suspended');


-- User táblába (public id uuid generált formátum: qwe123ghi789jkl012mno345pqr678hg, az egyszerûség kedvéért itt user-N)
INSERT INTO `user` (`email`, `SecondaryEmailAddress`, `password`, `registrationDate`, `isActivated`, `PublicId`, `LastOnline`, `isOnlineEnabled`) 
VALUES 
('john.doe@example.com', NULL, 'hashedpassword123', NOW(), 1, 'user-1', NOW(), 1),
('jane.smith@example.com', 'jane.secondary@example.com', 'hashedpassword456', NOW(), 1, 'user-2', NOW(), 1),
('user3@example.com', 'user3.secondary@example.com', 'hashedpassword456', NOW(), 1, 'user-3', NOW(), 1),
('walter.white@example.com', 'heisenberg@example.com', 'hashedpassword789', NOW(), true, 'user-4', NOW() - INTERVAL 2 DAY, true),
('jesse.pinkman@example.com', 'capn_cook@example.com', 'hashedpassword101', NOW(), true, 'user-5', NOW() - INTERVAL 1 DAY, true),
('tuco.salamanca@example.com', NULL, 'hashedpassword102', NOW(), true, 'user-6', NOW() - INTERVAL 4 DAY, false),
('saul.goodman@example.com', 'better.call@example.com', 'hashedpassword103', NOW(), true, 'user-7', NOW(), true),
('skyler.white@example.com', NULL, 'hashedpassword104', NOW(), true, 'user-8', NOW() - INTERVAL 3 DAY, false),
('gustavo.fring@example.com', NULL, 'hashedpassword105', NOW(), true, 'user-9', NOW() - INTERVAL 10 DAY, false),
('mike.ehrmantraut@example.com', NULL, 'hashedpassword106', NOW(), true, 'user-10', NOW() - INTERVAL 5 DAY, false);;
-- Personal táblába
INSERT INTO `personal` (`id`, `firstName`, `middleName`, `lastName`, `isMale`, `PlaceOfResidence`, `avatar`, `phoneNumber`, `DateOfBirth`, `PlaceOfBirth`, `Profession`, `Workplace`, `publicStudyId`) 
VALUES 
(1, 'John', NULL, 'Doe', 1, 'New York', NULL, '123456789', '1990-05-15', 'New York', 'Software Developer', 'Tech Corp', NULL),
(2, 'Jane', 'A.', 'Smith', 0, 'Los Angeles', NULL, '987654321', '1992-08-22', 'Los Angeles', 'Graphic Designer', 'Design Studio', NULL),
(3, 'First3', NULL, 'Last3', 1, 'Albuquerque', NULL, '', '1990-05-15', 'Albuquerque', 'Farmer', 'Farm Bt.', NULL),
(4, 'Walter', NULL, 'White', 1, 'Albuquerque', NULL, NULL, '1958-09-07', 'Albuquerque', 'Chemistry Teacher', 'J. P. Wynne High School', NULL),
(5, 'Jesse', NULL, 'Pinkman', 1, 'Albuquerque', NULL, NULL, '1984-09-24', 'Albuquerque', 'Meth Cook', 'Self-employed', NULL),
(6, 'Tuco', NULL, 'Salamanca', 1, 'Albuquerque', NULL, NULL, '1971-05-10', 'El Paso', 'Drug Dealer', 'Cartel', NULL),
(7, 'Saul', NULL, 'Goodman', 1, 'Albuquerque', NULL, NULL, '1960-11-12', 'Cicero', 'Lawyer', 'Saul Goodman & Associates', NULL),
(8, 'Skyler', NULL, 'White', 0, 'Albuquerque', NULL, NULL, '1970-08-11', 'Albuquerque', 'Accountant', 'Beneke Fabricators', NULL),
(9, 'Gustavo', NULL, 'Fring', 1, 'Albuquerque', NULL, NULL, '1956-04-18', 'Chile', 'Businessman', 'Los Pollos Hermanos', NULL),
(10, 'Mike', NULL, 'Ehrmantraut', 1, 'Philadelphia', NULL, NULL, '1945-06-25', 'Philadelphia', 'Fixer', 'Various', NULL);