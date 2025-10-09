using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    
    public interface ICandidateRepository
    {
        Task<Candidate> GetByIdAsync(int id);
        Task<List<Candidate>> GetAllAsync();
        Task<Candidate> AddAsync(Candidate entity);
        Task UpdateAsync(Candidate entity);
        Task DeleteAsync(Candidate entity);
        Task<bool> ExistsAsync(int id);
        Task<List<Candidate>> GetCandidatesByPartyAsync(int partyId);
        Task<List<Candidate>> GetCandidatesByPositionAsync(int positionId);
        Task<List<Candidate>> GetActiveCandidatesAsync();
        Task<bool> CandidateExistsInPartyAsync(int candidateId, int partyId);
        Task<List<Candidate>> GetCandidatesForElectionAsync(int electionId);
        Task<List<Candidate>> GetCandidatesByPartyAndPositionAsync(int partyId, int positionId);
    }
}
