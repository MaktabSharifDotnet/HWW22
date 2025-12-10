using App.Domain.Core.Dtos.OrderAgg;
using App.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.OrderAgg.AppService
{
    public interface IOrderAppService
    {
        public  Task Create(int userId, int cartId, CancellationToken cancellationToken);
        public Task<int> CheckOut(int userId, int cartId, CancellationToken cancellationToken);

        public Task<List<OrderDto>> GetOrderDtos(CancellationToken cancellationToken);
        public Task<DashboardDataDto> GetDashboardData(CancellationToken cancellationToken);
    }
}
