using System;
using System.ComponentModel.DataAnnotations;

namespace TaxCalculator.Models
{
    public class TaxType
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Municipality { get; set; }
        
        public TaxTypesEnum TaxTypeValue { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float TaxValue {get; set; }
    }

    public enum TaxTypesEnum
    {
        Daily = 0,
        Weekly = 1,
        Monthly = 2,
        Yearly =3       
    }
}
