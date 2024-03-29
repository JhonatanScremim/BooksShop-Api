using BooksShop.Catalog.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using AutoMapper;
using BooksShop.Catalog.Application;
using BooksShop.Catalog.Application.Interfaces;
using BooksShop.Catalog.Repository;
using BooksShop.Catalog.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("BooksShop-Catalog-Postgres")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//services
builder.Services.AddScoped<IBookService, BookService>();

//repositories
builder.Services.AddScoped<IBaseRepository, BaseRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
