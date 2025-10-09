using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ElectionDto
{
    public class CandidateResultDto
    {
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
        public string PartyName { get; set; }
        public int Votes { get; set; }
        public decimal Percentage { get; set; }
    }
}
