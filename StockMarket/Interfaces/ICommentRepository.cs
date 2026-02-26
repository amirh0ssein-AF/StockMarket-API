namespace StockMarket.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync();
}