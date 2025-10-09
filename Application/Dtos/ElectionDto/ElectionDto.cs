using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ElectionDto
{
    public class ElectionDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaRealizacion { get; set; }
        public string Estado { get; set; }
        public int TotalVotos { get; set; }
        public int PartidosParticipantes { get; set; }
    }
}
