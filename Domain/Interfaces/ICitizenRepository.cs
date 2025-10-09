using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICitizenRepository
    {
        Task<Citizen> GetByIdAsync(int id);
        Task<List<Citizen>> GetAllAsync();
        Task<Citizen> AddAsync(Citizen entity);
        Task UpdateAsync(Citizen entity);
        Task DeleteAsync(Citizen entity);
        Task<bool> ExistsAsync(int id);
        Task<Citizen> GetByDocumentNumberAsync(string documentNumber);
        Task<bool> HasVotedInElectionAsync(int citizenId, int electionId);
        Task<bool> IsCitizenActiveAsync(int citizenId);
        Task<List<Citizen>> GetActiveCitizensAsync();
    }
}
