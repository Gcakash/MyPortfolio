using AutoMapper;
using Portfolio.API.Common.Models;
using Portfolio.API.Models;

namespace Portfolio.API.Common.Mapper
{
    public class FeedbackMapper : Profile
    {
        public FeedbackMapper()
        {
            CreateMap<Feedback, FeedbackModel>().ReverseMap();
        }
    }
}
