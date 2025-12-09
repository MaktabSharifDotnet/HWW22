using App.Domain.Core.Contract.UserAgg.Repository;
using App.Domain.Core.Dtos.UserAgg;
using App.Domain.Core.Entities;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.UserAgg
{
    public class UserRepository(AppDbContext _context) : IUserRepository
    {
        public async Task<List<UserDto>> GetAllById(CancellationToken cancellationToken)
        {
            return await _context.Users.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username
            }).ToListAsync(cancellationToken);
        }

        public async Task<User?> GetById(int userId, CancellationToken cancellationToken)
        {
            return await _context.Users
                  .Include(u => u.Carts)
                  .ThenInclude(c => c.CartProducts)
                  .ThenInclude(cp => cp.Product)
                  .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User?> GetByUsername(string username, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
        }

        public async Task<int> Save(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
