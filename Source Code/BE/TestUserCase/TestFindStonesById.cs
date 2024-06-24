using API.Controllers;
using API.Model.StonesModel;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Repositories;
using Repositories.Entity;
using System.Globalization;

namespace TestUseCase
{
    [TestFixture]
    public class TestFindStonesById
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

        public class StonesTest
        {
            public int StoneID { get; set; }
            public string Kind { get; set; }
            public decimal Size { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }

        private static StonesTest[] ReadTestData(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<StonesTest>().ToArray();
            }
        }

        private static IEnumerable<StonesTest> TestDataFromCSV
        {
            get
            {
                string csvFilePath = (@"C:\Users\Admin\source\repos\SWP391Poject\TestUserCase\TestData\dataTestFindStones.csv");
                return ReadTestData(csvFilePath);
            }
        }



        [TestCaseSource(nameof(TestDataFromCSV))]
        [Test]
        public async Task FindById_ValidCredentials_ReturnsExpectedValue(StonesTest stonesTest)
        {
            // Arrange


            // Act
            IActionResult actionResult = _controller.GetStonesById(stonesTest.StoneID);

            // Assert
            if (actionResult is OkObjectResult okResult)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType<OkObjectResult>(actionResult);
                var stone = okResult.Value as ReponseStones;
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(stone);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(stonesTest.Kind, stone.Kind);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(stonesTest.Size, stone.Size);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(stonesTest.Quantity, stone.Quantity);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(stonesTest.Price, stone.Price);
            }
            else if (actionResult is BadRequestObjectResult)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType<BadRequestObjectResult>(actionResult);
                var badRequestResult = actionResult as BadRequestObjectResult;
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(badRequestResult.Value);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Stones is not existed", badRequestResult.Value.ToString());
            }
        }
    }
}
