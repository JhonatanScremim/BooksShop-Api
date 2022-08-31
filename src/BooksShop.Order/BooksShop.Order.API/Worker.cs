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
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var consumerService = scope.ServiceProvider.GetRequiredService<IRabbitMQMessageConsumer>();
                    await consumerService.ConsumerCheckout();
                }
                await Task.Delay(TimeSpan.FromSeconds(20), stoppingToken);
            }
        }
    }
}