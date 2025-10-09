using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.CandidateDtos
{
    public class UpdateCandidateDto
    {
        [Required] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string LastName { get; set; }
        public string Photo { get; set; }
        public bool Estado { get; set; }
    }
}
