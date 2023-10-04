using BooksShop.Basket.Domain;
using BooksShop.Basket.Services;
using BooksShop.Basket.Services.Interfaces;
using BooksShop.Basket.Services.Models;

namespace BooksShop.Basket.API
{
    public static class RoutesBasket
    {
        public static void AddRoutesBasket(this WebApplication app, WebApplicationBuilder builder)
        {
            app.MapGet("api/v1/Basket/{userName}", async Task<IResult>(string userName, IBasketService basketService) => {
    
                try{
                    return Results.Ok(await basketService.GetBasketAsync(userName) ??  new ShoppingCart(userName));
                }
                catch(Exception e){
                    return Results.BadRequest(e.Message);
                }
            });

            app.MapPut("api/v1/Basket", async Task<IResult>(ShoppingCart basket, IBasketService basketService, DiscountGrpcService discountGrpcService) => {
                
                try{
                    foreach(var item in basket.Items)
                    {
                        var coupon = await discountGrpcService.GetDiscount(item.BookId);
                        item.Price -= coupon.Amount;
                    }
                    return Results.Ok(await basketService.UpdateBasketAsync(basket));
                }
                catch(Exception e){
                    return Results.BadRequest(e.Message);
                }
            });

            app.MapDelete("api/v1/Basket/{userName}", async Task<IResult>(string userName, IBasketService basketService) => {

                try{
                    await basketService.DeleteBasket(userName);
                    return Results.Ok();
                }
                catch(Exception e){
                    return Results.BadRequest(e.Message);
                }
            });

            app.MapPost("api/v1/Basket/checkout", async Task<IResult>(BasketCheckout checkout, IBasketService basketService, IRabbitMQMessageSenderService senderService) => {

                try{
                    var basket = await basketService.GetBasketAsync(checkout.UserName);

                    if (basket == null)
                        return Results.NotFound();

                    senderService.SendMessage(checkout, builder.Configuration.GetValue<string>("RabbitMQSettings:CheckoutQueue"));
                    
                    return Results.Ok();
                }
                catch(Exception e){
                    return Results.BadRequest(e.Message);
                }
            });
        }
    }
}