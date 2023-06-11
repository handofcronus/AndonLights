using AndonLights.Controllers.Attributes;
using AndonLights.DAL;
using AndonLights.DAL.Repositories.Interfaces;
using AndonLights.Repositories;
using AndonLights.Services;
using AndonLights.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddOpenApiDocument();

builder.Services.AddHostedService<TimedHostedService>(); 

builder.Services.AddScoped<IAndonLightRepo, AndonLightRepository>();
builder.Services.AddScoped<IAndonLightService, AndonLightService>();

builder.Services.AddScoped<IStateRepo, StateRepository>();
builder.Services.AddScoped<IStateService, StateService>();

builder.Services.AddScoped<IApiKeyService, ApiKeyService>();
builder.Services.AddScoped<IClientService, ClientService>();

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
    //app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<AndonLightsDbContext>();
    context.Database.EnsureCreated();
}


ApiKeyService k = new ApiKeyService();
int i = 0;
while (i < 50)
{
    k.GenerateApiKey();
    i++;
}


app.Run();

