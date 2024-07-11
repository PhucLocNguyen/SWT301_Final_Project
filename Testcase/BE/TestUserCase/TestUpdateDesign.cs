using API.Controllers;
using Moq;
using NUnit.Framework;
using Repositories.Entity;
using Repositories;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using API.Model.DesignModel;
using Microsoft.AspNetCore.Mvc;

namespace TestUseCase
{
    [TestFixture]
    public class TestUpdateDesign
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

        public class UpdateDesignTest
        {
            public int DesignId { get; set; }
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

        private static UpdateDesignTest[] ReadTestData(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<UpdateDesignTest>().ToArray();
            }
        }

        private static IEnumerable<UpdateDesignTest> TestDataFromCSV
        {
            get
            {
                string csvFilePath = (@"D:\Slide\Sem_5\SWP\SWP\BE\TestUserCase\TestData\UpdateDesignDataTest.csv");
                return ReadTestData(csvFilePath);
            }
        }

        [Test, TestCaseSource(nameof(TestDataFromCSV))]
        public void UpdateDesign_ValidCredentials_ReturnsExpectedValue(UpdateDesignTest design)
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

            IActionResult actionResult = _controller.UpdateDesign(design.DesignId, DesignDTO);

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
