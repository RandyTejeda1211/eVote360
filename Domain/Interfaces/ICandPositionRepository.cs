using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    
    public interface ICandPositionRepository
    {
        Task<CandPosition> GetByIdAsync(int id);
        Task<List<CandPosition>> GetAllAsync();
        Task<CandPosition> AddAsync(CandPosition entity);
        Task UpdateAsync(CandPosition entity);
        Task DeleteAsync(CandPosition entity);
        Task<bool> ExistsAsync(int id);
        Task<bool> CandidateHasPositionInPartyAsync(int candidateId, int partyId);
        Task<List<CandPosition>> GetByElectionAndPositionAsync(int electionId, int positionId);
        Task<CandPosition> GetByCandidateAndPartyAsync(int candidateId, int partyId);
        Task<List<CandPosition>> GetByCandidateAsync(int candidateId);
        Task<List<CandPosition>> GetByPartyAsync(int partyId);
        Task<List<CandPosition>> GetByPositionAsync(int positionId);
        Task<bool> IsCandidateAssignedToPositionAsync(int candidateId, int positionId, int partyId);
    }
}
