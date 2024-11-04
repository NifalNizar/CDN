using CDN.Application.IServices;
using CDN.Core.Constants;
using CDN.Core.Entities;
using CDN.Requests;
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

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Add(UserSaveRequest model)
    {
        var item = new User();
        item.Status = Status.Active;
        item.CreatedBy = item.Id;
        item.Username = model.Username;
        item.EmailAddress = model.EmailAddress;
        item.MobileNo = model.MobileNo;
        item.Skills = model.Skills;
        item.Hobby = model.Hobby;

        await userService.AddAsync(item);
        return CreatedAtAction("GetAll", new { id = item.Id }, item);
    }
}
