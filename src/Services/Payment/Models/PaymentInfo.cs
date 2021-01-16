using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Models
{
    public class PaymentInfo
    {
        public decimal Total { get; set; }

        public Guid OrderId  { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string CardExpiration { get; set; }
    }
}
