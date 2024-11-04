using CDN.Core.Entities;
using CDN.Core.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CDN.Core.Repositories;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly CDNContext _context;
    public UserRepository(CDNContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User?> GetByUsername(string userName)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == userName.ToLower());
    }
}
