using AutoMapper;
using ResumeModuleApp.DTOs;
using ResumeModuleApp.Models;

namespace ResumeModuleApp
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<UserDTO, User>();
            CreateMap<SkillsDTO, Skills>();
            CreateMap<ExperienceDTO, Experience>();
            CreateMap<ResumeDTO, Resume>();
        }
    }
}
