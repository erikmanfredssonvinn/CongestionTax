namespace CongestionTax.Api.Domain;

public class CongestionTaxRule
{
    public string City { get; set; }
    public double MaxDailyRate { get; set; }
    public bool SingleChargeRule { get; set; }
    public List<TaxRateTimeSpans> TaxRates { get; set; }
    public List<string> TaxExemptVehicles { get; set; }
    public bool TollFreeWeekends { get; set; }
    public List<DateTime> TollFreeDates { get; set; }
}

public class TaxRateTimeSpans
{
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public double Rate { get; set; }
}