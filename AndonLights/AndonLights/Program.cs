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

app.Run();


using (var db = new AndonLightsDbContext())
{
    Session test = new Session { InTime= DateTime.Now, OutTime= new DateTime(2024,12,12), ErrorMessage="testerror1" };
    db.Sessions.Add(test);
    db.SaveChanges();
}



