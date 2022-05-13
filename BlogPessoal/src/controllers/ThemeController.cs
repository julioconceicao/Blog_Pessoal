using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.src.controllers
{
    [ApiController]

    [Route("api/Themes")]

    [Produces("application/json")]

    public class ThemeController : ControllerBase
    {
        #region Attributes

        private readonly ITheme _repository;

        #endregion 

        #region Constructors
        public ThemeController(ITheme repository)
        {
            _repository = repository;
        }

        #endregion 

        #region Methods

        [HttpPost, Authorize]
        public async Task<ActionResult> NewThemeAsync([FromBody] NewThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            await _repository.NewThemeAsync(theme);
            
            return Created($"api/Themes", theme);
        }

        [HttpPut, Authorize(Roles= "ADMIN")]
        public async Task<ActionResult> UpDateThemeAsync([FromBody] UpDateThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            await _repository.UpDateThemeAsync(theme);
            
            return Ok(theme);
        }

        [HttpDelete("delete/{idTheme}"), Authorize(Roles="ADMIN")]
        public async Task<ActionResult> DeleteThemeAsync([FromRoute] int idTheme)
        {
            await _repository.DeleteThemeAsync(idTheme);
            
            return NoContent();
        }

        [HttpGet, Authorize]
        public async Task<ActionResult> GetAllThemesAsync()
        {
            var list = await _repository.GetAllThemesAsync();

            if (list.Count < 1) return NoContent();
            
            return Ok(list);
        }

        [HttpGet("Id/{idTheme}"), Authorize]
        public async Task<ActionResult> GetThemeByIdAsync([FromRoute] int idTheme)
        {
            var theme = await _repository.GetThemeByIdAsync(idTheme);
            
            if (theme == null) return NotFound();
            
            return Ok(theme);
        }

        [HttpGet("search"), Authorize]
        public async Task<ActionResult> GetThemeByDescriptionAsync([FromQuery] string themeDescription)
        {
            var themes = await _repository.GetThemeByDescriptionAsync(themeDescription);
            
            if (themes.Count < 1) return NoContent();
            
            return Ok(themes);
        }

        #endregion Methods
    }
}