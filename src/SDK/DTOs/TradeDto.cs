using SDK.Enums;

namespace SDK.DTOs;

public class TradeDto
{
    public string? TradeId { get; set; }
    public string? CustomerAccount { get; set; }
    public string? CustomerName { get; set; }
    public string? CounterPartyAccount { get; set; }
    public string? CounterPartyName { get; set; }
    public string? BrokerAccount { get; set; }
    public string? BrokerName { get; set; }
    public BuySellDto BuySellSide { get; set; }
    public string? TradeDate { get; set; }
    public string? TradeNumber { get; set; }
    public string? SettlementDate { get; set; }
    public DateTime? ExecutionDateTime { get; set; }
}
