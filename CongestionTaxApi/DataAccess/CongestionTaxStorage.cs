using CongestionTax.Api.Domain;

namespace CongestionTax.Api.DataAccess;

public class CongestionTaxStorage
    : ICongestionTaxStorage
{
    private readonly List<CongestionTaxRule> _congestionTaxes;

    // TODO: Obviously add a real database for storage, store connection strings etc in key vault
    public CongestionTaxStorage()
    {
        _congestionTaxes = new List<CongestionTaxRule>()
        {
            new()
            {
                City = "Gothenburg",
                MaxDailyRate = 60,
                SingleChargeRule = true,
                TaxRates = new()
                {
                    new() { StartTime = TimeOnly.Parse("06:00"), EndTime = TimeOnly.Parse("06:29"), Rate = 8 },
                    new() { StartTime = TimeOnly.Parse("06:30"), EndTime = TimeOnly.Parse("06:59"), Rate = 13 },
                    new() { StartTime = TimeOnly.Parse("07:00"), EndTime = TimeOnly.Parse("07:59"), Rate = 18 },
                    new() { StartTime = TimeOnly.Parse("08:00"), EndTime = TimeOnly.Parse("08:29"), Rate = 13 },
                    new() { StartTime = TimeOnly.Parse("08:30"), EndTime = TimeOnly.Parse("14:59"), Rate = 8 },
                    new() { StartTime = TimeOnly.Parse("15:00"), EndTime = TimeOnly.Parse("15:29"), Rate = 13 },
                    new() { StartTime = TimeOnly.Parse("15:30"), EndTime = TimeOnly.Parse("16:59"), Rate = 18 },
                    new() { StartTime = TimeOnly.Parse("17:00"), EndTime = TimeOnly.Parse("17:59"), Rate = 13 },
                    new() { StartTime = TimeOnly.Parse("18:00"), EndTime = TimeOnly.Parse("18:29"), Rate = 8 },
                    new() { StartTime = TimeOnly.Parse("18:30"), EndTime = TimeOnly.Parse("05:59"), Rate = 0 },
                },
                TaxExemptVehicles = new()
                    { "Emergency", "Bus", "Diplomat", "Diplomat", "Motorcycle", "Military", "Foreign" },
                TollFreeWeekends = true,
                TollFreeDates = new()
                {
                    new DateTime(2013, 1, 1),
                    new DateTime(2013, 1, 6),
                    new DateTime(2013, 4, 15),
                    new DateTime(2013, 4, 17),
                    new DateTime(2013, 5, 1),
                    new DateTime(2013, 5, 26),
                    new DateTime(2013, 6, 6),
                    new DateTime(2013, 6, 25),
                    new DateTime(2023, 7, 1),
                    new DateTime(2023, 7, 2),
                    new DateTime(2023, 7, 3),
                    new DateTime(2023, 7, 4),
                    new DateTime(2023, 7, 5),
                    new DateTime(2023, 7, 6),
                    new DateTime(2023, 7, 7),
                    new DateTime(2023, 7, 8),
                    new DateTime(2023, 7, 9),
                    new DateTime(2023, 7, 10),
                    new DateTime(2023, 7, 11),
                    new DateTime(2023, 7, 12),
                    new DateTime(2023, 7, 13),
                    new DateTime(2023, 7, 14),
                    new DateTime(2023, 7, 15),
                    new DateTime(2023, 7, 16),
                    new DateTime(2023, 7, 17),
                    new DateTime(2023, 7, 18),
                    new DateTime(2023, 7, 19),
                    new DateTime(2023, 7, 20),
                    new DateTime(2023, 7, 21),
                    new DateTime(2023, 7, 22),
                    new DateTime(2023, 7, 23),
                    new DateTime(2023, 7, 24),
                    new DateTime(2023, 7, 25),
                    new DateTime(2023, 7, 26),
                    new DateTime(2023, 7, 27),
                    new DateTime(2023, 7, 28),
                    new DateTime(2023, 7, 29),
                    new DateTime(2023, 7, 30),
                    new DateTime(2023, 7, 31),
                    new DateTime(2013, 11, 5),
                    new DateTime(2013, 12, 24),
                    new DateTime(2013, 12, 25),
                    new DateTime(2013, 12, 26),
                    new DateTime(2013, 12, 31),
                }
            }
        };
    }

    public CongestionTaxRule GetCongestionTax(string city)
    {
        var tax = _congestionTaxes.FirstOrDefault(x => x.City == city);
        if (tax == null)
            throw new NullReferenceException("No congestion tax found for city: " + city);

        return tax;
    }
}