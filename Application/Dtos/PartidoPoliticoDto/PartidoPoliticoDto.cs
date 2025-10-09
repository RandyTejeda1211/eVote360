using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.PartidoPoliticoDto
{
    public class PartidoPoliticoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Siglas { get; set; }
        public string LogoPath { get; set; }
        public bool Estado { get; set; }
    }

    public class CreatePartidoPoliticoDto
    {
        [Required] public string Nombre { get; set; }
        public string Descripcion { get; set; }
        [Required] public string Siglas { get; set; }
        public string LogoPath { get; set; }
    }

    public class UpdatePartidoPoliticoDto
    {
        [Required] public int Id { get; set; }
        [Required] public string Nombre { get; set; }
        public string Descripcion { get; set; }
        [Required] public string Siglas { get; set; }
        public string LogoPath { get; set; }
        public bool Estado { get; set; }
    }
}
