using App.Domain.Core._common;
using App.Domain.Core.Dtos.UserAgg;
using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
