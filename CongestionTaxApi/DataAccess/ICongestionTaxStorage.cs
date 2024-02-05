using CongestionTax.Api.Domain;

namespace CongestionTax.Api.DataAccess;

public interface ICongestionTaxStorage
{
    CongestionTaxRule GetCongestionTax(string city);
}