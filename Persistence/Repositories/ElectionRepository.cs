
using Domain.Entities;
using Domain.Interfaces;
using eVote360.Persistence.Data;
using Microsoft.EntityFrameworkCore;

public class ElectionRepository : IElectionRepository
{
    private readonly AppDbContext _context;

    public ElectionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Elections> GetByIdAsync(int id)
        => await _context.Elecciones.FindAsync(id);

    public async Task<List<Elections>> GetAllAsync()
        => await _context.Elecciones.ToListAsync();

    public async Task<Elections> AddAsync(Elections entity)
    {
        _context.Elecciones.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Elections entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Elections entity)
    {
        _context.Elecciones.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
        => await _context.Elecciones.AnyAsync(e => e.Id == id);

    public async Task<Elections> GetActiveElectionAsync()
        => await _context.Elecciones.FirstOrDefaultAsync(e => e.State == "EnProceso");

    public async Task<bool> HasActiveElectionAsync()
        => await _context.Elecciones.AnyAsync(e => e.State == "EnProceso");

    public async Task<List<Elections>> GetElectionsByYearAsync(int year)
        => await _context.Elecciones.Where(e => e.DateRealization.Year == year).ToListAsync();

    public async Task<List<Elections>> GetElectionsWithResultsAsync()
        => await _context.Elecciones.Include(e => e.Votes).ToListAsync();

    public async Task FinalizeElectionAsync(int electionId)
    {
        var election = await GetByIdAsync(electionId);
        if (election != null)
        {
            election.State = "Finalizada";
            await UpdateAsync(election);
        }
    }

    public async Task<bool> CanCreateElectionAsync()
    {
       var activePositions = await _context.PuestosElectivos.CountAsync(p => p.state);
        var activeParties = await _context.PartidosPoliticos.CountAsync(p => p.State);

        return !await HasActiveElectionAsync() &&
               activePositions > 0 &&
               activeParties >= 2;
    }
    public async Task<List<Elections>> GetElectionsOrderedByDateAsync()
        => await _context.Elecciones.OrderByDescending(e => e.DateRealization).ToListAsync();
}