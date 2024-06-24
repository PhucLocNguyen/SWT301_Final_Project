using API.Controllers;
using API.Model.UserModel;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Repositories;
using Repositories.Email;
using Repositories.Entity;
using Repositories.Token;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TestUseCase.TestRegister;

namespace TestUseCase
{
    [TestFixture]
    public class TestEditProfile
    {

        private UserController _controller;
        private Mock<UnitOfWork> _mockUnitOfWork;
        private Mock<IToken> _mockTokenService;
        private MyDbContext _dbContext;
        private Mock<IEmailService> _emailService;

        [SetUp]
        public void Setup()
        {

            _dbContext = new MyDbContext();
            // Khởi tạo controller và các dependency cần thiết
            _mockUnitOfWork = new Mock<UnitOfWork>(_dbContext);

            _mockTokenService = new Mock<IToken>();

            _emailService = new Mock<IEmailService>();
            // Setup mock behaviors here

            _controller = new UserController(_mockUnitOfWork.Object, _mockTokenService.Object, _emailService.Object);
        }

        public class ProfileData
        {
            public int UserId { get; set; }
            public string Username { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string? Phone { get; set; }
            public string? Image { get; set; }
            public string ExpectedValue { get; set; }
        }

        private static ProfileData[] ReadTestData(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<ProfileData>().ToArray();
            }
        }

        private static IEnumerable<ProfileData> TestDataFromCSV
        {
            get
            {
                string csvFilePath = (@"C:\Users\Admin\source\repos\SWP391Poject\TestUserCase\TestData\dataTestEditProfile.csv");
                return ReadTestData(csvFilePath);
            }
        }



        [TestCaseSource(nameof(TestDataFromCSV))]
        [Test]
        public async Task EditProfile_ValidCredentials_ReturnsExpectedValue(ProfileData profile)
        {
            // Arrange
            var editProfile = new UserDTO()
            {
                UserId = profile.UserId,
                Username = profile.Username,
                Email = profile.Email,
                Name = profile.Name,
                Phone = profile.Phone,
                Image = profile.Image,
            };

            // Act
            IActionResult actionResult =  _controller.UpdateProfile(profile.UserId, editProfile);

            // Assert
            if (actionResult is OkObjectResult)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType<OkObjectResult>(actionResult);
                var okResult = actionResult as OkObjectResult;
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(okResult.Value);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(profile.ExpectedValue, okResult.Value.ToString());
            }
            else if (actionResult is BadRequestObjectResult)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType<BadRequestObjectResult>(actionResult);
                var badRequestResult = actionResult as BadRequestObjectResult;
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(badRequestResult.Value);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(profile.ExpectedValue, badRequestResult.Value.ToString());
            }
            else
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult);
                var conflicRequestResult = actionResult as NotFoundObjectResult;
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(conflicRequestResult.Value);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(profile.ExpectedValue, conflicRequestResult.Value.ToString());
            }
        }
    }
}
