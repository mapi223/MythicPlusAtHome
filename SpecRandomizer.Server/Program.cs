using Microsoft.EntityFrameworkCore;
using SpecRandomizer.Server.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<SpecRandomizerDbContext>(options => {
    options.UseSqlServer
    (@"Server=(localdb)\\mssqllocaldb;Database=SpecRandomizerServerContext-ac424fb8-a6f5-4d23-9e29-a0d4ba549bbb;Trusted_Connection=True;MultipleActiveResultSets=true");
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
