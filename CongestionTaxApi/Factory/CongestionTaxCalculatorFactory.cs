using CongestionTax.Api.Domain;

namespace CongestionTax.Api.Factory;

public static class CongestionTaxCalculatorFactory
{
    public static CongestionTaxCalculator CreateCalculator(CongestionTaxRule congestionTaxRule)
    {
        return new CongestionTaxCalculator(congestionTaxRule);
    }
}