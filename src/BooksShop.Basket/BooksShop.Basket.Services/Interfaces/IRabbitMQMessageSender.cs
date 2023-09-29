using BooksShop.Basket.Services.Models;

namespace BooksShop.Basket.Services.Interfaces
{
    public interface IRabbitMQMessageSenderService
    {
         void SendMessage(BaseMessage baseMessage, string queueName);
    }
}