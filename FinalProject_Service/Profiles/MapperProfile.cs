using AutoMapper;
using FinalProject_Core.Models;
using FinalProject_Service.Dto.ActorDtos;
using FinalProject_Service.Dto.DirectorDtos;
using FinalProject_Service.Dto.EpisodeDtos;
using FinalProject_Service.Dto.GenreDtos;
using FinalProject_Service.Dto.LanguageDtos;
using FinalProject_Service.Dto.MovieDtos;
using FinalProject_Service.Dto.SeasonDtos;
using FinalProject_Service.Dto.TVShowDtos;
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

            CreateMap<MovieCreateDto, Movie>()
                .ForMember(dest => dest.MovieGenres, opt => opt.MapFrom(src => new List<MovieGenre>()))
            .ForMember(dest => dest.MovieLanguages, opt => opt.MapFrom(src => new List<MovieLanguage>()))
            .ForMember(dest => dest.MovieActors, opt => opt.MapFrom(src => new List<MovieActor>()))
            .ForMember(s => s.Image, opt => opt.MapFrom(src => src.File.SaveImage("uploads/movies")))
            .ForMember(s => s.Video, opt => opt.MapFrom(src => src.Film.SaveImage("uploads/movies")));
            CreateMap<Movie, MovieCreateDto>();
            CreateMap<Movie, MovieUpdateDto>();
            CreateMap<MovieUpdateDto, Movie>()
                .ForMember(dest => dest.MovieGenres, opt => opt.MapFrom(src => new List<MovieGenre>()))
            .ForMember(dest => dest.MovieLanguages, opt => opt.MapFrom(src => new List<MovieLanguage>()))
            .ForMember(dest => dest.MovieActors, opt => opt.MapFrom(src => new List<MovieActor>()))
                .ForMember(s => s.Image, opt => opt.MapFrom(src => src.File.SaveImage("uploads/movies")))
                .ForMember(s => s.Video, opt => opt.MapFrom(src => src.Film.SaveImage("uploads/movies")))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Movie, MovieDeleteDto>();
            CreateMap<MovieDeleteDto, Movie>()
                .ForMember(dest => dest.MovieGenres, opt => opt.MapFrom(src => new List<MovieGenre>()))
            .ForMember(dest => dest.MovieLanguages, opt => opt.MapFrom(src => new List<MovieLanguage>()))
            .ForMember(dest => dest.MovieActors, opt => opt.MapFrom(src => new List<MovieActor>()))
                .ForMember(s => s.Image, opt => opt.MapFrom(src => src.File.SaveImage("uploads/movies")))
                .ForMember(s => s.Video, opt => opt.MapFrom(src => src.Film.SaveImage("uploads/movies")))
                .ForMember(dest => dest.Id, opt => opt.Ignore());


            CreateMap<TVShowCreateDto, TVShow>()
                .ForMember(dest => dest.TVShowGenres, opt => opt.MapFrom(src => new List<TVShowGenre>()))
            .ForMember(dest => dest.TVShowLanguages, opt => opt.MapFrom(src => new List<TVShowLanguage>()))
            .ForMember(dest => dest.TVShowActors, opt => opt.MapFrom(src => new List<TVShowActor>()))
            .ForMember(s => s.Image, opt => opt.MapFrom(src => src.File.SaveImage("uploads/movies")));
            CreateMap<SeasonCreateDto, Season>();
            CreateMap<EpisodeCreateDto, Episode>();
        }
    }
}
