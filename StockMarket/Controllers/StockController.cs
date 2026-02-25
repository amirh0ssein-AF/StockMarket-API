using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockMarket.Data;
using StockMarket.Dtos.Stock;
using StockMarket.Interfaces;
using StockMarket.Mappers;

namespace StockMarket.Controllers;

[Route("/api/stock")]
[ApiController]
public class StockController : ControllerBase   
{
    private readonly ApplicationDBContext  _context;
    private readonly IStockRepository _stockRepository;
    public StockController(ApplicationDBContext context,  IStockRepository stockRepository)
    {
        _context = context;
        _stockRepository = stockRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await _stockRepository.GetAllAsync();
        var stockDto = stocks.Select(s => s.ToStockDto());
        
        return Ok(stockDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute]int id)
    {
        var stock = await _stockRepository.GetByIdAsync(id);
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
    {
        var stockModel = stockDto.ToStockFromCreateDto();
        await _stockRepository.CreateAsync(stockModel);
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateRequestDto)
    {
        var stockModel = await _stockRepository.UpdateAsync(id , updateRequestDto);

        if (stockModel == null)
        {
            return NotFound();
        }
        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

        if (stockModel == null)
            return NotFound();
        
        _context.Stocks.Remove(stockModel);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
    
}