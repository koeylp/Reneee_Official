using AutoMapper;
using Microsoft.Extensions.Logging;
using Reneee.Application.Contracts.Persistence;
using Reneee.Application.DTOs.Transaction;
using Reneee.Application.Exceptions;
using Reneee.Domain.Entities;

namespace Reneee.Application.Services.Impl
{
    public class TransactionServiceImpl(ITransactionRepository transactionRepository,
                                        IOrderRepository orderRepository,
                                        IUserService userService,
                                        IUserRepository userRepository,
                                        IUnitOfWork unitOfWork,
                                        IMapper mapper,
                                        ILogger<TransactionServiceImpl> logger) : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository = transactionRepository;
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IUserService _userService = userService;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<TransactionServiceImpl> _logger = logger;

        public async Task<TransactionDto> GetTransactionByOrderId(int orderId)
        {
            var orderEntity = await _orderRepository.Get(orderId) ?? throw new NotFoundException("Order not found");
            var transactionEntity = await _transactionRepository.GetTransactionByOrder(orderEntity)
                ?? throw new NotFoundException($"Transaction with order ID {orderId} was not found");
            return _mapper.Map<TransactionDto>(transactionEntity);
        }

        public async Task<IReadOnlyList<TransactionDto>> GetTransactions()
        {
            var userEntity = await _userService.GetUserFromEmailClaims();
            var transactions = _mapper.Map<List<TransactionDto>>(await _transactionRepository.GetTransactionByUser(userEntity));
            var sortedTransactions = transactions.OrderByDescending(t => t.TransactionDate).ToList();
            return sortedTransactions.AsReadOnly();
        }

        public async Task<TransactionDto> SaveTransaction(CreateTransactionDto transactionRequest)
        {
            _logger.LogInformation("Enter saving transaction");
            var orderEntity = await _orderRepository.Get(transactionRequest.OrderId)
                                ?? throw new NotFoundException("Order not found");
            //var userEntity = await _userService.GetUserFromEmailClaims();
            var userEntity = _mapper.Map<User>(await _userRepository.Get(1));
            var transactionEntity = new Transaction
            {
                ClientSecret = transactionRequest.ClientSecret,
                Total = transactionRequest.Total,
                Status = transactionRequest.status,
                Order = orderEntity,
                User = userEntity,
                TransactionDate = DateTime.Now,
            };
            await _transactionRepository.Add(transactionEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TransactionDto>(transactionEntity);
        }

        public async Task<TransactionDto> UpdateStatus(int orderId, int status)
        {
            var orderEntity = await _orderRepository.Get(orderId)
                    ?? throw new NotFoundException("Order not found");
            var transactionEntity = await _transactionRepository.GetTransactionByOrder(orderEntity)
                ?? throw new NotFoundException("Transaction not found with orderId " + orderId);
            transactionEntity.Status = status;
            transactionEntity.TransactionDate = DateTime.Now;
            await _transactionRepository.Update(transactionEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TransactionDto>(transactionEntity);
        }
    }
}
