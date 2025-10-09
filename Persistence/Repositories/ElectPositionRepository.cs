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
    // Persistence/Repositories/ElectPositionRepository.cs
    public class ElectPositionRepository : IElectPositionRepository
    {
        private readonly AppDbContext _context;

        public ElectPositionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ElectPosition> GetByIdAsync(int id)
            => await _context.PuestosElectivos.FindAsync(id);

        public async Task<List<ElectPosition>> GetAllAsync()
            => await _context.PuestosElectivos.ToListAsync();

        public async Task<ElectPosition> AddAsync(ElectPosition entity)
        {
            _context.PuestosElectivos.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(ElectPosition entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ElectPosition entity)
        {
            _context.PuestosElectivos.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
            => await _context.PuestosElectivos.AnyAsync(p => p.Id == id);

        public async Task<List<ElectPosition>> GetActivePositionsAsync()
            => await _context.PuestosElectivos.Where(p => p.state).ToListAsync();

        public async Task<List<ElectPosition>> GetPositionsForElectionAsync()
            => await _context.PuestosElectivos
                .Where(p => p.state)
                .Include(p => p.CandPositions)
                .Where(p => p.CandPositions.Any(cp => cp.candidate.State))
                .ToListAsync();

        public async Task<bool> PositionHasCandidatesAsync(int positionId)
            => await _context.CandidatosPuestos.AnyAsync(cp => cp.ElectionPositionId == positionId && cp.candidate.State);

        public async Task<int> GetActivePositionsCountAsync()
            => await _context.PuestosElectivos.CountAsync(p => p.state);
    }
}
