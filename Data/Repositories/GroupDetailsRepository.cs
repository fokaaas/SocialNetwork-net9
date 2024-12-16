using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class GroupDetailsRepository : IGroupDetailsRepository
{
    private readonly SocialNetworkDbContext _context;
    
    public GroupDetailsRepository(SocialNetworkDbContext context)
    {
        _context = context;
    }
    
    public async Task<GroupDetails> GetByIdAsync(int id)
    {
        return await _context.GroupDetails.FindAsync(id);
    }
    
    public async Task DeleteByIdAsync(int id)
    {
        var groupDetails = await _context.GroupDetails.FindAsync(id);
        if (groupDetails is not null)
        {
            _context.GroupDetails.Remove(groupDetails);
        }
    }
    
    public async Task AddAsync(GroupDetails entity)
    {
        await _context.GroupDetails.AddAsync(entity);
    }
    
    public void Delete(GroupDetails entity)
    {
        _context.GroupDetails.Remove(entity);
    }
    
    public async Task<IEnumerable<GroupDetails>> GetAllAsync()
    {
        return await _context.GroupDetails.ToListAsync();
    }
    
    public void Update(GroupDetails entity)
    {
        _context.GroupDetails.Update(entity);
    }
}