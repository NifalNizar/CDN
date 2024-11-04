using CDN.Core.Entities;
using CDN.Core.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CDN.Core.Repositories;
public class UserRepository(CDNContext context) : GenericRepository<User>(context), IUserRepository
{
    private readonly CDNContext _context = context;

    public async Task<User?> GetByUsername(string userName)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Username.ToLower() == userName.ToLower());
    }

    public async Task<List<string>> GetUsernames()
    {
        return await _context.Users
            .AsNoTracking()
            .Select(x => x.Username)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<User>> GetAll(int page, int pageSize, string searchText)
    {
        if (page == 0) page = 1;
        if (pageSize == 0) pageSize = int.MaxValue;
        int skip = (page - 1) * pageSize;

        return await _context.Users
            .AsNoTracking()
            .Where(x =>
                searchText == "" 
                || x.Username.Contains(searchText)
                || x.MobileNo.Contains(searchText)
                || x.EmailAddress!.Contains(searchText)
            )
            .OrderByDescending(x => x.Id)
            .Skip(skip).Take(pageSize)
            .ToListAsync();
    }
}
