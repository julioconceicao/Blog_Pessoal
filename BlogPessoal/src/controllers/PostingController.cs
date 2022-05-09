using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
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

        #endregion Attributes

        #region Constructors
        public PostingController(IPosting repository)
        {
            _repository = repository;
        }

        #endregion Constructors

        #region Methods

        [HttpPost]
        public IActionResult NewPost([FromBody] NewPostDTO posting)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repository.NewPost(posting);
            return Created($"api/Postings/id/{posting.Id}", posting);
        }

        [HttpPut]
        public IActionResult UpDatePost([FromBody] UpDatePostDTO posting)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repository.UpDatePost(posting);
            return Ok(posting);
        }

        [HttpDelete("delete/{idPosting}")]
        public IActionResult DeletePost([FromRoute] int idPosting)
        {
            _repository.DeletePost(idPosting);
            return NoContent();
        }


        [HttpGet("id/{idPosting}")]
        public IActionResult GetPostByID([FromRoute] int idPost)
        {
            var post = _repository.GetPostByID(idPost);
            if (post == null) return NotFound();
            return Ok(post);
        }

        [HttpGet]
        public IActionResult GetAllPosts()
        {
            var list = _repository.GetAllPosts();
            if (list.Count < 1) return NoContent();
            return Ok(list);

        }

        [HttpGet]
        public IActionResult GetPostBySearch(
            [FromQuery] string title,
            [FromQuery] string themeDescription,
            [FromQuery] string emailCreator)
        {
            var postings = _repository.GetPostBySearch(title,
            themeDescription, emailCreator);
            if (postings.Count < 1) return NoContent();
            return Ok(postings);
        }
        #endregion

    }
}
