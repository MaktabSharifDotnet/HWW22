using App.Domain.Core.Contract.UserAgg.Repository;
using App.Domain.Core.Contract.UserAgg.Service;
using App.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.UserAgg
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task<int> Login(string username, string password, CancellationToken cancellationToken)
        {
            User? user=await userRepository.GetByUsername(username, cancellationToken);
            if (user==null || user.Password!=password)
            {
                throw new Exception("نام کاربری یا رمز عبور اشتباه است.");
            }

            LocalStorage.LoginUser=user;
            return  user.Id;


        }
    }
}
