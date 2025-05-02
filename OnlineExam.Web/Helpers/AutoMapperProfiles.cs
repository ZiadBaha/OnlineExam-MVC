using AutoMapper;
using OnlineExam.Core.DTOs.Identity;
using OnlineExam.Core.DTOs.Program.ExamDto;
using OnlineExam.Core.Entities;
using OnlineExam.Core.Entities.Identity;
using OnlineExam.Core.Enums;

namespace OnlineExam.Web.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
      
            CreateMap<AddUserDto, AppUser>()
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
               .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "User")) 
               .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone));
            CreateMap<AppUser, AddUserDto>();

            CreateMap<LoginDto, AppUser>();

            CreateMap<AppUser, LoginDto>();

            CreateMap<UpdateUserDto, AppUser>()
               .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))  
               .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
               .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
               .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone));

            CreateMap<ResetPasswordDto, AppUser>();

            CreateMap<UpdateUserDto, AppUser>().ReverseMap();



            CreateMap<AddExamDto, Exam>();
            CreateMap<UpdateExamDto, Exam>();
            CreateMap<Exam, GetExamDto>();

        }
    }
}
