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
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(int id)
            => await _context.Usuarios.FindAsync(id);

        public async Task<List<User>> GetAllAsync()
            => await _context.Usuarios.ToListAsync();

        public async Task<User> AddAsync(User entity)
        {
            _context.Usuarios.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(User entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User entity)
        {
            _context.Usuarios.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
            => await _context.Usuarios.AnyAsync(u => u.Id == id);

        public async Task<User> GetByUsernameAsync(string username)
            => await _context.Usuarios.FirstOrDefaultAsync(u => u.UserName == username);

        public async Task<User> GetByUsernameAndPasswordAsync(string username, string passwordHash)
            => await _context.Usuarios.FirstOrDefaultAsync(u => u.UserName == username && u.PasswordHash == passwordHash);

        public async Task<bool> UserHasAssignedPartyAsync(int userId)
            => await _context.DirigentesPartidos.AnyAsync(d => d.UserId == userId);

        public async Task<List<User>> GetUsersByRoleAsync(string role)
            => await _context.Usuarios.Where(u => u.Rol == role).ToListAsync();

        public async Task<List<User>> GetActiveUsersAsync()
            => await _context.Usuarios.Where(u => u.State).ToListAsync();

        public async Task<List<User>> GetPoliticalLeadersWithoutPartyAsync()
        {
            var usersWithParty = await _context.DirigentesPartidos.Select(d => d.UserId).ToListAsync();
            return await _context.Usuarios
                .Where(u => u.Rol == "Dirigente" && u.State && !usersWithParty.Contains(u.Id))
                .ToListAsync();
        }
    }
}
