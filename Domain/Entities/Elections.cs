using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Elections
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime DateRealization { get; set; } = DateTime.Now;

        [Required]
        [StringLength(20)]
        public string State { get; set; }

        public ICollection<Vote> Votes { get; set; } = [];
        public ICollection<EleccionPuesto>  EleccionPuestos { get; set; } = [];
        
    }
}
