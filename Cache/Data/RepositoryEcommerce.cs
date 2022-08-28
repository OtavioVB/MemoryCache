using Bogus;
using Cache.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace Cache.Data;

public class RepositoryEcommerce
{
    private readonly IList<Product> _products;
    private readonly Faker _faker;
    private readonly IMemoryCache _memoryCache;

    public RepositoryEcommerce(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        _products = new List<Product>();
        _faker = new Faker();

        for (int i = 0; i < 1000; i++)
        {
            _products.Add(AddRandomProduct());
        }
    }

    private Product AddRandomProduct()
    {
        return new Product(
                Guid.NewGuid(),
                _faker.Commerce.ProductName(),
                _faker.Commerce.Price()
            );  
    }

    public IEnumerable<Product> GetProducts()
    {
        var listProducts = _memoryCache.GetOrCreate("ProductsGetAll", entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromSeconds(15);
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
            entry.SetPriority(CacheItemPriority.High);

            System.Threading.Thread.Sleep(1000);
            return _products;
        });
        return listProducts;
    }
}
