using System;
using Ledger.Domain.BusinessRules;
using Ledger.Domain.Models.Enums;

namespace Ledger.Domain.Models;

public class Transaction 
{
    public Guid TransactionId { get; set; }
    public double Amount { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Timestamp { get; set; }
    public string Description { get; set; } = null!;


    private Transaction(Guid transactionId, double amount, TransactionType type, DateTime timestamp, string description)
    {
        TransactionId = transactionId;
        Amount = amount;
        Type = type;
        Timestamp = timestamp;
        Description = description;
    }

    public static Transaction CreateDeposit(double amount, string description) 
    {
        if(amount <= 0)
            throw new BuisnessRuleException("Amount must be greater than zero");

        if(amount >= 1.000000000000)
            throw new BuisnessRuleException("Amount must be less than 1 trillion");

        return new Transaction(Guid.NewGuid(), amount, TransactionType.Deposit, DateTime.UtcNow, description);

    }
        
    public static Transaction CreateWithdrawal(double amount, double  currentBalanceAmount, string description) 
    {
        if(amount <= 0)
            throw new BuisnessRuleException("Amount must be greater than zero");

        if(amount > currentBalanceAmount)
            throw new BuisnessRuleException("Amount must be less than available balance");

        return new Transaction(Guid.NewGuid(), amount, TransactionType.Withdrawal, DateTime.UtcNow, description);
    }
}
