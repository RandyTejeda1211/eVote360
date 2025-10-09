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
    // Persistence/Repositories/PoliticAllianceRepository.cs
    public class PoliticAllianceRepository : IPoliticAllianceRepository
    {
        private readonly AppDbContext _context;

        public PoliticAllianceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PoliticAlliance> GetByIdAsync(int id)
            => await _context.AlianzasPoliticas.FindAsync(id);

        public async Task<List<PoliticAlliance>> GetAllAsync()
            => await _context.AlianzasPoliticas.ToListAsync();

        public async Task<PoliticAlliance> AddAsync(PoliticAlliance entity)
        {
            _context.AlianzasPoliticas.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(PoliticAlliance entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PoliticAlliance entity)
        {
            _context.AlianzasPoliticas.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
            => await _context.AlianzasPoliticas.AnyAsync(a => a.Id == id);

        public async Task<List<PoliticAlliance>> GetPendingAlliancesByPartyAsync(int partyId)
            => await _context.AlianzasPoliticas
                .Where(a => a.PartidoReceptorID == partyId && a.State == "Pendiente")
                .Include(a => a.PartidoSolicitante)
                .ToListAsync();

        public async Task<List<PoliticAlliance>> GetSentAlliancesByPartyAsync(int partyId)
            => await _context.AlianzasPoliticas
                .Where(a => a.PartidoSolicitanteID == partyId)
                .Include(a => a.PartidoReceptor)
                .ToListAsync();

        public async Task<List<PoliticAlliance>> GetActiveAlliancesByPartyAsync(int partyId)
            => await _context.AlianzasPoliticas
                .Where(a => (a.PartidoSolicitanteID == partyId || a.PartidoReceptorID == partyId) &&
                           a.State == "Aceptada")
                .Include(a => a.PartidoSolicitante)
                .Include(a => a.PartidoReceptor)
                .ToListAsync();

        public async Task<bool> AllianceRequestExistsAsync(int solicitanteId, int receptorId)
            => await _context.AlianzasPoliticas
                .AnyAsync(a => a.PartidoSolicitanteID == solicitanteId &&
                              a.PartidoReceptorID == receptorId &&
                              a.State == "Pendiente");

        public async Task<List<PoliticAlliance>> GetPendingRequestsToPartyAsync(int partyId)
            => await _context.AlianzasPoliticas
                .Where(a => a.PartidoReceptorID == partyId && a.State == "Pendiente")
                .Include(a => a.PartidoSolicitante)
                .ToListAsync();

        public async Task AcceptAllianceAsync(int allianceId)
        {
            var alliance = await GetByIdAsync(allianceId);
            if (alliance != null)
            {
                alliance.State = "Aceptada";
                alliance.FechaRespuesta = DateTime.Now;
                await UpdateAsync(alliance);
            }
        }

        public async Task RejectAllianceAsync(int allianceId)
        {
            var alliance = await GetByIdAsync(allianceId);
            if (alliance != null)
            {
                alliance.State = "Rechazada";
                alliance.FechaRespuesta = DateTime.Now;
                await UpdateAsync(alliance);
            }
        }

        public async Task<bool> CanCreateAllianceRequestAsync(int solicitanteId, int receptorId)
        {
            
            return !await _context.AlianzasPoliticas
                .AnyAsync(a => ((a.PartidoSolicitanteID == solicitanteId && a.PartidoReceptorID == receptorId) ||
                               (a.PartidoSolicitanteID == receptorId && a.PartidoReceptorID == solicitanteId)) &&
                              a.State == "Pendiente");
        }
    }
}
