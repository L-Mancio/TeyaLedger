using System;

namespace Ledger.Domain.Models;

public class Balance
{
    public double Amount { get; set; }
    public DateTime AtTimestamp { get; set; }

    private Balance(double amount, DateTime atTimestamp) => (Amount, AtTimestamp) = (amount, atTimestamp);

    public static Balance Create(double amount, DateTime atTimestamp) => new(amount, atTimestamp);

    public void Deposit(double amount) => (Amount, AtTimestamp) = (Amount + amount, DateTime.UtcNow);

    public void Withdraw(double amount) => (Amount, AtTimestamp) = (Amount - amount, DateTime.UtcNow);
}
