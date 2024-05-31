using AutoMapper;
using Api.DTOs;
using Api.Models;

namespace Api.Mapping
{
    public class TitleProfile : Profile
    {
        public TitleProfile()
        {
            CreateMap<Title, TitleDTO>().ReverseMap();
        }
    }
}