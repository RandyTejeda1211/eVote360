using Application.Dtos.AllianceDtos;
using Application.Dtos.CandidateDtos;
using Application.Dtos.CitizenDto;
using Application.Dtos.DirigenteDtos;
using Application.Dtos.ElectionDto;
using Application.Dtos.ElectPositionDto;
using Application.Dtos.PartidoPoliticoDto;
using Application.Dtos.UserDto;
using Application.Dtos.VoteDto;

using AutoMapper;
using Domain.Entities;
using static System.Collections.Specialized.BitVector32;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Citizen
            CreateMap<Citizen, CitizenDto>();
            CreateMap<CreateCitizenDto, Citizen>();
            CreateMap<UpdateCitizenDto, Citizen>();

            // Election
            CreateMap<Elections, ElectionDto>();
            CreateMap<CreateElectionDto, Elections>();

            // User
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();

            // Candidate
            CreateMap<Candidate, CandidateDto>();
            CreateMap<CreateCandidateDto, Candidate>();
            CreateMap<UpdateCandidateDto, Candidate>();

            // PartidoPolitico
            CreateMap<PartidoPolitico, PartidoPoliticoDto>();
            CreateMap<CreatePartidoPoliticoDto, PartidoPolitico>();
            CreateMap<UpdatePartidoPoliticoDto, PartidoPolitico>();

            // ElectPosition
            CreateMap<ElectPosition, ElectPositionDto>();
            CreateMap<CreateElectPositionDto, ElectPosition>();
            CreateMap<UpdateElectPositionDto, ElectPosition>();

            // Vote
            CreateMap<Vote, VoteDto>();

            // CandPosition
            CreateMap<CandPosition, CandPositionDto>();
            CreateMap<CreateCandPositionDto, CandPosition>();

            // PoliticAlliance
            CreateMap<PoliticAlliance, PoliticAllianceDto>();
            CreateMap<CreatePoliticAllianceDto, PoliticAlliance>();

            // DirigentePartido
            CreateMap<DirigentePartido, DirigentePartidoDto>();
            CreateMap<CreateDirigentePartidoDto, DirigentePartido>();

            // EleccionPuesto
            CreateMap<EleccionPuesto, ElectPositionDto>();
           
        }
    }
}