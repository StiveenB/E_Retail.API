using E_Retail.API.Data;
using E_Retail.API.Data.TuProyecto.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de la cadena de conexi�n para PostgreSQL
builder.Configuration.AddJsonFile("appsettings.json");
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Agregar servicios
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();

// Configuraci�n de Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure el pipeline de solicitudes HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
