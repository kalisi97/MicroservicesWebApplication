using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace External.PaymentGateway.Model
{
    public class PaymentDto
    {
        public decimal Total { get; set; }

        public Guid OrderId { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string CardExpiration { get; set; }
    }
}
