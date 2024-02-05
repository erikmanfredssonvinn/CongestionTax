namespace CongestionTax.Api.Domain;

public class CongestionTaxCalculator : ICongestionTaxCalculator
{
    private readonly CongestionTaxRule _congestionTaxRule;

    public CongestionTaxCalculator(CongestionTaxRule congestionTaxRule)
    {
        _congestionTaxRule = congestionTaxRule;
    }

    /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total congestion tax for that day
         */
    
    public double CalculateTotalDailyTollFee(Vehicle vehicle, DateTime[] dateTimes)
    {
        Array.Sort(dateTimes);
        if (dateTimes.First().Date != dateTimes.Last().Date)
            throw new ArgumentException("The dates must be the same day", nameof(dateTimes));

        double dailyTotalFee = 0;
        var earlierPassage = dateTimes[0];
        for (int i = 0; i < dateTimes.Length; i++)
        {
            if (_congestionTaxRule.SingleChargeRule)
            {
                var currentPassage = dateTimes[i];
                if (i > 0) earlierPassage = dateTimes[i - 1];

                dailyTotalFee += CalculateTollFeeSingleChargeRule(earlierPassage, currentPassage, vehicle);
            }
            else
            {
                dailyTotalFee = CalculateTollFee(vehicle, dateTimes[i]);
            }

            dailyTotalFee = CalculateIfReachedMaxDailyRate(dailyTotalFee);
        }

        return dailyTotalFee;
    }

    private double CalculateIfReachedMaxDailyRate(double totalFee)
    {
        if (_congestionTaxRule.MaxDailyRate > 0 && totalFee >= _congestionTaxRule.MaxDailyRate)
            return _congestionTaxRule.MaxDailyRate;
        return totalFee;
    }

    private double CalculateTollFeeSingleChargeRule(DateTime earlierPassage, DateTime currentPassage, Vehicle vehicle)
    {
        if (currentPassage != earlierPassage && currentPassage - earlierPassage <= TimeSpan.FromMinutes(60))
        {
            var first = CalculateTollFee(vehicle, earlierPassage);
            var second = CalculateTollFee(vehicle, currentPassage);
            return first > second ? first : second;
        }

        return CalculateTollFee(vehicle, currentPassage);
    }

    private double CalculateTollFee(Vehicle vehicle, DateTime dateTime)
    {
        if (IsTollFreeDate(dateTime) || IsTollFreeVehicle(vehicle))
            return 0;

        foreach (var taxRate in _congestionTaxRule.TaxRates)
        {
            if (taxRate.StartTime <= TimeOnly.FromDateTime(dateTime) &&
                taxRate.EndTime >= TimeOnly.FromDateTime(dateTime))
                return taxRate.Rate;
        }

        return 0;
    }

    private bool IsTollFreeDate(DateTime date)
    {
        if (_congestionTaxRule.TollFreeWeekends &&
            (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday))
            return true;

        return _congestionTaxRule.TollFreeDates.Contains(date.Date);
    }

    private bool IsTollFreeVehicle(Vehicle vehicle)
    {
        return _congestionTaxRule.TaxExemptVehicles.Contains(vehicle.VehicleType);
    }
}