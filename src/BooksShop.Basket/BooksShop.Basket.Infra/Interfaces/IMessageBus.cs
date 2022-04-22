using BooksShop.Basket.Infra.Models;

namespace BooksShop.Basket.Infra.Interfaces
{
    public interface IMessageBus
    {
        Task PublicMessage(BaseMessage baseMessage, string queueName);
    }
}