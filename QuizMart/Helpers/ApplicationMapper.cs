using AutoMapper;
using QuizMart.Models.DomainModels;
using QuizMart.Models.ViewModels;
using BC = global::BCrypt.Net.BCrypt;


namespace QuizMart.Helpers
{
    public class ApplicationMapper : Profile
    {

        public ApplicationMapper()
        {
            // CRUD MAPPING

            // ACCESS MAPPING
            CreateMap<User, UserInfo>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "FreeUser"))
                .ReverseMap();

            CreateMap<SignupModel, User>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => BC.EnhancedHashPassword(src.Password, 13)))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender));
            
            CreateMap<UserInfo, User>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.HomeAddress, opt => opt.MapFrom(src => src.HomeAddress))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender));

            CreateMap<DeckModel, Deck>()
                .ForMember(dest => dest.DeckId, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.HostId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.DeckTitle))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.DeckDescription))
                .ForMember(dest => dest.PublishedAt, opt => opt.MapFrom(src => src.Published))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.ModeratorId, opt => opt.MapFrom(src => src.ModId));

            CreateMap<QuizModel, Quiz>()
                .ForMember(dest => dest.QuizId, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.DeckId, opt => opt.MapFrom(src => src.DeckID))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.QuestionText, opt => opt.MapFrom(src => src.QuestionText))
                .ForMember(dest => dest.Favorite, opt => opt.MapFrom(src => src.isFavorite));

            CreateMap<ChoiceModel, Choice>()
                .ForMember(dest => dest.ChoiceId, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.QuizId, opt => opt.MapFrom(src => src.QuizID))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.IsCorrect, opt => opt.MapFrom(src => src.IsCorrect));
        }
    }
}
