using Microsoft.EntityFrameworkCore;
using OrderMS.App.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace OrderMS.App.Data.Context
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }


        // Docker comand for CosmosDB local 
        // docker pull mcr.microsoft.com/cosmosdb/linux/azure-cosmos-emulator
        // docker run -p 8081:8081 --name=cosmos-emulator mcr.microsoft.com/cosmosdb/linux/azure-cosmos-emulator
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define Cosmos DB container mapping
            modelBuilder.Entity<Order>()
                .ToContainer("Orders")  //  EF Core will automatically create "Orders" container
                .HasPartitionKey(o => o.CustomerId); //  Without HasPartitionKey()CosmosDB queries will be slow and inefficient

            // Define Cosmos DB container mapping
            modelBuilder.Entity<ProductItem>()
                .ToContainer("ProductItems");  //  EF Core will automatically create "Orders" container

            base.OnModelCreating(modelBuilder);
        }
    }
}
