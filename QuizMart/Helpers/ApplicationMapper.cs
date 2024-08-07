﻿using AutoMapper;
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
                .ReverseMap();

            CreateMap<UpdateProfileVM, User>();

            CreateMap<SignupModel, User>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => BC.EnhancedHashPassword(src.Password, 13)))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender));


            CreateMap<AddDeckVM, Deck>()
                .ForMember(dest => dest.DeckId, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Quizzes, opt => opt.MapFrom(src => src.Quizzes));

            // Mapping for Quiz
            CreateMap<AddQuizVM, Quiz>()
                .ForMember(dest => dest.QuizId, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.QuestionText, opt => opt.MapFrom(src => src.QuestionText))
                .ForMember(dest => dest.Favorite, opt => opt.MapFrom(src => src.Favorite))
                .ForMember(dest => dest.Choices, opt => opt.MapFrom(src => src.Choices));

            // Mapping for Choice
            CreateMap<AddChoiceVM, Choice>()
                .ForMember(dest => dest.ChoiceId, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.IsCorrect, opt => opt.MapFrom(src => src.IsCorrect));

            CreateMap<EditDeckVM, Deck>()
                .ForMember(dest => dest.DeckId, opt => opt.MapFrom(src => src.DeckId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Quizzes, opt => opt.MapFrom(src => src.Quizzes));

            // Mapping for Quiz
            CreateMap<EditQuizVM, Quiz>()
                .ForMember(dest => dest.QuizId, opt => opt.MapFrom(src => src.QuizId))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.QuestionText, opt => opt.MapFrom(src => src.QuestionText))
                .ForMember(dest => dest.Favorite, opt => opt.MapFrom(src => src.Favorite))
                .ForMember(dest => dest.Choices, opt => opt.MapFrom(src => src.Choices));

            // Mapping for Choice
            CreateMap<EditChoiceVM, Choice>()
                .ForMember(dest => dest.ChoiceId, opt => opt.MapFrom(src => src.ChoiceId))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.IsCorrect, opt => opt.MapFrom(src => src.IsCorrect));



            CreateMap<Deck, DeckModel>()
                .ForMember(dest => dest.DeckId, opt => opt.MapFrom(src => src.DeckId))
                .ForMember(dest => dest.HostId, opt => opt.MapFrom(src => src.HostId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.PublishedAt, opt => opt.MapFrom(src => src.PublishedAt))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.ModeratorId, opt => opt.MapFrom(src => src.ModeratorId))
                .ForMember(dest => dest.Quizzes, opt => opt.MapFrom(src => src.Quizzes));

            CreateMap<QuizModel, Quiz>()
                .ForMember(dest => dest.QuizId, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.DeckId, opt => opt.MapFrom(src => src.DeckId))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.QuestionText, opt => opt.MapFrom(src => src.QuestionText))
                .ForMember(dest => dest.Favorite, opt => opt.MapFrom(src => src.Favorite))
                .ForMember(dest => dest.Choices, opt => opt.MapFrom(src => src.Choices));

            CreateMap<ChoiceModel, Choice>()
                .ForMember(dest => dest.ChoiceId, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.IsCorrect, opt => opt.MapFrom(src => src.IsCorrect))
                .ForMember(dest => dest.QuizId, opt => opt.MapFrom(src => src.QuizId));
            // Mapping from Domain Model to ViewModel
            CreateMap<Quiz, QuizModel>()
                .ForMember(dest => dest.QuizId, opt => opt.MapFrom(src => src.QuizId))
                .ForMember(dest => dest.DeckId, opt => opt.MapFrom(src => src.DeckId))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.QuestionText, opt => opt.MapFrom(src => src.QuestionText))
                .ForMember(dest => dest.Favorite, opt => opt.MapFrom(src => src.Favorite))
                .ForMember(dest => dest.Choices, opt => opt.MapFrom(src => src.Choices));

            CreateMap<Choice, ChoiceModel>()
                .ForMember(dest => dest.ChoiceId, opt => opt.MapFrom(src => src.ChoiceId))
                .ForMember(dest => dest.QuizId, opt => opt.MapFrom(src => src.QuizId))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.IsCorrect, opt => opt.MapFrom(src => src.IsCorrect));

            CreateMap<UserInfo, User>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.HomeAddress, opt => opt.MapFrom(src => src.HomeAddress))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender));
        }
    }
}
