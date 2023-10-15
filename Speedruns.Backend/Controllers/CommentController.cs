using Microsoft.AspNetCore.Mvc;
using Speedruns.Backend.Models;
using Speedruns.Backend.Interfaces;

namespace Speedruns.Backend.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentsRepository _comments;

        public CommentController(ICommentsRepository comments)
        {
            _comments = comments;
        }

        // GET: /api/comments/{runId}
        [HttpGet("{runId}")]
        public async Task<ActionResult<List<CommentModel>>> GetAll(long runId)
        {
            try
            {
                if (_comments == null)
                {
                    return BadRequest("There are no comments.");
                }

                return Ok(await _comments.GetComments(runId));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }

        // POST: /api/comments
        [HttpPost]
        public async Task<ActionResult<CommentModel>> AddComment(CommentModel comment)
        {
            try
            {
                return Ok(await _comments.AddComment(comment));
            } catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }

        // PUT: /api/comments/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<CommentModel>> UpdateComment(CommentModel comment)
        {
            try
            {
                var commentExists = await _comments.GetCommentById(comment.Id);

                if (commentExists == null)
                {
                    return NotFound();
                }

                await _comments.UpdateComment(comment);

                return Ok("Comment successfully updated.");

            } catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }

        // DELETE: /api/comments/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComment(long id)
        {
            try
            {
                var comment = await _comments.GetCommentById(id);

                if (comment == null)
                {
                    return NotFound();
                }

                await _comments.DeleteComment(comment);
                return Ok("Comment successfully deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }
    }
}
