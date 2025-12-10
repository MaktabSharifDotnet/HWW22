using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.OrderAgg
{
    public class DashboardDataDto
    {
        public int CountCustomer { get; set; }
        public int CountProduct { get; set; }
        public decimal TotalSales { get; set; }
        public List<DashboardChartDto> DashboardChartDtos { get; set; } = [];


    }
}
