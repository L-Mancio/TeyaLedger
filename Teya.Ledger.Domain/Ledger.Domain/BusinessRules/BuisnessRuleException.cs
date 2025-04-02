using System;

namespace Ledger.Domain.BusinessRules;

public class BuisnessRuleException(string message) : Exception(message);