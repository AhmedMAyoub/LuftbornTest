using MediatR;

namespace Application.Features.Posts.Commands
{
    public class DeletePostCommand : IRequest<bool>
    {
        public Guid PostId { get; set; }
        public DeletePostCommand(Guid id)
        {
            PostId = id;
        }
    }
}
