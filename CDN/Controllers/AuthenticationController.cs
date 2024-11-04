using CDN.Application.IServices;
using CDN.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CDN.Controllers;
public class AuthenticationController : GenericController<AuthenticationController>
{
    private readonly IConfiguration configuration;
    private readonly IUserService userService;
    public AuthenticationController(IConfiguration configuration, IUserService userService)
    {
        this.configuration = configuration;
        this.userService = userService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        var user = await userService.GetByUsername(model.Username);
        if (user == null) return BadRequest("User Not Found");

        if (model.Password != "1234") return BadRequest("Password Incorrect");

        List<Claim> claims = new()
        {
            new(ClaimTypes.Name, model.Username),
            new(ClaimTypes.Email, user.EmailAddress),
            new(type: ClaimTypes.GivenName, value: "Nifal"),
            new(type: ClaimTypes.Surname, value: "Nizar"),
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"] ?? string.Empty));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken
        (
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddHours(int.Parse(configuration["Jwt:Duration"]!)),
            signingCredentials: credentials
        );

        var securityToken = new JwtSecurityTokenHandler().WriteToken(token);
        return Ok(securityToken);
    }
}