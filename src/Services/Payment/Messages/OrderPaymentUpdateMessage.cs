using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Messages
{
    public class OrderPaymentUpdateMessage : IntegrationBaseMessage
    {
        public Guid OrderId { get; set; }
        public bool PaymentSuccess { get; set; }
    }
}
