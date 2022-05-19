using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.src.controllers
{
    [ApiController]
    [Route("api/Posts")]
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

        /// <summary>
        /// Create new post
        /// </summary>
        /// <param name="posting">NewPostDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Example:
        ///
        ///     POST /api/Posts
        ///     {  
        ///        "title": "Dotnet Core mudando o mundo", 
        ///        "description": "Uma ferramenta muito boa para desenvolver aplicações web",
        ///        "picture": "URLDAIMAGEM",
        ///        "emailCreator": "gustavo@email.com",
        ///        "themeDescription": "CSHARP"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns created posts</response>
        /// <response code="400">Requisition error</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PostingModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost, Authorize]
        public async Task<ActionResult> NewPostAsync([FromBody] NewPostDTO posting)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            await _repository.NewPostAsync(posting);
            
            return Created($"api/Postings", posting);
        }

        /// <summary>
        /// Update theme
        /// </summary>
        /// <param name="posting">UpDatePostDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Example:
        ///
        ///     PUT /api/Posts
        ///     {
        ///        "id": 1,   
        ///        "title": "Dotnet Core mudando o mundo", 
        ///        "description": "Uma ferramenta muito boa para desenvolver aplicações web",
        ///        "picture": "URLDAIMAGEM",
        ///        "themeDescription": "CSHARP"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns updated posts</response>
        /// <response code="400">Requisition error</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostingModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut, Authorize]
        public async Task<ActionResult> UpDatePostAsync([FromBody] UpDatePostDTO posting)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            await _repository.UpDatePostAsync(posting);
            
            return Ok(posting);
        }

        /// <summary>
        /// Delete post by Id
        /// </summary>
        /// <param name="idPosting">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Deleted post</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("deletar/{idPostagem}")]
        [HttpDelete("delete/{idPosting}"), Authorize]
        public async Task<ActionResult> DeletePostAsync([FromRoute] int idPosting)
        {
            await _repository.DeletePostAsync(idPosting);
            
            return NoContent();
        }

        /// <summary>
        /// Get post by Id
        /// </summary>
        /// <param name="idPosting">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns the post</response>
        /// <response code="404">Post not found</response>
        [HttpGet("id/{idPosting}"), Authorize]
        public async Task<ActionResult> GetPostByIdAsync([FromRoute] int idPosting)
        {
            var post = await _repository.GetPostByIdAsync(idPosting);
            
            if (post == null) return NotFound();
            
            return Ok(post);
        }

        /// <summary>
        /// Get all posts
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Posts list</response>
        /// <response code="204">Empty list</response>
        [HttpGet, Authorize]
        public async Task<ActionResult> GetAllPostsAsync()
        {
            var list = await _repository.GetAllPostsAsync();
            
            if (list.Count < 1) return NoContent();
            
            return Ok(list);
        }

        /// <summary>
        /// Get posts by search
        /// </summary>
        /// <param name="title">string</param>
        /// <param name="themeDescription">string</param>
        /// <param name="nameCreator">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Return posts</response>
        /// <response code="204">Not found posts for this search</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ThemeModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
