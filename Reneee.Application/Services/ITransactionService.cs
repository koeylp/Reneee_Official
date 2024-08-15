using Reneee.Application.DTOs.Transaction;

namespace Reneee.Application.Services
{
    public interface ITransactionService
    {
        Task<TransactionDto> GetTransactionByOrderId(int orderId);
        Task<IReadOnlyList<TransactionDto>> GetTransactions();
        Task<TransactionDto> SaveTransaction(CreateTransactionDto transactionRequest);
        Task<TransactionDto> UpdateStatus(int orderId, int status);
    }
}
