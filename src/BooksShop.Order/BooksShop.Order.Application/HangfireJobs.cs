using BooksShop.Order.Infra.Interfaces;
using Hangfire;

namespace BooksShop.Order.Application
{
    public class HangfireJobs
    {
        public static void Start()
        {
            RecurringJob.AddOrUpdate<IRabbitMQMessageConsumer>(x => x.ConsumerCheckout(), Cron.MinuteInterval(5));
        }
    }
}