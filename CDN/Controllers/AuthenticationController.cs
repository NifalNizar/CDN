using CDN.Application.IServices;
using CDN.Helpers;
using CDN.Requests;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace CDN.Controllers;
public class AuthenticationController : GenericController<AuthenticationController>
{
    private readonly JwtOptions jwtOptions;
    private readonly IUserService userService;
    private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()!.DeclaringType);
    public AuthenticationController(IOptionsSnapshot<JwtOptions> jwtOptions, IUserService userService)
    {
        this.jwtOptions = jwtOptions.Value;
        this.userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        var user = await userService.GetByUsername(model.Username);
        if (user == null) return BadRequest("User Not Found");

        if (model.Password != "1234")
        {
            log.Error("Password Incorrect for " + model.Username);
            return BadRequest("Password Incorrect");
        } 

        List<Claim> claims =
        [
            new(ClaimTypes.Name, model.Username),
            new(ClaimTypes.Email, user.EmailAddress),
            new(type: ClaimTypes.GivenName, value: "Nifal"),
            new(type: ClaimTypes.Surname, value: "Nizar"),
        ];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken
        (
            jwtOptions.Issuer,
            jwtOptions.Audience,
            claims,
            expires: DateTime.Now.AddHours(jwtOptions.Duration),
            signingCredentials: credentials
        );

        var securityToken = new JwtSecurityTokenHandler().WriteToken(token);
        return Ok(securityToken);
    }
}