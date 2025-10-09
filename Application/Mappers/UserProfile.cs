using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterDto, User>();
    }
}
