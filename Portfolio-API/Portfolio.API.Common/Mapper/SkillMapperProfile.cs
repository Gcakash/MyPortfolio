﻿using AutoMapper;
using Portfolio.API.Common.Models;
using Portfolio.API.Models;

namespace Portfolio.API.Common.Mapper
{
    public class SkillMapper : Profile
    {
        public SkillMapper()
        {
            CreateMap<Skill, SkillModel>().ReverseMap();
        }
    }
}
