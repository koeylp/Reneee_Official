using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reneee.Application.Constants;
using Reneee.Application.DTOs.Transaction;
using Reneee.Application.Services;

namespace Reneee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController(ITransactionService transactionService) : ControllerBase
    {
        private readonly ITransactionService _transactionService = transactionService;

        [HttpPost]
        [Authorize(Roles = RoleConstants.ROLE_CUSTOMER)]
        public async Task<ActionResult<TransactionDto>> SaveTransaction([FromBody] CreateTransactionDto transactionRequest)
        {
            return Ok(await _transactionService.SaveTransaction(transactionRequest));
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.ROLE_CUSTOMER + "," + RoleConstants.ROLE_STAFF)]
        public async Task<ActionResult<IReadOnlyList<TransactionDto>>> GetTransactions()
        {
            return Ok(await _transactionService.GetTransactions());
        }

        [HttpPut("{orderId}/{status}")]
        [Authorize(Roles = RoleConstants.ROLE_CUSTOMER)]
        public async Task<ActionResult<TransactionDto>> UpdateStatus([FromRoute] int orderId, int status)
        {
            return Ok(await _transactionService.UpdateStatus(orderId, status));
        }

        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<TransactionDto>> GetTransactionByOrderId([FromRoute] int orderId)
        {
            return Ok(await _transactionService.GetTransactionByOrderId(orderId));
        }
    }
}
