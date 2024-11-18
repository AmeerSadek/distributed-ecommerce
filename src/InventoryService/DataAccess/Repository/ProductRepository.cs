using InventoryService.Domain.Models;
using InventoryService.Domain.Repositories;
using System.Collections.Concurrent;

namespace InventoryService.DataAccess.Repository;

internal class ProductRepository : IProductRepository
{
    private static readonly ConcurrentDictionary<Guid, Product> _inventory;

    static ProductRepository()
    {
        var productsIds = new Guid[]
        {
            new("03f73288-baec-4dd4-9a90-cc71a19bdffd"),
            new("dd10ecd9-541a-446e-a093-6d52d531fb8a"),
            new("3acba57e-d1d1-4a00-a113-2ef26084c57b"),
            new("b7e5d8d3-4f4e-46ea-a04d-9d2be6074f0e"),
            new("48b058ed-0168-42c7-a4b0-2d76917d5f13"),
            new("a8d23a07-2d74-461e-b22c-fab4167f7560"),
            new("7de273f4-38e2-470b-a3c5-db4983f8856e"),
            new("5d7b9f3c-9b6f-4e00-87e3-5743928bb020"),
            new("bb112533-b187-4bb9-9e36-8fce5d2fa5b6"),
            new("23d05630-838c-42dd-bbbb-981279b8be10")
        };

        _inventory = new ConcurrentDictionary<Guid, Product>();

        foreach (var productId in productsIds)
        {
            _inventory.TryAdd(productId, new Product
            {
                ProductId = productId,
                Stock = 10
            });
        }
    }

    public ValueTask<bool> ReduceStockAsync(Guid productId, int quantity)
    {
        if (_inventory.TryGetValue(productId, out var product))
        {
            lock (product) // Synchronize access to this specific product
            {
                if (product.Stock >= quantity)
                {
                    product.Stock -= quantity;

                    return new ValueTask<bool>(true);
                }
            }
        }

        return new ValueTask<bool>(false);
    }
}
