using CDN.Core.Entities;

namespace CDN.Core.IRepositories;
public interface IUserRepository : IGenericRepository<User>
{
    public Task<User?> GetByUsername(string userName);

    public Task<List<string>> GetUsernames();

    public Task<IReadOnlyList<User>> GetAll(int page, int pageSize, string searchText);

    public Task<List<User>> GetAuditById(int id);

    public Task<bool> IsUsernameExists(int id, string value);

    public Task<bool> IsMobileNoExists(int id, string value);

    public Task<bool> IsEmailExists(int id, string value);
}
