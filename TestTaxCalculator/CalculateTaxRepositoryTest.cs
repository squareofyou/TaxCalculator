using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxCalculator.Controllers;
using Moq;
using TaxCalculator.Repository.IRepository;
using System;
using TaxCalculator.Data;
using TaxCalculator.Models;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TaxCalculator.TaxRules;
using TaxCalculator.Repository;

namespace TestTaxCalculator
{
    [TestClass]
    public class CalculateTaxRepositoryTest
    {
        private readonly Mock<IEnumerable<ICalculateTaxByRule>> _mockRule;
        public CalculateTaxRepositoryTest()
        {
            //
           _mockRule = new Mock<IEnumerable<ICalculateTaxByRule>>();
            var obj = new List<ICalculateTaxByRule>();
            obj.Add(new CalculateTaxByRuleOne());
            obj.Add(new CalculateTaxByRuleTwo());
            _mockRule.Setup(repo => repo.GetEnumerator()).Returns(() => obj.GetEnumerator());

        }
        [TestMethod]
        public void TestRepoMethods()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoDB")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new ApplicationDbContext(options))
            {
                context.municipalities.Add(new Municipality { Id = 1, MunicipalityName = "Vilnius", TaxRule = TaxRuleEnum.RuleTwo });
                context.municipalities.Add(new Municipality { Id = 2, MunicipalityName = "Kaunas", TaxRule = TaxRuleEnum.RuleOne });

                context.taxTypes.Add(new TaxType { Id = 1, Municipality = "Vilnius", TaxTypeValue = TaxTypesEnum.Daily, StartDate = Convert.ToDateTime("2020.01.01"), EndDate = Convert.ToDateTime("2020.01.01"), TaxValue = .1F });
                context.taxTypes.Add(new TaxType { Id = 2, Municipality = "Vilnius", TaxTypeValue = TaxTypesEnum.Daily, StartDate = Convert.ToDateTime("2020.12.25"), EndDate = Convert.ToDateTime("2020.12.25"), TaxValue = .1F });
                context.taxTypes.Add(new TaxType { Id = 3, Municipality = "Vilnius", TaxTypeValue = TaxTypesEnum.Monthly, StartDate = Convert.ToDateTime("2020.05.01"), EndDate = Convert.ToDateTime("2020.05.31"), TaxValue = .4F });
                context.taxTypes.Add(new TaxType { Id = 4, Municipality = "Vilnius", TaxTypeValue = TaxTypesEnum.Yearly, StartDate = Convert.ToDateTime("2020.01.01"), EndDate = Convert.ToDateTime("2020.12.31"), TaxValue = .2F });
                context.taxTypes.Add(new TaxType { Id = 5, Municipality = "Kaunas", TaxTypeValue = TaxTypesEnum.Weekly, StartDate = Convert.ToDateTime(" 2020.01.06"), EndDate = Convert.ToDateTime("2020.01.12"), TaxValue = .1F });
                context.taxTypes.Add(new TaxType { Id = 6, Municipality = "Kaunas", TaxTypeValue = TaxTypesEnum.Monthly, StartDate = Convert.ToDateTime("2020.01.01"), EndDate = Convert.ToDateTime("2020.01.31"), TaxValue = .2F });
                context.taxTypes.Add(new TaxType { Id = 7, Municipality = "Kaunas", TaxTypeValue = TaxTypesEnum.Yearly, StartDate = Convert.ToDateTime("2020.01.01"), EndDate = Convert.ToDateTime("2020.12.31"), TaxValue = .3F });

                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new ApplicationDbContext(options))
            {
                var sut = new CalculateTaxRepository(context, _mockRule.Object);
                //Act
                bool check = sut.MunicipalityExists("Vilnius");
                bool check1 = sut.MunicipalityExists("Kaunas");
                bool check2 = sut.MunicipalityExists("Vilnius1");

                float tax = sut.GetMunicipalityTax("Vilnius", Convert.ToDateTime("01-01-2020"));
                float tax1 = sut.GetMunicipalityTax("Vilnius", Convert.ToDateTime("02-05-2020"));
                float tax2 = sut.GetMunicipalityTax("Vilnius", Convert.ToDateTime("10-07-2020"));
                float tax3 = sut.GetMunicipalityTax("Vilnius", Convert.ToDateTime("16-03-2020"));
                float tax4 = sut.GetMunicipalityTax("Kaunas", Convert.ToDateTime("01-01-2020"));
                float tax5 = sut.GetMunicipalityTax("Kaunas", Convert.ToDateTime("08-01-2020"));
                float tax6 = sut.GetMunicipalityTax("Kaunas", Convert.ToDateTime("10-02-2020"));
                float tax7 = sut.GetMunicipalityTax("Kaunas", Convert.ToDateTime("16-03-2020"));

                //Assert
                Assert.AreEqual(true, check);
                Assert.AreEqual(true,check1);
                Assert.AreEqual(false,check2);

                Assert.AreEqual("0.1", tax.ToString());
                Assert.AreEqual("0.4", tax1.ToString());
                Assert.AreEqual("0.2", tax2.ToString());
                Assert.AreEqual("0.2", tax3.ToString());
                Assert.AreEqual("0.5", tax4.ToString());
                Assert.AreEqual("0.6", tax5.ToString());
                Assert.AreEqual("0.3", tax6.ToString());
                Assert.AreEqual("0.3", tax7.ToString());
            }
        }

    }


}
