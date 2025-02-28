using Application.Features.Posts.Commands;
using Application.Features.Posts.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDTO postDto)
        {
            var command = new CreatePostCommand(postDto);
            var post = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreatePost), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdatePost(Guid id, [FromBody] UpdatePostDTO updatePostDTO)
        {
            var result = await _mediator.Send(new UpdatePostCommand(id, updatePostDTO));
            if (!result) return NotFound();

            var response = new { message = "Post Updated successfully" };
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var result = await _mediator.Send(new DeletePostCommand(id));

            if (!result) return NotFound();

            var response = new { message = "Post deleted successfully" };
            return Ok(response);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetPostById(Guid id)
        //{
        //    var post = await _mediator.Send(new GetPostByIdQuery(id));
        //    return post != null ? Ok(post) : NotFound();
        //}

        //[HttpGet("user/{userId}")]
        //public async Task<IActionResult> GetPostsByUserId(Guid userId)
        //{
        //    var posts = await _mediator.Send(new GetPostsByUserIdQuery(userId));
        //    return Ok(posts);
        //}
    }
}
