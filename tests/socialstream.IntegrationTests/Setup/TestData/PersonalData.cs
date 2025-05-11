using shared_libraries.Models;

namespace socialstream.IntegrationTests.Setup.TestData
{
    public static class PersonalData
    {
        public static List<Personal> GetPersonalData()
        {
            return new List<Personal>()
            {
                new Personal()
                {
                    id = 1,
                    firstName = "John",
                    middleName = null,
                    lastName = "Doe",
                    isMale = true,
                    placeOfResidence = "New York",
                    avatar = "avatar.jpg",
                    phoneNumber = "123456789",
                    dateOfBirth = new DateOnly(1990, 01, 01),
                    placeOfBirth = "New York",
                    profession = "Software Developer",
                    workplace = "Tech Corp",
                    publicStudyId = null
                },
                new Personal()
                {
                    id = 2,
                    firstName = "Jane",
                    middleName = "A.",
                    lastName = "Smith",
                    isMale = false,
                    placeOfResidence = "Los Angeles",
                    avatar = "1.jpg",
                    phoneNumber = "987654321",
                    dateOfBirth = new DateOnly(1990, 01, 01),
                    placeOfBirth = "Los Angeles",
                    profession = "Designer",
                    workplace = "Tech Corp",
                    publicStudyId = null
                },
                new Personal()
                {
                    id = 3,
                    firstName = "Hank",
                    middleName = null,
                    lastName = "Schrader",
                    isMale = true,
                    placeOfResidence = "Albuquerque",
                    avatar = null,
                    phoneNumber = null,
                    dateOfBirth = new DateOnly(1966, 03, 01),
                    placeOfBirth = "Albuquerque",
                    profession = "DEA Agent",
                    workplace = "DEA",
                    publicStudyId = null
                },
                new Personal()
                {
                    id = 4,
                    firstName = "Walter",
                    middleName = null,
                    lastName = "White",
                    isMale = true,
                    placeOfResidence = "Albuquerque",
                    avatar = null,
                    phoneNumber = null,
                    dateOfBirth = new DateOnly(1958, 09, 07),
                    placeOfBirth = "Albuquerque",
                    profession = "Chemistry Teacher",
                    workplace = "J. P. Wynne High School",
                    publicStudyId = null
                },
                new Personal()
                {
                    id = 5,
                    firstName = "Jesse",
                    middleName = null,
                    lastName = "Pinkman",
                    isMale = true,
                    placeOfResidence = "Albuquerque",
                    avatar = null,
                    phoneNumber = null,
                    dateOfBirth = new DateOnly(1984, 09, 24),
                    placeOfBirth = "Albuquerque",
                    profession = "Meth Cook",
                    workplace = "RV / Underground Lab",
                    publicStudyId = null
                },
                new Personal()
                {
                    id = 6,
                    firstName = "Tuco",
                    middleName = null,
                    lastName = "Salamanca",
                    isMale = true,
                    placeOfResidence = "El Paso",
                    avatar = null,
                    phoneNumber = null,
                    dateOfBirth = new DateOnly(1971, 01, 10),
                    placeOfBirth = "Mexico",
                    profession = "Drug Dealer",
                    workplace = "Juarez Cartel",
                    publicStudyId = null
                },
                new Personal()
                {
                    id = 7,
                    firstName = "Saul",
                    middleName = null,
                    lastName = "Goodman",
                    isMale = true,
                    placeOfResidence = "Albuquerque",
                    avatar = null,
                    phoneNumber = null,
                    dateOfBirth = new DateOnly(1960, 11, 12),
                    placeOfBirth = "Cicero",
                    profession = "Lawyer",
                    workplace = "Saul Goodman & Associates",
                    publicStudyId = null
                },
                new Personal()
                {
                    id = 8,
                    firstName = "Skyler",
                    middleName = null,
                    lastName = "White",
                    isMale = false,
                    placeOfResidence = "Albuquerque",
                    avatar = null,
                    phoneNumber = null,
                    dateOfBirth = new DateOnly(1970, 08, 11),
                    placeOfBirth = "New Mexico",
                    profession = "Accountant",
                    workplace = "Beneke Fabricators",
                    publicStudyId = null
                },
                new Personal()
                {
                    id = 9,
                    firstName = "Lydia",
                    middleName = null,
                    lastName = "Rodarte-Quayle",
                    isMale = false,
                    placeOfResidence = "Houston",
                    avatar = null,
                    phoneNumber = null,
                    dateOfBirth = new DateOnly(1975, 05, 19),
                    placeOfBirth = "Texas",
                    profession = "Executive",
                    workplace = "Madrigal Electromotive",
                    publicStudyId = null
                },
                new Personal()
                {
                    id = 10,
                    firstName = "Mike",
                    middleName = null,
                    lastName = "Ehrmantraut",
                    isMale = true,
                    placeOfResidence = "Albuquerque",
                    avatar = null,
                    phoneNumber = null,
                    dateOfBirth = new DateOnly(1950, 02, 02),
                    placeOfBirth = "Philadelphia",
                    profession = "Fixer",
                    workplace = "Gus Fring's operation",
                    publicStudyId = null
                }
            };
        }
    }
}
