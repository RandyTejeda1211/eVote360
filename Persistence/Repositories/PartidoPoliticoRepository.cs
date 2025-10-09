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
    public class PartidoPoliticoRepository : IPartidoPoliticoRepository
    {
        private readonly AppDbContext _context;

        public PartidoPoliticoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PartidoPolitico> GetByIdAsync(int id)
            => await _context.PartidosPoliticos.FindAsync(id);

        public async Task<List<PartidoPolitico>> GetAllAsync()
            => await _context.PartidosPoliticos.ToListAsync();

        public async Task<PartidoPolitico> AddAsync(PartidoPolitico entity)
        {
            _context.PartidosPoliticos.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(PartidoPolitico entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PartidoPolitico entity)
        {
            _context.PartidosPoliticos.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
            => await _context.PartidosPoliticos.AnyAsync(p => p.Id == id);

        public async Task<List<PartidoPolitico>> GetActivePartiesAsync()
            => await _context.PartidosPoliticos.Where(p => p.State).ToListAsync();

        public async Task<PartidoPolitico> GetBySiglasAsync(string siglas)
            => await _context.PartidosPoliticos.FirstOrDefaultAsync(p => p.Siglas == siglas);

        public async Task<bool> HasCandidatesForAllPositionsAsync(int partyId, List<int> positionIds)
        {
            var partyCandidates = await _context.CandidatosPuestos
                .Where(cp => cp.PartidoPoliticoId == partyId)
                .Select(cp => cp.ElectionPositionId)
                .Distinct()
                .ToListAsync();

            return positionIds.All(positionId => partyCandidates.Contains(positionId));
        }

        public async Task<List<PartidoPolitico>> GetPartiesWithCandidatesAsync()
            => await _context.PartidosPoliticos
                .Include(p => p.candidates)
                .Where(p => p.State && p.candidates.Any(c => c.State))
                .ToListAsync();

        public async Task<List<PartidoPolitico>> GetPartiesForElectionAsync()
            => await _context.PartidosPoliticos
                .Where(p => p.State)
                .Include(p => p.candidates)
                .Where(p => p.candidates.Any(c => c.State))
                .ToListAsync();

        public async Task<int> GetActivePartiesCountAsync()
            => await _context.PartidosPoliticos.CountAsync(p => p.State);
    }
}
