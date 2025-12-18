using App.Domain.Core._common;
using App.Domain.Core.Contract.CartAgg.Service;
using App.Domain.Core.Contract.CategoryAgg.Service;
using App.Domain.Core.Contract.UserAgg.AppService;
using App.Domain.Core.Contract.UserAgg.Service;
using App.Domain.Core.Dtos.UserAgg;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.UserAgg
{
    public class UserAppService(IUserService _userService, SignInManager<IdentityUser<int>> _signInManager
        , UserManager<IdentityUser<int>> _userManager , ILogger<UserAppService> _logger) : IUserAppService
    {
        public async Task<IdentityResult> AddToRole(IdentityUser<int> identityUser, string role)
        {
           return  await _userManager.AddToRoleAsync(identityUser, "Customer");
        }

        public async Task<int> ChangeDatabaseUsername(int identityUserId, string newUsername, CancellationToken cancellationToken)
        {
            return await _userService.ChangeDatabaseUsername(identityUserId, newUsername, cancellationToken);
        }

        public async Task<IdentityResult> Create(IdentityUser<int> identityUser, string pass)
        {
           return await _userManager.CreateAsync(identityUser, pass);
        }

        public async Task<IdentityResult> Delete(IdentityUser<int> identityUser)
        {
          return   await _userManager.DeleteAsync(identityUser);
        }

        public async Task<IdentityUser<int>?> FindByName(string username )
        {
           return await _userManager.FindByNameAsync(username);
        }

        public async Task<List<UserDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _userService.GetAll(cancellationToken);
        }

        public async Task<Result<UserDetailDto>> GetDetailById(int userId, CancellationToken cancellationToken)
        {
            UserDetailDto? userDetailDto = await _userService.GetDetailById(userId, cancellationToken);
            if (userDetailDto == null)
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

        public async Task<SignInResult> Login(string username, string password, bool rememberMe, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(
                 username,
                 password,
                 isPersistent: rememberMe,
                 lockoutOnFailure: false
             );

            return result;
        }

        public void LogOut()
        {
            _userService.LogOut();
        }

        public async Task<SignInResult> PasswordSignIn(string username, string password, bool isPersistent, bool lockoutOnFailure)
        {
           return await _signInManager.PasswordSignInAsync(username , password, isPersistent, lockoutOnFailure);
        }

        public async Task<int> RegisterUser(string username, int identityUserId, CancellationToken cancellationToken)
        {
            return await _userService.RegisterUser(username, identityUserId, cancellationToken);
        }

        public async Task SignIn(IdentityUser<int> identityUser)
        {
            await _signInManager.SignInAsync(identityUser, false);
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
