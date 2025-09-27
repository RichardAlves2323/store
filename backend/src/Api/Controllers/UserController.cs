using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var user = await _userService.GetByEmailAsync(email);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }  

    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        try
        {
            var createdUser = await _userService.AddAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}