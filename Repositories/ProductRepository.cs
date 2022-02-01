using DAW.Data;
using DAW.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAW.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DAWContext context) : base(context)
        {

        }

        public void GroupBy()
        {
            var groupedProducts = from p in _table
                                  group p by p.Category;

            foreach (var prodGroupByCated in groupedProducts)
            {
                Console.WriteLine("Product category: " + prodGroupByCated.Key);

                foreach (Product p in prodGroupByCated)
                {
                    Console.WriteLine("Product title: " + p.Title);
                }
            }

            // Method syntax
            var groupedProducts2 = _table.GroupBy(s => s.Category);
        }

        public void OrderByPrice()
        {
            var productsOrderedAsc1 = _table.OrderBy(x => x.Price);
            var productsOrderedDesc1 = _table.OrderByDescending(x => x.Price);

            // linq query syntax
            var productsOrderedAsc2 = from p in _table
                                      orderby p.Price
                                      select p;

            var productsOrderedDesc2 = from p in _table
                                       orderby p.Price descending
                                       select p;
        }
    }
}
