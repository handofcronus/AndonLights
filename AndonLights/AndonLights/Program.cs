using AndonLights.DAL;
using AndonLights.Model;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AndonLightsDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AndonLights")));


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

using var db = new AndonLightsDbContext();
Session session = new Session() { InTime =new DateTime(2023,03,18), ErrorMessage = "test1" };
session.closeSession(DateTime.Now);
db.Sessions.Add(session);
db.SaveChanges();
Console.WriteLine(session.Id);
app.Run();


