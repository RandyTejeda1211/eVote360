using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PartidoPolitico
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [StringLength(10)]
        public string Siglas { get; set; }


        [StringLength(255)]
        public string Logo { get; set; }
        public string State { get; set; }

        public ICollection<Candidate> candidates { get; set; } = [];
        public ICollection<DirigentePartido> Dirigentes { get; set; } = [];
        public ICollection<PoliticAlliance> AlianzasSolicitadas { get; set; } = [];
        public ICollection<PoliticAlliance> AlianzasRecibidas { get; set; } = [];
        public virtual ICollection<CandPosition> CandidatoPuestos { get; set; } = [];
    }
}
