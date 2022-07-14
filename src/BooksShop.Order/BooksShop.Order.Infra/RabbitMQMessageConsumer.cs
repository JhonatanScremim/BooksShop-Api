using System.Text;
using System.Text.Json;
using BooksShop.Order.Infra.Interfaces;
using BooksShop.Order.Infra.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BooksShop.Order.Infra
{
    public class RabbitMQMessageConsumer : IRabbitMQMessageConsumer
    {
        private readonly IConfiguration _configuration;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQMessageConsumer(IConfiguration configuration)
        {
            _configuration = configuration;

            var factory = new ConnectionFactory()
            {
                HostName = _configuration.GetValue<string>("RabbitMQSettings:HostName"),
                UserName = _configuration.GetValue<string>("RabbitMQSettings:Username"),
                Password = _configuration.GetValue<string>("RabbitMQSettings:Password")
            };
            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "Checkout Queue", false, false, false, arguments: null); 
        }

        public async Task ConsumerCheckout()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (channel, ev) => 
            {
                //Obter conteudo em um array de bytes e converter para uma string
                var content = Encoding.UTF8.GetString(ev.Body.ToArray());
                var order = JsonSerializer.Deserialize<BasketCheckout>(content);
                ProccessOrder(order).GetAwaiter().GetResult();
                
                //Exclui pedido da fila
                _channel.BasicAck(ev.DeliveryTag, false);
            };

            _channel.BasicConsume("Checkout Queue", false, consumer);
        }

        private async Task ProccessOrder(BasketCheckout order)
        {
            throw new NotImplementedException();
        }
    }
}