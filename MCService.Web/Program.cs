using MCService.Services;
using MCService.Database;
using MCService.Web.Controllers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("MSSQL");

builder.Services.AddControllers();

builder.Services.AddScoped<AdressService>();
builder.Services.AddScoped<LocationService>();
builder.Services.AddScoped<WorkService>();
builder.Services.AddScoped<PriceService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<SqlDriver>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

