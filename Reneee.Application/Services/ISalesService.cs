

using Reneee.Application.DTOs.Sales;

namespace Reneee.Application.Services
{
    public interface ISalesService
    {
        Task<MonthlyOrderCountReportDto> GetMonthlyOrderCountReport();
    }
}
