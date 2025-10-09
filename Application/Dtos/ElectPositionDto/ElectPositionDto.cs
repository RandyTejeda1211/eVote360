using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ElectPositionDto
{
    public class ElectPositionDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
    }

    public class CreateElectPositionDto
    {
        [Required] public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class UpdateElectPositionDto
    {
        [Required] public int Id { get; set; }
        [Required] public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
    }

    public class CandPositionDto
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int ElectionPositionId { get; set; }
        public int PartidoPoliticoId { get; set; }
        public bool Estado { get; set; }
    }

    public class CreateCandPositionDto
    {
        [Required] public int CandidateId { get; set; }
        [Required] public int ElectionPositionId { get; set; }
        [Required] public int PartidoPoliticoId { get; set; }
    }
}
