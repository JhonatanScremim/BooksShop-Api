using System.Text;
using System.Text.Json;
using BooksShop.Basket.Services.Interfaces;
using BooksShop.Basket.Services.Models;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace BooksShop.Basket.Services
{
    public class RabbitMQMessageSenderService : IRabbitMQMessageSenderService
    {
        private readonly IConfiguration _configuration;
        private readonly string _hostName;
        private readonly string _username;
        private readonly string _password;
        private IConnection? _connection;

        public RabbitMQMessageSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
            _hostName = _configuration.GetValue<string>("RabbitMQSettings:HostName");
            _username = _configuration.GetValue<string>("RabbitMQSettings:Username");
            _password = _configuration.GetValue<string>("RabbitMQSettings:Password");
        }

        public void SendMessage(BaseMessage baseMessage, string queueName)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostName,
                UserName = _username,
                Password = _password
            };
            _connection = factory.CreateConnection();

            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: queueName, false, false, false, arguments: null); 
            byte[] body = GetMessageAsByteArray(baseMessage);
            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
        }

        private byte[] GetMessageAsByteArray(BaseMessage baseMessage)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true //Escrever classes filhas
            };
            var json = JsonSerializer.Serialize<BasketCheckout>((BasketCheckout)baseMessage, options);
            return Encoding.UTF8.GetBytes(json);
        }
    }
}