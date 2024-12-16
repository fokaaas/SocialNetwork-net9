using Data.Entities;

namespace Data.Interfaces;

public interface IGroupDetailsRepository : IRepository<GroupDetails>
{
    Task<GroupDetails> GetByIdAsync(int id);
    
    Task DeleteByIdAsync(int id);
}