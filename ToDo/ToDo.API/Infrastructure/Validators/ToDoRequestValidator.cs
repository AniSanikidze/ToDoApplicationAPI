using FluentValidation;
using ToDoApp.Application.ToDos.Requests;

namespace ToDoApp.API.Infrastructure.Validators
{
    public class ToDoRequestValidator : AbstractValidator<ToDoRequestModel>
    {
        public ToDoRequestValidator()
        {
            RuleFor(x => x.Title)
                .Must(x => x.Length > 2 && x.Length <= 100)
                .WithMessage("Title Length should be between 2 and 100");
        }
    }
}
