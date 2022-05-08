using System;

namespace TaxCalculator.Repository.IRepository
{
    public interface ICalculateTaxRepository
    {

        bool MunicipalityExists(string municipality);
        float GetMunicipalityTax(string municipality,DateTime taxDate);

    }
}
