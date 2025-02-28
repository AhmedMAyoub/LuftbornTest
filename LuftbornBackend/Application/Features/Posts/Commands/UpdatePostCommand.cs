using Application.Features.Posts.DTOs;
using MediatR;

namespace Application.Features.Posts.Commands
{
    public class UpdatePostCommand : IRequest<bool>
    {
        public Guid PostId { get; set; }
        public UpdatePostDTO PostDTO { get; set; }
        public UpdatePostCommand(Guid postId, UpdatePostDTO updatePostDTO)
        {
            PostId = postId;
            PostDTO = updatePostDTO;
        }
    }
}
