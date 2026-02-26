using System.Security.AccessControl;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Interfaces;
using StockMarket.Mappers;
namespace StockMarket.Controllers;

[Route("/api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    public CommentController(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var commnets = await _commentRepository.GetAllAsync();

        var commenDto = commnets.Select(x => x.ToCommentDto());
        return Ok(commenDto);
    }
}