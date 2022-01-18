using DAW.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAW.Data.Abstractions
{
    public interface IProductManager
    {
        Task<Product> CreateProductAsync(Product product);
        public Product GetProductById(int productId);
        Task<Product> UpdateParoductAsync(Product productToUpdate);
        Task<bool> DeleteProduct(Product productToDelete);
        List<Product> GetAllProducts();
    }
}
