using CDN.Core.Entities;
using CDN.Core.IRepositories;

namespace CDN.Core.Repositories;
public class UserRepository(CDNContext context) : GenericRepository<User>(context), IUserRepository
{
}
