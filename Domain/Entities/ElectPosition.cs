using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ElectPosition
    {
        public int Id{ get; set; }

        [Required]
        [StringLength(100)]
        public string Name{ get; set; }
        
        [StringLength(500)]
        public string Description{ get; set; }
        public bool state{ get; set; }

        public ICollection<CandPosition> CandPositions { get; set; } = [];
        public ICollection<EleccionPuesto> EleccionPuestos { get; set; } = [];
         
    }
}
