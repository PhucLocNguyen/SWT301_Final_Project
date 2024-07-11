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
using System.Globalization;
using API.Model.StonesModel;
using Microsoft.AspNetCore.Mvc;
using static TestUseCase.TestCreateStone;
using API.Model.DesignModel;

namespace TestUseCase
{
    [TestFixture]
    public class TestCreateDesign
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

        public class CreateDesignTest()
        {
            public int? ParentId { get; set; } = null;

            public string? Image { get; set; }

            public string? DesignName { get; set; }

            public string? Description { get; set; }

            public int? StonesId { get; set; }

            public int? MasterGemstoneId { get; set; }

            public int? ManagerId { get; set; }

            public int? TypeOfJewelleryId { get; set; }

            public int? MaterialId { get; set; }
            public string ExpectedValue { get; set; }
        }

        private static CreateDesignTest[] ReadTestData(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<CreateDesignTest>().ToArray();
            }
        }

        private static IEnumerable<CreateDesignTest> TestDataFromCSV
        {
            get
            {
                string csvFilePath = (@"D:\Slide\Sem_5\SWP\SWP\BE\TestUserCase\TestData\CreateDesignDataTest.csv");
                return ReadTestData(csvFilePath);
            }
        }

        [TestCaseSource(nameof(TestDataFromCSV))]
        [Test]
        public /*async Task*/void CreateDesign_ValidCredentials_ReturnsExpectedValue(CreateDesignTest design)
        {
            var DesignDTO = new RequestCreateDesignModel
            {
                ParentId = design.ParentId,
                Image = design.Image,
                DesignName = design.DesignName,
                Description = design.Description,
                StonesId = design.StonesId,
                MasterGemstoneId = design.MasterGemstoneId,
                ManagerId = design.ManagerId,
                TypeOfJewelleryId = design.TypeOfJewelleryId,
                MaterialId = design.MaterialId
            };

            IActionResult actionResult = /*await*/ _controller.CreateDesignForManager(DesignDTO);

            if (actionResult is OkObjectResult)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType<OkObjectResult>(actionResult);
                var okResult = actionResult as OkObjectResult;
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(okResult.Value);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(design.ExpectedValue, okResult.Value.ToString());
            }
            else
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType<BadRequestObjectResult>(actionResult);
                var badRequestResult = actionResult as BadRequestObjectResult;
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(badRequestResult.Value);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(design.ExpectedValue, badRequestResult.Value.ToString());
            }
        }
    }
}
