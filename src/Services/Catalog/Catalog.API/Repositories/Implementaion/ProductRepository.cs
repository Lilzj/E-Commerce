using Catalog.API.Data;
using Catalog.API.Entities;
using Catalog.API.Repositories.Contract;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories.Implementaion
{
    public class ProductRepository : IProductRepository
    {
        private ICatalogContext _ctx;

        public ProductRepository(ICatalogContext context)
        {
            _ctx = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _ctx
                            .Products
                            .Find(p => true)
                            .ToListAsync();
        }
        public async Task<Product> GetProduct(string id)
        {
            return await _ctx
                           .Products
                           .Find(p => p.Id == id)
                           .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);

            return await _ctx
                            .Products
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

            return await _ctx
                            .Products
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await _ctx.Products.InsertOneAsync(product);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _ctx
                                        .Products
                                        .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _ctx
                                                .Products
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}
