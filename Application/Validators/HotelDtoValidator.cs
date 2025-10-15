using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class HotelDtoValidator : AbstractValidator<HotelDto>
{
    public HotelDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Назва готелю є обов’язковою.")
            .MaximumLength(100)
            .WithMessage("Назва готелю не може перевищувати 100 символів.");

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage("Адреса є обов’язковою.")
            .MaximumLength(200)
            .WithMessage("Адреса не може перевищувати 200 символів.");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("Опис не може перевищувати 1000 символів.");
    }
}
