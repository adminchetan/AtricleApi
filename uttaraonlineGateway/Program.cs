using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("Ocelot.json");
builder.Services.AddOcelot();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod());
});

var app = builder.Build();
app.UseCors("AllowOrigin");

app.UseOcelot().Wait();  // we can use async and awat alos with ocelot 
app.MapControllers();
app.Run();
