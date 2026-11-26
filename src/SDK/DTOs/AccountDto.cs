using SDK.Common;

namespace SDK.DTOs;

public class AccountDto
{
    public string? AccountNumber { get; set; }
    public string? AccountName { get; set; }
    public Enums.AccountTypeDto AccountType { get; set; }
    public AddressDto? Address { get; set; }
}
