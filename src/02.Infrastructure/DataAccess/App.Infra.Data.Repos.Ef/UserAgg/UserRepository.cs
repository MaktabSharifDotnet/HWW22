using App.Domain.Core.Contract.UserAgg.Repository;
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
        public async Task<User?> GetByUsername(string username, CancellationToken cancellationToken)
        {
          return await _context.Users.FirstOrDefaultAsync(u => u.Username == username , cancellationToken);
        }
    }
}
