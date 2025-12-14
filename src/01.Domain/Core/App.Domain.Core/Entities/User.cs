using App.Domain.Core.Enums.UserAgg;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Entities
{
    public class User
    {

        public int IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }

        public RoleEnum RoleEnum { get; set; }
        public List<Cart> Carts { get; set; } = [];
        public bool IsDeleted { get; set; }
     
    }
}
