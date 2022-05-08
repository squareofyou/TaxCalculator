using TaxCalculator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using TaxCalculator.Models;
using TaxCalculator.Repository.IRepository;
using TaxCalculator.TaxRules;

namespace TaxCalculator.Repository
{
    public class CalculateTaxRepository : ICalculateTaxRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IEnumerable<ICalculateTaxByRule> _taxByRules;
        public CalculateTaxRepository(ApplicationDbContext db, IEnumerable<ICalculateTaxByRule> taxByRules)
        {
            _db = db;
            _taxByRules = taxByRules;
        }

        public bool MunicipalityExists(string municipality)
        {
            bool value = _db.municipalities.Any(a => a.MunicipalityName.ToLower().Trim() == municipality.ToLower().Trim());
            return value;
        }

        public float GetMunicipalityTax(string municipality, DateTime taxDate)
        {
            float tax = 0;
            List<Municipality> municipalities = _db.municipalities.ToList();

            TaxRuleEnum taxRule = municipalities.FirstOrDefault(a => a.MunicipalityName == municipality).TaxRule;
            List<TaxType> taxTypes = _db.taxTypes.Where(a => a.Municipality == municipality && (a.StartDate <= taxDate && a.EndDate >= taxDate)).ToList();
            foreach (ICalculateTaxByRule taxByRule in _taxByRules)
            {
                if (taxByRule.Name != taxRule)
                {
                    continue;
                }

                tax = taxByRule.GetTax(taxTypes);
            }
            return tax;
        }
      
    }
}
