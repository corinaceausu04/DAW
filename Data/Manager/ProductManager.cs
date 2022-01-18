using DAW.Data.Abstractions;
using DAW.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAW.Data.Manager
{
    public class ProductManager : IProductManager
    {
        private readonly DAWContext _context;
        public ProductManager(DAWContext context)
        {
            _context = context;
        }
        public async Task<Product> CreateProductAsync(Product product)
        {
            var res = await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return res.Entity;
        }

        public async Task<bool> DeleteProduct(Product productToDelete)
        {
            try
            {
                _context.Products.Remove(productToDelete);
                await _context.SaveChangesAsync();

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int productId)
        {
            try
            {
                return _context.Products.FirstOrDefault(p => p.Id == productId);
            }
            catch(Exception)
            {
                return null;
            }
        }

        public async Task<Product> UpdateParoductAsync(Product productToUpdate)
        {
            var prevProduct = _context.Products.FirstOrDefault(p => p.Id == productToUpdate.Id);

            if (prevProduct == null)
                return null;

            prevProduct.Title = productToUpdate.Title;
            prevProduct.Author = productToUpdate.Author;
            prevProduct.Description = productToUpdate.Description;
            prevProduct.Category = productToUpdate.Category;
            prevProduct.Image = productToUpdate.Image;
            prevProduct.Price = productToUpdate.Price;

            try
            {
                await _context.SaveChangesAsync();
                return prevProduct;
            }
            catch
            {
                return null;
            }
        }
    }
}
