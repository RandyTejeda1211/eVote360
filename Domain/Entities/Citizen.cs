using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Citizen
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Required]
        [StringLength(100)]
        public required string LastName { get; set; }


        [Required]
        [StringLength(100)]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(100)]
        public required string DocumentNumber { get; set; }
        public bool state { get; set; }

        public ICollection<Vote> Votes { get; set; } = [];
    }
}
