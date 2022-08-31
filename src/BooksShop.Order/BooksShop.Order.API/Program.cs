using BooksShop.Order.API;
using BooksShop.Order.Infra;
using BooksShop.Order.Infra.Interfaces;
using BooksShop.Order.Repository;
using BooksShop.Order.Repository.Context;
using BooksShop.Order.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("BooksShop-Order")));
        
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRabbitMQMessageConsumer, RabbitMQMessageConsumer>();

builder.Services.AddScoped<IBaseRepository, BaseRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddHostedService<Worker>();

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


