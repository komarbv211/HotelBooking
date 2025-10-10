using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<BookingDto, Booking>();
    }

}
