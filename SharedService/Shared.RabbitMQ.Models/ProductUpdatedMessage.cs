using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.RabbitMQ.Models
{
    public class ProductUpdatedMessage
    {
        [JsonProperty("productId")]
        public Guid ProductId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("stockAvailable")]
        public int StockAvailable { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("eventType")]
        public EventType EventType { get; set; } 
    }
}
