using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.CitizenDto;
using Application.Dtos.OCRDto;
using Application.Dtos.VoteDto;
using Microsoft.AspNetCore.Http;

namespace Application.Interface
{
    public interface IVoteService
    {
        Task<CitizenValidationDto> ValidateCitizenForVoting(string documentNumber);
        Task<OCRValidationDto> ProcessOCRValidationAsync(string documentNumber, IFormFile cedulaImage);
        Task<bool> RegisterVoteAsync(VoteRequestDto voteRequest);
        Task<VoteConfirmationDto> GetVoteConfirmationAsync(int citizenId);
        Task SendVoteConfirmationEmailAsync(int citizenId);
        Task<List<PositionCandidatesDto>> GetPositionsWithCandidatesAsync();
    }
}
