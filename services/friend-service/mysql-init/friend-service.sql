CREATE DATABASE IF NOT EXISTS `friend-service`;

USE `friend-service`;


CREATE TABLE `friend` (
  `FriendshipID` int(11) AUTO_INCREMENT NOT NULL,
  `UserId` int(11) NOT NULL,
  `FriendId` int(11) DEFAULT NULL,
  `StatusId` int(11) NOT NULL,
  `FriendshipSince` datetime DEFAULT NULL,
  PRIMARY KEY (`FriendshipID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


CREATE TABLE `friendshipstatus` (
  `FK_Id` int(11) NOT NULL,
  `Status` varchar(15) DEFAULT NULL,
  PRIMARY KEY (`FK_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

INSERT INTO `friendshipstatus` (`FK_Id`, `Status`) VALUES
(1, 'friends'),
(2, 'nonFriend'),
(3, 'Sent'),
(4, 'Rejected'),
(5, 'Accept'),
(6, 'Blocked');


INSERT INTO `friend` (`UserId`, `FriendId`, `StatusId`, `FriendshipSince`) VALUES
(1, 2, 1, '2025-04-25 11:09');