using System.Collections.Generic;
using System.Threading.Tasks;
using Ledger.API.Features.GetCurrentBalance.ApiContract;
using Ledger.Application.Features.Deposit;
using Ledger.Application.Features.Deposit.ApiContract;
using Ledger.Application.Features.GetCurrentBalance;
using Ledger.Application.Features.GetTransactions;
using Ledger.Application.Features.GetTransactions.ApiContracts;
using Ledger.Application.Features.SendTransaction.ApiContracts;
using Ledger.Application.Features.Withdraw;
using Ledger.Application.Features.Withdraw.ApiContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ledger.API.Controllers
{
    [Route("api/ledger")]
    [ApiController]
    public class LedgerController(
        GetBalanceQueryHandler getBalanceQueryHandler, 
        DepositQueryHandler depositQueryHandler,
        WithdrawQueryHandler withdrawQueryHandler,
        TransactionsQueryHandler transactionsQueryHandler) : ControllerBase
    {
        /// <summary>
        /// Get the current balance of the ledger
        /// </summary>
        /// <returns>The current balance of the ledger</returns>
        [HttpGet("balance")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BalanceResponse))]
        public async Task<IActionResult> GetBalance()
        {
            var balanceResponse = await getBalanceQueryHandler.HandleAsync();

            return Ok(balanceResponse);
        }

        /// <summary>
        /// Get the history of transactions, by time if needed
        /// </summary>
        /// <param name="request">The request containing the filter parameters</param>
        /// <returns>The history of transactions</returns>
        [HttpGet("history")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<TransactionResponse>))]
        public async Task<IActionResult> GetTransactionsHistory(
            [FromQuery] TransactionsRequest request
        )
        {
            var transactionResponses = await transactionsQueryHandler.HandleAsync(request); 

            return Ok(transactionResponses);
        }

        /// <summary>
        /// Deposit a money into ledger balance
        /// </summary>
        /// <param name="request">The request containing the deposit amount</param>
        /// <returns>The deposit transaction</returns>
        [HttpPost("deposit")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DepositResponse))]
        public async Task<IActionResult> DepositTransaction(
            [FromBody] DepositRequest request)
        {
            var depositResponse = await depositQueryHandler.HandleAsync(request);

            return CreatedAtAction(nameof(DepositTransaction), new { id = depositResponse.Id }, depositResponse);
        }

        /// <summary>
        /// Withdraw a money from ledger balance
        /// </summary>
        /// <param name="request">The request containing the withdraw amount</param>
        /// <returns>The withdraw transaction</returns>
        [HttpPost("withdraw")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WithdrawResponse))]
        public async Task<IActionResult> WithdrawTransaction(
            [FromBody] WithdrawRequest request)
        {
            var withdrawResponse = await withdrawQueryHandler.HandleAsync(request);

            return CreatedAtAction(nameof(WithdrawTransaction), new { id = withdrawResponse.Id }, withdrawResponse);
        }

        // account object balance, transactions history
        // transaction between two accounts
        // send money, receive money

        [HttpPost("send")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SendResponse))]
        public async Task<IActionResult> SendTransaction(
            [FromBody] SendRequest request)
        {
            var sendResponse = await transactionsQueryHandler.HandleSendAsync(request);

            return CreatedAtAction(nameof(SendTransaction), new { id = sendResponse.Id }, sendResponse);
        }

    }
}
