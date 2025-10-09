using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
   
    public interface IPoliticAllianceRepository
    {
        Task<PoliticAlliance> GetByIdAsync(int id);
        Task<List<PoliticAlliance>> GetAllAsync();
        Task<PoliticAlliance> AddAsync(PoliticAlliance entity);
        Task UpdateAsync(PoliticAlliance entity);
        Task DeleteAsync(PoliticAlliance entity);
        Task<bool> ExistsAsync(int id);

        
        Task<List<PoliticAlliance>> GetPendingAlliancesByPartyAsync(int partyId);
        Task<List<PoliticAlliance>> GetSentAlliancesByPartyAsync(int partyId);
        Task<List<PoliticAlliance>> GetActiveAlliancesByPartyAsync(int partyId);
        Task<bool> AllianceRequestExistsAsync(int solicitanteId, int receptorId);
        Task<List<PoliticAlliance>> GetPendingRequestsToPartyAsync(int partyId);
        Task AcceptAllianceAsync(int allianceId);
        Task RejectAllianceAsync(int allianceId);
        Task<bool> CanCreateAllianceRequestAsync(int solicitanteId, int receptorId);
    }
}
