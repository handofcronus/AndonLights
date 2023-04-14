using AndonLights.DAL;
using AndonLights.DAL.Repositories.Interfaces;
using AndonLights.DTOs;
using AndonLights.Model;
using AndonLights.Repositories;
using AndonLights.Services;
using AndonLights.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


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




var connHost = Environment.GetEnvironmentVariable("SQL:HOST");
var connUser = Environment.GetEnvironmentVariable("SQL:USER");
var connPsw = Environment.GetEnvironmentVariable("SQL:PW");
var connString = "";
if(connHost is null || connUser is null || connPsw is null )
{
    connString = builder.Configuration.GetConnectionString("AndonLights");
}
else
{
    connString = $"Server={connHost};Database = AndonLightsDB;User Id ={connUser};Password={connPsw};MultipleActiveResultSets=true; TrustServerCertificate=true";
}

builder.Services.AddDbContext<AndonLightsDbContext>(options => options.UseSqlServer(connString));


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

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<AndonLightsDbContext>();
    context.Database.EnsureCreated();
}

app.Run();

