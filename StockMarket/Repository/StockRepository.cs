using Microsoft.EntityFrameworkCore;
using StockMarket.Data;
using StockMarket.Interfaces;

namespace StockMarket.Repository;

public class StockRepository : IStockRepository
{
    private readonly ApplicationDBContext  _context;

    public StockRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public Task<List<Stock>> GetAllAsync()
    {
        return _context.Stocks.ToListAsync();
    }
}