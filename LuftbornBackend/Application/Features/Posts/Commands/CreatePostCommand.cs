using MediatR;
using Application.Features.Posts.DTOs;

namespace Application.Features.Posts.Commands
{
    public class CreatePostCommand : IRequest<PostDTO>
    {
        public CreatePostDTO Post { get; set; }

        public CreatePostCommand(CreatePostDTO createPostDTO)
        {
            Post = createPostDTO;
        }
    }
}