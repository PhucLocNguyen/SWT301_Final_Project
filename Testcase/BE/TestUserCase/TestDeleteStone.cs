using API.Controllers;
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
    public class TestDeleteStone
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

        public class DeleteStoneDataTest
        {
            public int StoneId { get; set; }
            public string ExpectedValue { get; set; }
        }

        private static DeleteStoneDataTest[] ReadTestData(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<DeleteStoneDataTest>().ToArray();
            }
        }

        private static IEnumerable<DeleteStoneDataTest> TestDataFromCSV
        {
            get
            {
                string csvFilePath = (@"C:\Users\Admin\source\repos\SWP391Poject\TestUserCase\TestData\DeleteStoneDataTest.csv");
                return ReadTestData(csvFilePath);
            }
        }

        [TestCaseSource(nameof(TestDataFromCSV))]
        [Test]
        public /*async Task*/void DeleteStone_ValidCredentials_ReturnsExpectedValue(DeleteStoneDataTest stone)
        {
            /*var StoneDTO = new RequestCreateStonesModel
            {
                StonesId = stone.StoneId,
                Kind = stone.Kind,
                Size = stone.Size,
                Quantity = stone.Quantity,
                Price = stone.Price
            };*/

            IActionResult actionResult = _controller.DeleteStones(stone.StoneId);

            if (actionResult is OkObjectResult okResult)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType<OkObjectResult>(actionResult);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(okResult.Value);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(stone.ExpectedValue, okResult.Value.ToString());
            }
            else if (actionResult is BadRequestObjectResult badRequestResult)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType<BadRequestObjectResult>(actionResult);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(badRequestResult.Value);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(stone.ExpectedValue, badRequestResult.Value.ToString());
            }
            else if (actionResult is NotFoundObjectResult notFoundResult)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(notFoundResult.Value);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(stone.ExpectedValue, notFoundResult.Value.ToString());
            }
            else
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail($"Unexpected action result type: {actionResult.GetType().Name}");
            }
        }
    }
}
