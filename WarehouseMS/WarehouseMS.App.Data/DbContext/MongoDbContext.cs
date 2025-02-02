using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseMS.App.Data.DbContext
{
    public class MongoDbContext
    {
        //private readonly IMongoDatabase _database;

        //public MongoDbContext(IConfiguration configuration)
        //{
        //    var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
        //    _database = client.GetDatabase(configuration["MongoDB:DatabaseName"]);
        //}

        //public IMongoCollection<Order> Orders => _database.GetCollection<Order>("Orders");
    }
}
