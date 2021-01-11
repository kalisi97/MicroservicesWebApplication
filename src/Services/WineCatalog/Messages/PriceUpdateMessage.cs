using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WineCatalog.Messages
{
    public class PriceUpdateMessage:IntegrationBaseMessage
    {
        public Guid WineId  { get; set; }
        public decimal Price  { get; set; }
    }
}
