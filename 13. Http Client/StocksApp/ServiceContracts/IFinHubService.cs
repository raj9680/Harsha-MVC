namespace Dependency_Injection.ServiceContracts
{
    public interface IFinHubService
    {
        Task<Dictionary<string, object>?> GetStockPriceQuote(string? stickSymbol);
    }
}
