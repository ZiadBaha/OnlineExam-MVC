using AutoMapper;
using OnlineExam.Core.DTOs.Identity;
using OnlineExam.Core.Entities.Identity;
using OnlineExam.Core.Enums;

namespace OnlineExam.Web.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // AddUserDto -> AppUser (Register User)
            CreateMap<AddUserDto, AppUser>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.Parse<UserRoles>(src.Role, true))) // Mapping Role
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)); // Email as UserName for Identity

            // AppUser -> AddUserDto (To return AppUser details after registration)
            CreateMap<AppUser, AddUserDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString())); // Map enum to string

            // LoginDto -> AppUser (For Login)
            CreateMap<LoginDto, AppUser>();

            // AppUser -> LoginDto (For returning user data after successful login)
            CreateMap<AppUser, LoginDto>();

            // ResetPasswordDto -> AppUser (For Resetting Password)
            CreateMap<ResetPasswordDto, AppUser>();

            // AppUser -> UpdateUserDto (To return AppUser after updating data)
            CreateMap<AppUser, UpdateUserDto>();

            // UpdateUserDto -> AppUser (For updating user data)
            CreateMap<UpdateUserDto, AppUser>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.Parse<UserRoles>(src.Role, true)));

        }
    }
}
