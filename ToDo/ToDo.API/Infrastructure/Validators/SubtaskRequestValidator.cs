using FluentValidation;
using System.Diagnostics.Metrics;
using ToDoApp.Application.Subtasks.Requests;

namespace ToDoApp.API.Infrastructure.Validators
{
    public class SubtaskRequestValidator : AbstractValidator<SubtaskRequestModel>
    {
        public SubtaskRequestValidator()
        {
            RuleFor(x => x.Title)
                .Must(x => x.Length > 2 && x.Length <= 100)
                .WithMessage("Title Length should be between 2 and 100");
        }
    }
}
