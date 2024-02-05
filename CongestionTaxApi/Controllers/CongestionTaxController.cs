using CongestionTax.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTax.Api.Controllers;

[ApiController]
[Route("congestionTax")]
public class CongestionTaxController
{
    private readonly ILogger<CongestionTaxController> _logger;
    private readonly IVehicleCongestionTaxService _vehicleCongestionTaxService;

    public CongestionTaxController(ILogger<CongestionTaxController> logger,
        IVehicleCongestionTaxService vehicleCongestionTaxService
    )
    {
        _logger = logger;
        _vehicleCongestionTaxService = vehicleCongestionTaxService;
    }

    [HttpPost("{city}/{licensePlate}")]
    public IActionResult CreateVehicleCongestionTax(string city, string licensePlate,
        [FromBody] DateTime[] dateTimes)
    {
        // Sorting here and returning BadRequest if the dates are not the same day so that the service doesn't have to handle it and waste resources.
        // I am doing the same sorting in the CongestionTaxCalculator, but it's better to do it here so that the service doesn't have to handle it and waste resources.
        // But I have to do it in the service as well because the service is used by other services and applications.
        Array.Sort(dateTimes);
        // TODO: Something better than this, like HTTP response classes that hides creating logic.
        if (dateTimes.First().Date != dateTimes.Last().Date)
            return new BadRequestObjectResult(new { message = "The dates must be the same day" });

        var taxAmount = _vehicleCongestionTaxService.CalculateVehicleCongestionTax(city, licensePlate, dateTimes);
        // TODO: Something better than this, like HTTP response classes that hides creating logic.
        return new ObjectResult(new { taxAmount = taxAmount })
        {
            StatusCode = StatusCodes.Status201Created
        };
    }

    // TODO: Add filtering for date range
    [HttpGet("{city}/{licensePlate}")]
    public IActionResult GetVehicleCongestionTax(string city, string licensePlate)
    {
        var congestionTax = _vehicleCongestionTaxService.GetVehicleCongestionTax(city, licensePlate);
        if (congestionTax == null)
            return new NotFoundResult();
        return new OkObjectResult(congestionTax);
    }
    // TODO: Add more APIs, CRUD Vehicle, CRUD CongestionTaxRule etc, will ofc add more code in service/data layers as well as domain layer.
}