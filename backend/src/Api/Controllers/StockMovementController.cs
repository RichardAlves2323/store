using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class StockMovementController : ControllerBase
{
    private readonly IStockMovementService _stockMovementService;
    
    public StockMovementController(IStockMovementService stockMovementService)
    {
        _stockMovementService = stockMovementService;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById(int id)
    {
        var stockMovement = await _stockMovementService.GetByIdAsync(id);
        if (stockMovement == null)
        {
            return NotFound();
        }
        return Ok(stockMovement);
    }

    [HttpGet("product/{productId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetByProductId(int productId)
    {
        var stockMovements = await _stockMovementService.GetByProductIdAsync(productId);
        return Ok(stockMovements);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(StockMovement stockMovement)
    {
        try
        {
            var createdStockMovement = await _stockMovementService.AddAsync(stockMovement);
            return CreatedAtAction(nameof(GetById), new { id = createdStockMovement.Id }, createdStockMovement);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}

