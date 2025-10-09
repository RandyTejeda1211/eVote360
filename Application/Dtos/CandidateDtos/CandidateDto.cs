using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.CandidateDtos
{
    public class CandidateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
        public int PartidoPoliticoId { get; set; }
        public bool Estado { get; set; }
    }
}
