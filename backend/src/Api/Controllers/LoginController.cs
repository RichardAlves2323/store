using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Domain.Interfaces.HashPassword;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{

    private readonly string _key = "minha-chave-secreta-super-segura-123!";
    private readonly IUserService _userService;

    private readonly IHashPassword _hashPassword;

    public LoginController(IUserService userService, IHashPassword hashPassword)
    {
        _userService = userService;
        _hashPassword = hashPassword;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest login)
    {
        var user = await _userService.GetByEmailAsync(login.Email);
        if (user == null || !_hashPassword.Verify(login.Password, user.Password))
        {
            return Unauthorized(new { message = "Usuário ou senha inválidos" });
        }

        var token = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var createdToken = token.CreateToken(tokenDescriptor);
        var jwt = token.WriteToken(createdToken);

        return Ok(new { token = jwt });
    }
}


public record LoginRequest(string Email, string Password);