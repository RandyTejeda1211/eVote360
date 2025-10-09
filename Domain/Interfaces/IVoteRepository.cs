using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IVoteRepository
    {
        Task<Vote> GetByIdAsync(int id);
        Task<List<Vote>> GetAllAsync();
        Task<Vote> AddAsync(Vote entity);
        Task UpdateAsync(Vote entity);
        Task DeleteAsync(Vote entity);
        Task<bool> ExistsAsync(int id);
        Task<bool> HasCitizenVotedAsync(int citizenId, int electionId);
        Task<int> GetTotalVotesByElectionAsync(int electionId);
        Task<List<Vote>> GetVotesByElectionAndPositionAsync(int electionId, int positionId);
        Task RegisterVoteAsync(Vote vote);
        Task<List<Vote>> GetVotesByElectionAsync(int electionId);
        Task<int> GetVotesByCandidateAndElectionAsync(int candidateId, int electionId);
    }
}
