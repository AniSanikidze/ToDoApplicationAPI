using FluentValidation;
using ToDoApp.Application.Subtasks.Requests;
using ToDoApp.Application.Users.Requests;

namespace ToDoApp.API.Infrastructure.Validators
{
    public class UserRequestValidator : AbstractValidator<UserRequestModel>
    {
        public UserRequestValidator()
        {
            RuleFor(x => x.Username)
            .Must(x => x.Length >= 1 && x.Length <= 50)
            .WithMessage("Title Length should be between 1 and 50");

            RuleFor(x => x.Username)
            .Must(x => x.Length >= 1)
            .WithMessage("Password cannot be empty");
        }
    }
}
