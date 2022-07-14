using BooksShop.Order.Infra.Interfaces;

namespace BooksShop.Order.API
{
    public class Worker : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Option 1
            while (!stoppingToken.IsCancellationRequested)
            {
                // do async work
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var consumerService = scope.ServiceProvider.GetRequiredService<IRabbitMQMessageConsumer>();
                    await consumerService.ConsumerCheckout();
                }
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}