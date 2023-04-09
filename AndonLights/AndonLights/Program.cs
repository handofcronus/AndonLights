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

builder.Services.AddScoped<ISessionRepo, SessionRepository>();
builder.Services.AddScoped<ISessionService, SessionService>();


builder.Services.AddDbContext<AndonLightsDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AndonLightsDocker")));


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

