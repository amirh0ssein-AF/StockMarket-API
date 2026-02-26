using Microsoft.EntityFrameworkCore;
using StockMarket.Data;
using StockMarket.Interfaces;

namespace StockMarket.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDBContext _context;
    public CommentRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<List<Comment>> GetAllAsync()
    {
        return await _context.Comments.ToListAsync();
    }
}