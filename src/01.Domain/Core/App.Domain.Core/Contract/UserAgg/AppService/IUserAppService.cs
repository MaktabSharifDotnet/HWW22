using App.Domain.Core._common;
using App.Domain.Core.Dtos.UserAgg;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.UserAgg.AppService
{
    public interface IUserAppService
    {
        public  Task<int> Login(string username, string password, CancellationToken cancellationToken);
        public void LogOut();

        public Task<List<UserDto>> GetAll(CancellationToken cancellationToken);

        public Task<Result<UserDetailDto>> GetDetailById(int userId, CancellationToken cancellationToken);

        public Task<int> GetUserIdByIdentityId(int identityUserId, CancellationToken cancellationToken);
        public Task<int> RegisterUser(string username, int identityUserId, CancellationToken cancellationToken);

        public Task<int> ChangeDatabaseUsername(int identityUserId, string newUsername, CancellationToken cancellationToken);

        //==========================================
        public Task<SignInResult> Login(string username, string password, bool rememberMe, CancellationToken cancellationToken);

        public Task<SignInResult> PasswordSignIn(string username , string password , bool isPersistent , bool lockoutOnFailure );
        public Task<IdentityUser<int>?> FindByName(string username );
        public Task SignOut();
        public Task SignIn(IdentityUser<int> identityUser);
        public Task<IdentityResult> Delete(IdentityUser<int> identityUser);
        public Task<IdentityResult> Create(IdentityUser<int> identityUser , string pass);
        public Task<IdentityResult> AddToRole(IdentityUser<int> identityUser , string role);
        public Task<IdentityUser<int>?> GetUser(ClaimsPrincipal user);

    }
}
