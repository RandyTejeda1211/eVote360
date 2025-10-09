using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IElectPositionRepository
    {
        Task<ElectPosition> GetByIdAsync(int id);
        Task<List<ElectPosition>> GetAllAsync();
        Task<ElectPosition> AddAsync(ElectPosition entity);
        Task UpdateAsync(ElectPosition entity);
        Task DeleteAsync(ElectPosition entity);
        Task<bool> ExistsAsync(int id);

        Task<List<ElectPosition>> GetActivePositionsAsync();
        Task<List<ElectPosition>> GetPositionsForElectionAsync();
        Task<bool> PositionHasCandidatesAsync(int positionId);
        Task<int> GetActivePositionsCountAsync();
    }
}
