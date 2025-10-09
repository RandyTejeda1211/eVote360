using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.CitizenDto
{
    public class CitizenValidationDto
    {
        public bool CanVote { get; set; }
        public string Message { get; set; }
        public int? CitizenId { get; set; }
        public string CitizenName { get; set; }
    }
}
