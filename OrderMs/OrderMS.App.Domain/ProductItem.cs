using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMS.App.Domain
{
    public class ProductItem
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int StockAvailable { get; set; }

        public string Category { get; set; }
    }
}
