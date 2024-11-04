using CDN.Core.Entities;
using CDN.Core.IRepositories;
using CDN.Application.IServices;

namespace CDN.Application.Services;
public class UserService(IUserRepository UserRepository) : IUserService
{
    private readonly IUserRepository _userRepository = UserRepository;

    async Task<IReadOnlyList<User>> IGenericService<User>.GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    async Task<User?> IGenericService<User>.GetByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    async Task IGenericService<User>.AddAsync(User entity)
    {
        await _userRepository.AddAsync(entity);
    }

    async Task IGenericService<User>.UpdateAsync(User entity)
    {
        await _userRepository.UpdateAsync(entity);
    }

    public async Task<User?> GetByUsername(string userName)
    {
        return await _userRepository.GetByUsername(userName);
    }

    public async Task<List<string>> GetUsernames()
    {
        return await _userRepository.GetUsernames();
    }

    public async Task<IReadOnlyList<User>> GetAll(int page, int pageSize, string searchText)
    {
        return await _userRepository.GetAll(page, pageSize, searchText);
    }

    public async Task<List<User>> GetAuditById(int id)
    {
        return await _userRepository.GetAuditById(id);
    }

    public async Task<bool> IsUsernameExists(int id, string value)
    {
        return await _userRepository.IsUsernameExists(id, value);
    }

    public async Task<bool> IsMobileNoExists(int id, string value)
    {
        return await _userRepository.IsMobileNoExists(id, value);
    }

    public async Task<bool> IsEmailExists(int id, string value)
    {
        return await _userRepository.IsEmailExists(id, value);
    }
}
