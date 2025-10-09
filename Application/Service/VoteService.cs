using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.CitizenDto;
using Application.Dtos.OCRDto;
using Application.Dtos.VoteDto;
using Application.Interface;
using Domain.Interfaces;
using Infraestructure.Interface;
using Microsoft.AspNetCore.Http;

namespace Application.Service
{
    public class VoteService : IVoteService
    {
        private readonly ICitizenRepository _citizenRepo;
        private readonly IElectionRepository _electionRepo;
        private readonly IVoteRepository _voteRepo;
        private readonly ICandidateRepository _candidateRepo;
        private readonly ICandPositionRepository _candPositionRepo;
        private readonly IOCRService _ocrService;


        public VoteService(ICitizenRepository citizenRepo, 
                         IElectionRepository electionRepo,
                         IVoteRepository voteRepo, 
                         ICandidateRepository candidateRepo,
                         ICandPositionRepository candPositionRepo,
                         IOCRService ocrService)
        {
            _citizenRepo = citizenRepo;
            _electionRepo = electionRepo;
            _voteRepo = voteRepo;
            _candidateRepo = candidateRepo;
            _candPositionRepo = candPositionRepo;
            _ocrService = ocrService;
        }

        public Task<VoteConfirmationDto> GetVoteConfirmationAsync(int citizenId)
        {
            throw new NotImplementedException();
        }

        public async Task<OCRValidationDto> ProcessOCRValidationAsync(string documentNumber, IFormFile cedulaImage)
        {
            try
            {
                // Validar que se haya subido una imagen
                if (cedulaImage == null || cedulaImage.Length == 0)
                {
                    return new OCRValidationDto
                    {
                        IsValid = false,
                        Message = "Debe subir una foto de su cédula"
                    };
                }

                // Validar tipo de archivo
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".bmp" };
                var fileExtension = Path.GetExtension(cedulaImage.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return new OCRValidationDto
                    {
                        IsValid = false,
                        Message = "Solo se permiten archivos JPG, JPEG, PNG o BMP"
                    };
                }

                // Validar tamaño de archivo (máximo 5MB)
                if (cedulaImage.Length > 5 * 1024 * 1024)
                {
                    return new OCRValidationDto
                    {
                        IsValid = false,
                        Message = "La imagen no puede ser mayor a 5MB"
                    };
                }

                // Procesar con OCR Tesseract REAL
                var isValid = await _ocrService.ValidateDocumentNumberAsync(cedulaImage, documentNumber);

                if (isValid)
                {
                    return new OCRValidationDto
                    {
                        IsValid = true,
                        Message = "Validación de identidad exitosa",
                        RedirectUrl = "/Vote/SelectPositions"
                    };
                }
                else
                {
                    return new OCRValidationDto
                    {
                        IsValid = false,
                        Message = "Los datos extraídos de la foto no coinciden con los datos previamente ingresados. Por favor, suba una nueva imagen de la cédula."
                    };
                }
            }
            catch (Exception ex)
            {
                return new OCRValidationDto
                {
                    IsValid = false,
                    Message = $"Error procesando la cédula: {ex.Message}"
                };
            }
        }



        public Task<bool> RegisterVoteAsync(VoteRequestDto voteRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<CitizenValidationDto> ValidateCitizenForVoting(string documentNumber)
        {
            try
            {
                var activeElection = await _electionRepo.GetActiveElectionAsync();
                if (activeElection == null)
                    return new CitizenValidationDto
                    {
                        CanVote = false,
                        Message = "No hay ningún proceso electoral en estos momentos"
                    };

                var citizen = await _citizenRepo.GetByDocumentNumberAsync(documentNumber);
                if (citizen == null)
                    return new CitizenValidationDto
                    {
                        CanVote = false,
                        Message = "Ciudadano no encontrado"
                    };

                if (!citizen.state)
                    return new CitizenValidationDto
                    {
                        CanVote = false,
                        Message = "Ciudadano inactivo"
                    };

                if (await _citizenRepo.HasVotedInElectionAsync(citizen.Id, activeElection.Id))
                    return new CitizenValidationDto
                    {
                        CanVote = false,
                        Message = "Ya ha ejercido su derecho al voto"
                    };

                return new CitizenValidationDto
                {
                    CanVote = true,
                    CitizenId = citizen.Id,
                    CitizenName = $"{citizen.Name} {citizen.LastName}",
                    Message = "Puede proceder con la validación de identidad"
                };
            }
            catch (Exception ex)
            {
                return new CitizenValidationDto
                {
                    CanVote = false,
                    Message = $"Error validando ciudadano: {ex.Message}"
                };
            }
        }

        Task<bool> IVoteService.ProcessOCRValidationAsync(string documentNumber, IFormFile cedulaImage)
        {
            throw new NotImplementedException();
        }
    }
}
