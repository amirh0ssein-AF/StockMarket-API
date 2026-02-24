namespace StockMarket.Interfaces;

public interface IStockRepository
{
    public Task<List<Stock>> GetAllAsync();
}