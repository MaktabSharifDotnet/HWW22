using App.Domain.Core._common;
using App.Domain.Core.Contract.CartAgg.Service;
using App.Domain.Core.Contract.CategoryAgg.Service;
using App.Domain.Core.Contract.UserAgg.AppService;
using App.Domain.Core.Contract.UserAgg.Service;
using App.Domain.Core.Dtos.UserAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.UserAgg
{
    public class UserAppService(IUserService _userService ) : IUserAppService
    {
        public async Task<List<UserDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _userService.GetAll(cancellationToken);
        }

        public async Task<Result<UserDetailDto>> GetDetailById(int userId, CancellationToken cancellationToken)
        {
            UserDetailDto? userDetailDto= await _userService.GetDetailById(userId, cancellationToken);
            if (userDetailDto==null)
            {
                return Result<UserDetailDto>.Failure("همیچن کابری موجود نیست.");
            }

            return Result<UserDetailDto>.Success(userDetailDto);
        }

        public async Task<int> GetUserIdByIdentityId(int identityUserId, CancellationToken cancellationToken)
        {
           return await _userService.GetUserIdByIdentityId(identityUserId, cancellationToken);
        }

        public async Task<int> Login(string username, string password, CancellationToken cancellationToken)
        {
          return await _userService.Login(username, password, cancellationToken);
        }
        public void LogOut()
        {
            _userService.LogOut ();
        }

        public async Task<int> RegisterUser(string username, int identityUserId, CancellationToken cancellationToken)
        {
           return await _userService.RegisterUser(username, identityUserId, cancellationToken);
        }
    }
}
