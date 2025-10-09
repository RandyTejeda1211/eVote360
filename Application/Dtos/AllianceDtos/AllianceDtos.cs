using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.AllianceDtos
{
    public class PoliticAllianceDto
    {
        public int Id { get; set; }
        public int PartidoSolicitanteID { get; set; }
        public int PartidoReceptorID { get; set; }
        public string Estado { get; set; }
        public DateTime FechaSolicitud { get; set; }
    }

    public class CreatePoliticAllianceDto
    {
        [Required] public int PartidoSolicitanteID { get; set; }
        [Required] public int PartidoReceptorID { get; set; }
    }
}

