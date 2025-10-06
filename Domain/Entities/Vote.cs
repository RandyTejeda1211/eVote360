using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Vote
    {
        public int Id { get; set; }

        [Required]
        public int CitizenId { get; set; }

        [Required]
        public int ElectionId { get; set; }

        [Required]
        public int CandidatoPuestoID { get; set; }
        public DateTime DateVote { get; set; } = DateTime.Now;

        public Citizen Citizen { get; set; }
        public Elections Elections { get; set; }
        public CandPosition CandPosition { get; set; }
    }
}
