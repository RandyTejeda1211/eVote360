using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ElectionDto
{
    public class ElectionResultDto
    {
        public int ElectionId { get; set; }
        public string ElectionName { get; set; }
        public List<PositionResultDto> PositionResults { get; set; } = new();
    }
}
