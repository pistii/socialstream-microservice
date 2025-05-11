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

-- User 3 barát kérelmet küldött User 1-nak - ConfirmFriendRequest teszt
INSERT INTO `friend` (`UserId`, `FriendId`, `StatusId`, `FriendshipSince`) VALUES
(1, 3, 3, '2025-05-08 11:09');

-- User 2 barát kérelmet küldött User 1-nek  - RejectFriendRequest teszt
INSERT INTO `friend` (`UserId`, `FriendId`, `StatusId`, `FriendshipSince`) VALUES
(2, 1, 3, '2025-05-08 11:09');

-- User 4 barát kérelmet küldött User 1-nak - ConfirmFriendRequestFromProfilePage teszt
INSERT INTO `friend` (`UserId`, `FriendId`, `StatusId`, `FriendshipSince`) VALUES
(1, 4, 3, '2025-05-08 11:09');

-- User 5 barát kérelmet elutasította User 1-nek - RejectFriendRequestFromProfilePage teszt
INSERT INTO `friend` (`UserId`, `FriendId`, `StatusId`, `FriendshipSince`) VALUES
(1, 5, 4, '2025-05-08 11:09');

-- User 1 barátja elutasította User 6-nak - GetAll teszt
INSERT INTO `friend` (`UserId`, `FriendId`, `StatusId`, `FriendshipSince`) VALUES
(1, 6, 1, '2025-05-08 11:09');


INSERT INTO `friend` (`UserId`, `FriendId`, `StatusId`, `FriendshipSince`) VALUES
(4, 5, 1, NOW()),     -- Walter & Jesse are friends
(4, 6, 4, NULL),      -- Walter rejected Tuco
(5, 7, 3, NULL),      -- Jesse sent request to Saul
(6, 4, 2, NULL),      -- Tuco is not friend with Walter
(7, 4, 5, NOW()),     -- Saul accepted Walter
(8, 4, 1, NOW()),     -- Skyler is friends with Walter
(9, 10, 6, NULL),     -- Gus blocked Mike
(5, 8, 1, NOW()),     -- Jesse is friends with Skyler
(10, 4, 1, NOW());    -- Mike is friends with Walter


ALTER TABLE `friend` ADD CONSTRAINT `friend_ibfk_1` FOREIGN KEY (`StatusId`) REFERENCES `friendshipstatus` (`FK_Id`);
