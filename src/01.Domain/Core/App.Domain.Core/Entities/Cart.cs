using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsFinished { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
        public List<CartProduct> CartProducts { get; set; } = [];
    }
}
