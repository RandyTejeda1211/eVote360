using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPartidoPoliticoRepository
    {
        Task<PartidoPolitico> GetByIdAsync(int id);
        Task<List<PartidoPolitico>> GetAllAsync();
        Task<PartidoPolitico> AddAsync(PartidoPolitico entity);
        Task UpdateAsync(PartidoPolitico entity);
        Task DeleteAsync(PartidoPolitico entity);
        Task<bool> ExistsAsync(int id);
        Task<List<PartidoPolitico>> GetActivePartiesAsync();
        Task<PartidoPolitico> GetBySiglasAsync(string siglas);
        Task<bool> HasCandidatesForAllPositionsAsync(int partyId, List<int> positionIds);
        Task<List<PartidoPolitico>> GetPartiesWithCandidatesAsync();
        Task<List<PartidoPolitico>> GetPartiesForElectionAsync();
        Task<int> GetActivePartiesCountAsync();
    }
}
