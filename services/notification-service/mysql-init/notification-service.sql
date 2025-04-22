
CREATE TABLE `notification` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `PublicId` varchar(32) NOT NULL,
  `AuthorId` int(11) NOT NULL,
  `AuthorPublicId` varchar(32) NOT NULL,
  `AuthorAvatar` varchar(100) NOT NULL,
  `Message` varchar(300) NOT NULL,
  `CreatedAt` datetime NOT NULL,
  `ExpirationDate` datetime NOT NULL,
  `notificationType` enum('FriendRequest','FriendRequestAccepted','Birthday','NewPost') NOT NULL,
PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


CREATE TABLE `usernotification` (
  `PK_Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) NOT NULL,
  `NotificationId` int(11) NOT NULL,
  `IsRead` int(11) NOT NULL,
PRIMARY KEY `PK_Id`,
KEY `NotificationId` (`NotificationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

ADD CONSTRAINT `usernotification_ibfk_1` FOREIGN KEY (`NotificationId`) REFERENCES `notification` (`Id`) ON DELETE CASCADE;
