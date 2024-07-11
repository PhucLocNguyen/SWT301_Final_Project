using API.Controllers;
using Moq;
using NUnit.Framework;
using Repositories.Entity;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using static TestUseCase.TestDeleteStone;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace TestUseCase
{
    [TestFixture]
    public class TestDeleteDesign
    {
        DesignController _controller;
        private Mock<UnitOfWork> _mockUnitOfWork;
        private MyDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            _dbContext = new MyDbContext();
            _mockUnitOfWork = new Mock<UnitOfWork>(_dbContext);
            _controller = new DesignController(_mockUnitOfWork.Object);
        }

        public class DeleteDesignDataTest
        {
            public int DesignId { get; set; }
            public string ExpectedValue { get; set; }
        }

        private static DeleteDesignDataTest[] ReadTestData(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<DeleteDesignDataTest>().ToArray();
            }
        }

        private static IEnumerable<DeleteDesignDataTest> TestDataFromCSV
        {
            get
            {
                string csvFilePath = (@"D:\Slide\Sem_5\SWP\SWP\BE\TestUserCase\TestData\DeleteDesignDataTest.csv");
                return ReadTestData(csvFilePath);
            }
        }

        [TestCaseSource(nameof(TestDataFromCSV))]
        [Test]
        public void DeleteDesign_ValidCredentials_ReturnsExpectedValue(DeleteDesignDataTest design)
        {
            IActionResult actionResult = _controller.DeleteDesign(design.DesignId);

            if (actionResult is OkObjectResult okResult)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType<OkObjectResult>(actionResult);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(okResult.Value);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(design.ExpectedValue, okResult.Value.ToString());
            }
            else if (actionResult is BadRequestObjectResult badRequestResult)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType<BadRequestObjectResult>(actionResult);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(badRequestResult.Value);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(design.ExpectedValue, badRequestResult.Value.ToString());
            }
            else if (actionResult is NotFoundObjectResult notFoundResult)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType<NotFoundObjectResult>(actionResult);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(notFoundResult.Value);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(design.ExpectedValue, notFoundResult.Value.ToString());
            }
            else
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail($"Unexpected action result type: {actionResult.GetType().Name}");
            }
        }
    }
}
