using BooksShop.Order.API;
using BooksShop.Order.Application;
using BooksShop.Order.Infra;
using BooksShop.Order.Infra.Interfaces;
using BooksShop.Order.Repository;
using BooksShop.Order.Repository.Context;
using BooksShop.Order.Repository.Interfaces;
using Hangfire;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("BooksShop-Order")));

var builderDb = new DbContextOptionsBuilder<DataContext>();
builderDb.UseSqlServer(builder.Configuration.GetConnectionString("BooksShop-Order"));

//Hangfire
builder.Services.AddHangfire(options => 
{
    options.UseSqlServerStorage(builder.Configuration.GetConnectionString("BooksShop-Order"));
});
builder.Services.AddHangfireServer();
        
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IRabbitMQMessageConsumer, RabbitMQMessageConsumer>();

builder.Services.AddTransient<IBaseRepository, BaseRepository>();
builder.Services.AddSingleton(new OrderRepository(builderDb.Options));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard();
HangfireJobs.Start();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


