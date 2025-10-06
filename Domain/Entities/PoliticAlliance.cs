using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PoliticAlliance
    {
        public int Id { get; set; }

        [Required]
        public int PartidoSolicitanteID { get; set; }
        [Required]
        public int PartidoReceptorID { get; set; }
        [Required]
        [StringLength(20)]
        public string State { get; set; }
        public DateTime FechaSolicitud { get; set; } = DateTime.Now;
        public DateTime? FechaRespuesta { get; set; } 

        public PartidoPolitico PartidoSolicitante { get; set; }
        public PartidoPolitico PartidoReceptor { get; set; }

    }
}
