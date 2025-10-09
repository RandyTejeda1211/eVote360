using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IElectionRepository
    {

        Task<Elections> GetByIdAsync(int id);
        Task<List<Elections>> GetAllAsync();
        Task<Elections> AddAsync(Elections entity);
        Task UpdateAsync(Elections entity);
        Task DeleteAsync(Elections entity);
        Task<bool> ExistsAsync(int id);
        Task<Elections> GetActiveElectionAsync();
        Task<bool> HasActiveElectionAsync();
        Task<List<Elections>> GetElectionsByYearAsync(int year);
        Task<List<Elections>> GetElectionsWithResultsAsync();
        Task FinalizeElectionAsync(int electionId);
        Task<bool> CanCreateElectionAsync();
        Task<List<Elections>> GetElectionsOrderedByDateAsync();
    }
}
