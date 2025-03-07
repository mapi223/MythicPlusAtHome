using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using SpecRandomizer.Server.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using SpecRandomizer.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });
builder.Services.AddScoped<ConfigurationService>();
builder.Services.AddScoped<GroupConfigurationService>();
builder.Services.AddDbContext<SpecRandomizerDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("SpecRandomizerServerContext"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => {
            policy.WithOrigins("http://localhost:63600", "https://localhost:63600") // Allow both HTTP and HTTPS
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials(); 
        });
});

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


var app = builder.Build();

app.UseCors("AllowAngular");

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
