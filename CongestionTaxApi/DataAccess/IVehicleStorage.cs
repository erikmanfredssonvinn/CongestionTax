using CongestionTax.Api.Domain;

namespace CongestionTax.Api.DataAccess;

public interface IVehicleStorage
{
    Vehicle GetVehicle(string licensePlate);
    void UpdateVehicle(Vehicle updateVehicle);
}