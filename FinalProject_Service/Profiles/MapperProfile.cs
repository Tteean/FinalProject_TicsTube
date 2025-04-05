using AutoMapper;
using FinalProject_Core.Models;
using FinalProject_Service.Dto.ActorDtos;
using FinalProject_Service.Dto.DirectorDtos;
using FinalProject_Service.Dto.GenreDtos;
using FinalProject_Service.Dto.LanguageDtos;
using FinalProject_Service.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinalProject_Service.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ActorCreateDto, Actor>()
                .ForMember(s => s.Image, opt => opt.MapFrom(src => src.File.SaveImage("uploads/actors")));
            CreateMap<Actor, ActorReturnDto>();
            CreateMap<Actor, ActorUpdateDto>();
            CreateMap<ActorUpdateDto, Actor>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(s => s.Image, opt => opt.MapFrom(src => src.File.SaveImage("uploads/actors")));
            CreateMap<Actor, ActorDeleteDto>();
            CreateMap<ActorDeleteDto, Actor>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(s => s.Image, opt => opt.MapFrom(src => src.File.SaveImage("uploads/actors")));


            CreateMap<CreateGenreDto, Genre>();
            CreateMap<Genre, GenreReturnDto>();
            CreateMap<Genre, UpdateGenreDto>();
            CreateMap<UpdateGenreDto, Genre>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Genre, DeleteGenreDto>();
            CreateMap<DeleteGenreDto, Genre>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<LanguageCreateDto, Language>();
            CreateMap<Language, LanguageReturnDto>();
            CreateMap<Language, LanguageUpdateDto>();
            CreateMap<LanguageUpdateDto, Language>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Language, LanguageDeleteDto>();
            CreateMap<LanguageDeleteDto, Language>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<DirectorCreateDto, Director>()
                .ForMember(s => s.Image, opt => opt.MapFrom(src => src.File.SaveImage("uploads/directors")));
            CreateMap<Director, DirectorReturnDto>();
            CreateMap<Director, DirectorUpdateDto>();
            CreateMap<DirectorUpdateDto, Director>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(s => s.Image, opt => opt.MapFrom(src => src.File.SaveImage("uploads/directors")));
            CreateMap<Director, DirectorDeleteDto>();
            CreateMap<DirectorDeleteDto, Director>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(s => s.Image, opt => opt.MapFrom(src => src.File.SaveImage("uploads/directors")));
        }
    }
}
