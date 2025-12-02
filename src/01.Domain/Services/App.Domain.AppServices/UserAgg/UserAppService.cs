using App.Domain.Core.Contract.UserAgg.AppService;
using App.Domain.Core.Contract.UserAgg.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.UserAgg
{
    public class UserAppService(IUserService userService) : IUserAppService
    {
        public async Task<int> Login(string username, string password, CancellationToken cancellationToken)
        {
          return await userService.Login(username, password, cancellationToken);
        }
    }
}
