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
 
    public class CandidateRepository : ICandidateRepository
    {
        private readonly AppDbContext _context;

        public CandidateRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Candidate> GetByIdAsync(int id)
            => await _context.Candidatos.FindAsync(id);

        public async Task<List<Candidate>> GetAllAsync()
            => await _context.Candidatos.ToListAsync();

        public async Task<Candidate> AddAsync(Candidate entity)
        {
            _context.Candidatos.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Candidate entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Candidate entity)
        {
            _context.Candidatos.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
            => await _context.Candidatos.AnyAsync(c => c.Id == id);

        public async Task<List<Candidate>> GetCandidatesByPartyAsync(int partyId)
            => await _context.Candidatos.Where(c => c.PartidoPoliticoId == partyId).ToListAsync();

        public async Task<List<Candidate>> GetCandidatesByPositionAsync(int positionId)
            => await _context.Candidatos
                .Include(c => c.CandidatoPuesto)
                .Where(c => c.CandidatoPuesto.Any(cp => cp.ElectionPositionId == positionId))
                .ToListAsync();

        public async Task<List<Candidate>> GetActiveCandidatesAsync()
            => await _context.Candidatos.Where(c => c.State).ToListAsync();

        public async Task<bool> CandidateExistsInPartyAsync(int candidateId, int partyId)
            => await _context.Candidatos.AnyAsync(c => c.Id == candidateId && c.PartidoPoliticoId == partyId);

        public async Task<List<Candidate>> GetCandidatesForElectionAsync(int electionId)
            => await _context.Candidatos
                .Include(c => c.CandidatoPuesto)
                .Where(c => c.State && c.CandidatoPuesto.Any())
                .ToListAsync();

        public async Task<List<Candidate>> GetCandidatesByPartyAndPositionAsync(int partyId, int positionId)
            => await _context.Candidatos
                .Include(c => c.CandidatoPuesto)
                .Where(c => c.PartidoPoliticoId == partyId &&
                           c.CandidatoPuesto.Any(cp => cp.ElectionPositionId == positionId))
                .ToListAsync();
    }
}
