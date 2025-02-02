using OrderMS.App.Domain;
using OrderMS.App.Infrastructure.Interface;
using System.Collections.Concurrent;

public class InMemoryProductItemRepository : IRepository<ProductItem>
{
    private readonly ConcurrentDictionary<Guid, ProductItem> _productItems = new();

    // Seed sample product items
    public InMemoryProductItemRepository()
    {
        SeedSampleData();
    }

    private void SeedSampleData()
    {
        var items = new List<ProductItem>
        {
            new ProductItem { Id = Guid.NewGuid(),  Name = "Laptop", Price = 1200, StockAvailable = 10, Category = "Electronics" },
            new ProductItem { Id = Guid.NewGuid(),  Name = "Mouse", Price = 25, StockAvailable = 100, Category = "Accessories" },
            new ProductItem { Id = Guid.NewGuid(),  Name = "Keyboard", Price = 50, StockAvailable = 75, Category = "Accessories" }
        };

        foreach (var item in items)
        {
            _productItems[item.Id] = item;
        }
    }

    public Task<ProductItem> CreateAsync(ProductItem item)
    {
        _productItems[item.Id] = item;
        return Task.FromResult(item);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        return Task.FromResult(_productItems.TryRemove(id, out _));
    }

    public Task<List<ProductItem>> GetAsync()
    {
        return Task.FromResult(_productItems.Values.ToList());
    }

    public Task<ProductItem> GetAsync(Guid id)
    {
        _productItems.TryGetValue(id, out var item);
        return Task.FromResult(item);
    }

    public Task<ProductItem> UpdateAsync(ProductItem item)
    {
        _productItems[item.Id] = item;
        return Task.FromResult(item);
    }
}
