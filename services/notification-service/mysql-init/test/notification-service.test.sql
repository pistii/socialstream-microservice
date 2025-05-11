CREATE DATABASE IF NOT EXISTS `notification-service`;
USE `notification-service`;

CREATE TABLE `notification` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `PublicId` varchar(32) NOT NULL,
  `AuthorId` int(11) NOT NULL,
  `AuthorPublicId` varchar(32) NOT NULL,
  `AuthorAvatar` varchar(100) NOT NULL,
  `Message` varchar(300) NOT NULL,
  `CreatedAt` datetime NOT NULL,
  `ExpirationDate` datetime NOT NULL,
  `NotificationType` enum('FriendRequest','FriendRequestAccepted','Birthday','NewPost') NOT NULL,
PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `usernotification` (
  `PK_Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) NOT NULL,
  `NotificationId` int(11) NOT NULL,
  `IsRead` int(11) NOT NULL,
  PRIMARY KEY (`PK_Id`),
  KEY `NotificationId` (`NotificationId`),
  CONSTRAINT `fk_notification` FOREIGN KEY (`NotificationId`) REFERENCES `notification`(`Id`) ON DELETE CASCADE
);

-- User 2 barát kérelmet küldött user 1-nek -  RejectFriendRequest
INSERT INTO `notification` (`PublicId`, `AuthorId`, `AuthorPublicId`, `AuthorAvatar`, `Message`, `CreatedAt`, `ExpirationDate`, `NotificationType`)
VALUES ('notif-123', 2, 'user-2', 'avatar.jpg', '', NOW(), NOW() + INTERVAL 7 DAY, 'FriendRequest');
INSERT INTO `usernotification` (`UserId`, `NotificationId`, `IsRead`) 
VALUES (1, 1, 0);

-- User 3 barát kérelmet küldött user 1-nek - ConfirmFriendRequest
INSERT INTO `notification` (`PublicId`, `AuthorId`, `AuthorPublicId`, `AuthorAvatar`, `Message`, `CreatedAt`, `ExpirationDate`, `NotificationType`)
VALUES ('notif-124', 3, 'user-3', 'avatar.jpg', '', NOW(), NOW() + INTERVAL 7 DAY, 'FriendRequest');
INSERT INTO `usernotification` (`UserId`, `NotificationId`, `IsRead`) 
VALUES (1, 2, 0);


-- User 5 barát kérelmet küldött user 1-nek és õ elfogadta
INSERT INTO `notification` (`PublicId`, `AuthorId`, `AuthorPublicId`, `AuthorAvatar`, `Message`, `CreatedAt`, `ExpirationDate`, `NotificationType`)
VALUES ('notif-125', 5, 'user-5', 'avatar.jpg', '', NOW(), NOW() + INTERVAL 1 DAY, 'FriendRequestAccepted');
INSERT INTO `usernotification` (`UserId`, `NotificationId`, `IsRead`) 
VALUES (1, 3, 0);

INSERT INTO notification (Id, PublicId, AuthorId, AuthorPublicId, AuthorAvatar, Message, CreatedAt, ExpirationDate, `NotificationType`) VALUES
(1, 'notif-1', 4, 'user-4', 'walter.png', 'Walter White sent you a friend request.', NOW() - INTERVAL 120 MINUTE, NOW() + INTERVAL 7 DAY, 'FriendRequest'),
(2, 'notif-2', 5, 'user-5', 'jesse.png', 'Jesse Pinkman accepted your friend request.', NOW() - INTERVAL 60 MINUTE, NOW() + INTERVAL 7 DAY, 'FriendRequestAccepted'),
(3, 'notif-3', 6, 'user-6', 'tuco.png', 'Tuco Salamanca sent you a friend request.', NOW() - INTERVAL 1 HOUR, NOW() + INTERVAL 7 DAY, 'FriendRequestAccepted'),
(4, 'notif-4', 7, 'user-7', 'saul.png', 'Happy birthday! Saul Goodman sent you a gift.', NOW() - INTERVAL 1 DAY, NOW() + INTERVAL 6 DAY, 'Birthday'),
(5, 'notif-5', 4, 'user-4', 'walter.png', 'Walter White posted a new article.', NOW(), NOW() + INTERVAL 3 DAY, 'NewPost');

INSERT INTO usernotification (PK_Id, UserId, NotificationId, IsRead) VALUES
(1, 5, 1, 1),
(2, 6, 1, 0),
(3, 4, 2, 1),
(4, 7, 3, 0),
(5, 8, 4, 1),
(6, 9, 4, 0),
(7, 10, 5, 0),
(8, 5, 5, 1),
(9, 6, 5, 0);
