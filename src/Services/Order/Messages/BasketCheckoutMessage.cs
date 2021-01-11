using System;
using System.Collections.Generic;
using Messages;

namespace Ordering.Messages
{
    public class BasketCheckoutMessage : IntegrationBaseMessage
    {
        public Guid UserId { get; set; }

        //payment information
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string CardExpiration { get; set; }

        //order info
        public decimal BasketTotal { get; set; }
    }
}
