using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Candidate
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(255)]
        public string Photo { get; set; }
        [Required]
        public int PartidoPoliticoId{ get; set; }
        public bool State { get; set; }

        public PartidoPolitico PartidoPolitico { get; set; }
        public ICollection<CandPosition> CandidatoPuesto { get; set; } = [];
        public ICollection<Vote> Votes { get; set; } = [];
    }
}
