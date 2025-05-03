using AutoMapper;
using OnlineExam.Core.DTOs.Identity;
using OnlineExam.Core.DTOs.Program;
using OnlineExam.Core.DTOs.Program.ChoiceDto;
using OnlineExam.Core.DTOs.Program.ExamDto;
using OnlineExam.Core.DTOs.Program.questionDto;
using OnlineExam.Core.DTOs.Program.ResultDto;
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


            CreateMap<Exam, GetExamDto>()
                .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => src.Difficulty.ToString())); // Converting enum to string for display
            CreateMap<AddExamDto, Exam>()
                .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => Enum.Parse<ExamDifficulty>(src.Difficulty.ToString()))); // Converting string to enum
            CreateMap<UpdateExamDto, Exam>()
                .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => Enum.Parse<ExamDifficulty>(src.Difficulty.ToString())));

            CreateMap<Choice, choiceDto>();
            CreateMap<ChoiceCreateDto, Choice>();
            CreateMap<ChoiceUpdateDto, Choice>();

            CreateMap<ExamResult, ExamResultDto>();
            CreateMap<ExamResultCreateDto, ExamResult>();
            CreateMap<UpdateExamResultDto, ExamResult>();
      
            CreateMap<Question, QuestionDto>();
            CreateMap<QuestionCreateDto, Question>();
            CreateMap<QuestionUpdateDto, Question>();

            CreateMap<SubmitExamDto, ExamResultCreateDto>()
             .ForMember(dest => dest.CorrectAnswers, opt => opt.MapFrom(src => src.AnswerIds.Count(a => a == 1))) 
             .ForMember(dest => dest.TotalQuestions, opt => opt.MapFrom(src => src.AnswerIds.Count))
             .ForMember(dest => dest.Score, opt => opt.MapFrom(src => (int)((src.AnswerIds.Count(a => a == 1) / (double)src.AnswerIds.Count) * 100)))
             .ForMember(dest => dest.IsPassed, opt => opt.MapFrom(src => (int)((src.AnswerIds.Count(a => a == 1) / (double)src.AnswerIds.Count) * 100) >= 60));


        }
    }
}
