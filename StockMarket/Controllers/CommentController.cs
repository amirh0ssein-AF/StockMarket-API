using System.Security.AccessControl;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Dtos.Comment;
using StockMarket.Interfaces;
using StockMarket.Mappers;
namespace StockMarket.Controllers;

[Route("/api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IStockRepository _stockRepository;
    public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
    {
        _commentRepository = commentRepository;
        _stockRepository = stockRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var commnets = await _commentRepository.GetAllAsync();

        var commenDto = commnets.Select(x => x.ToCommentDto());
        return Ok(commenDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var commnet = await _commentRepository.GetByIdAsync(id);
        if (commnet == null)
            return NotFound();
        
        return Ok(commnet.ToCommentDto());
    }

    [HttpPost("{stockId}")]
    public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto commentDto)
    {
        if (!await _stockRepository.StockExist(stockId))
        {
            return BadRequest("Stock does not exist");
        }

        var commentModel = commentDto.ToCommentFromCreate(stockId);
        await _commentRepository.CreateAsync(commentModel);
        return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
    {
        var comment = await _commentRepository.UpdateAsync(id, updateDto.ToCommentFromUpdate());
        if (comment  == null)
        {
            return NotFound("Comment not found");
        }
        
        return Ok(comment.ToCommentDto());
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var commentModel = await _commentRepository.DeleteAsync(id);
        if (commentModel == null)
        {
            return NotFound("comment does not exist");
        }
        
        return  Ok(commentModel);
    }
}