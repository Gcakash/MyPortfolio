using AutoMapper;
using Portfolio.API.Common.Models;
using Portfolio.API.Models;

namespace Portfolio.API.Common.Mapper
{
    public class WorkExperienceMapper : Profile
    {
        public WorkExperienceMapper()
        {
            CreateMap<WorkExperience, WorkExperienceModel>().ReverseMap();
        }
    }
}
