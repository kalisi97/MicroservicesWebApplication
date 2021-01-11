using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShoppingBasket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ShoppingBasket.Repositories;

namespace ShoppingBasket.Services.Worker
{
    public class ServiceBusListener : IHostedService
    {
        private readonly IConfiguration configuration;
        private ISubscriptionClient subscriptionClient;
        private readonly IBasketLinesIntegrationRepository basketLinesRepository;

        public ServiceBusListener(IConfiguration configuration, BasketLinesIntegrationRepository basketLinesRepository)
        {
            this.configuration = configuration;
            this.basketLinesRepository = basketLinesRepository;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            subscriptionClient = new SubscriptionClient(configuration.GetValue<string>("ServiceBusConnectionString"), configuration.GetValue<string>("PriceUpdatedMessageTopic"), configuration.GetValue<string>("subscriptionName"));

            var messageHandlerOptions = new MessageHandlerOptions(e =>
            {
                ProcessError(e.Exception);
                return Task.CompletedTask;
            })
            {
                MaxConcurrentCalls = 3,
                AutoComplete = false
            };

            subscriptionClient.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);

            return Task.CompletedTask;
        }

        private async Task ProcessMessageAsync(Message message, CancellationToken token)
        {
            var messageBody = Encoding.UTF8.GetString(message.Body);
            PriceChanged priceUpdate = JsonConvert.DeserializeObject<PriceChanged>(messageBody);

            await basketLinesRepository.UpdatePrices(priceUpdate);

            await subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);

        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await this.subscriptionClient.CloseAsync();
        }

        protected void ProcessError(Exception e)
        {
        }
    }
    
}
