using Application.Interfaces.Repository;
using MediatR;
using System.Security.Claims;

namespace Application.Features.Posts.Commands
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, bool>
    {
        private readonly IPostRepository _postRepository;
        public UpdatePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<bool> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(request.PostId);
            if (post == null)
            {
                return false;
            }
            post.Title = request.PostDTO.Title ?? post.Title;
            post.Content = request.PostDTO.Content ?? post.Content;
            await _postRepository.UpdateAsync(post);
            return true;
        }
    }
}
