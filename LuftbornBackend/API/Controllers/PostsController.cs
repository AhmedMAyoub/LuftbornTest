using Application.Features.Posts.Commands;
using Application.Features.Posts.DTOs;
using Application.Features.Posts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            // get user id from token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
                return Unauthorized();

            if (!Guid.TryParse(userIdClaim, out var userId))
                return BadRequest("Invalid user ID format.");

            postDto.AuthorId = userId;
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

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _mediator.Send(new GetAllPostsQuery());
            return Ok(posts);
        }

        [HttpGet("my-posts")]
        [Authorize]
        public async Task<IActionResult> GetMyPosts()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
                return Unauthorized();
            if (!Guid.TryParse(userIdClaim, out var userId))
                return BadRequest("Invalid user ID format.");
            var posts = await _mediator.Send(new GetAllPostsByUserIdQuery(userId));
            return Ok(posts);
        }
    }
}
