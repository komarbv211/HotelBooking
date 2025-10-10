using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class CreateBookingDtoValidator : AbstractValidator<CreateBookingDto>
{
    public CreateBookingDtoValidator()
    {
        RuleFor(x => x.RoomId)
            .GreaterThan(0)
            .WithMessage("Невірний ідентифікатор кімнати.");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("Ідентифікатор користувача є обов'язковим.");

        RuleFor(x => x.CheckIn)
            .NotEmpty()
            .WithMessage("Дата заїзду є обов'язковою.")
            .Must(BeAFutureDate)
            .WithMessage("Дата заїзду повинна бути у майбутньому.");

        RuleFor(x => x.CheckOut)
            .NotEmpty()
            .WithMessage("Дата виїзду є обов'язковою.")
            .Must(BeAFutureDate)
            .WithMessage("Дата виїзду повинна бути у майбутньому.");

        RuleFor(x => x)
            .Must(x => x.CheckOut > x.CheckIn)
            .WithMessage("Дата виїзду повинна бути пізніше дати заїзду.");
    }

    private bool BeAFutureDate(DateTime date)
    {
        return date.Date >= DateTime.Today;
    }
}
