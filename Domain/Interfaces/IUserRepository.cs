using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<List<User>> GetAllAsync();
        Task<User> AddAsync(User entity);
        Task UpdateAsync(User entity);
        Task DeleteAsync(User entity);
        Task<bool> ExistsAsync(int id);
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByUsernameAndPasswordAsync(string username, string passwordHash);
        Task<bool> UserHasAssignedPartyAsync(int userId);
        Task<List<User>> GetUsersByRoleAsync(string role);
        Task<List<User>> GetActiveUsersAsync();
        Task<List<User>> GetPoliticalLeadersWithoutPartyAsync();
    }
}
