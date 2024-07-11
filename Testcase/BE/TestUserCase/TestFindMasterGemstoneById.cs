using API.Controllers;
using Moq;
using NUnit.Framework;
using Repositories.Token;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using static TestUseCase.TestRegister;
using System.Globalization;
using API.Model.UserModel;
using Microsoft.AspNetCore.Mvc;
using Repositories.Entity;
using API.Model.MasterGemstoneModel;

namespace TestUseCase
{
    [TestFixture]
    public class TestFindMasterGemstoneById
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

        public class MasterGemStoneTest
        {
            public int masterGemSotneId { get; set; }
            public string Kind { get; set; }
            public decimal Size { get; set; }
            public decimal Price { get; set; }
            public string Clarity { get; set; }
            public string Cut { get; set; }
            public decimal Weight { get; set; }
            public string Shape { get; set; }
            public string Image { get; set; }
        }

        private static MasterGemStoneTest[] ReadTestData(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<MasterGemStoneTest>().ToArray();
            }
        }

        private static IEnumerable<MasterGemStoneTest> TestDataFromCSV
        {
            get
            {
                string csvFilePath = (@"C:\Users\Admin\source\repos\SWP391Poject\TestUserCase\TestData\dataTestMaterGemStone.csv");
                return ReadTestData(csvFilePath);
            }
        }

        

        [TestCaseSource(nameof(TestDataFromCSV))]
        [Test]
        public async Task FindById_ValidCredentials_ReturnsExpectedValue(MasterGemStoneTest masterGemStoneTest)
        {
            // Arrange
            

            // Act
            IActionResult actionResult = _controller.GetMasterGemstoneById(masterGemStoneTest.masterGemSotneId);

            // Assert
            if (actionResult is OkObjectResult okResult)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType<OkObjectResult>(actionResult);
                var gemstone = okResult.Value as ReponseMasterGemstone;
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(gemstone);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(masterGemStoneTest.Kind, gemstone.Kind);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(masterGemStoneTest.Size, gemstone.Size);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(masterGemStoneTest.Price, gemstone.Price);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(masterGemStoneTest.Clarity, gemstone.Clarity);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(masterGemStoneTest.Cut, gemstone.Cut);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(masterGemStoneTest.Weight, gemstone.Weight);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(masterGemStoneTest.Shape, gemstone.Shape);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(masterGemStoneTest.Image, gemstone.Image);
            }
            else if (actionResult is BadRequestObjectResult)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType<BadRequestObjectResult>(actionResult);
                var badRequestResult = actionResult as BadRequestObjectResult;
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(badRequestResult.Value);
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("MasterGemstone is not existed", badRequestResult.Value.ToString());
            }
        }
    }
}
