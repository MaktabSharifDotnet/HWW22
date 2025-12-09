using App.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.OrderAgg
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }

        public decimal TotalAmount { get; set; }


        public DateTime CreatedAt { get; set; }
        public bool IsPaid { get; set; }

    }
}
