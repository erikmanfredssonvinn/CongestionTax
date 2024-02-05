namespace CongestionTax.Api.Domain;

public interface ICongestionTaxCalculator
{
    double CalculateTotalDailyTollFee(Vehicle vehicle, DateTime[] dateTimes);
}