using System.Text;
using System.Text.Json;
using BooksShop.Order.Infra.Interfaces;
using BooksShop.Order.Infra.Models;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using BooksShop.Order.Repository.Interfaces;
using System.Threading.Tasks;
using System;

namespace BooksShop.Order.Infra
{
    public class RabbitMQMessageConsumer : IRabbitMQMessageConsumer
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IConfiguration _configuration;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQMessageConsumer(IBaseRepository baseRepository, IConfiguration configuration)
        {
            _baseRepository = baseRepository;
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
                try
                {
                    Console.WriteLine("--------------------------------------Entrou na leitura----------------------");

                     //Obter conteudo em um array de bytes e converter para uma string
                     var content = Encoding.UTF8.GetString(ev.Body.ToArray());
                    var order = JsonSerializer.Deserialize<BasketCheckout>(content);

                    ProccessOrder(order).GetAwaiter().GetResult();
                    Console.WriteLine("Sucesso");
                    //Exclui pedido da fila
                    _channel.BasicAck(ev.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    _channel.BasicNack(ev.DeliveryTag, true, false);
                }
            };

            Console.WriteLine("--------------------------------------Saiu do metodo----------------------");

            _channel.BasicConsume("Checkout Queue", false, consumer);
        }

        public async Task<bool> ProccessOrder(BasketCheckout basketCheckout)
        {
            Console.WriteLine("--------------------------------------Entrou no processo----------------------");

            var order = new Domain.Order(
                    basketCheckout.UserName,
                    basketCheckout.TotalPrice,
                    JsonSerializer.Serialize(basketCheckout.UserInformation),
                    string.Join(", ", basketCheckout.BooksIds),
                    JsonSerializer.Serialize(basketCheckout.Address),
                    JsonSerializer.Serialize(basketCheckout.PaymentInformation)
                );

            Console.WriteLine("--------------------------------------Ir para salvar----------------------");

            _baseRepository.Create(order);
            
            Console.WriteLine("--------------------------------------Salvou com sucesso----------------------");
            
            return await _baseRepository.SaveChangesAsync();
            
        }
    }
}