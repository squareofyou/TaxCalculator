using System;
using System.Collections.Generic;
using TaxCalculator.Models;

namespace TaxCalculator.TaxRules
{
    public interface ICalculateTaxByRule
    {
        public TaxRuleEnum Name { get; }
        float GetTax(List<TaxType> taxTypes);
    }
}
