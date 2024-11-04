using CDN.Application.IServices;
using CDN.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CDN.Controllers;
public class UsersController : GenericController<UsersController>
{
    private readonly IUserService userService;
    public UsersController(IUserService userService)
    {
        this.userService = userService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IReadOnlyList<User>> GetAll()
    {
        var items = await userService.GetAllAsync();
        return items;
    }
}
