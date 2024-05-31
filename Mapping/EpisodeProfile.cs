using AutoMapper;
using Api.DTOs;
using Api.Models;

namespace Api.Mapping
{
    public class EpisodeProfile : Profile
    {
        public EpisodeProfile()
        {
            CreateMap<Episode, EpisodeDTO>().ReverseMap();
        }
    }
}