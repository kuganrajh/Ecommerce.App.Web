using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product.App.Domain;
namespace Product.App.Data.Context
{    
    public class ProductDBContext : DbContext
    {
        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options)
        {
            //dotnet ef migrations add InitialMigration --project Product.App.Data --startup-project Product.App.Web
            //dotnet ef database update --project Product.App.Data --startup-project Product.App.Web
        }
       
        public DbSet<ProductItem> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
        }
    }
}
