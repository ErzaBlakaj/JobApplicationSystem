using AutoMapper;
using ResumeModuleApp.DTOs;
using ResumeModuleApp.Models;

namespace ResumeModuleApp
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<ResumeDTO, Resume>();
            CreateMap<UserDTO, User>();
        }
    }
}
