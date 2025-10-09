using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.UserDto
{
    public class UpdateUserDto
    {
        [Required] public int Id { get; set; }
        [Required] public string Nombre { get; set; }
        [Required] public string Apellido { get; set; }
        [Required] public string Email { get; set; }
        public string Password { get; set; }
        [Required] public string Rol { get; set; }
        public bool Estado { get; set; }
    }
}
