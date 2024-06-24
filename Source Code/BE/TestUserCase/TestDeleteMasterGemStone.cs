using API.Controllers;
using API.Model.MasterGemstoneModel;
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
    public class TestDeleteMasterGemStone
    {
        private MasterGemstoneController _controller;
        private Mock<UnitOfWork> _mockUnitOfWork;
        private MyDbContext _dbContext;

        [SetUp]
        public void Setup()
        {

            _dbContext = new MyDbContext();
            // Khởi tạo controller và các dependency cần thiết
            _mockUnitOfWork = new Mock<UnitOfWork>(_dbContext);

            // Setup mock behaviors here

            _controller = new MasterGemstoneController(_mockUnitOfWork.Object);
        }

        public class DeleteMasterGemStoneTest
        {
            public int MasterGemstoneId { get; set; }
            public string ExpectedValue { get; set; }
        }
        private static DeleteMasterGemStoneTest[] ReadTestData(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<DeleteMasterGemStoneTest>().ToArray();
            }
        }

        private static IEnumerable<DeleteMasterGemStoneTest> TestDataFromCSV
        {
            get
            {
                string csvFilePath = (@"C:\Users\Admin\source\repos\SWP391Poject\TestUserCase\TestData\dataTestDeleteMasterGemStone.csv");
                return ReadTestData(csvFilePath);
            }
        }

        [TestCaseSource(nameof(TestDataFromCSV))]
        [Test]
        public async Task Create_ValidCredentials_ReturnsExpectedValue(DeleteMasterGemStoneTest createMasterGemStoneTest)
        {
            
            IActionResult actionResult = _controller.DeleteMasterGemstone(createMasterGemStoneTest.MasterGemstoneId);

            // Assert
            if (actionResult is OkObjectResult)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType<OkObjectResult>(actionResult);
                var okResult = actionResult as OkObjectResult;
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(okResult.Value);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(createMasterGemStoneTest.ExpectedValue, okResult.Value.ToString());
            }
            else if (actionResult is BadRequestObjectResult)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType<BadRequestObjectResult>(actionResult);
                var badRequestResult = actionResult as BadRequestObjectResult;
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(badRequestResult.Value);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(createMasterGemStoneTest.ExpectedValue, badRequestResult.Value.ToString());
            }else if (actionResult is NotFoundObjectResult)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult);
                var badRequestResult = actionResult as NotFoundObjectResult;
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(badRequestResult.Value);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(createMasterGemStoneTest.ExpectedValue, badRequestResult.Value.ToString());
            }
        }
    }
}
