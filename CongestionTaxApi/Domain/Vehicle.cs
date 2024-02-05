namespace CongestionTax.Api.Domain;

public class Vehicle
{
    public string VehicleType { get; set; }
    public string LicensePlate { get; set; }
    public List<CityCongestionTax>? CityTollPassages { get; set; }
    
    public void SetCityTollPassage(string city, DateOnly date, double tollFee)
    {
        if (CityTollPassages == null)
            CityTollPassages = new List<CityCongestionTax>();

        var cityTollPassage = CityTollPassages.FirstOrDefault(x => x.City == city);
        if (cityTollPassage == null)
        {
            cityTollPassage = new CityCongestionTax
                { City = city, DailyTollPassageCosts = new List<TollPassage>() };
            CityTollPassages.Add(cityTollPassage);
        }

        cityTollPassage.DailyTollPassageCosts.Add(new TollPassage { Date = date, TollPassageInSek = tollFee });
    }
}

public class CityCongestionTax
{
    public string City { get; set; }
    public List<TollPassage> DailyTollPassageCosts { get; set; }
}

public class TollPassage
{
    public DateOnly Date { get; set; }
    public double TollPassageInSek { get; set; }
}