using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.src.controllers
{
    [ApiController]
    [Route("api/Postings")]
    [Produces("application/json")]
    public class PostingController : ControllerBase
    {
        #region Attributes

        private readonly IPosting _repository;

        #endregion 

        #region Constructors
        public PostingController(IPosting repository)
        {
            _repository = repository;
        }

        #endregion

        #region Methods

        [HttpPost, Authorize]
        public async Task<ActionResult> NewPostAsync([FromBody] NewPostDTO posting)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            await _repository.NewPostAsync(posting);
            
            return Created($"api/Postings", posting);
        }

        [HttpPut, Authorize]
        public async Task<ActionResult> UpDatePostAsync([FromBody] UpDatePostDTO posting)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            await _repository.UpDatePostAsync(posting);
            
            return Ok(posting);
        }

        [HttpDelete("delete/{idPosting}"), Authorize]
        public async Task<ActionResult> DeletePostAsync([FromRoute] int idPosting)
        {
            await _repository.DeletePostAsync(idPosting);
            
            return NoContent();
        }


        [HttpGet("id/{idPosting}"), Authorize]
        public async Task<ActionResult> GetPostByIdAsync([FromRoute] int idPost)
        {
            var post = await _repository.GetPostByIdAsync(idPost);
            
            if (post == null) return NotFound();
            
            return Ok(post);
        }

        [HttpGet, Authorize]
        public async Task<ActionResult> GetAllPostsAsync()
        {
            var list = await _repository.GetAllPostsAsync();
            
            if (list.Count < 1) return NoContent();
            
            return Ok(list);
        }

        [HttpGet("search"), Authorize]
        public async Task<ActionResult> GetPostBySearchAsync(
            [FromQuery] string title,
            [FromQuery] string themeDescription,
            [FromQuery] string nameCreator)
        {
            var postings = await _repository.GetPostBySearchAsync(title,
            themeDescription, nameCreator);
            
            if (postings.Count < 1) return NoContent();
            
            return Ok(postings);
        }
        
        #endregion
    }
}
