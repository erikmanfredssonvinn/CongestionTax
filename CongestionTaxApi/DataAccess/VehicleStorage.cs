using CongestionTax.Api.Domain;

namespace CongestionTax.Api.DataAccess;

public class VehicleStorage : IVehicleStorage
{
    private readonly List<Vehicle> _vehicles;
    
    // TODO: Obviously add a real database for storage, store connection strings etc in key vault
    public VehicleStorage()
    {
        _vehicles = new()
        {
            new()
            {
                LicensePlate = "ABC123",
                VehicleType = "Car",
                CityTollPassages = new List<CityCongestionTax>()
            },
            new()
            {
                LicensePlate = "DEF456",
                VehicleType = "Diplomat",
                CityTollPassages = new List<CityCongestionTax>()
            }
        };
    }

    public Vehicle GetVehicle(string licensePlate)
    {
        var vehicle = _vehicles.FirstOrDefault(x => x.LicensePlate == licensePlate);
        if (vehicle == null)
            throw new NullReferenceException("No vehicle found for license plate: " + licensePlate);

        return vehicle;
    }

    public void UpdateVehicle(Vehicle updateVehicle)
    {
        var existingVehicle = _vehicles.FirstOrDefault(x => x.LicensePlate == updateVehicle.LicensePlate);
        if (existingVehicle == null)
            throw new NullReferenceException("No vehicle found for license plate: " + updateVehicle.LicensePlate);

        _vehicles.Remove(existingVehicle);
        _vehicles.Add(updateVehicle);
    }
}