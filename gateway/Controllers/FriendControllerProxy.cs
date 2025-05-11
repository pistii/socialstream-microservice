using gateway.Realtime;
using GrpcServices;
using GrpcServices.Interfaces;
using GrpcServices.Mappers;
using GrpcServices.Protos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using shared_libraries.DTOs;
using shared_libraries.Models;
using dbModel = shared_libraries.Models;

namespace gateway.Controllers
{
    [ApiController]
    [Route("api/friend")]
    public class FriendControllerProxy : BaseController
    {
        private readonly IHubContext<NotificationHub, INotificationClient> _notificationHub;
        private readonly IUserGrpcClient _userGrpcClient;
        private readonly IFriendGrpcClient _friendGrpcClient;
        private readonly INotificationGrpcClient _notificationGrpcClient;

        public FriendControllerProxy(
            IHubContext<NotificationHub, INotificationClient> notificationHub,
            IUserGrpcClient userGrpcClient, 
            IFriendGrpcClient friendGrpcClient,
            INotificationGrpcClient notificationGrpcClient)
        {
            _notificationHub = notificationHub;
            _userGrpcClient = userGrpcClient;
            _friendGrpcClient = friendGrpcClient;
            _notificationGrpcClient = notificationGrpcClient;
        }



        [HttpGet("getInmemory")]
        public async Task<IActionResult> GetAllUser()
        {
            var foundUsers = await _userGrpcClient.GetAllUserTest(new UserRequest() { PublicId = "a1" });
           
            return Ok(foundUsers.FoundUsers);
        }


        [HttpGet("getAllFriend/{userId}")]
        public async Task<IActionResult> GetAllTest(int userId)
        {
            var foundUsers = await _friendGrpcClient.GetFriendsForUserAsync(new GetFriendIdsRequest() { UserId = userId });

            return Ok(foundUsers.Friends);
        }


        [HttpGet("getAll/{publicId}/{currentPage}/{qty}")]
        public async Task<IActionResult> GetAll(string publicId, int currentPage = 1, int qty = 9)
        {
            var userWithPublicId = await _userGrpcClient.GetUserAsync(new UserRequest() { PublicId = publicId });

            if (userWithPublicId == null || !userWithPublicId.Success || userWithPublicId.UserId < 1) return NotFound("User not found");
            else
            {
                Console.WriteLine("DEBUG: |GetAll| requesting user with id: " + userWithPublicId);
            }


            var friendIds = await _friendGrpcClient.GetFriendsForUserAsync(new GetFriendIdsRequest()
            {
                UserId = userWithPublicId.UserId
            });
            Console.WriteLine("DEBUG: |GetAll| GetFriendsForUserAsync result: " + friendIds);

            if (friendIds == null) return BadRequest("Request failed.");
            if (!friendIds.Success) return NotFound();
            if (friendIds.Friends.Count == 0) return NotFound("No friends found.");

            var getAllUserRequestParam = new GetAllUserRequest();
            getAllUserRequestParam.UserIds.AddRange(friendIds.Friends);
            Console.WriteLine("DEBUG: |gETaLL| UserIdk hozzáadva?: " + getAllUserRequestParam);

            Console.WriteLine("DEBUG: |GetAll| GetAllUserByIdAsync request: " + getAllUserRequestParam);

            var friends = await _userGrpcClient.GetAllUserByIdAsync(getAllUserRequestParam);
            if (!friends.Success) return BadRequest("Failed to request friends");
            

            var sorted = friends.Users.Skip((currentPage - 1) * qty).Take(qty);
            Console.WriteLine("Friends: " + friends);

            if (sorted == null) Console.WriteLine("ERROR: a sorted üres a getAllban, nem voltam képes átalakítani az usereket");

            else
            {
                foreach (var item in sorted)
                {
                    Console.WriteLine(item);
                }
                var mappedData = sorted.Select(n => new UserDetailsDto()
                {
                    //Avatar = n.Personal.Avatar ?? "",
                    //FirstName = n.Personal.FirstName ?? "",
                    //LastName = n.Personal.LastName ?? "",
                    //MiddleName = n.Personal.MiddleName ?? "",
                    PublicId = n.PublicId ?? ""
                }); return Ok(mappedData);

            }

            return NotFound();
        }



