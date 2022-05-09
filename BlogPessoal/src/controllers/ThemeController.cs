using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
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

        #endregion Attributes

        #region Constructors
        public ThemeController(ITheme repository)
        {
            _repository = repository;
        }

        #endregion Constructors

        #region Methods

        [HttpPost]
        public IActionResult NewTheme([FromBody] NewThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            _repository.NewTheme(theme);
            
            return Created($"api/Themes", theme);
        }

        [HttpPut]
        public IActionResult UpDateTheme([FromBody] UpDateThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            _repository.UpDateTheme(theme);
            
            return Ok(theme);
        }

        [HttpDelete("delete/{idTheme}")]
        public IActionResult DeleteTheme([FromRoute] int idTheme)
        {
            _repository.DeleteTheme(idTheme);
            
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetAllThemes()
        {
            var list = _repository.GetAllThemes();

            if (list.Count < 1) return NoContent();
            
            return Ok(list);
        }

        [HttpGet("Id/{idTheme}")]
        public IActionResult GetThemeByID([FromRoute] int idTheme)
        {
            var theme = _repository.GetThemeByID(idTheme);
            
            if (theme == null) return NotFound();
            
            return Ok(theme);
        }

        [HttpGet]
        public IActionResult GetThemeByDescription([FromQuery] string themeDescription)
        {
            var themes = _repository.GetThemeByDescription(themeDescription);
            
            if (themes.Count < 1) return NoContent();
            
            return Ok(themes);
        }


        #endregion Methods

    }
}