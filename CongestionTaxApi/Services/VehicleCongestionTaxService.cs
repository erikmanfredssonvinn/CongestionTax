using CongestionTax.Api.DataAccess;
using CongestionTax.Api.Domain;
using CongestionTax.Api.Factory;

namespace CongestionTax.Api.Services;

public class VehicleCongestionTaxService : IVehicleCongestionTaxService
{
    private readonly ICongestionTaxStorage _congestionTaxStorage;
    private readonly IVehicleStorage _vehicleStorage;

    public VehicleCongestionTaxService(ICongestionTaxStorage congestionTaxStorage,
        IVehicleStorage vehicleStorage)
    {
        _congestionTaxStorage = congestionTaxStorage;
        _vehicleStorage = vehicleStorage;
    }

    public double CalculateVehicleCongestionTax(string city, string licensePlate, DateTime[] dates)
    {
        var vehicle = _vehicleStorage.GetVehicle(licensePlate);
        var congestionTaxRule = _congestionTaxStorage.GetCongestionTax(city);
        // Factory so that I can better control the dependency when unit testing...
        var congestionTaxCalculator = CongestionTaxCalculatorFactory.CreateCalculator(congestionTaxRule);
        var totalTollFee = congestionTaxCalculator.CalculateTotalDailyTollFee(vehicle, dates);
        var dateOnly = DateOnly.FromDateTime(dates[0]);
        vehicle.SetCityTollPassage(city, dateOnly, totalTollFee);
        _vehicleStorage.UpdateVehicle(vehicle);
        return totalTollFee;
    }

    public CityCongestionTax GetVehicleCongestionTax(string city, string licensePlate)
    {
        var vehicle = _vehicleStorage.GetVehicle(licensePlate);
        return vehicle.CityTollPassages?.Find(x => x.City == city);
    }
}