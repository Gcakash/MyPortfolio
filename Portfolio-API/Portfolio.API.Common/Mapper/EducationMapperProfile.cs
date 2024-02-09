using AutoMapper;
using Portfolio.API.Common.Models;
using Portfolio.API.Models;

namespace Portfolio.API.Common.Mapper
{
    public class EducationMapper : Profile
    {
        public EducationMapper()
        {
            CreateMap<Education, EducationModel>().ReverseMap();
        }
    }
}
