using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context
               .Products
               .Find(p => true)
               .ToListAsync();
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _context
                .Products
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        // public Task CreateProduct(Product product)
        // {
        //     throw new NotImplementedException();
        // }

        // public Task<bool> DeleteProduct(string id)
        // {
        //     throw new NotImplementedException();
        // }



        // public Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        // {
        //     throw new NotImplementedException();
        // }

        // public Task<IEnumerable<Product>> GetProductByName(string name)
        // {
        //     throw new NotImplementedException();
        // }



        // public Task<bool> UpdateProduct(Product product)
        // {
        //     throw new NotImplementedException();
        // }
    }
}