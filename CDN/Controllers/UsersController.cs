using AutoMapper;
using CDN.Application.IServices;
using CDN.Core.Constants;
using CDN.Core.Entities;
using CDN.Dtos;
using CDN.Requests;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace CDN.Controllers;
public class UsersController(IMapper mapper, IUserService userService) : GenericController<UsersController>
{
    private readonly IMapper mapper = mapper;
    private readonly IUserService userService = userService;

    [HttpGet]
    [HttpGet("page/{page:int}/size/{size:int}")]
    public async Task<IReadOnlyList<UserDto>> GetAll(int page = 1, int size = 100, string searchText = "")
    {
        var items = await userService.GetAll(page, size, searchText);
        return mapper.Map<IReadOnlyList<UserDto>>(items);
    }

    [HttpGet("Usernames")]
    public async Task<IActionResult> GetUsernames()
    {
        var items = await userService.GetUsernames();
        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await userService.GetByIdAsync(id);
        if (item == null)
        {
            return BadRequest("User Not Found");
        }
        return Ok(mapper.Map<UserDto>(item));
    }

    [HttpGet("{id:int}/Audit")]
    public async Task<IActionResult> GetAuditById(int id)
    {
        var items = await userService.GetAuditById(id);
        return Ok(mapper.Map<IReadOnlyList<UserAuditDto>>(items));
    }

    [HttpPost]
    public async Task<IActionResult> Add(UserSaveRequest model)
    {
        string message = await Validate(0, model.MobileNo, model.EmailAddress, model.Username);
        if (!string.IsNullOrEmpty(message))
        {
            return BadRequest(message);
        }

        var item = new User
        {
            Username = model.Username,
            EmailAddress = model.EmailAddress,
            MobileNo = model.MobileNo,
            Skills = model.Skills,
            Hobby = model.Hobby,

            Status = Status.Active
        };
        item.CreatedBy = item.Id;

        await userService.AddAsync(item);
        return CreatedAtAction("GetAll", new { id = item.Id }, mapper.Map<UserDto>(item));
    }

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
        string message = await Validate(id, model.MobileNo, model.EmailAddress, model.Username);
        if (!string.IsNullOrEmpty(message))
        {
            return BadRequest(message);
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

    private async Task<string> Validate(int id, string mobileNo, string email, string username)
    {
        if (await userService.IsMobileNoExists(id, mobileNo))
        {
            return "Mobile No Already Exist";
        }

        if (await userService.IsEmailExists(id, mobileNo))
        {
            return "Email Already Exist";
        }

        if (await userService.IsUsernameExists(id, mobileNo))
        {
            return "Username Already Exist";
        }

        return string.Empty;
    }
}
