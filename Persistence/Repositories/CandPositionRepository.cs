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
    // Persistence/Repositories/CandPositionRepository.cs
    public class CandPositionRepository : ICandPositionRepository
    {
        private readonly AppDbContext _context;

        public CandPositionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CandPosition> GetByIdAsync(int id)
            => await _context.CandidatosPuestos.FindAsync(id);

        public async Task<List<CandPosition>> GetAllAsync()
            => await _context.CandidatosPuestos.ToListAsync();

        public async Task<CandPosition> AddAsync(CandPosition entity)
        {
            _context.CandidatosPuestos.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(CandPosition entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(CandPosition entity)
        {
            _context.CandidatosPuestos.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
            => await _context.CandidatosPuestos.AnyAsync(cp => cp.Id == id);

        public async Task<bool> CandidateHasPositionInPartyAsync(int candidateId, int partyId)
            => await _context.CandidatosPuestos.AnyAsync(cp => cp.CandidateId == candidateId && cp.PartidoPoliticoId == partyId);

        public async Task<List<CandPosition>> GetByElectionAndPositionAsync(int electionId, int positionId)
            => await _context.CandidatosPuestos
                .Include(cp => cp.candidate)
                .Include(cp => cp.PartidoPolitico)
                .Where(cp => cp.ElectionPositionId == positionId)
                .ToListAsync();

        public async Task<CandPosition> GetByCandidateAndPartyAsync(int candidateId, int partyId)
            => await _context.CandidatosPuestos
                .FirstOrDefaultAsync(cp => cp.CandidateId == candidateId && cp.PartidoPoliticoId == partyId);

        public async Task<List<CandPosition>> GetByCandidateAsync(int candidateId)
            => await _context.CandidatosPuestos
                .Where(cp => cp.CandidateId == candidateId)
                .Include(cp => cp.ElectPosition)
                .ToListAsync();

        public async Task<List<CandPosition>> GetByPartyAsync(int partyId)
            => await _context.CandidatosPuestos
                .Where(cp => cp.PartidoPoliticoId == partyId)
                .Include(cp => cp.candidate)
                .Include(cp => cp.ElectPosition)
                .ToListAsync();

        public async Task<List<CandPosition>> GetByPositionAsync(int positionId)
            => await _context.CandidatosPuestos
                .Where(cp => cp.ElectionPositionId == positionId)
                .Include(cp => cp.candidate)
                .Include(cp => cp.PartidoPolitico)
                .ToListAsync();

        public async Task<bool> IsCandidateAssignedToPositionAsync(int candidateId, int positionId, int partyId)
            => await _context.CandidatosPuestos
                .AnyAsync(cp => cp.CandidateId == candidateId &&
                               cp.ElectionPositionId == positionId &&
                               cp.PartidoPoliticoId == partyId);
    }
}
