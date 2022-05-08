using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxCalculator.Controllers;
using Moq;
using TaxCalculator.Repository.IRepository;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestTaxCalculator
{
    [TestClass]
    public class CalculateTaxControllerTest
    {
        private readonly Mock<ICalculateTaxRepository> _mockRepo;
        private readonly Mock<ILogger<CalculateTaxController>> _mockLogger;
        private readonly CalculateTaxController _controller;
        public CalculateTaxControllerTest()
        {
            _mockRepo = new Mock<ICalculateTaxRepository>();
            _mockLogger = new Mock<ILogger<CalculateTaxController>>();
            _controller = new CalculateTaxController(_mockRepo.Object, _mockLogger.Object);
            _mockRepo.Setup(repo => repo.GetMunicipalityTax("Vilnius", Convert.ToDateTime("02-05-2020"))).Returns(0.4F);

        }
        [TestMethod]
        public void GetMunicipalityTaxTest()
        {
            _mockRepo.Setup(repo => repo.MunicipalityExists("Vilnius")).Returns(true);
            _mockRepo.Setup(repo => repo.MunicipalityExists("Kaunas")).Returns(true);
            var result = (OkObjectResult)_controller.GetTax("Vilnius", "02-05-2020");
            Assert.AreEqual("0.4",result.Value.ToString());
        }


    }
}
