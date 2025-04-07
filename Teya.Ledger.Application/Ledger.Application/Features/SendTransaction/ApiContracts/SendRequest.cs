namespace Ledger.Application.Features.SendTransaction.ApiContracts;

public record SendRequest(Guid FromAccountId, Guid ToAccountId, double Amount, string Description);