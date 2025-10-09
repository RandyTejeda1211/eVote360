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
    // Persistence/Repositories/EleccionPuestoRepository.cs
    public class EleccionPuestoRepository : IEleccionPuestoRepository
    {
        private readonly AppDbContext _context;

        public EleccionPuestoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EleccionPuesto> GetByIdAsync(int id)
            => await _context.EleccionesPuestos.FindAsync(id);

        public async Task<List<EleccionPuesto>> GetAllAsync()
            => await _context.EleccionesPuestos.ToListAsync();

        public async Task<EleccionPuesto> AddAsync(EleccionPuesto entity)
        {
            _context.EleccionesPuestos.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(EleccionPuesto entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(EleccionPuesto entity)
        {
            _context.EleccionesPuestos.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
            => await _context.EleccionesPuestos.AnyAsync(ep => ep.Id == id);

        public async Task<List<EleccionPuesto>> GetByElectionIdAsync(int electionId)
            => await _context.EleccionesPuestos
                .Where(ep => ep.EleccionID == electionId)
                .Include(ep => ep.ElectPosition)
                .ToListAsync();

        public async Task<bool> ElectionHasPositionAsync(int electionId, int positionId)
            => await _context.EleccionesPuestos
                .AnyAsync(ep => ep.EleccionID == electionId && ep.PuestoElectivoID == positionId);

        public async Task<List<EleccionPuesto>> GetPositionsByElectionAsync(int electionId)
            => await _context.EleccionesPuestos
                .Where(ep => ep.EleccionID == electionId)
                .Include(ep => ep.ElectPosition)
                .ToListAsync();

        public async Task<int> GetPositionsCountByElectionAsync(int electionId)
            => await _context.EleccionesPuestos.CountAsync(ep => ep.EleccionID == electionId);
    }
}
