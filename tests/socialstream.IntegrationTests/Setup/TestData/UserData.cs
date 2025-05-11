using shared_libraries.Models;
namespace socialstream.IntegrationTests.Setup.TestData
{
    public static class UserData
    {
        public static List<User> GetUserData()
        {
            return new List<User>()
            {
                new User()
                {
                    userId = 1,
                    email = "john.doe@example.com",
                    SecondaryEmailAddress = null,
                    password = "hashedpassword123",
                    registrationDate = DateTime.Now,
                    isActivated = true,
                    publicId = "user-1",
                    LastOnline = DateTime.Now.AddDays(-1),
                    isOnlineEnabled = true
                },

                new User()
                {
                    userId = 2,
                    email = "jane.smith@example.com",
                    SecondaryEmailAddress = "jane.secondary@example.com",
                    password = "hashedpassword456",
                    registrationDate = DateTime.Now,
                    isActivated = true,
                    publicId = "user-2",
                    LastOnline = DateTime.Now.AddDays(-1),
                    isOnlineEnabled = true
                },

                new User()
                {
                    userId = 3,
                    email = "user3@example.com",
                    SecondaryEmailAddress = "user3.secondary@example.com",
                    password = "hashedpassword456",
                    registrationDate = DateTime.Now,
                    isActivated = true,
                    publicId = "user-3",
                    LastOnline = DateTime.Now.AddDays(-1),
                    isOnlineEnabled = true
                },
                new User()
                {
                    userId = 4,
                    email = "walter.white@example.com",
                    SecondaryEmailAddress = "heisenberg@lab.com",
                    password = "hashedWWpass",
                    registrationDate = DateTime.Now.AddMonths(-12),
                    isActivated = true,
                    publicId = "user-4",
                    LastOnline = DateTime.Now.AddMinutes(-5),
                    isOnlineEnabled = true
                },
                new User()
                {
                    userId = 5,
                    email = "jesse.pinkman@example.com",
                    SecondaryEmailAddress = "yo@bitch.net",
                    password = "hashedJPpass",
                    registrationDate = DateTime.Now.AddMonths(-10),
                    isActivated = true,
                    publicId = "user-5",
                    LastOnline = DateTime.Now.AddHours(-3),
                    isOnlineEnabled = true
                },
                new User()
                {
                    userId = 6,
                    email = "tuco.salamanca@example.com",
                    SecondaryEmailAddress = null,
                    password = "hashedTUCOpass",
                    registrationDate = DateTime.Now.AddMonths(-8),
                    isActivated = true,
                    publicId = "user-6",
                    LastOnline = DateTime.Now.AddDays(-2),
                    isOnlineEnabled = false
                },
                new User()
                {
                    userId = 7,
                    email = "saul.goodman@example.com",
                    SecondaryEmailAddress = "better.call@saul.com",
                    password = "hashedSAULpass",
                    registrationDate = DateTime.Now.AddMonths(-6),
                    isActivated = true,
                    publicId = "user-7",
                    LastOnline = DateTime.Now,
                    isOnlineEnabled = true
                },
                new User()
                {
                    userId = 8,
                    email = "skyler.white@example.com",
                    SecondaryEmailAddress = null,
                    password = "hashedSKYLERpass",
                    registrationDate = DateTime.Now.AddMonths(-7),
                    isActivated = true,
                    publicId = "user-8",
                    LastOnline = DateTime.Now.AddDays(-1),
                    isOnlineEnabled = true
                },
                new User()
                {
                    userId = 9,
                    email = "lydia.quayle@example.com",
                    SecondaryEmailAddress = "lydia@madElectro.com",
                    password = "hashedLYDIpass",
                    registrationDate = DateTime.Now.AddMonths(-5),
                    isActivated = true,
                    publicId = "user-9",
                    LastOnline = DateTime.Now.AddMinutes(-30),
                    isOnlineEnabled = true
                },
                new User()
                {
                    userId = 10,
                    email = "mike.ehrmantraut@example.com",
                    SecondaryEmailAddress = null,
                    password = "hashedMIKEpass",
                    registrationDate = DateTime.Now.AddMonths(-4),
                    isActivated = true,
                    publicId = "user-10",
                    LastOnline = DateTime.Now.AddDays(-3),
                    isOnlineEnabled = false
                }
            };
        }
    }
}
