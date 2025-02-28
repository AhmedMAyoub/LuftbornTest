using Application.Features.Posts.DTOs;
using Application.Interfaces.Repository;
using Application.Features.Posts.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Posts.Commands
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, PostDTO>
    {
        private readonly IPostRepository _postRepository;
        private readonly IValidator<CreatePostDTO> _validator;

        public CreatePostCommandHandler(IPostRepository postRepository, IValidator<CreatePostDTO> validator)
        {
            _postRepository = postRepository;
            _validator = validator;
        }

        public async Task<PostDTO> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.Post, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var post = new Post(request.Post.Title, request.Post.Content, request.Post.AuthorId);

            await _postRepository.AddAsync(post);

            return new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                AuthorId = post.AuthorId,
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt
            };
        }
    }
}
