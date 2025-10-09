using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEleccionPuestoRepository
    {
        Task<EleccionPuesto> GetByIdAsync(int id);
        Task<List<EleccionPuesto>> GetAllAsync();
        Task<EleccionPuesto> AddAsync(EleccionPuesto entity);
        Task UpdateAsync(EleccionPuesto entity);
        Task DeleteAsync(EleccionPuesto entity);
        Task<bool> ExistsAsync(int id);

        Task<List<EleccionPuesto>> GetByElectionIdAsync(int electionId);
        Task<bool> ElectionHasPositionAsync(int electionId, int positionId);
        Task<List<EleccionPuesto>> GetPositionsByElectionAsync(int electionId);
        Task<int> GetPositionsCountByElectionAsync(int electionId);
    }
}
