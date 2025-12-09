using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.OrderItemAgg
{
    public class OrderItemDto
    {

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
