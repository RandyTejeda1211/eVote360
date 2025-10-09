using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.CitizenDto
{
    public class CitizenDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string DocumentNumber { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }


}
