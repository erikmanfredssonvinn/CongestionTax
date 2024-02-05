using CongestionTax.Api.DataAccess;
using CongestionTax.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IVehicleStorage, VehicleStorage>();
builder.Services.AddScoped<ICongestionTaxStorage, CongestionTaxStorage>();
builder.Services.AddScoped<IVehicleCongestionTaxService, VehicleCongestionTaxService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();