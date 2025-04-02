namespace Ledger.Application.Features.GetTransactions.ApiContracts;

public record TransactionsRequest(DateTime? FromDateTime, DateTime? ToDateTime);
