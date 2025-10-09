using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.DirigenteDtos
{
    public class DirigentePartidoDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PartidoPoliticoId { get; set; }
        public bool Estado { get; set; }
    }

    public class CreateDirigentePartidoDto
    {
        [Required] public int UserId { get; set; }
        [Required] public int PartidoPoliticoId { get; set; }
    }
}
