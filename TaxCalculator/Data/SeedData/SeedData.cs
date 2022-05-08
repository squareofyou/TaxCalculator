using TaxCalculator.Data;
using System;
using System.Collections.Generic;
using TaxCalculator.Models;

namespace TaxCalculator.Data.SeedData
{
    public class SeedData
    {
        public static void SeedDataInMemoryDB(ApplicationDbContext context)
        {
            List<Municipality> municipalities = new List<Municipality>
            {
               new Municipality { Id=1, MunicipalityName="Vilnius", TaxRule =TaxRuleEnum.RuleTwo },
               new Municipality { Id=2, MunicipalityName="Kaunas", TaxRule = TaxRuleEnum.RuleOne  }
            };

            List<TaxType> taxTypes = new List<TaxType>
            {
               new TaxType { Id=1, Municipality="Vilnius", TaxTypeValue =TaxTypesEnum.Daily,StartDate= Convert.ToDateTime("2020.01.01"),EndDate= Convert.ToDateTime("2020.01.01"),TaxValue = .1F},
               new TaxType { Id=2, Municipality="Vilnius", TaxTypeValue =TaxTypesEnum.Daily,StartDate= Convert.ToDateTime("2020.12.25"),EndDate= Convert.ToDateTime("2020.12.25"),TaxValue = .1F},
               new TaxType { Id=3, Municipality="Vilnius", TaxTypeValue =TaxTypesEnum.Monthly,StartDate= Convert.ToDateTime("2020.05.01"),EndDate= Convert.ToDateTime("2020.05.31"),TaxValue = .4F},
               new TaxType { Id=4, Municipality="Vilnius", TaxTypeValue =TaxTypesEnum.Yearly,StartDate= Convert.ToDateTime("2020.01.01"),EndDate= Convert.ToDateTime("2020.12.31"),TaxValue = .2F},
               new TaxType { Id=5, Municipality="Kaunas", TaxTypeValue =TaxTypesEnum.Weekly,StartDate= Convert.ToDateTime(" 2020.01.06"),EndDate= Convert.ToDateTime("2020.01.12"),TaxValue = .1F},
               new TaxType { Id=6, Municipality="Kaunas", TaxTypeValue =TaxTypesEnum.Monthly,StartDate= Convert.ToDateTime("2020.01.01"),EndDate= Convert.ToDateTime("2020.01.31"),TaxValue = .2F},
               new TaxType { Id=7, Municipality="Kaunas", TaxTypeValue =TaxTypesEnum.Yearly,StartDate= Convert.ToDateTime("2020.01.01"),EndDate= Convert.ToDateTime("2020.12.31"),TaxValue = .3F}
            };

            context.municipalities.AddRange(municipalities);
            context.taxTypes.AddRange(taxTypes);
            context.SaveChanges();

        }
    }
}
