using System.Collections.Generic;
using System.Linq;
using TaxCalculator.Models;

namespace TaxCalculator.TaxRules
{
    public class CalculateTaxByRuleOne : ICalculateTaxByRule
    {
        public CalculateTaxByRuleOne()
        {
            Name = TaxRuleEnum.RuleOne;
        }

        public TaxRuleEnum Name { get; }
        public float GetTax(List<TaxType> taxTypes)
        {
            float total = taxTypes.Sum(item => item.TaxValue);
            return total;
        }
    }
}
