using Payment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Services
{
    public interface IExternalGatewayPaymentService
    {
        Task<bool> PerformPayment(PaymentInfo paymentInfo);
    }
}
