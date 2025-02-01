using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMS.App.Domain
{
    public class Order
    {
        [JsonProperty("id")]
        public Guid Id { get; set; } = Guid.NewGuid(); // CosmosDB requires a string id

        public string CustomerId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        public string Status { get; set; } = "Pending"; // Pending, Processing, Completed

        public decimal TotalAmount { get; set; }
    }
}
