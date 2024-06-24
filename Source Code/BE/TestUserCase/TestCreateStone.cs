using API.Controllers;
using Moq;
using NUnit.Framework;
using Repositories;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using System.Globalization;
using API.Model.StonesModel;
using Repositories.Entity;

namespace TestUseCase
{
    [TestFixture]
    public class TestCreateStone
    {
        private StonesController _controller;
        private Mock<UnitOfWork> _mockUnitOfWork;
        private MyDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            _dbContext = new MyDbContext();
            // Khởi tạo controller và các dependency cần thiết
            _mockUnitOfWork = new Mock<UnitOfWork>(_dbContext);

            // Setup mock behaviors here
            _controller = new StonesController(_mockUnitOfWork.Object);
        }

        public class CreateStoneDataTest
        {
            public string Kind { get; set; }
            public decimal Size { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public string ExpectedValue { get; set; }
        }

        [TestCaseSource(nameof(TestDataFromCSV))]
        [Test]
        public /*async Task*/void CreateStone_ValidCredentials_ReturnsExpectedValue(CreateStoneDataTest stone)
        {
            var StoneDTO = new RequestCreateStonesModel
            {
                Kind = stone.Kind,
                Size = stone.Size,
                Quantity = stone.Quantity,
                Price = stone.Price
            };

            IActionResult actionResult = /*await*/ _controller.CreateStones(StoneDTO);

            if (actionResult is OkObjectResult)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType<OkObjectResult>(actionResult);
                var okResult = actionResult as OkObjectResult;
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(okResult.Value);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(stone.ExpectedValue, okResult.Value.ToString());
            }
            else
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType<BadRequestObjectResult>(actionResult);
                var badRequestResult = actionResult as BadRequestObjectResult;
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(badRequestResult.Value);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(stone.ExpectedValue, badRequestResult.Value.ToString());
            }
        }


        private static CreateStoneDataTest[] ReadTestData(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<CreateStoneDataTest>().ToArray();
            }
        }

        private static IEnumerable<CreateStoneDataTest> TestDataFromCSV
        {
            get
            {
                string csvFilePath = (@"C:\Users\Admin\source\repos\SWP391Poject\TestUserCase\TestData\StonesDataTest.csv");
                return ReadTestData(csvFilePath);
            }
        }
    }


}
