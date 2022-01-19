using DAW.Data.Abstractions;
using DAW.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAW.Data.Manager
{
    public class OrderManager : IOrderManager
    {
        private readonly DAWContext _context;

        public OrderManager(DAWContext context)
        {
            _context = context;
        }
        public async Task<Order> CreateOrder(Order orderToAdd, ICollection<OrderItem> orderProducts)
        {
            var res = await _context.Order.AddAsync(orderToAdd);
            await _context.SaveChangesAsync();

            foreach(var prod in orderProducts)
            {
                await _context.OrderItems.AddAsync(
                    new OrderItem { OrderId = res.Entity.Id, ProductId = prod.Id });
                await _context.SaveChangesAsync();
            }
            return res.Entity;
        }

        public async Task<bool> DeteleOrder(Order order)
        {
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return true;
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Order.FirstOrDefault(o => o.Id == orderId);
        }

        public List<Product> GetOrderProducts(int orderId)
        {
            List<Product> products = new();
            var getProducts = _context.OrderItems.Where(o => o.OrderId == orderId);

            foreach (var prod in getProducts)
                products.Add(_context.Products.FirstOrDefault(p => p.Id == prod.ProductId));
            return products;
        }
    }
}
