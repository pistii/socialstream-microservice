CREATE DATABASE IF NOT EXISTS `post-service`;

USE `post-service`;

CREATE TABLE `post` (
  `Id` int(11) AUTO_INCREMENT NOT NULL,
  `Token` varchar(36) NOT NULL,
  `Likes` int(4) UNSIGNED DEFAULT NULL,
  `Dislikes` int(4) UNSIGNED DEFAULT NULL,
  `DateOfPost` datetime NOT NULL,
  `LastModified` datetime DEFAULT NULL,
  `PostContent` varchar(500) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


CREATE TABLE `personalpost` (
  `PersonalPostId` int(16) AUTO_INCREMENT NOT NULL,
  `AuthorId` int(11) NOT NULL,
  `PostedToId` int(11) NOT NULL,
  `PostId` int(11) NOT NULL,
  PRIMARY KEY (`PersonalPostId`),
  CONSTRAINT `fk_personalpost` FOREIGN KEY (`PostId`) REFERENCES `post`(`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


CREATE TABLE `postreaction` (
  `PK_Id` int(11) AUTO_INCREMENT NOT NULL,
  `PostId` int(11) DEFAULT NULL,
  `UserId` int(11) DEFAULT NULL,
  `ReactionTypeId` int(2) NOT NULL,
  PRIMARY KEY (`PK_Id`),
  CONSTRAINT `fk_postreaction` FOREIGN KEY (`PostId`) REFERENCES `post`(`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


CREATE TABLE `comment` (
  `commentId` int(11) AUTO_INCREMENT NOT NULL,
  `postId` int(11) NOT NULL,
  `FK_AuthorId` int(11) NOT NULL,
  `PublicId` varchar(36) NOT NULL,
  `CommentDate` datetime NOT NULL,
  `CommentText` varchar(500) NOT NULL,
  `LastModified` datetime DEFAULT NULL,
  PRIMARY KEY (`commentId`),
  CONSTRAINT `fk_comment` FOREIGN KEY (`postId`) REFERENCES `post`(`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


CREATE TABLE `commentreaction` (
  `PK_Id` int(11) AUTO_INCREMENT NOT NULL,
  `FK_CommentId` int(11) NOT NULL,
  `FK_UserId` int(11) NOT NULL,
  `ReactionTypeId` int(11) NOT NULL,
  PRIMARY KEY (`PK_Id`),
  CONSTRAINT `fk_commentreaction` FOREIGN KEY (`FK_CommentId`) REFERENCES `comment`(`commentId`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


CREATE TABLE `mediacontent` (
  `Id` int(11) AUTO_INCREMENT NOT NULL,
  `FK_PostId` int(11) NOT NULL,
  `FileName` varchar(100) DEFAULT NULL,
  `mediaType` varchar(80) NOT NULL,
  `FileSize` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  CONSTRAINT `fk_mediacontent` FOREIGN KEY (`FK_PostId`) REFERENCES `post`(`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;



CREATE TABLE `reactiontypes` (
  `Id` int(11) NOT NULL,
  `Name` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

INSERT INTO `reactiontypes` (`Id`, `Name`) VALUES
(1, 'like'),
(2, 'dislike');