using FluentValidation;
using RestAirline.Domain.Booking;

namespace RestAirline.CommandHandlers.Passenger
{
    public class AddPassengerCommandValidator : AbstractValidator<AddPassengerCommand>
    {
        public AddPassengerCommandValidator()
        {
            RuleFor(x => x.Age)
                .InclusiveBetween(1, 100)
                .WithMessage("Age must between 15 to 100")
                .When(x => x.PassengerType != PassengerType.Infant);

            RuleFor(x => x.Age)
                .LessThan(1)
                .WithMessage("Infant must less 1")
                .When(x => x.PassengerType == PassengerType.Infant);

            RuleFor(x => x.Name)
                .Length(2, 50).WithMessage("Name can not be empty");

            RuleFor(x => x.Email).EmailAddress();
        }
    }
}