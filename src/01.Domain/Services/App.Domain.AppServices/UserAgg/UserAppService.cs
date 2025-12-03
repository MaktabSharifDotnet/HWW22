using App.Domain.Core.Contract.CartAgg.Service;
using App.Domain.Core.Contract.CategoryAgg.Service;
using App.Domain.Core.Contract.UserAgg.AppService;
using App.Domain.Core.Contract.UserAgg.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.UserAgg
{
    public class UserAppService(IUserService _userService ) : IUserAppService
    {

        public async Task<int> Login(string username, string password, CancellationToken cancellationToken)
        {
          return await _userService.Login(username, password, cancellationToken);
        }
        public void LogOut()
        {
            _userService.LogOut ();
        }
    }
}
