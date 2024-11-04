using AutoMapper;
using CDN.Application.IServices;
using CDN.Core.Constants;
using CDN.Core.Entities;
using CDN.Dtos;
using CDN.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CDN.Controllers;
public class UsersController : GenericController<UsersController>
{
    private readonly IMapper mapper;
    private readonly IUserService userService;
    public UsersController(IMapper mapper, IUserService userService)
    {
        this.mapper = mapper;
        this.userService = userService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IReadOnlyList<UserDto>> GetAll()
    {
        var items = await userService.GetAllAsync();
        return mapper.Map<IReadOnlyList<UserDto>>(items);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Add(UserSaveRequest model)
    {
        var item = new User();
        item.Username = model.Username;
        item.EmailAddress = model.EmailAddress;
        item.MobileNo = model.MobileNo;
        item.Skills = model.Skills;
        item.Hobby = model.Hobby;

        item.Status = Status.Active;
        item.CreatedBy = item.Id;

        await userService.AddAsync(item);
        return CreatedAtAction("GetAll", new { id = item.Id }, mapper.Map<UserDto>(item));
    }

    [AllowAnonymous]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UserSaveRequest model)
    {
        var item = await userService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("User Not Found");
        }
        else if (item.Status == Status.Deleted)
        {
            return BadRequest("User Is Deleted");
        }

        item.Username = model.Username;
        item.EmailAddress = model.EmailAddress;
        item.MobileNo = model.MobileNo;
        item.Skills = model.Skills;
        item.Hobby = model.Hobby;
        item.ModifiedBy = 1;
        item.ModifiedOn = DateTime.UtcNow;

        await userService.UpdateAsync(item);
        return NoContent();
    }

    [AllowAnonymous]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await userService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("User Not Found");
        }
        else if (item.Status == Status.Deleted)
        {
            return BadRequest("User Is Deleted");
        }

        item.Status = Status.Deleted;
        item.ModifiedBy = 1;
        item.ModifiedOn = DateTime.UtcNow;

        await userService.UpdateAsync(item);
        return NoContent();
    }
}
