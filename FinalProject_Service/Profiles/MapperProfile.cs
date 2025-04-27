using AutoMapper;
using FinalProject_Core.Models;
using FinalProject_Service.Dto.ActorDtos;
using FinalProject_Service.Dto.BasketDtos;
using FinalProject_Service.Dto.DirectorDtos;
using FinalProject_Service.Dto.EpisodeDtos;
using FinalProject_Service.Dto.GenreDtos;
using FinalProject_Service.Dto.LanguageDtos;
using FinalProject_Service.Dto.MovieDtos;
using FinalProject_Service.Dto.ProductDtos;
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
            CreateMap<ActorCreateDto, Actor>();
            CreateMap<Actor, ActorReturnDto>();
            CreateMap<Actor, ActorUpdateDto>();
            CreateMap<ActorUpdateDto, Actor>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Actor, ActorDeleteDto>();
            CreateMap<ActorDeleteDto, Actor>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());


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


            CreateMap<DirectorCreateDto, Director>();
            CreateMap<Director, DirectorReturnDto>();
            CreateMap<Director, DirectorUpdateDto>();
            CreateMap<DirectorUpdateDto, Director>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Director, DirectorDeleteDto>();
            CreateMap<DirectorDeleteDto, Director>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());


            CreateMap<MovieCreateDto, Movie>()
                .ForMember(dest => dest.MovieGenres, opt => opt.MapFrom(src => new List<MovieGenre>()))
            .ForMember(dest => dest.MovieLanguages, opt => opt.MapFrom(src => new List<MovieLanguage>()))
            .ForMember(dest => dest.MovieActors, opt => opt.MapFrom(src => new List<MovieActor>()));
            CreateMap<Movie, MovieCreateDto>();
            CreateMap<Movie, MovieUpdateDto>()
                .ForMember(dest => dest.GenreIds, opt => opt.MapFrom(src => src.MovieGenres.Select(x => x.GenreId)))
                .ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.MovieLanguages.Select(x => x.LanguageId)))
                .ForMember(dest => dest.ActorId, opt => opt.MapFrom(src => src.MovieActors.Select(x => x.ActorId)));
            CreateMap<MovieUpdateDto, Movie>()
                .ForMember(dest => dest.MovieGenres, opt => opt.MapFrom(src => new List<MovieGenre>()))
            .ForMember(dest => dest.MovieLanguages, opt => opt.MapFrom(src => new List<MovieLanguage>()))
            .ForMember(dest => dest.MovieActors, opt => opt.MapFrom(src => new List<MovieActor>()))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Movie, MovieDeleteDto>();
            CreateMap<MovieDeleteDto, Movie>()
                .ForMember(dest => dest.MovieGenres, opt => opt.MapFrom(src => new List<MovieGenre>()))
            .ForMember(dest => dest.MovieLanguages, opt => opt.MapFrom(src => new List<MovieLanguage>()))
            .ForMember(dest => dest.MovieActors, opt => opt.MapFrom(src => new List<MovieActor>()))
                .ForMember(dest => dest.Id, opt => opt.Ignore());


            CreateMap<TVShowCreateDto, TVShow>()
                .ForMember(dest => dest.TVShowGenres, opt => opt.MapFrom(src => new List<TVShowGenre>()))
            .ForMember(dest => dest.TVShowLanguages, opt => opt.MapFrom(src => new List<TVShowLanguage>()))
            .ForMember(dest => dest.TVShowActors, opt => opt.MapFrom(src => new List<TVShowActor>()));
            CreateMap<TVShow, TVShowUpdateDto>()
                .ForMember(dest => dest.GenreIds, opt => opt.MapFrom(src => src.TVShowGenres.Select(x => x.GenreId)))
                .ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.TVShowLanguages.Select(x => x.LanguageId)))
                .ForMember(dest => dest.ActorId, opt => opt.MapFrom(src => src.TVShowActors.Select(x => x.ActorId))); ;
            CreateMap<TVShowUpdateDto, TVShow>()
                .ForMember(dest => dest.TVShowGenres, opt => opt.MapFrom(src => new List<TVShowGenre>()))
            .ForMember(dest => dest.TVShowLanguages, opt => opt.MapFrom(src => new List<TVShowLanguage>()))
            .ForMember(dest => dest.TVShowActors, opt => opt.MapFrom(src => new List<TVShowActor>()))
            .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<TVShow, TVShowDeleteDto>();
            CreateMap<TVShowDeleteDto, TVShow>()
            .ForMember(dest => dest.TVShowGenres, opt => opt.MapFrom(src => new List<TVShowGenre>()))
            .ForMember(dest => dest.TVShowLanguages, opt => opt.MapFrom(src => new List<TVShowLanguage>()))
            .ForMember(dest => dest.TVShowActors, opt => opt.MapFrom(src => new List<TVShowActor>()))
            .ForMember(dest => dest.Id, opt => opt.Ignore()); 


            CreateMap<SeasonCreateDto, Season>();
            CreateMap<Season, SeasonCreateDto>();
            CreateMap<Season, SeasonDeleteDto>();
            CreateMap<SeasonDeleteDto, Season>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); 
            CreateMap<Season, SeasonUpdateDto>();
            CreateMap<SeasonUpdateDto, Season>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());


            CreateMap<EpisodeCreateDto, Episode>();
            CreateMap<Episode, EpisodeCreateDto>();
            CreateMap<Episode, EpisodeDeleteDto>();
            CreateMap<EpisodeDeleteDto, Episode>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); 
            CreateMap<Episode, EpisodeUpdateDto>();
            CreateMap<EpisodeUpdateDto, Episode>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());


            CreateMap<ProductCreateDto, Product>()
                .ForMember(dest => dest.MovieProducts, opt => opt.Ignore())
                .ForMember(dest => dest.tvShowProducts, opt => opt.Ignore());
            CreateMap<Product, ProductUpdateDto>();
            CreateMap<ProductUpdateDto, Product>()
                .ForMember(dest => dest.MovieProducts, opt => opt.Ignore())
    .ForMember(dest => dest.tvShowProducts, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Product, ProductDeleteDto>();
            CreateMap<ProductDeleteDto, Product>()
                .ForMember(dest => dest.MovieProducts, opt => opt.MapFrom(src => new List<MovieProduct>()))
            .ForMember(dest => dest.tvShowProducts, opt => opt.MapFrom(src => new List<TvShowProduct>()))
            .ForMember(dest => dest.Id, opt => opt.Ignore());


            CreateMap<Product, BasketItemDto>()
           .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.CostPrice))
           .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));

            CreateMap<BasketItem, CheckoutItemDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.TotalItemPrice, opt => opt.MapFrom(src => src.Count * src.Product.CostPrice))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count));

            CreateMap<OrderDto, Order>();

        }
    }
}
