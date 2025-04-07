using System;
using Ledger.Domain.Models.Enums;

namespace Ledger.Domain.Models;

public class Account
{
    public Account(Guid accountId, double balance)
    {
        AccountId = accountId;
        Balance = balance;

        Console.WriteLine($"Account created with ID: {accountId} and balance: {balance}");
    }
    public Guid AccountId { get; set; } 

    public double Balance { get; set; } 


    public static Transaction Send(Account sender, Account receiver, double amount, string description)
    {
        //validation 
        sender.Balance -= amount;
        receiver.Balance += amount;

        return Transaction.Create(amount, TransactionType.Send, description);
    }
}
