using AutoMapper;
using Portfolio.API.Common.Models;
using Portfolio.API.Models;

namespace Portfolio.API.Common.Mapper
{
    public class ContactMapper : Profile
    {
        public ContactMapper()
        {
            CreateMap<Contact, ContactModel>().ReverseMap();
        }
    }
}
