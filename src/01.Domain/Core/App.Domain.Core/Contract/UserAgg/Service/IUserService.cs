using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.UserAgg.Service
{
    public interface IUserService
    {
        public Task<int> Login(string username, string password , CancellationToken cancellationToken);
    }
}
