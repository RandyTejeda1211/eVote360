using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EleccionPuesto 
    {
        public int Id { get; set; }

        [Required]
        public int EleccionID { get; set; }

        [Required]
        public int PuestoElectivoID { get; set; }

        public Elections Elections { get; set; }
        public ElectPosition ElectPosition { get; set; }
    }
}
