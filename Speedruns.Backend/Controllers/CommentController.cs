using Microsoft.AspNetCore.Mvc;
using Speedruns.Backend.Entities;
using Speedruns.Backend.Interfaces;

namespace Speedruns.Backend.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentsRepository _comments;
        private readonly IRunRepository _runs;

        public CommentController(ICommentsRepository comments, IRunRepository runs)
        {
            _comments = comments;
            _runs = runs;
        }

        // GET: /api/comments/{runId}
        [HttpGet("{runId}")]
        public async Task<ActionResult<List<CommentEntity>>> GetAll(long runId)
        {
            try
            {
                var run = await _runs.GetById(runId);

                if (run == null)
                {
                    return NotFound("Run does not exist.");
                }

                var comments = await _comments.GetComments(runId);

                return Ok(comments);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }

        // GET: /api/comments/{commentId}
        [HttpGet("{commentId}")]
        public async Task <ActionResult<CommentEntity>> GetById(long commentId)
        {
            try
            {
                var comment = await _comments.GetCommentById(commentId);

                if (comment == null)
                {
                    return NotFound("Comment not found.");
                }

                return Ok(comment);

            } catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }

        // POST: /api/comments
        [HttpPost]
        public async Task<ActionResult<CommentEntity>> AddComment(CommentEntity comment)
        {
            try
            {
                var createdComment = await _comments.AddComment(comment);

                return CreatedAtAction("GetById", new { id = createdComment.Id }, createdComment);
            } catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500);
            }
        }

        // PUT: /api/comments/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<CommentEntity>> UpdateComment(CommentEntity comment)
        {
            try
            {
                var commentExists = await _comments.GetCommentById(comment.Id);

                if (commentExists == null)
                {
                    return NotFound("Comment not found.");
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
                    return NotFound("Comment not found.");
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
