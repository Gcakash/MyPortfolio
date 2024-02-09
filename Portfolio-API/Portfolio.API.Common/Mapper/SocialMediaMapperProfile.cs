using AutoMapper;
using Portfolio.API.Common.Models;
using Portfolio.API.Models;

namespace Portfolio.API.Common.Mapper
{
    public class SocialMediaMapper : Profile
    {
        public SocialMediaMapper()
        {
            CreateMap<SocialMedia, SocialMediaModel>().ReverseMap();
        }
    }
}
