using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.VoteDto
{
    public class VoteDto
    {
        public int Id { get; set; }
        public int CitizenId { get; set; }
        public int ElectionId { get; set; }
        public int CandidatoPuestoID { get; set; }
        public DateTime DateVote { get; set; }
    }

    public class VoteRequestDto
    {
        [Required] public string DocumentNumber { get; set; }
        [Required] public List<PositionVoteDto> PositionVotes { get; set; } = new();
    }

    public class PositionVoteDto
    {
        [Required] public int PositionId { get; set; }
        [Required] public int CandidateId { get; set; }
    }

    public class VoteConfirmationDto
    {
        public string CitizenName { get; set; }
        public string ElectionName { get; set; }
        public DateTime VoteDate { get; set; }
    }


}
