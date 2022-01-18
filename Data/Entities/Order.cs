using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAW.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int Id_userId { get; set; }
        public DateTime OrderDate { get; set; }

        public ICollection<OrderItem> Items {get; set;}
    }
}
