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
    // Persistence/Repositories/DirigentePartidoRepository.cs
    public class DirigentePartidoRepository : IDirigentePartidoRepository
    {
        private readonly AppDbContext _context;

        public DirigentePartidoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DirigentePartido> GetByIdAsync(int id)
            => await _context.DirigentesPartidos.FindAsync(id);

        public async Task<List<DirigentePartido>> GetAllAsync()
            => await _context.DirigentesPartidos.ToListAsync();

        public async Task<DirigentePartido> AddAsync(DirigentePartido entity)
        {
            _context.DirigentesPartidos.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(DirigentePartido entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(DirigentePartido entity)
        {
            _context.DirigentesPartidos.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
            => await _context.DirigentesPartidos.AnyAsync(d => d.Id == id);

        public async Task<DirigentePartido> GetByUserIdAsync(int userId)
            => await _context.DirigentesPartidos
                .Include(d => d.PartidoPolitico)
                .FirstOrDefaultAsync(d => d.UserId == userId);

        public async Task<List<DirigentePartido>> GetByPartyIdAsync(int partyId)
            => await _context.DirigentesPartidos
                .Where(d => d.partidoPoliticoId == partyId)
                .Include(d => d.User)
                .ToListAsync();

        public async Task<bool> UserIsAssignedToPartyAsync(int userId)
            => await _context.DirigentesPartidos.AnyAsync(d => d.UserId == userId);

        public async Task<PartidoPolitico> GetPartyByUserIdAsync(int userId)
        {
            var dirigente = await GetByUserIdAsync(userId);
            return dirigente?.PartidoPolitico;
        }

        public async Task<bool> AssignUserToPartyAsync(int userId, int partyId)
        {
            if (await UserIsAssignedToPartyAsync(userId))
                return false;

            var assignment = new DirigentePartido
            {
                UserId = userId,
                partidoPoliticoId = partyId
            };

            await AddAsync(assignment);
            return true;
        }

        public async Task<bool> RemoveUserFromPartyAsync(int userId)
        {
            var assignment = await GetByUserIdAsync(userId);
            if (assignment != null)
            {
                await DeleteAsync(assignment);
                return true;
            }
            return false;
        }
    }
}
