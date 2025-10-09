using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IDirigentePartidoRepository
    {
        Task<DirigentePartido> GetByIdAsync(int id);
        Task<List<DirigentePartido>> GetAllAsync();
        Task<DirigentePartido> AddAsync(DirigentePartido entity);
        Task UpdateAsync(DirigentePartido entity);
        Task DeleteAsync(DirigentePartido entity);
        Task<bool> ExistsAsync(int id);

        Task<DirigentePartido> GetByUserIdAsync(int userId);
        Task<List<DirigentePartido>> GetByPartyIdAsync(int partyId);
        Task<bool> UserIsAssignedToPartyAsync(int userId);
        Task<PartidoPolitico> GetPartyByUserIdAsync(int userId);
        Task<bool> AssignUserToPartyAsync(int userId, int partyId);
        Task<bool> RemoveUserFromPartyAsync(int userId);
    }
}
