using FluentValidation;

namespace WeCare.Application.Courses.Commands.CreateCourse;
public class CreateCourseCommandValidation : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidation()
    {
        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty().NotNull();
    }
}