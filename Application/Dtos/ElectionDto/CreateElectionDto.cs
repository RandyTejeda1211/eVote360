using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ElectionDto
{
    public class CreateElectionDto
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public DateTime FechaRealizacion { get; set; }
    }

    
}
