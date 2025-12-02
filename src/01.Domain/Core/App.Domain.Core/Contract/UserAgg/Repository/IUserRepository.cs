using App.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.UserAgg.Repository
{
    public interface IUserRepository
    {
        public Task<User?> GetByUsername(string username , CancellationToken cancellationToken);
    }
}
