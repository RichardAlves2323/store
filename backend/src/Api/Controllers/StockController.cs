using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    private readonly IStockService _stockService;

    public StockController(IStockService stockService)
    {
        _stockService = stockService;
    }

    [HttpGet("product/{productId}")]
    public async Task<IActionResult> GetByProductId(int productId)
    {
        var stock = await _stockService.GetByProductIdAsync(productId);
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Stock stock)
    {
        var createdStock = await _stockService.AddAsync(stock);
        return CreatedAtAction(nameof(GetByProductId), new { productId = createdStock.ProductId }, createdStock);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Stock stock)
    {
        if (id != stock.Id)
        {
            return BadRequest();
        }

        var updatedStock = await _stockService.UpdateAsync(stock);
        return Ok(updatedStock);
    }
}