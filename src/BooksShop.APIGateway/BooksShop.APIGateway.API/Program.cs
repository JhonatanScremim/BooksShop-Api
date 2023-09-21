using Ocelot.Middleware;
using Ocelot.DependencyInjection;
using System.Net;
using BooksShop.APIGateway.API.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("Ocelot.json");

builder.Services.AddControllers();

builder.Services.ConfigureDownstreamHostAndPortsPlaceholders(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseOcelot();

app.Run();
