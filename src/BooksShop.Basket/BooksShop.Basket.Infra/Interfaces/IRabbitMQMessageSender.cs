using BooksShop.Basket.Infra.Models;

namespace BooksShop.Basket.Infra.Interfaces
{
    public interface IRabbitMQMessageSender
    {
         void SendMessage(BaseMessage baseMessage, string queueName);
    }
}