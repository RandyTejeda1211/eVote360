using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using eVote360.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class VoteRepository :IVoteRepository
    {
        private readonly AppDbContext _context;
        public VoteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Vote> GetByIdAsync(int id)
       => await _context.Votos.FindAsync(id);

        public async Task<List<Vote>> GetAllAsync()
            => await _context.Votos.ToListAsync();

        public async Task<Vote> AddAsync(Vote entity)
        {
            _context.Votos.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Vote entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Vote entity)
        {
            _context.Votos.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
            => await _context.Votos.AnyAsync(v => v.Id == id);

        public async Task<bool> HasCitizenVotedAsync(int citizenId, int electionId)
            => await _context.Votos.AnyAsync(v => v.CitizenId == citizenId && v.ElectionId == electionId);

        public async Task<int> GetTotalVotesByElectionAsync(int electionId)
            => await _context.Votos.CountAsync(v => v.ElectionId == electionId);

        public async Task<List<Vote>> GetVotesByElectionAndPositionAsync(int electionId, int positionId)
            => await _context.Votos
                .Include(v => v.CandPosition)
                .Where(v => v.ElectionId == electionId && v.CandPosition.ElectionPositionId == positionId)
                .ToListAsync();

        public async Task RegisterVoteAsync(Vote vote)
            => await AddAsync(vote);

        public async Task<List<Vote>> GetVotesByElectionAsync(int electionId)
            => await _context.Votos.Where(v => v.ElectionId == electionId).ToListAsync();

        public async Task<int> GetVotesByCandidateAndElectionAsync(int candidateId, int electionId)
            => await _context.Votos
                .CountAsync(v => v.ElectionId == electionId && v.CandPosition.CandidateId == candidateId);
    }
}