        //[Authorize]
        [HttpGet("request/{userId}/{authorId}")]
        public async Task<IActionResult> CreateFriendRequest(string userId, int authorId = 5)
        {
            //var authorId = 5; //GetUserId(); TODO: Majd JWT-ből lesz kinyerve...

            //Nincs authorId, personal.avatar mappelve
            var author = await _userGrpcClient.GetUserByIdRequestAsync(
                new GrpcServices.UserRequestByPrivateId()
                { UserId = authorId });

            var receiverUser = await _userGrpcClient.GetUserAsync(
                new GrpcServices.UserRequest()
                { PublicId = userId });
            Console.WriteLine("DEBUG: |CreateFriendRequest| GetUserByIdRequestAsync eredmény: " + author);
            Console.WriteLine("DEBUG: |CreateFriendRequest| GetUserAsync eredmény: " + receiverUser);


            if (receiverUser == null || author == null) return NotFound("Invalid user id.");
            if (authorId == receiverUser.UserId) return BadRequest();


            var friend = await _friendGrpcClient.CreateFriendshipIfNotExistsAsync(
                new GrpcServices.Protos.FriendObj()
                {
                    AuthorId = authorId,
                    ReceiverId = receiverUser.UserId,
                    FriendshipStatusId = 3
                });
            Console.WriteLine("DEBUG: |CreateFriendRequest| CreateFriendshipIfNotExistsAsync eredmény: " + friend);

            if (friend is not FriendObj)
            {
                //TODO: Something went wrong while creating friend request... should handle
                Console.WriteLine("friend is null from service");
                return BadRequest();
            }

            //Save notification
            dbModel.Notification notification = new(authorId, "", dbModel.NotificationType.FriendRequest, author.PublicId, author.Personal.Avatar, receiverUser.UserId);
            
            try
            {
                var mappedNotification = ObjectMapper.Map<dbModel.Notification, CreateNotificationRequest>(notification);
                try
                {
                    await _notificationGrpcClient.CreateNotification(mappedNotification);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: Failed to create notification");
                    Console.WriteLine("Leírás:" + ex.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: Failed to map notification:");
                Console.WriteLine("Leírás:" + ex.ToString());
            }

            Personal authorPersonal = null;
            try
            {
                authorPersonal = ObjectMapper.Map<PersonalResponse, dbModel.Personal>(author.Personal);
                authorPersonal.User = ObjectMapper.Map<UserResponse, dbModel.User>(author);
                
            }
            catch (Exception ex) 
            {
                Console.WriteLine("ERROR: Failed to map userResponse to personal");
                Console.WriteLine("Leírás:" + ex.ToString());
            }
            if (authorPersonal.User == null) throw new Exception("User üres!");

            if (authorPersonal != null)
            {
                dbModel.GetNotification getNotification = new(notification, authorPersonal);

                await _notificationHub.Clients.Users(receiverUser.UserId.ToString())
                    .ReceiveNotification(receiverUser.UserId, getNotification);
            } else
            {
                Console.WriteLine("Az author null volt, nem lett értesítés elküldve.....");
            }
            return Ok();
        }


        //[Authorize]
        [HttpGet("reject/fromProfile/{otherUserId}/{userId}")]
        public async Task<IActionResult> RejectFriendRequestFromProfilepage(string otherUserId, int userId = 4)
        {
            //var userId = 7; //GetUserId();

            var otherUser = await _userGrpcClient.GetUserPrivateIdByPublicId(new UserRequest() { PublicId = otherUserId });
            Console.WriteLine("DEBUG: |RejectFriendRequestFromProfilepage| GetUserPrivateIdByPublicId result: " + otherUser);

            if (!otherUser.UserFound) return NotFound("Invalid user id.");

            var initialFriendshipExist = await _friendGrpcClient.GetFriendship(
                new FriendObj()
                {
                    AuthorId = otherUser.UserId,
                    ReceiverId = userId,
                    FriendshipStatusId = 3
                }); 
            
            Console.WriteLine("DEBUG: |RejectFriendRequestFromProfilepage| initialFriendshipExist result. Expected friendshipstatusId = 3: " + otherUser);

            if (initialFriendshipExist == null || !initialFriendshipExist.Success) return NotFound();

            //Lehet hogy már barátok, vagy egyáltalán nem történt ismerősnek jelölés sem.
            else if (initialFriendshipExist.FriendshipStatusId != 3) return BadRequest();

            var request = new RemoveFriendRequestNotificationRequest()
            {
                UserId = otherUser.UserId,
                NotificationType = NotificationTypeEnum.FriendRequest,
            };
            //var result = await _notificationGrpcClient.RemoveFriendRequestNotificationRequest(request);
            //if (!result.Success)
            //{
            //    Log.Error("Failed to remove friend request notification", request);
            //}

            await _friendGrpcClient.RemoveFriendshipIfExists(initialFriendshipExist);

            return Ok();
        }


        //[Authorize]
        [HttpGet("request/confirm/fromProfile/{otherUserId}/{userId}")]
        public async Task<IActionResult> ConfirmFriendRequestFromProfilepage(string otherUserId, int userId = 7)
        {
            //var userId = 7;//GetUser();

            var otherUser = await _userGrpcClient.GetUserPrivateIdByPublicId(new UserRequest() { PublicId = otherUserId });
            if (otherUser == null) return NotFound("Invalid user id.");
            Console.WriteLine("DEBUG: |ConfirmFriendRequestFromProfilepage| GetUserPrivateIdByPublicId eredmény: " + otherUser);

            var initialFriendshipExist = await _friendGrpcClient.GetFriendship(
                new FriendObj()
                {
                    AuthorId = otherUser.UserId,
                    ReceiverId = userId,
                    FriendshipStatusId = 3
                });
            Console.WriteLine("DEBUG: |ConfirmFriendRequestFromProfilepage| initialFriendshipExist eredmény: " + initialFriendshipExist);

            if (initialFriendshipExist == null) return NotFound();

            //Lehet hogy már barátok, vagy egyáltalán nem történt ismerősnek jelölés sem.
            else if (initialFriendshipExist.FriendshipStatusId != 3) return BadRequest();

            //Létrehozzuk a baráti kapcsolatot.
            Console.WriteLine("DEBUG: |ConfirmFriendRequestFromProfilepage| CreateFriendshipIfNotExistsAsync lekérése ezzel az adattal-val: " + initialFriendshipExist);

            await _friendGrpcClient.CreateFriendshipIfNotExistsAsync(initialFriendshipExist);
            Console.WriteLine("DEBUG: |ConfirmFriendRequestFromProfilepage| CreateFriendshipIfNotExistsAsync végbement... return Ok");

            //Kikeressük azt az értesítést amikor a másik felhasználó küldött barátkérelmet
            //Notification result = await _notificationRepository.GetEntityByPredicateFirstOrDefaultAsync<Notification>(p => p.NotificationType == NotificationType.FriendRequest && p.AuthorId == otherUser.userID);
            //var notification = await _notificationGrpcClient.GetNotification(new NotificationRequest() {  })

            //if (result != null)
            //{
            //    if ((result.ExpirationDate - DateTime.Now).TotalDays < 1)
            //    {
            //        result.ExpirationDate = result.ExpirationDate.AddDays(1);
            //    }
            //    result.NotificationType = NotificationType.FriendRequestAccepted;

            //    await _notificationRepository.UpdateThenSaveAsync(result);

            //    GetNotification getNotification = new(result);
            //    //await _notificationHub.Clients.Users(otherUser.userID.ToString()).ReceiveNotification(otherUser.userID, getNotification);
            //}
            //else
            //{
            //    var userWithPersonal = await _userRepository.GetWithIncludeAsync<Personal, user>(u => u.users, u => u.users.PublicId == user.PublicId);

            //    Notification notification = new(user.userID, "Friend request confirmed", NotificationType.FriendRequestAccepted, user.PublicId, userWithPersonal.avatar, otherUser.userID);
            //    GetNotification getNotification = new(notification, userWithPersonal);

            //    //await _notificationHub.Clients.Users(otherUser.userID.ToString()).ReceiveNotification(otherUser.userID, getNotification);
            //}


            return Ok();
        }



        //[Authorize]
        [HttpGet("request/reject/fromNotification/{notificationId}/{userId}")]
        public async Task<IActionResult> RejectFriendRequest(string notificationId, int userId = 1)
        {
            //var userId = 1; //GetUserId();
            var notification = await _notificationGrpcClient.GetNotificationByPublicId(new NotificationRequest() { PublicId = notificationId });
            if (notification is null or not NotificationResponse) return NotFound();
            Console.WriteLine("DEBUG: |RejectFriendRequest| GetNotificationByPublicId eredmény: " + notification);


            var initialFriendshipExist = await _friendGrpcClient.GetFriendship(new FriendObj() { 
                AuthorId = notification.AuthorId, 
                ReceiverId = userId
            });
            Console.WriteLine("DEBUG: |RejectFriendRequest| GetFriendship eredmény: friendshipstatusId várt = 3" + initialFriendshipExist);

            if (initialFriendshipExist == null || !initialFriendshipExist.Success) return NotFound();

            //Lehet hogy már barátok, vagy egyáltalán nem történt ismerősnek jelölés sem.
            else if (initialFriendshipExist.FriendshipStatusId != 3) return BadRequest();

            var isRemoved = await _notificationGrpcClient.RemoveNotificationById(new NotificationRequest() { PublicId = notificationId });
            Console.WriteLine("DEBUG: |RejectFriendRequest| RemoveNotificationById eredmény: " + notificationId);

            if (!isRemoved.Success) BadRequest("Failed to remove notification");
            return Ok();
        }


        [HttpGet("request/confirm/fromNotification/{notificationId}/{userId}")]
        public async Task<IActionResult> ConfirmFriendRequest(string notificationId, int userId = 1)
        {
            //var userId = 1;//GetUserId();

            var notification = await _notificationGrpcClient.GetNotificationByPublicId(new NotificationRequest { PublicId = notificationId });
            if (!notification.Success) return NotFound("Notification not found");
            Console.WriteLine("DEBUG: |ConfirmFriendRequest| GetNotificationByPublicId eredmény: " + notification);

            var initialFriendshipExist = await _friendGrpcClient.GetFriendship(
                new FriendObj()
                {
                    AuthorId = notification.AuthorId,
                    ReceiverId = userId,
                    FriendshipStatusId = 3
                });
            Console.WriteLine("DEBUG: |ConfirmFriendRequest| initialFriendshipExist eredmény: " + initialFriendshipExist);

            if (!initialFriendshipExist.Success) return NotFound("No existing friend request found.");

            //Lehet hogy már barátok, vagy egyáltalán nem történt ismerősnek jelölés sem.
            else if (initialFriendshipExist.FriendshipStatusId != 3) return BadRequest("No modifications made.");

            initialFriendshipExist.FriendshipStatusId = 1;
            Console.WriteLine("DEBUG: |ConfirmFriendRequest| barátnak jelölés a friendGrpc-ben: " + initialFriendshipExist);

            var created = await _friendGrpcClient.CreateFriendshipIfNotExistsAsync(initialFriendshipExist);
            Console.WriteLine("DEBUG: |ConfirmFriendRequest| created eredmény: " + created);

            if (created.Success) return Ok();

            return BadRequest("Failed to create friendship.");
        }
    }
}
