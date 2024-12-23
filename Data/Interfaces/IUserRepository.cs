using Data.Entities;

namespace Data.Interfaces;

public interface IUserRepository: IRepository<User>
{
    Task<User> GetByIdAsync(int id);
    
    Task DeleteByIdAsync(int id);
    
    Task<IEnumerable<User>> GetAllAsync();
    
    Task<bool> ExistsByEmailAsync(string email);
    
    Task<bool> IsEmailInUseAsync(string email, int userId);
    
    Task<User> GetByEmailAsync(string email);
}