using Domain.Entities;
using Domain.Interfaces;
using eVote360.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class CitizenRepository : ICitizenRepository
    {
        private readonly AppDbContext _context;
        public CitizenRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Citizen> GetByIdAsync(int id)
       => await _context.Ciudadanos.FindAsync(id);

        public async Task<List<Citizen>> GetAllAsync()
            => await _context.Ciudadanos.ToListAsync();

        public async Task<Citizen> AddAsync(Citizen entity)
        {
            _context.Ciudadanos.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Citizen entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Citizen entity)
        {
            _context.Ciudadanos.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
            => await _context.Ciudadanos.AnyAsync(c => c.Id == id);

        public async Task<Citizen> GetByDocumentNumberAsync(string documentNumber)
            => await _context.Ciudadanos.FirstOrDefaultAsync(c => c.DocumentNumber == documentNumber);

        public async Task<bool> HasVotedInElectionAsync(int citizenId, int electionId)
            => await _context.Votos.AnyAsync(v => v.CitizenId == citizenId && v.ElectionId == electionId);

        public async Task<bool> IsCitizenActiveAsync(int citizenId)
            => await _context.Ciudadanos.AnyAsync(c => c.Id == citizenId && c.state);

        public async Task<List<Citizen>> GetActiveCitizensAsync()
            => await _context.Ciudadanos.Where(c => c.state).ToListAsync();
    }
}
