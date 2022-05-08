using System.Collections.Generic;
using TaxCalculator.Models;
using System.Linq;

namespace TaxCalculator.TaxRules
{
    public class CalculateTaxByRuleTwo : ICalculateTaxByRule
    {
        public CalculateTaxByRuleTwo()
        {
            Name = TaxRuleEnum.RuleTwo;
        }

        public TaxRuleEnum Name { get; }
        public float GetTax(List<TaxType> taxTypes)
        {
            var taxTypeWithMinDuration = taxTypes.Min(p => (int)p.TaxTypeValue);
            var tax = taxTypes.FirstOrDefault(x => (int)x.TaxTypeValue == taxTypeWithMinDuration).TaxValue;
            return tax;
        }
    }
}
