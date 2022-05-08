using System.ComponentModel.DataAnnotations;

namespace TaxCalculator.Models
{
    public class Municipality
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string MunicipalityName { get; set; }
        [Required]
        public TaxRuleEnum TaxRule { get; set; }
    }

    public enum TaxRuleEnum
    {
        RuleOne = 1,
        RuleTwo = 2
    }
}
