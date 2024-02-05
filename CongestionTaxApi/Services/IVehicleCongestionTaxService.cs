using CongestionTax.Api.Domain;

namespace CongestionTax.Api.Services;

public interface IVehicleCongestionTaxService
{
    double CalculateVehicleCongestionTax(string city, string licensePlate, DateTime[] dates);
    CityCongestionTax GetVehicleCongestionTax(string city, string licensePlate);
}