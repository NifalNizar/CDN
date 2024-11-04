using CDN.Core.Entities;

namespace CDN.Core.IRepositories;
public interface IUserRepository : IGenericRepository<User>
{
    public Task<User?> GetByUsername(string userName);

    public Task<List<string>> GetUsernames();
}
