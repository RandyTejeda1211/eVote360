using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eVote360.Persistence.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        
        public DbSet<Citizen> Ciudadanos { get; set; }
        public DbSet<PartidoPolitico> PartidosPoliticos { get; set; }
        public DbSet<ElectPosition> PuestosElectivos { get; set; }
        public DbSet<User> Usuarios { get; set; }
        public DbSet<Elections> Elecciones { get; set; }
        public DbSet<Candidate> Candidatos { get; set; }
        public DbSet<PoliticAlliance> AlianzasPoliticas { get; set; }
        public DbSet<CandPosition> CandidatosPuestos { get; set; }
        public DbSet<DirigentePartido> DirigentesPartidos { get; set; }
        public DbSet<EleccionPuesto> EleccionesPuestos { get; set; }
        public DbSet<Vote> Votos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
       
            modelBuilder.Entity<Citizen>()
                .HasIndex(c => c.DocumentNumber)
                .IsUnique();
            modelBuilder.Entity<PartidoPolitico>()
                .HasIndex(p => p.Siglas)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            
            
            modelBuilder.Entity<Citizen>()
                .HasMany(c => c.Votes)
                .WithOne(v => v.Citizen)
                .HasForeignKey(v => v.CitizenId);

           
            modelBuilder.Entity<PartidoPolitico>()
                .HasMany(p => p.candidates)
                .WithOne(c => c.PartidoPolitico)
                .HasForeignKey(c => c.PartidoPoliticoId);

            
            modelBuilder.Entity<User>()
                .HasMany(u => u.dirigentePartidos)
                .WithOne(d => d.User)
                .HasForeignKey(d => d.UserId);

          
            modelBuilder.Entity<PartidoPolitico>()
                .HasMany(p => p.Dirigentes)
                .WithOne(d => d.PartidoPolitico)
                .HasForeignKey(d => d.partidoPoliticoId);

            
            modelBuilder.Entity<Elections>()
                .HasMany(e => e.Votes)
                .WithOne(v => v.Elections)
                .HasForeignKey(v => v.ElectionId);

            modelBuilder.Entity<Elections>()
                .HasMany(e => e.EleccionPuestos)
                .WithOne(ep => ep.Elections)
                .HasForeignKey(ep => ep.EleccionID);

            modelBuilder.Entity<ElectPosition>()
                .HasMany(p => p.EleccionPuestos)
                .WithOne(ep => ep.ElectPosition)
                .HasForeignKey(ep => ep.PuestoElectivoID);

            modelBuilder.Entity<ElectPosition>()
                .HasMany(p => p.CandPositions)
                .WithOne(cp => cp.ElectPosition)
                .HasForeignKey(cp => cp.ElectionPositionId);

            modelBuilder.Entity<Candidate>()
                .HasMany(c => c.CandidatoPuesto)
                .WithOne(cp => cp.candidate)
                .HasForeignKey(cp => cp.CandidateId);

            modelBuilder.Entity<CandPosition>()
                .HasMany(cp => cp.votes)
                .WithOne(v => v.CandPosition)
                .HasForeignKey(v => v.CandidatoPuestoID);

            modelBuilder.Entity<PartidoPolitico>()
                .HasMany(p => p.CandidatoPuestos)
                .WithOne(cp => cp.PartidoPolitico)
                .HasForeignKey(cp => cp.PartidoPoliticoId);

            modelBuilder.Entity<PoliticAlliance>()
                .HasOne(a => a.PartidoSolicitante)
                .WithMany(p => p.AlianzasSolicitadas)
                .HasForeignKey(a => a.PartidoSolicitanteID);

            modelBuilder.Entity<PoliticAlliance>()
                .HasOne(a => a.PartidoReceptor)
                .WithMany(p => p.AlianzasRecibidas)
                .HasForeignKey(a => a.PartidoReceptorID);

            modelBuilder.Entity<CandPosition>()
                .HasIndex(cp => new { cp.CandidateId, cp.ElectionPositionId, cp.PartidoPoliticoId }).IsUnique();

            modelBuilder.Entity<DirigentePartido>()
                .HasIndex(dp => dp.UserId).IsUnique();

            modelBuilder.Entity<Vote>()
                .HasIndex(v => new { v.CitizenId, v.ElectionId }).IsUnique();

            modelBuilder.Entity<EleccionPuesto>()
                .HasIndex(ep => new { ep.EleccionID, ep.PuestoElectivoID }).IsUnique();

            modelBuilder.Entity<PoliticAlliance>()
                .HasIndex(a => new { a.PartidoSolicitanteID, a.PartidoReceptorID }).IsUnique();
        }
    }
}