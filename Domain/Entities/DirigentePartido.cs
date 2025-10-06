using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DirigentePartido
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int partidoPoliticoId { get; set; }

        public User User { get; set; }
        public PartidoPolitico PartidoPolitico { get; set; }
    }
}
