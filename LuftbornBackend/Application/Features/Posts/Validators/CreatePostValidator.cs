using Application.Features.Posts.DTOs;
using FluentValidation;

namespace Application.Features.Posts.Validators
{
    public class CreatePostValidator : AbstractValidator<CreatePostDTO>
    {
        public CreatePostValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(255).WithMessage("Title cannot exceed 255 characters");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required");
        }
    }
}