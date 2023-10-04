using BooksShop.Basket.Services;
using BooksShop.Basket.Services.Interfaces;
using BooksShop.Basket.API;
using BooksShop.Discount.Grpc.API.Protos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Services.AddStackExchangeRedisCache(x => 
{
    x.Configuration = builder.Configuration.GetValue<string>("ConnectionStringRedis");
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IRabbitMQMessageSenderService, RabbitMQMessageSenderService>();
builder.Services.AddScoped<DiscountGrpcService>();
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(
    options => options.Address = new Uri("http://localhost:5149"));

var app = builder.Build();

//Definição endpoints
app.AddRoutesBasket(builder);

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