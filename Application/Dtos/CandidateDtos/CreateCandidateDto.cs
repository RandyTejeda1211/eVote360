using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.CandidateDtos
{
    public class CreateCandidateDto
    {
        [Required] public string Name { get; set; }
        [Required] public string LastName { get; set; }
        public string Photo { get; set; }
        [Required] public int PartidoPoliticoId { get; set; }
    }
}
