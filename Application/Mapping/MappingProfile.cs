using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Bid;
using Application.DTOs.User;
using Application.Features.Users.Commands.LoginUser;
using Application.Features.Users.Commands.RegisterUser;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserCommand, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => UserRole.User));

            CreateMap<UserRegistrationDto, RegisterUserCommand>();
            CreateMap<UserLoginDto, LoginUserCommand>();

            // Queries
            CreateMap<Bid, BidDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Bidder.Username));

            CreateMap<Bid, UserBidDto>()
                .ForMember(dest => dest.ListingTitle, opt => opt.MapFrom(src => src.Listing.Title));

            // Commands
            CreateMap<Bid, BidCreatedDto>();
            CreateMap<Bid, BidRetractedDto>();
        }
    }
}
