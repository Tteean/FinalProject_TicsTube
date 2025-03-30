using AutoMapper;
using FinalProject_Core.Models;
using FinalProject_Service.Dto.ActorDtos;
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
                .ForMember(s => s.Image, opt => opt.MapFrom(src => src.File.SaveImage("uploads")));
            CreateMap<Actor, ActorReturnDto>();
        }
    }
}
