using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HelloController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var product = new Product(1, "Sample Product", "This is a sample product.", 9.99m);
        return Ok(product); 
    }
}
