using Domain.Common.Entities;
using Domain.Common.Enums;

namespace Domain.Entities;

public class Account
{
    public string? AccountNumber { get; set; }
    public string? AccountName { get; set; }
    public AccountType AccountType { get; set; }
    public Address? Address { get; set; }
}
