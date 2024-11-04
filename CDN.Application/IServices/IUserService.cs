using CDN.Core.Entities;

namespace CDN.Application.IServices;
public interface IUserService : IGenericService<User>
{
    public Task<User?> GetByUsername(string userName);
}
