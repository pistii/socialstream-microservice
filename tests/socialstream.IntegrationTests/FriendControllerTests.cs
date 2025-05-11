using friend_service;
using Microsoft.EntityFrameworkCore;
using notification_service;
using System.Net;
using System.Text.Json;

namespace socialstream.IntegrationTests
{

    public class NotificationTest
    {
        public NotificationTest()
        {
            
        }
        public string publicId { get; set; }
        public string authorId { get; set; }
        public string message { get; set; }
    }

    [TestFixture]
    public class FriendControllerTests
    {
        private HttpClient _client;
        private FriendDbContext _friendDbContext;
        private NotificationDBContext _notificationDbContext;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            _client = new HttpClient { BaseAddress = new Uri("http://localhost:5050") };
            var friendOptions = new DbContextOptionsBuilder<FriendDbContext>()
                .UseMySql("server=localhost;port=4003;user=root;password=jelszo;database=friend-service",
                    ServerVersion.Parse("8.0.42"))
                .Options;
            var notificationOptions = new DbContextOptionsBuilder<NotificationDBContext>()
                .UseMySql("server=localhost;port=4001;user=root;password=jelszo;database=notification-service",
                    ServerVersion.Parse("8.0.42"))
                .Options;
            
            _friendDbContext = new FriendDbContext(friendOptions);
            _notificationDbContext = new NotificationDBContext(notificationOptions);
        }


        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _client.Dispose();
            _friendDbContext.Dispose();
            _notificationDbContext.Dispose();
        }

        [Test]
        public async Task GetAllUserTest()
        {
            var response = await  _client.GetAsync("api/friend/getInmemory");
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.IsTrue(Int32.Parse(responseString) > 0);
        }

        [Test]
        public async Task GetAllNotificationTest()
        {
            var userId = 1;
            var response = await _client.GetAsync($"api/notification/{userId}");
            var responseString = await response.Content.ReadAsStringAsync();
            //Assert.AreEqual(responseString, "");
            //var notifications = JsonSerializer.Deserialize<List<shared_libraries.Models.Notification>>(responseString);
            Assert.That(responseString.Contains("notification"));
            Assert.That(responseString.Contains("publicId"));
        }

        [Test]
        public async Task GetAllFriendTest()
        {
            int userId = 5;
            var response = await _client.GetAsync($"api/friend/getAllFriend/{userId}");
            var responseString = await response.Content.ReadAsStringAsync();
            var friends = JsonSerializer.Deserialize<List<int>>(responseString);

            Assert.IsTrue(friends.Count > 0);
        }

        [Test]
        public async Task GetAll_Should_Return200OK_WithExpectedUsers()
        {
            // Arrange
            var publicId = "user-5";                

            // Act
            var response = await _client.GetAsync($"api/friend/getAll/{publicId}/1/9");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


        [Test]
        public async Task CreateFriendRequest_Should_Return200OK()
        {
            // Arrange
            var publicId = "user-8";
            int authorId = 5;

            await using var ft = await _friendDbContext.Database.BeginTransactionAsync();
            await using var nt = await _notificationDbContext.Database.BeginTransactionAsync();

            // Act
            var response = await _client.GetAsync($"/api/friend/request/{publicId}/{authorId}");
            await ft.RollbackAsync();
            await nt.RollbackAsync();

            var message = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.AreEqual("", message);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            //var responseString = await response.Content.ReadAsStringAsync();
            //Assert.AreEqual(responseString, "example@test.com");
        }

        [Test]
        [Ignore("")]
        public async Task RejectFriendRequestFromProfilePage_Should_Return200OK()
        {
            // Arrange
            var publicId = "user-4";
            var userId = 7;

            // Act
            var response = await _client.GetAsync($"api/friend/reject/fromProfile/{publicId}/{userId}");

            var message = await response.Content.ReadAsStringAsync();
            // Assert
            //Assert.AreEqual("", message);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        [Ignore("")]
        public async Task ConfirmFriendRequestFromProfilePage_Should_Return200OK()
        {
            // Arrange
            var otherUserId = "user-4";
            var userId = 7;
            // Act
            var response = await _client.GetAsync($"api/friend/request/confirm/fromProfile/{otherUserId}/{userId}");

            var message = await response.Content.ReadAsStringAsync();
            // Assert
            //Assert.AreEqual("", message);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


        [Test]
        [Ignore("")]
        public async Task RejectFriendRequest_Should_Return200OK()
        {
            // Arrange
            var notificationId = "notif-1";
            var userId = 1;

            // Act
            var response = await _client.GetAsync($"api/friend/request/reject/fromNotification/{notificationId}/{userId}");

            //await transaction.RollbackAsync();

            var message = await response.Content.ReadAsStringAsync();
            // Assert
            //Assert.AreEqual("", );
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }



        [Test]
        [Ignore("")]
        public async Task ConfirmFriendRequest_Should_Return200OK()
        {
            // Arrange
            var notificationId = "notif-1";
            var userId = 1;

            // Act
            var response = await _client.GetAsync($"api/friend/request/confirm/fromNotification/{notificationId}/{userId}");

            var message = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.AreEqual("", message);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }

}
