using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ElectionDto
{
    public class PositionResultDto
    {
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public List<CandidateResultDto> CandidateResults { get; set; } = new();
        public int TotalVotos { get; set; }
    }
}
