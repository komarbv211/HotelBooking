using Application.DTOs;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Validators;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Ім’я користувача є обов’язковим.")
            .MinimumLength(2)
            .MaximumLength(50)
            .WithMessage("Ім’я користувача має містити від 2 до 50 символів.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Електронна пошта є обов’язковою.")
            .EmailAddress()
            .WithMessage("Некоректний формат електронної пошти.");

        RuleFor(x => x.Password)
             .NotEmpty().WithMessage("Пароль є обов’язковим.")
             .MinimumLength(6).WithMessage("Пароль має містити щонайменше 6 символів.")
             .Must(ContainUppercase).WithMessage("Пароль має містити принаймні одну велику літеру.")
             .Must(ContainLowercase).WithMessage("Пароль має містити принаймні одну малу літеру.")
             .Must(ContainDigit).WithMessage("Пароль має містити принаймні одну цифру.")
             .Must(ContainSpecialChar).WithMessage("Пароль має містити принаймні один спеціальний символ.");

        RuleFor(x => x.BirthDate)
            .LessThan(DateTime.Today)
            .When(x => x.BirthDate.HasValue)
            .WithMessage("Дата народження повинна бути в минулому.");
    }
    private bool ContainUppercase(string password) =>
        Regex.IsMatch(password, "[A-Z]");

    private bool ContainLowercase(string password) =>
        Regex.IsMatch(password, "[a-z]");

    private bool ContainDigit(string password) =>
        Regex.IsMatch(password, "[0-9]");

    private bool ContainSpecialChar(string password) =>
        Regex.IsMatch(password, @"[\W_]");
}
