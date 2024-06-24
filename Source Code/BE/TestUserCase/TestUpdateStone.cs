using API.Controllers;
using API.Model.StonesModel;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Repositories;
using Repositories.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUseCase
{
    [TestFixture]
    public class TestUpdateStone
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

        public class UpdateStoneDataTest
        {
            public int StoneId { get; set; }
            public string Kind { get; set; }
            public decimal Size { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public string ExpectedValue { get; set; }
        }

        [TestCaseSource(nameof(TestDataFromCSV))]
        [Test]
        public /*async Task*/void UpdateStone_ValidCredentials_ReturnsExpectedValue(UpdateStoneDataTest stone)
        {
            var StoneDTO = new RequestCreateStonesModel
            {
                StonesId = stone.StoneId,
                Kind = stone.Kind,
                Size = stone.Size,
                Quantity = stone.Quantity,
                Price = stone.Price
            };

            IActionResult actionResult = _controller.UpdateStones(stone.StoneId, StoneDTO);

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


        private static UpdateStoneDataTest[] ReadTestData(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<UpdateStoneDataTest>().ToArray();
            }
        }

        private static IEnumerable<UpdateStoneDataTest> TestDataFromCSV
        {
            get
            {
                string csvFilePath = (@"C:\Users\Admin\source\repos\SWP391Poject\TestUserCase\TestData\UpdateStoneDataTest.csv");
                return ReadTestData(csvFilePath);
            }
        }
    }
}
