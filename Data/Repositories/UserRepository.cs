using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SocialNetworkDbContext _context;
    
    public UserRepository(SocialNetworkDbContext context)
    {
        _context = context;
    }
    
    public async Task<User> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }
    
    public async Task DeleteByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is not null)
        {
            _context.Users.Remove(user);
        }
    }
    
    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }
    
    public async Task<bool> IsEmailInUseAsync(string email, int userId)
    {
        return await _context.Users.AnyAsync(u => u.Email == email && u.Id != userId);
    }
    
    public async Task<User> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
    
    public async Task AddAsync(User entity)
    {
        await _context.Users.AddAsync(entity);
    }
    
    public void Delete(User entity)
    {
        _context.Users.Remove(entity);
    }
    
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }
    
    public void Update(User entity)
    {
        _context.Users.Update(entity);
    }
}