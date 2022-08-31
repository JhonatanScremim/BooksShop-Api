using System.Threading.Tasks;

namespace BooksShop.Order.Infra.Interfaces
{
    public interface IRabbitMQMessageConsumer
    {
        Task ConsumerCheckout();
    }
}