using API.Controllers;
using API.Model.UserModel;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Repositories.Token;
using Repositories;
using System.Globalization;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CsvHelper.Configuration;
using System.Data;

namespace TestUserCase
{
    [TestFixture]
    public class TestLogin
    {

        private UserController _controller;
        private Mock<UnitOfWork> _mockUnitOfWork;
        private Mock<IToken> _mockTokenService;
        private MyDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            _dbContext = new MyDbContext();
            // Khởi tạo controller và các dependency cần thiết
            _mockUnitOfWork = new Mock<UnitOfWork>(_dbContext);
            _mockTokenService = new Mock<IToken>();

            // Setup mock behaviors here

            _controller = new UserController(_mockUnitOfWork.Object, _mockTokenService.Object);
        }

        public class LoginTestData
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string ExpectedValue { get; set; }
        }

       

        [TestCaseSource(nameof(TestDataFromCSV))]
        [Test]
        public async Task Login_ValidCredentials_ReturnsExpectedValue(LoginTestData login)
        {
            // Arrange
            var loginDTO = new RequestLoginAccount()
            {
                Username = login.Username,
                Password = login.Password,
            };
            await Console.Out.WriteLineAsync(loginDTO.Username);
            await Console.Out.WriteLineAsync(loginDTO.Password);

            // Act
            IActionResult actionResult = await _controller.Login(loginDTO);

            await Console.Out.WriteLineAsync(actionResult.ToString());

            // Assert
            if (actionResult is OkObjectResult)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType<OkObjectResult>(actionResult);
                var okResult = actionResult as OkObjectResult;
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(okResult.Value);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(login.ExpectedValue, okResult.Value.ToString());
            }
            else
             {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType<BadRequestObjectResult>(actionResult);
            var badRequestResult = actionResult as BadRequestObjectResult;
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(badRequestResult.Value);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(login.ExpectedValue, badRequestResult.Value.ToString());
            }

            
        }

        private static LoginTestData[] ReadTestData(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<LoginTestData>().ToArray();
            }
        }

        private static IEnumerable<LoginTestData> TestDataFromCSV
        {
            get
            {
                string csvFilePath = (@"C:\Users\Admin\source\repos\SWP391Poject\TestUserCase\TestData\dataTest.csv");
                return ReadTestData(csvFilePath);
            }
        }
    }
}