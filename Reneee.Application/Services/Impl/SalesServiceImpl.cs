using AutoMapper;
using Reneee.Application.Contracts.Persistence;
using Reneee.Application.DTOs.Sales;

namespace Reneee.Application.Services.Impl
{
    public class SalesServiceImpl(ISalesRepository salesRepository,
                                  IOrderRepository orderRepository,
                                  IMapper mapper) : ISalesService
    {
        private readonly ISalesRepository _salesRepository = salesRepository;
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<MonthlyOrderCountReportDto> GetMonthlyOrderCountReport()
        {
            var orderReport = new MonthlyOrderCountReportDto
            {
                TotalSales = await _salesRepository.GetTotalSales(),
                TotalOrder = await _orderRepository.GetTotalOrders(),
            };
            return orderReport;
        }
    }
}
