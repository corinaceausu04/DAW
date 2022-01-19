using DAW.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAW.Data.Abstractions
{
    public interface IOrderManager
    {
        Task<Order> CreateOrder(Order orderToAdd, ICollection<OrderItem> orderProducts);
        Order GetOrderById(int orderId);
        List<Product> GetOrderProducts(int orderId);
        Task<bool> DeteleOrder(Order order);
    }
}
