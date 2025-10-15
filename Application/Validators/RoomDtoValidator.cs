using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class RoomDtoValidator : AbstractValidator<RoomDto>
{
    public RoomDtoValidator()
    {
        RuleFor(x => x.HotelId)
            .GreaterThan(0)
            .WithMessage("Некоректний ідентифікатор готелю.");

        RuleFor(x => x.Number)
            .NotEmpty()
            .WithMessage("Номер кімнати є обов’язковим.")
            .MaximumLength(20)
            .WithMessage("Номер кімнати не може перевищувати 10 символів.");

        RuleFor(x => x.PricePerNight)
            .GreaterThan(0)
            .WithMessage("Ціна за ніч повинна бути більшою за 0.");

        RuleFor(x => x.Capacity)
            .InclusiveBetween(1, 10)
            .WithMessage("Місткість повинна бути від 1 до 10 осіб.");
    }
}
