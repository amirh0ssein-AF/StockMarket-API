using StockMarket.Dtos.Stock;
using StockMarket.Helpers;

namespace StockMarket.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync(QueryObject query);
    Task<Stock?> GetByIdAsync(int id);
    Task<Stock> CreateAsync(Stock stockModel);
    Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockRequestDto);
    Task<Stock?> DeleteAsync(int id);
    Task<bool> StockExist(int id);
}