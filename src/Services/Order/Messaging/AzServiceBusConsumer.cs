
using MessagingBus;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Ordering.Entities;
using Ordering.Helpers;
using Ordering.Messages;
using Ordering.Repositories;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Messaging
{
    public class AzServiceBusConsumer: IAzServiceBusConsumer
    {
        private readonly string subscriptionName = "wineshoporder";
        private readonly IReceiverClient checkoutMessageReceiverClient;
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly OrderRepository _orderRepository;
        private readonly IMessageBus _messageBus;

        private readonly string checkoutMessageTopic;

        public AzServiceBusConsumer(IConfiguration configuration, IMessageBus messageBus, OrderRepository orderRepository, IServiceScopeFactory scopeFactory)
        {
            _configuration = configuration;
            _orderRepository = orderRepository;
            // _logger = logger;
            _messageBus = messageBus;

            var serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            checkoutMessageTopic = _configuration.GetValue<string>("CheckoutMessageTopic");

            checkoutMessageReceiverClient = new SubscriptionClient(serviceBusConnectionString, checkoutMessageTopic, subscriptionName);
            _scopeFactory = scopeFactory;
        }

        public void Start()
        {
            var messageHandlerOptions = new MessageHandlerOptions(OnServiceBusException) { MaxConcurrentCalls = 4 };

            checkoutMessageReceiverClient.RegisterMessageHandler(OnCheckoutMessageReceived, messageHandlerOptions);
        }

        private async Task OnCheckoutMessageReceived(Message message, CancellationToken arg2)
        {
            try
            {
                var body = Encoding.UTF8.GetString(message.Body);//json from service bus

                // Save order with status "not paid"
                BasketCheckoutMessage basketCheckoutMessage = JsonConvert.DeserializeObject<BasketCheckoutMessage>(body);


                using (var scope = _scopeFactory.CreateScope())
                {
                    var tokenValidationService = scope.ServiceProvider
                        .GetRequiredService<TokenValidationService>();

                    if (!await tokenValidationService.ValidateTokenAsync(
                            basketCheckoutMessage.SecurityContext.AccessToken))
                    {
                        // log, cleanup, ... but don't throw an exception as that will result
                        // in the message not being regarded as handled.  
                        return;
                    }
                }


                var orderId = Guid.NewGuid();

                var order = new Order
                {
                    UserId = basketCheckoutMessage.UserId,
                    Id = orderId,
                    OrderPaid = false,
                    OrderPlaced = DateTime.Now,
                    OrderTotal = basketCheckoutMessage.BasketTotal
                };

                await _orderRepository.AddOrder(order);

                //message count should be 0
                //on the Service Bus Subscription page, the Active message count is 0.
                //It's because a receiver has received messages from this subscription and completed the messages.

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        private Task OnServiceBusException(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine(exceptionReceivedEventArgs);

            return Task.CompletedTask;
        }

        public void Stop()
        {
        }
    }
}
