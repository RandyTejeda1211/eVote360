using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CandPosition
    {
        public int Id { get; set; }
        [Required]
        public int CandidateId { get; set; }

        [Required]
        public int ElectionPositionId { get; set; }

        [Required]
        public int PartidoPoliticoId { get; set; }

        public Candidate candidate { get; set; }
        public ElectPosition ElectPosition { get; set; }
        public PartidoPolitico PartidoPolitico { get; set; }

        public ICollection<Vote> votes { get; set; } = [];

    }
}
