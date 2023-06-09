using AndonLights.DAL;
using AndonLights.DAL.Repositories.Interfaces;
using AndonLights.DTOs;
using AndonLights.Model;
using AndonLights.Repositories;
using AndonLights.Services;
using AndonLights.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<TimedHostedService>(); 

builder.Services.AddScoped<IAndonLightRepo, AndonLightRepository>();
builder.Services.AddScoped<IAndonLightService, AndonLightService>();

builder.Services.AddScoped<IStateRepo, StateRepository>();
builder.Services.AddScoped<IStateService, StateService>();

var connHost = Environment.GetEnvironmentVariable("PGQL__HOST");
var connUser = Environment.GetEnvironmentVariable("PGQL__USER");
var connPsw = Environment.GetEnvironmentVariable("PGQL__PW");
var connPort = Environment.GetEnvironmentVariable("PGQL__PORT");
var connDBname = Environment.GetEnvironmentVariable("PGQL__DB");
var connString = "";
if (connHost is null || connUser is null || connPsw is null || connPort is null || connDBname is null)
{
    connString = builder.Configuration.GetConnectionString("AndonLightsPostgres");
}
else
{
    NpgsqlConnectionStringBuilder connStringBuilder = new NpgsqlConnectionStringBuilder();
    connStringBuilder.Host = connHost;
    connStringBuilder.Port = Int32.Parse(connPort);
    connStringBuilder.Database = connDBname;
    connStringBuilder.Username = connUser;
    connStringBuilder.Password = connPsw;
    connString = connStringBuilder.ToString();
}

builder.Services.AddDbContext<AndonLightsDbContext>(options => options.UseNpgsql(connString, o=> o.UseNodaTime()));


var app = builder.Build();
app.Logger.LogInformation(connString);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<AndonLightsDbContext>();
    context.Database.EnsureCreated();
}





app.Run();

